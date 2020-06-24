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

    public partial class PR : Form
    {
        List<actor> d = new List<actor>();
        List<actor> c = new List<actor>();
        List<columnss> columns = new List<columnss>();
        public PR()
        {
            InitializeComponent();
        }
        
        float classs;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = open.FileName;
            }
        }
        public class actor : IComparable<actor>
        {
            public float value;
            public float classs;
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
        private void button2_Click(object sender, EventArgs e)
        {
            string pathconn = "provider=Microsoft.jet.OLEDB.4.0;Data source=" + textBox1.Text + ";Extended properties=\"Excel 8.0;HDR=Yes;\";";
            OleDbConnection con = new OleDbConnection(pathconn);
            OleDbDataAdapter mydataadapter = new OleDbDataAdapter("Select * From [" + "sheet1" + "$]", con);
            DataTable dt = new DataTable();
            mydataadapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        float p1;
        float total;
        float mostrepeated;
        float temp;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

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
            d.Sort();
            int max = -999999;
            int ct = 0;
            int count = Convert.ToInt32(textBox7.Text);

            for (int i = 0; i < count; i++)
            {
                ct = 0;
                for (int j = 0; j < count; j++)
                {
                    if (d[i].classs == d[j].classs)
                    {
                        ct++;
                        if (ct > max)
                        {
                            max = ct;
                            mostrepeated = d[i].classs;
                        }
                    }
                }
            }
            for (int i = 0; i < count; i++)
            {
                MessageBox.Show("d: " + d[i].value + "c: " + d[i].classs);
            }
            MessageBox.Show("most repeated class is:  " + mostrepeated);

        }
    }
}
