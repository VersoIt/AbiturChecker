using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace AbiturChecker.Core.Abstract
{
    public interface IParseable <T> where T : class
    {
        public Task<T> Parse(IHtmlDocument document, string baseUrl = "");
    }
}
