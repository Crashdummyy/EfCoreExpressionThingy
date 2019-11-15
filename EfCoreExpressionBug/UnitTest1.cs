using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Api.Controllers;
using Data;
using Models;
using Newtonsoft.Json;
using Xunit;

namespace EfCoreExpressionBug
{
    public class UnitTest1 : DbContextFixture<AdminContext>
    {
        /// <summary>
        ///     Inizializes a new Instance of the <see cref="UnitTest1"/>-class.
        /// </summary>
        public UnitTest1()
        {
            if (this.Context.Set<User>().Any())
                return;

            var binPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData");
            
            this.Context.Set<Location>()
                .AddRange(JsonConvert.DeserializeObject<IEnumerable<Location>>(File.ReadAllText(Path.Combine(binPath, "Location.json"))));
            this.Context.SaveChanges();
            this.Context.Set<User>()
                .AddRange(JsonConvert.DeserializeObject<IEnumerable<User>>(File.ReadAllText(Path.Combine(binPath, "User.json"))));
            this.Context.SaveChanges();
        }
        
        [Fact]
        public void Test1()
        {
            var underTest = new WeatherForecastController(new Repository<User>(this.Context));

            underTest.Get(1);
        }
    }
}