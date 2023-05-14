using Microsoft.EntityFrameworkCore;
using MorphoLiveScan.Models;
using System.ComponentModel;
using System.Net.Sockets;
using System.Xml.Linq;

namespace MorphoLiveScan
{
    public class MorphoLiveScanContext: DbContext
    {
        public MorphoLiveScanContext(DbContextOptions<MorphoLiveScanContext> options) : base(options)
        {
        }

        public DbSet<Signature> Signatures { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //k > docker run--name mysql-container - e MYSQL_ROOT_PASSWORD = password - e MYSQL_USER = username - e MYSQL_PASSWORD = userpassword - d mysql
        //    //optionsBuilder.UseMySQL("connection_string_here");
        //    //"Server=172.17.0.2;Port=3306;Database=mydatabase;Uid=myusername;Pwd=mypassword;Connect Timeout=30;"
        //    optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;Database=mydatabase;Uid=username;Pwd=userpassword;Connect Timeout=30;");

        //}
    }
}

// docker run--name mysql-container - e MYSQL_ROOT_PASSWORD = password - e MYSQL_USER = username - e MYSQL_PASSWORD = userpassword - d mysql
//docker run -p 3306:3306 mysql - e MYSQL_ROOT_PASSWORD = password - e MYSQL_USER = username - e MYSQL_PASSWORD = userpassword
