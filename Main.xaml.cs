using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.Documents.Model.Drawing.Charts;

namespace WorkLogCollector
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : RadWindow
    {
        int Count = 0;
        public Main()
        {
            InitializeComponent();
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();

        }
        string Time;
        void timer_Tick(object sender, EventArgs e)
        {

            Time = DateTime.Now.ToString("HH:mm:ss");
            LiveTimeLabel.Content = "Time is : " + Time;

        }


        private void buttonAddwork_Click(object sender, RoutedEventArgs e)
        {
            if (workTitle.Text.Length == 0|| workDecription.Text.Length == 0)
            {

                RadWindow.Alert(new DialogParameters()
                {
                    Content = "Enter job Title And Description "
                });

            }
            else
            {

                Count = Count + 1;

                string Task = "Task : " + Count.ToString() + " With Name : " + workTitle.Text + " With Detail " + workDecription.Text + " At " + Time + " Is Done.";
                Listbox.Items.Add(Task);
                workDecription.Clear();
                workTitle.Clear();

            }
        }

        private void buttonClearWorkLog_Click(object sender, RoutedEventArgs e)
        {
            Listbox.Items.Clear();
        }

        private void buttonDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            Listbox.Items.Remove(Listbox.SelectedItem);
        }

        private void buttonResetCount_Click(object sender, RoutedEventArgs e)
        {
            Count = 0;
        }


        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {

            //const string sPath = "Export\\Report.txt";

            RadSaveFileDialog radSaveFileDialog = new RadSaveFileDialog();

            radSaveFileDialog.Filter = "Text file(*.txt)|*.txt";
            radSaveFileDialog.FilterIndex = 1;

            if (radSaveFileDialog.ShowDialog() != radSaveFileDialog.DialogResult) { return; }
            string dirLocationString = radSaveFileDialog.FileName;
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(radSaveFileDialog.FileName);
            foreach (var item in Listbox.Items)
            {
                SaveFile.WriteLine(item);
            }

            SaveFile.Close();

            RadWindow.Alert(new DialogParameters()
            {
                Content = "Task Log Was Saved "
            });
        }
    }
}
