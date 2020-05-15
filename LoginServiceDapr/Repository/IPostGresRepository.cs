using LoginServiceDapr.Model;
using LoginServiceDapr.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LoginServiceDapr.Repository
{
    public interface IPostGresRepository
    {
        Person GetLoginUser(string email, string password);
    }
}
