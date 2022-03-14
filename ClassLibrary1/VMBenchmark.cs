using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Specialized;

namespace ClassLibrary1
{
    public class VMBenchmark : INotifyPropertyChanged//следим за именением в коллекциях
    {
        [DllImport("..\\..\\..\\..\\x64\\Debug\\Dll_Lab1_6.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Global_func(double[] v, int n, double[] res, string mode,
               ref double v_time, ref int ret, int f);

        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<VMTime> t_list { get; set; }
        public ObservableCollection<VMAccuracy> acc_list { get; set; }
        public VMBenchmark()
        {
            t_list = new ObservableCollection<VMTime>();
            acc_list = new ObservableCollection<VMAccuracy>();
            t_list.CollectionChanged += Coll_Change;
            acc_list.CollectionChanged += Coll_Change;
        }

        private void Coll_Change(object sender, NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(time_rel_min_HA)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(time_rel_min_EP)));
        }

        public void AddVMTime(VMGrid g_in, VMf f_in)
        {
            double[] res = new double[g_in.n];
            VMTime t_struct = new();
            t_struct.grid = new(g_in);
            t_struct.f = f_in;
            double[] v = new double[g_in.n];
            for (int i = 0; i < g_in.n; i++)
            {
                v[i] = g_in.start + i * g_in.step;
            }
            //time_res_HA
            double v_time = 0;
            int ret = -1;
            string mode = "VML_HA";
            Global_func(v, t_struct.grid.n, res, mode,
                    ref v_time, ref ret, (int)t_struct.f);
            t_struct.time_res_HA = v_time;
            //time_res_EP
            v_time = 0;
            ret = -1;
            mode = "VML_EP";
            Global_func(v, t_struct.grid.n, res, mode,
                    ref v_time, ref ret, (int)t_struct.f);
            t_struct.time_res_EP = v_time;
            //time_res_NONE
            v_time = 0;
            ret = -1;
            mode = "None";
            Global_func(v, t_struct.grid.n, res, mode,
                    ref v_time, ref ret, (int)t_struct.f);
            t_struct.time_res_NONE = v_time;
            //time_rel_HA
            t_struct.time_rel_HA = t_struct.time_res_NONE / t_struct.time_res_HA;//for HA mode
            //time_rel_HA
            t_struct.time_rel_EP = t_struct.time_res_NONE / t_struct.time_res_EP;//for EP mode
            t_list.Add(t_struct);
        }

        public void AddVMAccuracy(VMGrid g_in, VMf f_in)
        {
            VMAccuracy acc_struct = new();
            acc_struct.grid = new(g_in.start, g_in.end, g_in.n);
            acc_struct.f = f_in;
            double[] v = new double[g_in.n];
            for (int i = 0; i < g_in.n; i++)
            {
                v[i] = g_in.start + i * g_in.step;
            }
            //VMAcc_max_rel
            //For mode = VML_HA
            int ret = -1;
            string mode = "VML_HA";
            double time_work = 0;
            double[] res_HA = new double[g_in.n];
            Global_func(v, acc_struct.grid.n, res_HA, mode,
                    ref time_work, ref ret, (int)acc_struct.f);
            //For mode = VML_EP
            ret = -1;
            mode = "VML_EP";
            time_work = 0;
            double[] res_EP = new double[g_in.n];
            Global_func(v, acc_struct.grid.n, res_EP, mode,
                    ref time_work, ref ret, (int)acc_struct.f);
            //For mode = NONE
            ret = -1;
            mode = "None";
            time_work = 0;
            double[] res_NONE = new double[g_in.n];
            Global_func(v, acc_struct.grid.n, res_NONE, mode,
                    ref time_work, ref ret, (int)acc_struct.f);
            //-----------------Searching for max---------------------
            double max_res = Math.Abs(res_HA[0] - res_EP[0]);
            int max_diff_i = 0;
            for (int i = 1; i < acc_struct.grid.n; i++)
            {
                double cur_diff = Math.Abs(res_HA[i] - res_EP[i]);
                if (cur_diff > max_res)
                {
                    max_diff_i = i;
                    max_res = cur_diff;
                }
            }
            acc_struct.VMAcc_max_rel = max_res;
            double[] cur = new double[3];
            cur[0] = v[max_diff_i];//arg
            cur[1] = res_HA[max_diff_i];//HA value
            cur[2] = res_EP[max_diff_i];//EP value
            acc_struct.VMAcc_max_diff = cur;
            acc_list.Add(acc_struct);
        }

        public double time_rel_min_HA
        {
            get
            {
                if (t_list.Count < 1)
                {
                    return -1;
                }
                double res;//HA
                res = t_list[0].time_rel_HA;
                for (int i = 1; i < t_list.Count; i++)
                {
                    if (t_list[i].time_rel_HA < res)
                    {
                        res = t_list[i].time_rel_HA;
                    }
                }
                return res;
            }
        }

        public double time_rel_min_EP
        {
            get
            {
                if (t_list.Count < 1)
                {
                    return -1;
                }
                double res;//EP
                res = t_list[0].time_rel_EP;
                for (int i = 1; i < t_list.Count; i++)
                {
                    if (t_list[i].time_rel_EP < res)
                    {
                        res = t_list[i].time_rel_EP;
                    }
                }
                return res;
            }
        }
    }
}
