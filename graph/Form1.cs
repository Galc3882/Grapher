using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics g = null;
        BinTree.BinTreeNode<string> tr = null;
        SolidBrush brush = new SolidBrush(Color.Blue);
        short plane = 1; //0 = XY, 1 = XZ, 2 = YZ
        int col = 0;
        PointF[] point = new PointF[4];
        double miny, maxy;
        double acc = 0.1, a = -45 * Math.PI / 180, zoom = 25; //-45 * Math.PI / 180

        static double CalculateTreeOfParser(BinTree.BinTreeNode<string> t, double x, double y)
        {
            if (t != null)
                if (t.GetInfo() == "+")
                    return CalculateTreeOfParser(t.GetLeft(), x, y) + CalculateTreeOfParser(t.GetRight(), x, y);
                else if (t.GetInfo() == "-")
                    return CalculateTreeOfParser(t.GetLeft(), x, y) - CalculateTreeOfParser(t.GetRight(), x, y);
                else if (t.GetInfo() == "*")
                    return CalculateTreeOfParser(t.GetLeft(), x, y) * CalculateTreeOfParser(t.GetRight(), x, y);
                else if (t.GetInfo() == "/")
                    return CalculateTreeOfParser(t.GetLeft(), x, y) / CalculateTreeOfParser(t.GetRight(), x, y);
                else if (t.GetInfo() == "^")
                    return Math.Pow(CalculateTreeOfParser(t.GetLeft(), x, y), CalculateTreeOfParser(t.GetRight(), x, y));
                else if (t.GetInfo() == "log")
                    return Math.Log(CalculateTreeOfParser(t.GetLeft(), x, y), CalculateTreeOfParser(t.GetRight(), x, y));
                else if (t.GetInfo() == "sin")
                    return Math.Sin(CalculateTreeOfParser(t.GetRight(), x, y));
                else if (t.GetInfo() == "cos")
                    return Math.Cos(CalculateTreeOfParser(t.GetRight(), x, y));
                else if (t.GetInfo() == "tan")
                    return Math.Tan(CalculateTreeOfParser(t.GetRight(), x, y));
                else if (t.GetInfo() == "abs")
                    return Math.Abs(CalculateTreeOfParser(t.GetRight(), x, y));
                else if (t.GetInfo() == "exp")
                    return Math.Exp(CalculateTreeOfParser(t.GetRight(), x, y));
                else if (t.GetInfo() == "x")
                    return x;
                else if (t.GetInfo() == "y")
                    return y;
            return double.Parse(t.GetInfo());
        }

    
        static BinTree.BinTreeNode<string> BuildTreeOfParser(String str)
        {
            BinTree.BinTreeNode<string> tr = null;
            int a = 0;
            if (tr == null)
            {
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == ')')
                        a++;
                    else if (str[i] == '(')
                        a--;
                    if (a == 0 && (str[i] == '+' || str[i] == '-'))
                    {
                        tr = new BinTree.BinTreeNode<string>(str[i].ToString());
                        tr.SetRight(BuildTreeOfParser(str.Substring(i + 1, str.Length - i - 1)));
                        tr.SetLeft(BuildTreeOfParser(str.Substring(0, i)));
                        break;
                    }
                }
            }
            if (tr == null)
            {
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == ')')
                        a++;
                    else if (str[i] == '(')
                        a--;
                    if (a == 0 && (str[i] == '*' || str[i] == '/'))
                    {
                        tr = new BinTree.BinTreeNode<string>(str[i].ToString());
                        tr.SetRight(BuildTreeOfParser(str.Substring(i + 1, str.Length - i - 1)));
                        tr.SetLeft(BuildTreeOfParser(str.Substring(0, i)));
                        break;
                    }
                }
            }
            if (tr == null)
            {
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == ')')
                        a++;
                    else if (str[i] == '(')
                        a--;
                    if (a == 0 && str[i] == '^')
                    {
                        tr = new BinTree.BinTreeNode<string>(str[i].ToString());
                        tr.SetRight(BuildTreeOfParser(str.Substring(i + 1, str.Length - i - 1)));
                        tr.SetLeft(BuildTreeOfParser(str.Substring(0, i)));
                        break;
                    }
                }
            }
            if (tr == null)
            {
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == ')')
                        a++;
                    else if (str[i] == '(')
                        a--;
                    if (a == 0 && str[i] == 'g'&& i==2)    //log
                    {
                        tr = new BinTree.BinTreeNode<string>(str.Substring(i - 2, 3));
                        for (i = str.Length - 1; i >= 0; i--)
                        {
                            if (str[i] == ')')
                                a++;
                            else if (str[i] == '(')
                                a--;
                            if (a == 1 && str[i] == ',')
                            {
                                tr.SetRight(BuildTreeOfParser(str.Substring(i + 1, str.Length - i - 2)));
                                tr.SetLeft(BuildTreeOfParser(str.Substring(4, i - 4)));
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            if (tr == null)
            {
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == ')')
                        a++;
                    else if (str[i] == '(')
                        a--;
                    if (a==0&&i == 2 && (str[i] == 'n' || str[i] == 's' || str[i] == 'p'))    //sin, cos, tan
                    {
                        tr = new BinTree.BinTreeNode<string>(str.Substring(i - 2, 3));
                        tr.SetRight(BuildTreeOfParser(str.Substring(i + 2, str.Length - i - 3)));
                    }

                }
            }
            if (tr == null)
            {
                if (str[0] == '(')
                    tr = BuildTreeOfParser(str.Substring(1, str.Length - 2));
                else
                    tr = new BinTree.BinTreeNode<string>(str);
            }
            return tr;
        }
        static BinTree.BinTreeNode<string> PreBuildTreeOfParser(String str)
        {
            if (str[0] == '-')
                str = "0" + str;
            return BuildTreeOfParser(str);
        }

        static void DrawAxis(Panel panel)
        {
            Graphics g = panel.CreateGraphics();
            g.Clear(Color.White);
            g.DrawLine(Pens.Blue, 0, panel.Height / 2, panel.Width, panel.Height / 2);
            g.DrawLine(Pens.Blue, panel.Width / 2, 0, panel.Width / 2, panel.Height);
            for (int i = 25; i < panel.Width - 25; i += 25)
                g.DrawLine(Pens.Blue, i, panel.Height / 2 - 2, i, panel.Height / 2 + 2);
            for (int i = 25; i < panel.Height - 25; i += 25)
                g.DrawLine(Pens.Blue, panel.Width / 2 - 2, i, panel.Width / 2 + 2, i);

        }

        static double X_px2x_al(double x, double w)
        {
            return x - w / 2;
        }

        static double X_al2x_px(double x, double w)
        {
            return x + w / 2;
        }
        static double Y_al2y_px(double y, double h)
        {
            return -y + h / 2;
        }

        private void Render_Click(object sender, EventArgs e)
        {
            //init
            miny = 0;
            maxy = 0;
            plane = 1;
            zoom = (double)numericUpDown1.Value;
            acc = (double)numericUpDown2.Value/10;
            a = (double)numericUpDown3.Value;
            int arrSize = (int)numericUpDown4.Value;
            double[,] XYZ = new double[arrSize, arrSize];
            double[,] axis1T = new double[arrSize, arrSize];
            double[,] axis2T = new double[arrSize, arrSize];
            //init

            g = panel.CreateGraphics();
            DrawAxis(panel);
            tr = PreBuildTreeOfParser(textBox1.Text);

            //calc x, y and z points
            for (int i = 0; i < XYZ.GetLength(0); i++)
                for (int j = 0; j < XYZ.GetLength(0); j++)
                {
                    double x = (j - XYZ.GetLength(0)/2) * acc, y = (i - XYZ.GetLength(0) / 2) * acc;
                    XYZ[i, j] = CalculateTreeOfParser(tr, x, y);
                    if (plane == 0)
                    {
                        axis1T[i, j] = (x + XYZ[i, j] * Math.Cos(a)) * zoom;
                        axis2T[i, j] = (y + XYZ[i, j] * Math.Sin(a)) * zoom;
                    }
                    else if (plane == 1)
                    {
                        axis1T[i, j] = (x + y * Math.Cos(a)) * zoom;
                        axis2T[i, j] = (XYZ[i, j] + y * Math.Sin(a)) * zoom;
                    }
                    else
                    {
                        axis1T[i, j] = (XYZ[i, j] + x * Math.Cos(a)) * zoom;
                        axis2T[i, j] = (y + x * Math.Sin(a)) * zoom;
                    }
                }

            //if coloring enabled, find min and max y
            if (!grid.Checked)
            {
                for (int i = 0; i < XYZ.GetLength(0) - 1; i++)
                    for (int j = 0; j < XYZ.GetLength(0) - 1; j++)
                    {
                        if (!double.IsInfinity(XYZ[i, j]))
                        {
                            if (XYZ[i, j] < miny)
                                miny = ((float)XYZ[i, j]);
                            else if (XYZ[i, j] > maxy)
                                maxy = ((float)XYZ[i, j]);
                        }
                    }
            }
            for (int i = XYZ.GetLength(0) - 2; i > 0; i--)
                for (int j = 0; j < XYZ.GetLength(0)-1; j++)
                {
                    if (double.IsNaN(XYZ[i, j]) == false && double.IsInfinity(XYZ[i, j]) == false
                        && double.IsNaN(XYZ[i + 1, j]) == false && double.IsInfinity(XYZ[i + 1, j]) == false
                        && double.IsNaN(XYZ[i, j + 1]) == false && double.IsInfinity(XYZ[i, j + 1]) == false
                        && double.IsNaN(XYZ[i + 1, j + 1]) == false && double.IsInfinity(XYZ[i + 1, j + 1]) == false)
                    {
                        point[0] = new PointF((float)X_al2x_px(acc * axis1T[i, j], panel.Width), (float)Y_al2y_px(acc * axis2T[i, j], panel.Height));
                        point[1] = new PointF((float)X_al2x_px(acc * axis1T[i, j + 1], panel.Width), (float)Y_al2y_px(acc * axis2T[i, j + 1], panel.Height));
                        point[2] = new PointF((float)X_al2x_px(acc * axis1T[i + 1, j + 1], panel.Width), (float)Y_al2y_px(acc * axis2T[i + 1, j + 1], panel.Height));
                        point[3] = new PointF((float)X_al2x_px(acc * axis1T[i + 1, j], panel.Width), (float)Y_al2y_px(acc * axis2T[i + 1, j], panel.Height));
                        if (grid.Checked)
                        {
                            g.DrawLine(Pens.Black, point[0], point[1]);
                            g.DrawLine(Pens.Black, point[0], point[3]);
                        }
                        else
                        {
                            col = (int)((XYZ[i, j] - miny) / ((maxy - miny) / 255));
                            if (r1.Checked)
                                brush = new SolidBrush(Color.FromArgb(255, col, 0, 50));
                            else if (r2.Checked)
                                brush = new SolidBrush(Color.FromArgb(255, 20, 0, col));
                            else
                                brush = new SolidBrush(Color.FromArgb(255, 0, col, 30));
                            g.FillPolygon(brush, point);
                        }

                    }
                }


            //BinTree.BinTreeNode<string> tr = PreBuildTreeOfParser(textBox1.Text);
            //double zoom = (double)numericUpDown1.Value;
            //int x_length = 1;
            //float y_last = (float)(Y_al2y_px(zoom * CalculateTreeOfParser(tr, X_px2x_al(-panel.Width / 2, panel.Width) / zoom, 1), panel.Height));
            //float temp = 0;

            //for (int x_px = 0; x_px < panel.Width; x_px += x_length)
            //{
            //    temp = (float)(Y_al2y_px(zoom * CalculateTreeOfParser(tr, X_px2x_al(x_px, panel.Width) / zoom, 1), panel.Height));
            //    try { g.DrawLine(Pens.Black, x_px - x_length, y_last, x_px, temp); }
            //    catch { }
            //    y_last = temp;
            //}

            //g.DrawLine(Pens.Blue, 0, panel.Height / 2, panel.Width, panel.Height / 2);
            //g.DrawLine(Pens.Blue, panel.Width / 2, 0, panel.Width / 2, panel.Height);
            //for (int i = 25; i < panel.Width - 25; i += 25)
            //    g.DrawLine(Pens.Blue, i, panel.Height / 2 - 2, i, panel.Height / 2 + 2);
            //for (int i = 25; i < panel.Height - 25; i += 25)
            //    g.DrawLine(Pens.Blue, panel.Width / 2 - 2, i, panel.Width / 2 + 2, i);
        }
    }
}