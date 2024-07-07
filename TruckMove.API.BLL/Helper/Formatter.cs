using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Helper
{
    internal class Formatter
    {
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class DateFormatAttribute : Attribute
    {
        public string Format { get; }

        public DateFormatAttribute(string format)
        {
            Format = format;
        }
    }
}
