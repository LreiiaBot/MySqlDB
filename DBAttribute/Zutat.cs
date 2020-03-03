using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAttribute
{
    [Tabelle("zutat")]
    class Zutat
    {
        [Spalte("bez")]
        public string Bezeichnung { get; set; }
        [Spalte("preis")]
        public decimal Preis { get; set; }
    }
}
