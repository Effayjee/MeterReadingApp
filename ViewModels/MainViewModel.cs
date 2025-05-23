using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MeterReadingApp.Models;
using MeterReadingApp.Helpers;

namespace MeterReadingApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MeterReading> _readings;
        public ObservableCollection<MeterReading> Readings
        {
            get { return _readings; }
            set { _readings = value; OnPropertyChanged(nameof(Readings)); }
        }

        private MeterReading _selectedReading;
        public MeterReading SelectedReading
        {
            get { return _selectedReading; }
            set { _selectedReading = value; 
                OnPropertyChanged(nameof(SelectedReading));
                DeleteCommand?.RaiseCanExecuteChanged();
                }
        }

        private string _selectedResourceType;
        public string SelectedResourceType
        {
            get { return _selectedResourceType; }
            set { _selectedResourceType = value; OnPropertyChanged(nameof(SelectedResourceType)); }
        }

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand InfCommand { get; private set; }

        public MainViewModel()
        {
            Readings = new ObservableCollection<MeterReading>(DataFileManager.Load());
            AddCommand = new RelayCommand(AddReading);
            DeleteCommand = new RelayCommand(DeleteReading, () => SelectedReading != null);
            SaveCommand = new RelayCommand(SaveToFile);
            //InfCommand = new RelayCommand(InfAdd);
        }

        //private void InfAdd()
        //{
        //    var fioMessage = new MeterReadingApp.Views.FIO();
        //    fioMessage.ShowDialog();
        //}

        private void AddReading()
        {
            try
            {
                if (SelectedResourceType == "Вода")
                {
                    Readings.Add(new WaterReading
                    {
                        Date = DateTime.Now,
                        Value = 0,
                        Salinity = 0,
                    });
                }
                else if (SelectedResourceType == "Электричество")
                {
                    Readings.Add(new ElectricityReading
                    {
                        Date = DateTime.Now,
                        Value = 0,
                    });
                }
                else
                {
                    ShowError("Не выбран тип ресурса.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void DeleteReading()
        {
            try
            {
                if (SelectedReading != null)
                {
                    Readings.Remove(SelectedReading);
                }
                else
                {
                    ShowError("Не выбрано показание для удаления.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при удалении: {ex.Message}");
            }
        }

        private void SaveToFile()
        {
            try
            {
                DataFileManager.Save(Readings);
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            var errorWindow = new MeterReadingApp.Views.ErrorDialog(message);
            errorWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
