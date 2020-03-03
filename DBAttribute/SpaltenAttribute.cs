using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    class SpalteAttribute : Attribute
    {
        public string SpaltenName { get; set; }
        //vllt noch den typen von sql oder so
        public SpalteAttribute(string spaltenName)
        {
            SpaltenName = spaltenName;
        }
    }
}
