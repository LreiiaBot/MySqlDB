using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAttribute
{
    public enum ZStatus
    {
        Legal,
        Illegal,
        Grauzone
    }
    [Tabelle("zigarette")]
    class Zigarette
    {
        [Spalte("name")]
        public string Bezeichnung { get; set; }
        [Spalte("tabak")]
        public string Tabak { get; set; }
        public bool Cool { get; set; } = false;
        [Spalte("status")]
        public ZStatus Status { get; set; }

        public override string ToString()
        {
            return $"{Bezeichnung} {Tabak} {Status}";
        }
    }
}
