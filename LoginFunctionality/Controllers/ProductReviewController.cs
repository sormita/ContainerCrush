using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoginFunctionality.Models;
using LoginFunctionality.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LoginFunctionality.Controllers
{
    public class ProductReviewController : Controller
    {
        private IApiClient apiProxy;
        public ProductReviewController(IApiClient apiClient)
        {
            apiProxy = apiClient;
        }
        public async Task<IActionResult> Index()
        {
            List<Review> lstreview;
            Review obj = new Review();            
            string gqlQuery = GraphQLQueryBuilder.QueryBuilder(obj);                
            lstreview = await apiProxy.PostAsyncGraphQL<Review>("product/GraphQLPostQuery", new StringContent(gqlQuery, Encoding.UTF8, "application/json"));


            return View(lstreview);

        }
    }
}