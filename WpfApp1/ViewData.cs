using System;
using ClassLibrary1;
using System.Windows;
using System.IO;
using System.ComponentModel;
using System.Collections.Specialized;

namespace WpfApp1
{
    public class ViewData : INotifyPropertyChanged
    {
        public VMBenchmark bank { get; set; }
        // Изменение коллекции
        private bool change;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool change_property
        {
            get
            {
                return change;
            }
            set
            {
                if (value != change)
                {
                    change = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(change_property)));
                }
            }
        }
        public ViewData()
        {
            bank = new();
            change = false;
            bank.t_list.CollectionChanged += Coll_changed;
            bank.acc_list.CollectionChanged += Coll_changed;
        }
        private void Coll_changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            change_property = true;
        }



        public void AddVMTime(VMGrid grid, VMf f)
        {
            bank.AddVMTime(grid, f);
        }
        public void AddVMAccuracy(VMGrid grid, VMf f)
        {
            bank.AddVMAccuracy(grid, f);
        }
        public bool Save(string filename)
        {
            //save VMBenchmark in "filename" file
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                StreamWriter writer = new(fs);

                writer.WriteLine(bank.t_list.Count);
                for (int i = 0; i < bank.t_list.Count; i++)
                {
                    writer.WriteLine((int)bank.t_list[i].f);
                    writer.WriteLine(bank.t_list[i].grid.start);
                    writer.WriteLine(bank.t_list[i].grid.end);
                    writer.WriteLine(bank.t_list[i].grid.n);
                }
                writer.WriteLine(bank.acc_list.Count);
                for (int i = 0; i < bank.acc_list.Count; i++)
                {
                    writer.WriteLine((int)bank.acc_list[i].f);
                    writer.WriteLine(bank.acc_list[i].grid.start);
                    writer.WriteLine(bank.acc_list[i].grid.end);
                    writer.WriteLine(bank.acc_list[i].grid.n);
                }
                writer.Close();
            }
            catch (Exception ex)
            {
                bank.t_list.Clear();
                bank.acc_list.Clear();
                MessageBox.Show("Ошибка записи в файл\n", ex.Message);
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return true;
        }

        public bool Load(string filename)
        {
            //read BenchMark to vmb from "filename" file
            FileStream fs = null;
            try
            {
                bank.t_list.Clear();
                bank.acc_list.Clear();
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                StreamReader reader = new(fs);

                int len_time_list = Convert.ToInt32(reader.ReadLine());
                for (int i = 0; i < len_time_list; i++)
                {
                    VMf f = (VMf)Convert.ToInt32(reader.ReadLine());
                    double start = Convert.ToDouble(reader.ReadLine());
                    double end = Convert.ToDouble(reader.ReadLine());
                    int n = Convert.ToInt32(reader.ReadLine());
                    VMGrid grid = new(start, end, n);
                    bank.AddVMTime(grid, f);
                }

                int len_acc_list = Convert.ToInt32(reader.ReadLine());
                for (int i = 0; i < len_acc_list; i++)
                {
                    VMf f = (VMf)Convert.ToInt32(reader.ReadLine());
                    double start = Convert.ToDouble(reader.ReadLine());
                    double end = Convert.ToDouble(reader.ReadLine());
                    int n = Convert.ToInt32(reader.ReadLine());
                    VMGrid grid = new(start, end, n);
                    bank.AddVMAccuracy(grid, f);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                bank.t_list.Clear();
                bank.acc_list.Clear();
                MessageBox.Show("Ошибка чтения из файла\n");
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return true;
        }

    }
}
