using MeterReadingApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeterReadingApp.Parsers
{
    public static class ReadingParser
    {
        public static MeterReading Parse(string line)
        {
            var rawParts = SplitLine(line);

            if (rawParts.Count < 3)
            {
                throw new FormatException("Неверный формат строки");
            }

            string type = rawParts[0];
            DateTime date = DateTime.ParseExact(rawParts[1], "yyyy.MM.dd", CultureInfo.InvariantCulture);
            double value = double.Parse(rawParts[2], CultureInfo.InvariantCulture);

            if (type == "Вода")
            {
                int salinity = rawParts.Count > 3 ? int.Parse(rawParts[3]) : 0;
                return new WaterReading { Date = date, Value = value, Salinity = salinity };
            }

            else if (type == "Электричество")
            {
                return new ElectricityReading { Date = date, Value = value };
            }

            throw new NotSupportedException("Неизвестный тип ресурса: " + type);
        }

        private static List<string> SplitLine(string line)
        {
            var parts = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (char.IsWhiteSpace(c) && !inQuotes)
                {
                    if (current.Length > 0)
                    {
                        parts.Add(current.ToString());
                        current.Clear();
                    }
                }
                else
                {
                    current.Append(c);
                }
            }

            if (current.Length > 0)
            {
                parts.Add(current.ToString());
            }

            return parts;
        }
    }
}
