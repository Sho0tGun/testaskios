using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace testaskios
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";
            dataGridView1.Rows.Add(2014, "Corolla", "Toyota","C","sedan");
            dataGridView1.Rows.Add(2012, "6", "Mazda", "D", "sedan");
            dataGridView1.Rows.Add(2012, "A4avante", "Audi", "D", "universal");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i=dataGridView1.RowCount-1;
            dataGridView1.Rows.AddCopy(i);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Invalidate();
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string[] str;
            int num = 0;
            try
            {
                string[] str1 = sr.ReadToEnd().Split('$');
                num = str1.Count();
                dataGridView1.RowCount = num - 1;
                dataGridView1.Rows.Clear();
                for (int i = 0; i < num - 1; i++)
                {
                    dataGridView1.Rows.Add();
                    str = str1[i].Split('#');
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = str[j];
                    }
                }
                fs.Close();
                sr.Close();
            }
            catch
            {
                MessageBox.Show("File open failed");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            StreamWriter sw = new StreamWriter(filename,false);
            try
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    for (int i = 0; i < dataGridView1.Rows[j].Cells.Count; i++)
                    {
                        sw.Write(dataGridView1.Rows[j].Cells[i].Value + "#");
                    }
                    sw.Write("$");
                }
                sw.Close();
                MessageBox.Show("File saved");
            }
            catch
            {
                MessageBox.Show("File save failed");
            }   
        }
    }
}
