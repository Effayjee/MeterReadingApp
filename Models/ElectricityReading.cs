using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingApp.Models
{
    public class ElectricityReading : MeterReading
    {
        public override string ResourceType => "Электричество";

        public override string ToString()
        {
            return $"{ResourceType} {Date:yyyy.MM.dd} {Value.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
