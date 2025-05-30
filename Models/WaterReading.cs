using MeterReadingApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingApp.Models
{
    public class WaterReading : MeterReading
    {
        private int _salinity;

        public int Salinity
        {
            get { return _salinity; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(Salinity), value, 
                        "Соленость должна быть не больше 100 и не меньше 0");
                _salinity = value;
                OnPropertyChanged(nameof(Salinity));
            }
        }

        public override string ResourceType => "Вода";

        public override string ToString()
        {
            return $"{ResourceType} {Date:yyyy.MM.dd} {Value.ToString(CultureInfo.InvariantCulture)} {Salinity}";
        }
    }
}
