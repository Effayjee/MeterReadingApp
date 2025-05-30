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
        internal static readonly string FilePath = "data.txt";

        public static List<MeterReading> Load()
        {
            var result = new List<MeterReading>();

            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    try
                    {
                        result.Add(ReadingParser.Parse(line));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Ошибка при разборе строки: \"{line}\". {ex.Message})");
                    }
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
