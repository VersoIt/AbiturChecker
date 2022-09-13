using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturChecker.Core
{
    public record ParserSettings(string BaseUrl, string[]? Indexes = null);
}
