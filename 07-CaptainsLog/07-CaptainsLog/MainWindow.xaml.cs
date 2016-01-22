using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using CaptainsLog.Core;
using Newtonsoft.Json;
using System.IO;


namespace _07_CaptainsLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<LogEntry> LogList;
        int id;

        public MainWindow()
        {
            InitializeComponent();
            LogList = new ObservableCollection<LogEntry>();

            if (File.Exists("LogList.json"))
            {
                string json = File.ReadAllText("LogList.json");
                LogList = JsonConvert.DeserializeObject<ObservableCollection<LogEntry>>(json);
            }

            DataGrid.ItemsSource = LogList;
            id = LogList.Count + 1;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (LogText.Text.Length > 0)
            {
                LogEntry log = new LogEntry();
                log.ID = id;
                id = id + 1;
                log.dateTime = DateTime.Now;
                log.entry = LogText.Text;
                LogList.Add(log);
                LogText.Clear();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex >= 0)
            {
                LogList.RemoveAt(DataGrid.SelectedIndex);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(LogList);
            File.WriteAllText("LogList.json", json);
        }

        

    }
}
