using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturChecker.Core.Abstract
{
    public abstract class ParseLinker<T> where T : class
    {

        private bool _isActive = false;

        public ParseLinker(ParserSettings settings, IParseable<T> parser)
        {
            ParserSettings = settings;
            Parser = parser;
        }

        public bool IsActive { get => _isActive; }

        public ParserSettings ParserSettings { get; init; }

        public IParseable<T> Parser { get; init; }

        public async Task<ICollection<T>> GetDataFromParserAsync()
        {
            _isActive = true;
            return await GetDataAsync();
        }

        public static async Task<List<K>> LoadRecordAsync<K>(ParseLinker<K> parseLinker) where K : class
        {
            var result = await parseLinker.GetDataFromParserAsync();

            return (List<K>) result;
        }

        public void Abort()
        {
            _isActive = false;
        }

        protected abstract Task<ICollection<T>> GetDataAsync();

    }
}
