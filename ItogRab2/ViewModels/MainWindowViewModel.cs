using ItogRab2.Commands;
using ItogRab2.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ClosedXML.Excel;

namespace ItogRab2.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Incident> _incidents;
    private string _searchText;
        private string _title;
        private string _description;

        public ObservableCollection<Incident> Incidents
        {
            get { return _incidents; }
            set
            {
                _incidents = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddIncidentCommand { get; }
        public ICommand ExportToExcelCommand { get; }
        public ICommand DeleteIncidentCommand { get; }

        public MainWindowViewModel()
        {
            Incidents = new ObservableCollection<Incident>()
        {
             new Incident { Title = "Пришел на пару", Description = "Опоздал", DataReported = DateTime.Now, Status = "В обработке" },
             new Incident{Title = "Принес запрещенный продукт",Description = "Угостил Сергея Михайловича",DataReported = DateTime.Now,Status = "Снова..."},
             new Incident { Title = "Патаюсь сделать практическую", Description = "4 пары...", DataReported = DateTime.Now, Status = "Ничего не вышло" },
             new Incident{Title = "Ушли в магазин",Description = "На целую пару...",DataReported = DateTime.Now,Status = "Домой("},
        };

            AddIncidentCommand = new RelayCommand(AddIncident);
            ExportToExcelCommand = new RelayCommand(ExportToExcel);
            DeleteIncidentCommand = new RelayCommand(DeleteIncident);
        }

        private void AddIncident(object parameter)
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Description))
            {
                Incidents.Add(new Incident
                {
                    Title = Title,
                    Description = Description,
                    DataReported = DateTime.Now,
                    Status = "Новый"
                });

                Title = string.Empty;
                Description = string.Empty;
            }
        }

        private void ExportToExcel(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Incidents");

                    // Заголовки
                    worksheet.Cell(1, 1).Value = "Заголовок";
                    worksheet.Cell(1, 2).Value = "Описание";
                    worksheet.Cell(1, 3).Value = "Дата";
                    worksheet.Cell(1, 4).Value = "Статус";

                    // Данные
                    for (int i = 0; i < Incidents.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = Incidents[i].Title;
                        worksheet.Cell(i + 2, 2).Value = Incidents[i].Description;
                        worksheet.Cell(i + 2, 3).Value = Incidents[i].DataReported;
                        worksheet.Cell(i + 2, 4).Value = Incidents[i].Status;
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                }

                MessageBox.Show("Данные успешно экспортированы в Excel!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void DeleteIncident(object parameter)
        {
            if (parameter is Incident incidentToDelete)
            {
                Incidents.Remove(incidentToDelete);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

