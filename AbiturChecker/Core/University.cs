using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturChecker.Core
{
    public record University(string Name = "", string Image = "", string StudyDirectionsUrl = "", int Rating = 0)
    {
        public override string ToString() => $"{Name}";
    }
}
