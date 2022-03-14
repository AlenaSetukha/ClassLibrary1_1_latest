using System;
using System.Windows;
using ClassLibrary1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewData v_data { get; set; }
        public VMGrid v_grid { get; set; } = new(0.2, 1.2, 10);//значение по умолчанию
        public VMf f { get; set; } = VMf.vmdErfInv;//значение по умолчанию

        public void New_Click(object sender, RoutedEventArgs e)
        {
            if (v_data.change_property)
            {
                MessageBox.Show("Данные могут быть потеряны");
                string messageBoxText = "Do you want to save changes?";//сама надпись
                string caption = "Word Processor";//заголовок
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                // Display message box
                MessageBox.Show(messageBoxText, caption, button, icon);
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        // User pressed Yes button
                        Microsoft.Win32.SaveFileDialog dlg = new();
                        dlg.FileName = "Bank"; // Default file name
                        dlg.DefaultExt = ".txt"; // Default file extension
                        dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                        // Show open file dialog box
                        Nullable<bool> res = dlg.ShowDialog();
                        // Process open file dialog box results
                        if (res == true)
                        {
                            // Open document
                            string filename = dlg.FileName; ;
                            v_data.Save(filename);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка сохранение в файл");
                        }
                        break;
                }
            }
            v_data.bank.t_list.Clear();
            v_data.bank.acc_list.Clear();
            v_data.change_property = false;
        }
        public void Open_Click(object sender, RoutedEventArgs e)
        {
            if (v_data.change_property)
            {
                MessageBox.Show("Данные могут быть потеряны");
                string messageBoxText = "Do you want to save changes?";//сама надпись
                string caption = "Word Processor";//заголовок
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                // Display message box
                MessageBox.Show(messageBoxText, caption, button, icon);
                MessageBoxResult res = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        // User pressed Yes button
                        Microsoft.Win32.SaveFileDialog dlg1 = new();
                        dlg1.FileName = "Bank"; // Default file name
                        dlg1.DefaultExt = ".txt"; // Default file extension
                        dlg1.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                        // Show open file dialog box
                        Nullable<bool> res1 = dlg1.ShowDialog();
                        // Process open file dialog box results
                        if (res1 == true)
                        {
                            // Open document
                            string filename = dlg1.FileName; ;
                            v_data.Save(filename);
                            v_data.change_property = false;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка сохранение в файл");
                        }
                        break;
                }
            }
            Microsoft.Win32.OpenFileDialog dlg = new();
            dlg.FileName = "Bank"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                //ViewData v_d = new();
                v_data.Load(filename);
                v_data.change_property = false;
            }
            else
            {
                MessageBox.Show("Ошибка считывания из файла");
            }
        }
        public void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new();
            dlg.FileName = "Bank"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName; ;
                v_data.Save(filename);
                v_data.change_property = false;
            }
            else
            {
                MessageBox.Show("Ошибка открытия файла, сохранение в файл");
            }
        }

        public void AddVMTime_Click(object sender, RoutedEventArgs e)
        {
            v_data.AddVMTime(v_grid, f);
        }
        public void AddVMAccuracy_Click(object sender, RoutedEventArgs e)
        {
            v_data.AddVMAccuracy(v_grid, f);
        }

        public MainWindow()
        {
            InitializeComponent();
            v_data = new();
            DataContext = this;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (v_data.change_property)
            {
                MessageBox.Show("Данные могут быть потеряны");
                string messageBoxText = "Do you want to save changes?";//сама надпись
                string caption = "Word Processor";//заголовок
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                // Display message box
                MessageBox.Show(messageBoxText, caption, button, icon);
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        // User pressed Yes button
                        Microsoft.Win32.SaveFileDialog dlg = new();
                        dlg.FileName = "Bank"; // Default file name
                        dlg.DefaultExt = ".txt"; // Default file extension
                        dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                        // Show open file dialog box
                        Nullable<bool> res = dlg.ShowDialog();
                        // Process open file dialog box results
                        if (res == true)
                        {
                            // Open document
                            string filename = dlg.FileName; ;
                            v_data.Save(filename);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка сохранение в файл");
                        }
                        break;
                }
            }
            base.OnClosing(e);
        }
    }
}