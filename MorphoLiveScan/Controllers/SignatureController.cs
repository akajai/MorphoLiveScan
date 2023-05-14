using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MorphoLiveScan.Models;
using MySqlConnector.Logging;
using System;
using System.Text;

namespace MorphoLiveScan.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignatureController : ControllerBase
    {
        private readonly MorphoLiveScanContext _morphoLiveScanContext = null;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<SignatureController> _logger;

        public SignatureController(ILogger<SignatureController> logger, MorphoLiveScanContext morphoLiveScanContext)
        {
            _logger = logger;
            _morphoLiveScanContext = morphoLiveScanContext;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Signature> Get()
        {
            return _morphoLiveScanContext.Signatures.ToList();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
        //localhost:3000/checking -for check if record are already on database if no records create new record for scanning
        [HttpPost("checking")]
        public async Task<bool> Checking(IFormFile file)
        {
            Signature signature = new Signature();
            string temp=Helper.ConvertToBase64String(file);
            bool isPresent=_morphoLiveScanContext.Signatures.Any(s => s.Description == temp);
            if (!isPresent)
            {
                signature.Description= temp;
                signature.Image= Encoding.UTF8.GetBytes(temp);
                await _morphoLiveScanContext.Signatures.AddAsync(signature);
                await _morphoLiveScanContext.SaveChangesAsync();
            }
            return true;
        }
        //localhost:3000/enroll -for start scanning fingerprint
        [HttpGet("enroll/{imagestring?}")]
        public bool Enroll(string ?imagestring)
        {
            return true;
        }
        //localhost:3000/match -for checking match record on database
        [HttpPost("match")]
        public async Task<bool> match(IFormFile file)
        {
            string temp = Helper.ConvertToBase64String(file);
            bool isPresent = _morphoLiveScanContext.Signatures.Any(s => s.Description == temp);
            return isPresent;
        }
        //localhost:3000/API/scanning , so the API from this URL will call SDK to start scanning from device
        [HttpGet("api/scanning/{imagestring}")]
        public bool Scanning(string imagestring)
        {
            return true;
        }

    }
}