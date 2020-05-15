using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoginFunctionality.Utility
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string requestUrl);
        Task<List<T>> GetListAsync<T>(string requestUrl);
        Task<T> PostAsync<T>(Uri requestUrl, T content);
        Task<List<T>> PostAsyncGraphQL<T>(string v, StringContent stringContent);
    }
}
