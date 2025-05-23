using MeterReadingApp.Models;
using MeterReadingApp.Parsers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingApp.Helpers
{
    public static class DataFileManager
    {
        private const string FilePath = "data.txt";

        public static List<MeterReading> Load()
        {
            var result = new List<MeterReading>();
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    result.Add(ReadingParser.Parse(line));
                }
            }
            return result;
        }

        public static void Save(IEnumerable<MeterReading> readings)
        {
            var lines = readings.Select(r => r.ToString());
            File.WriteAllLines(FilePath, lines);
        }
    }
}
