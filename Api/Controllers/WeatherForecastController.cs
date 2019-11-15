using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepository<User> _Repo;

        /// <summary>
        ///     Inizializes a new Instance of the <see cref="WeatherForecastController"/>-class.
        /// </summary>
        public WeatherForecastController(IRepository<User> repo)
        {
            this._Repo = repo;
        }
        [HttpGet]
        public IActionResult Get(int id)
        {
            return this.Ok(this._Repo.GetSingle(x => x.Id == id, x => x.Location));
        }
    }
}