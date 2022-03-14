namespace ClassLibrary1
{
    public struct VMTime
    {
        public VMGrid grid { get; set; }
        public VMf f { get; set; }
        public double time_res_HA { get; set; }
        public double time_res_EP { get; set; }
        public double time_res_NONE { get; set; }
        public double time_rel_HA { get; set; }
        public double time_rel_EP { get; set; }

        public override string ToString()
        {
            string format = "F2";
            string res = (int)f + "\n";
            res += grid.start.ToString(format) + "\n"
                + grid.end.ToString(format) + "\n"
                + grid.n.ToString() + "\n";
            return res;
        }
    }
}
