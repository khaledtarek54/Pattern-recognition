using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace PR_project
{
    public partial class Form4 : Form
    {
        List<actor> d = new List<actor>();
        List<actor> c = new List<actor>();
        List<actor> ms = new List<actor>();
        List<columnss> columns = new List<columnss>();

        public Form4()
        {
            InitializeComponent();
        }
        public class actor : IComparable<actor>
        {
            public double value;
            public double classs;
            public double weighted;
            public double visited = 0;
            public int CompareTo(actor other)
            {
                if (this.value > other.value)
                {
                    return 1;
                }
                else if (this.value < other.value)
                {
                    return -1;
                }
                else
                    return 0;
            }

        }

        public class columnss
        {
            public int c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = open.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pathconn = "provider=Microsoft.jet.OLEDB.4.0;Data source=" + textBox1.Text + ";Extended properties=\"Excel 8.0;HDR=Yes;\";";
            OleDbConnection con = new OleDbConnection(pathconn);
            OleDbDataAdapter mydataadapter = new OleDbDataAdapter("Select * From [" + "sheet1" + "$]", con);
            DataTable dt = new DataTable();
            mydataadapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        double classs;
        double p1;
        double total;
        double mostrepeated;
        double temp;
        double count;
        double m;
        double power;
        double Base;
        double all;
        double tot;
        double Base1;
        double all1;
        double tot1;
        double max=-99999;
        double belongclass;
        private void button3_Click(object sender, EventArgs e)
        {
            string s = textBox3.Text;
            int b;
            string v;
            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i]))
                {
                    v = Convert.ToString(s[i]);
                    b = int.Parse(v);
                    columnss pnn = new columnss();
                    pnn.c = b;
                    columns.Add(pnn);
                }
            }
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                total = 0;
                for (int j = 0; j < columns.Count; j++)
                {
                    temp = 0;
                    p1 = 0;
                    p1 = Convert.ToSingle(dataGridView1.Rows[i].Cells[j].Value);
                    classs = Convert.ToSingle(dataGridView1.Rows[i].Cells[columns.Count].Value);
                    temp = columns[j].c - p1;
                    total += temp * temp;

                }
                actor pnn = new actor();
                pnn.value = total;
                pnn.classs = classs;
                d.Add(pnn);
            }
            m = Convert.ToInt32(textBox7.Text);

            power = 2 / (m - 1);
            for (int i = 0; i < d.Count; i++)
            {
                tot = 0;
                tot1 = 0;
                for (int j = 0; j < d.Count; j++)
                {
                    if (d[i].classs == d[j].classs && i != j && d[j].visited == 0)
                    {
                        d[j].visited = 1;
                        Base = Math.Pow(power, d[i].value);
                        all = 1 / Base;
                        tot += all;
                    }
                }
                for (int k = 0; k < d.Count; k++)
                {
                    Base1 = Math.Pow(power, d[i].value);
                    all1 = 1 / Base;
                    tot1 += all;
                }

                actor pnn = new actor();
                pnn.value = tot/tot1;
                pnn.classs = d[i].classs;
                ms.Add(pnn);
            }
            for (int l = 0; l < ms.Count; l++)
            {
                if (ms[l].value > max)
                {
                    max = ms[l].value;
                    belongclass = ms[l].classs;

                }
            }
            MessageBox.Show("belonged class:  " + belongclass);
        }
    }
    
}
