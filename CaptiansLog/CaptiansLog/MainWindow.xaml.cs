using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CaptiansLog.Core;
using LitJson;
using System.IO;
using Newtonsoft.Json;

namespace CaptiansLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<LogEntry> LogList;
        public MainWindow()
        {
            LogList = new ObservableCollection<LogEntry>();
            
            InitializeComponent();
            
            if (File.Exists(@"C:\dev\origin\07-CaptainsLog/LogList.json"))
            {
                 var JsonString = File.ReadAllText(@"C:\dev\origin\07-CaptainsLog/LogList.json");

            if (JsonString != "")
            {
                ObservableCollection<LogEntry> loaded = JsonConvert.DeserializeObject<ObservableCollection<LogEntry>>(JsonString);
                LogList = loaded;
            }
            }
                dataGrid.ItemsSource = LogList;

                id = LogList.Count + 1;
           
        }
        int id; 
            
        

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            LogEntry log = new LogEntry();

            
            log.ID = id;
            id = id + 1;
            log.dateTime = DateTime.Now;
            log.entry = logText.Text;           
            LogList.Add(log);
            logText.Clear();

            JsonData JSonLogList;
            JSonLogList = JsonMapper.ToJson(LogList);
            File.WriteAllText(@"C:\dev\origin\07-CaptainsLog" + "/LogList.json", JSonLogList.ToString());


        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0)
            {
            LogList.RemoveAt(dataGrid.SelectedIndex);
            JsonData JSonLogList;
            JSonLogList = JsonMapper.ToJson(LogList);
            File.WriteAllText(@"C:\dev\origin\07-CaptainsLog" + "/LogList.json", JSonLogList.ToString());
            }

        }
    }
}
