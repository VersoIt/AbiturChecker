using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturChecker.Core
{
    public record Student(string Id = "", int Sum = 0, string SelectedDirection = "")
    {
        public override string ToString() => $"{Id}: {Sum}";

        public bool IsAgree { get => SelectedDirection is null; }

    }
}
