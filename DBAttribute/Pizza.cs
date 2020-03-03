using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAttribute
{
    [Tabelle("pizza")]
    class Pizza
    {
        [Spalte("bezeichnung")]
        public string Bezeichnung { get; set; }
        [Spalte("bestellung")]
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{Bezeichnung} {Date}";
        }
    }
}
