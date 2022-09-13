using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturChecker.Core.Abstract
{
    public interface IHtmlLoader<T> where T : class
    {

        public Task<T> LoadDataAsync(string url);
    }
}
