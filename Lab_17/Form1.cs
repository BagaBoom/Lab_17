using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lab_17
{
    public partial class Form1 : Form
    {

        Cross cross;
        BinaryFormatter format;
        [Serializable]
        public class Cross
        {
            double a;
            double b;

            public double A
            {
                set { a = value; }
                get { return a; }
            }
            public double B
            {
                set { b = value; }
                get { return b; }
            }

            public Cross()
            {
                b = 0;
                a = 0;
            }

            public Cross(double a, double b)
            {
                this.a = a;
                this.b = b;
            }

            public void Draw(SolidBrush sb, Pen p , Graphics g , int x , int y)
            {
                int a = (int)A;
                int b = (int)B;

                Point[] points= new Point[13];
                points[0] = new Point(x + b , y + 0);
                points[1] = new Point(x + b , y + b);
                points[2] = new Point(x + 0, y + b);
                points[3] = new Point(x + 0, y +(b + a));
                points[4] = new Point(x + b , y + (b + a));
                points[5] = new Point(x + b, y + (b + a + b));
                points[6] = new Point(x + (b + a), y + (b + a + b));
                points[7] = new Point(x + (b + a), y + (b + a));
                points[8] = new Point(x + (b + a + b), y + (b + a));
                points[9] = new Point(x + (b + a + b), y + b);
                points[10] = new Point(x + (b + a), y + b);
                points[11] = new Point(x + (b + a), y + 0);
                points[12] = new Point(x + b, y + 0);
               
                g.DrawPolygon(p, points);
                g.FillPolygon(sb, points);
            }
        }



        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text == "")
            {
                cross = new Cross();
                MessageBox.Show("Заповніть значення а.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            if(textBox2.Text == "")
            {
                cross = new Cross();
                MessageBox.Show("Заповніть значення b.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(Double.Parse(textBox1.Text) <= 1)
            {
                cross = new Cross();
                MessageBox.Show("Значення а не може бути меншим 1!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Double.Parse(textBox2.Text) <= 1)
            {
                cross = new Cross();
                MessageBox.Show("Значення b не може бути меншим 1!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                double a = Double.Parse(textBox1.Text);
                double b = Double.Parse(textBox2.Text);

                cross = new Cross(a,b);
            }
        }

        

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (cross == null)
            {
                MessageBox.Show("Об'єкт класу не створено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Graphics g = CreateGraphics();
                Pen p = new Pen(Color.Black);
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
                g.Clear(BackColor);
                cross.Draw(solidBrush, p, g, e.X, e.Y);

                Type t = typeof(Cross);
                MemberInfo[] x = t.GetMembers();
                foreach ( MemberInfo m in x )
                {
                    label5.Text = "";
                }
                foreach(MemberInfo m in x)
                {
                    label5.Text += (m.ToString()) + "\n";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cross == null)
            {
                MessageBox.Show("Відсутній об'єкт для зберігання", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                XmlSerializer format = new XmlSerializer(typeof(Cross));
                FileStream f;
                f = new FileStream("cross.xml", FileMode.OpenOrCreate);
                format.Serialize(f, cross);
                f.Close();
                MessageBox.Show("Файл створенно", "Створено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists("cross.xml"))
            {
                FileStream f;
                f = new FileStream("cross.xml", FileMode.Open);
                XmlSerializer format = new XmlSerializer(typeof(Cross));
                cross = (Cross) format.Deserialize(f);
                textBox1.Text = cross.A.ToString() ;
                textBox2.Text = cross.B.ToString() ;
                f.Close() ;
                MessageBox.Show("Відновлено.", "XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Відсутність об'єкт для відновлення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cross == null)
            {
                MessageBox.Show("Відсутній об'єкт для зберігання", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                format = new BinaryFormatter();
                FileStream f;
                f = new FileStream("cross.dat", FileMode.OpenOrCreate);
                format.Serialize(f, cross);
                f.Close();
                MessageBox.Show("Файл створенно", "Створено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (File.Exists("cross.dat"))
            {
                FileStream f;
                f = new FileStream("cross.dat", FileMode.Open);
                format = new BinaryFormatter();
                cross = (Cross)format.Deserialize(f);
                textBox1.Text = cross.A.ToString() ;
                textBox2.Text = cross.B.ToString() ;
                f.Close() ;
                MessageBox.Show("Відновлено.", "Binary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Відсутність об'єкт для відновлення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
