using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poly_Rect_Calc
{ 
    class Point : IComparable<Point>
    {
        public double x;
        public double y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public int CompareTo(Point point)
        {
            if (this.x > point.x)
                return 1;
            else if (this.x < point.x)
                return -1;
            else
                return 0;
        }
    }
}
