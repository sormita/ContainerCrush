using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFunctionality.Models
{
    public class BaseModel : IDisposable
    {
        public void Dispose(bool disposing=true)
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
