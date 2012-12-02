using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Security;

namespace Poly_Rect_Calc
{
    public partial class Form1 : Form
    {
        private Calculator calc;
        public Form1()
        {
            calc = new Calculator();
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x = 0;
            double y = 0;
            try
            {
                x = double.Parse(textBox1.Text);
                y = double.Parse(textBox2.Text);
            }
            catch (System.FormatException ex)
            {
                label3.Text="输入数据不合法！";
                return;
            }
           
           
            
            textBox1.Text="";
            textBox2.Text="";
            calc.addPoint(new Point(x, y));
            string listStr="("+x+","+y+")";
            listBox1.Items.Add(listStr);
            label3.Text = "";
        }

        void textBox2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double x = 0;
                double y = 0;
                try
                {
                    x = double.Parse(textBox1.Text);
                    y = double.Parse(textBox2.Text);
                }
                catch (System.FormatException ex)
                {
                    label3.Text = "输入数据不合法！";
                    return;
                }



                textBox1.Text = "";
                textBox2.Text = "";
                calc.addPoint(new Point(x, y));
                string listStr = "(" + x + "," + y + ")";
                listBox1.Items.Add(listStr);
                textBox1.Focus();
                label3.Text = "";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            calc.deleteByIndex(index);
            listBox1.Items.RemoveAt(index);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            calc.clearAll();
            listBox1.Items.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            double area = calc.getArea();
            if (area == -1)
            {
                label3.Text = "输入数据不能构成凸多边形！";
            }
            else
            {
                label4.Text = "面积: " + area;
            }
           
        }
    }
}
