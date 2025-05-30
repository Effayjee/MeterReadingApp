using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingApp.Models
{
    public abstract class MeterReading : INotifyPropertyChanged
    {
        private DateTime _date;
        private double _value;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        public double Value
        {
            get { return _value; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Value), "Значение не может быть отрицательным");
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public abstract string ResourceType { get; }

        public abstract override string ToString();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
