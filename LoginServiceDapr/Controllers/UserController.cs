using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoginServiceDapr.Model;
using LoginServiceDapr.Repository;
using LoginServiceDapr.Repository.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginServiceDapr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IPostGresRepository repos;
        private readonly IMapper mapper;

        public UserController(IPostGresRepository irepos, IMapper imapper)
        {
            repos = irepos;
            mapper = imapper;
        }

        [HttpGet]
        public User GetUser(string email, string password)
        {
            Person usrObj = repos.GetLoginUser(email, password);

            User usr = mapper.Map<User>(usrObj);

            return usr;
        }
    }
}