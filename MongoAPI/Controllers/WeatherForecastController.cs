using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace MongoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMongoCollection<UserDetails> _collection;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoDatabase database)
        {
            _logger = logger;
            string connectionString = "mongodb://localhost:27017";
            string databaseName = "mydatabase";
           _collection = database.GetCollection<UserDetails>("UserDetails");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<UserDetails> Get()
        {
            var users = _collection.Find(u => true).ToList();
            return users;
        }
        [HttpPost]
        public ActionResult<UserDetails> Create(UserDetails user)
        {
            // Create a new user
            _collection.InsertOne(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _collection.DeleteOne(u => u.Id == id);
            if (result.DeletedCount == 0)
                return NotFound();
            return NoContent();
        }
    }
}
    public class UserDetails
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
