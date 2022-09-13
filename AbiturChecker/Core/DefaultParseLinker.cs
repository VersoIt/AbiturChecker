using System;
using AbiturChecker.Core.Abstract;
using AngleSharp.Html.Parser;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AbiturChecker.Core
{
    public class DefaultParseLinker<T> : ParseLinker<T> where T : class
    {
        private readonly HtmlParser _domParser = new HtmlParser();
        private IHtmlLoader<string> _htmlLoader;

        public DefaultParseLinker(ParserSettings settings, IParseable<T> parser)
            : base(settings, parser)
        {
            _htmlLoader = new DefaultHtmlLoader();
        }

        public event Action<object>? Completed = null;
        public event Action<object, T>? NewData = null;

        protected override async Task<ICollection<T>> GetDataAsync()
        {
            List<T> list = new List<T>();
            if (ParserSettings.Indexes != null)
            {
                foreach (var link in ParserSettings.Indexes)
                {
                    string currentUrl = $"{ParserSettings.BaseUrl}/{link}";
                    if (!IsActive)
                    {
                        Abort();
                        Completed?.Invoke(this);
                        return list;
                    }

                    var data = await GetRenderedDataAsync(currentUrl);

                    NewData?.Invoke(this, data);
                    list.Add(data);
                }
            }
            else
            {
                list.Add(await GetRenderedDataAsync(ParserSettings.BaseUrl));
                NewData?.Invoke(this, await GetRenderedDataAsync(ParserSettings.BaseUrl));
            }

            Abort();
            Completed?.Invoke(this);
            return list;
        }

        private async Task<T> GetRenderedDataAsync(string url)
        {

            string? documentSource = await _htmlLoader.LoadDataAsync(url);

            var htmlDocument = await _domParser.ParseDocumentAsync(documentSource);
            var result = await Parser.Parse(htmlDocument, ParserSettings.BaseUrl);

            return result;
        }
    }
}
