using System;
using System.Runtime.InteropServices;

namespace ClassLibrary1
{
    public struct VMAccuracy
    {
        public VMGrid grid { get; set; }
        public VMf f { get; set; }
        public double VMAcc_max_rel { get; set; }
        public double[] VMAcc_max_diff { get; set; }
    }
}
