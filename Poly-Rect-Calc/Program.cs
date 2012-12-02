using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Poly_Rect_Calc
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /*Calculator calc = new Calculator();
            calc.addPoint(new Point(0, 1));
            calc.addPoint(new Point(1, 2));
            calc.addPoint(new Point(3, 2));
            calc.addPoint(new Point(3, 0));
            calc.addPoint(new Point(1, 0));
            double area = calc.getArea();
            if (area == -1)
                Console.WriteLine("error!!");
            else
                Console.WriteLine(area);*/
        }
    }
}
