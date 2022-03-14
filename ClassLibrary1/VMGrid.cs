using System;

namespace ClassLibrary1
{
    public class VMGrid
    {
        public double start { get; set; }
        public double end { get; set; }
        public int n { get; set; }
        public double step
        {
            get
            {
                return Math.Abs(end - start) / (n - 1);
            }
        }

        public VMGrid(double s, double e, int nn)
        {
            this.start = s;
            this.end = e;
            this.n = nn;
        }

        public VMGrid(VMGrid in_grid)
        {
            start = in_grid.start;
            end = in_grid.end;
            n = in_grid.n;
        }
    }
}
