using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoginFunctionality.Models;
using LoginFunctionality.Utility;
using LoginService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace LoginFunctionality.Controllers
{    
    public class ProductController : Controller
    {
        private IApiClient apiProxy;        

        public ProductController(IApiClient apiClient)
        {                     
            apiProxy = apiClient;
        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            List<Product> lstproducts = await apiProxy.GetListAsync<Product>("product/GetAll");
                        
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Username = User.Identity.Name;
                List<string> lstClaims = new List<string>();
                foreach(var claims in User.Claims)
                {
                    lstClaims.Add(claims.Value);
                }

            }


            return View(lstproducts);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> QuerySpecificProducts()
        //{
        //    string myJson = "{\"query\": \"{productReviews{reviewDate,reviewerName}}\"}";
        //    var lstproducts = await apiProxy.PostAsyncGraphQL<Product>("product/GraphQLPostQuery", new StringContent(myJson, Encoding.UTF8, "application/json"));
        //    //List<Product> listProduct= JsonConvert.DeserializeObject<List<Product>>(lstproducts.ToString());
        //    //ViewBag.products = lstproducts;
        //    return PartialView("ProductReview", lstproducts);
        //}


    }
}