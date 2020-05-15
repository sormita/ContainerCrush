using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostGresqlDapper.Model;
using PostGresqlDapper.Repository;

namespace PostGresqlDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        readonly IPostGresRepository<Actor> repos;
        private readonly ILogger<ActorController> logger;
        public ActorController(IPostGresRepository<Actor> _repos, ILogger<ActorController> _logger)
        {
            repos = _repos;
            logger = _logger;
        }

        [HttpGet]
        public IEnumerable<Actor> Get()
        {
            var actors = repos.GetAll();
            return actors.ToList();
        }
    }
}