using ProductService.GraphQL.GraphQLTypes;
//using DataRepository.Repository;
using GraphQL.Types;
using ProductService.Repository;

namespace ProductService.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IProductRepository repository)
        {
            Field<ListGraphType<ProductType>>(
               "Product",
               resolve: context => repository.GetAllProducts()
           );            
        }
    }
}
