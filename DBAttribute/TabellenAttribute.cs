using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    class TabelleAttribute : Attribute
    {
        public string TabellenName { get; set; }
        public TabelleAttribute(string tabellenName)
        {
            TabellenName = tabellenName;
        }
    }
}
