using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Entities;
using ProductService.GraphQL.GraphQLParameter;
using ProductService.GraphQL.GraphQLQueries;
using ProductService.GraphQL.GraphQLSchema;
using ProductService.Repository;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repos;

        private readonly AppQuery _graphQLQuery;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly AppSchema _schema;

        public ProductController(IProductRepository irepos,
            AppQuery graphQLQuery,
            IDocumentExecuter documentExecuter,
            AppSchema schema)
        {
            repos = irepos;
            _graphQLQuery = graphQLQuery;
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            List<Product> lstProd = new List<Product>();

            Product p1 = new Product
            {
                ProductId = 1001,
                Name = "Frooti"
            };
            Product p2 = new Product
            {
                ProductId = 1002,
                Name = "Maaza"
            };

            lstProd.Add(p1);
            lstProd.Add(p2);

            return lstProd;
        }

        // GET: api/Product
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Product> GetAll()
        {
            return repos.GetAllProducts();
        }
                
        [HttpPost]
        [Route("GraphQLPostQuery")]
        public async Task<IActionResult> GraphQLPostQuery([FromBody] GraphQLParameter query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            //var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query                 
            };
            var result = await _documentExecuter
                .ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);            
        }
    }
}