using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        Form2 Forma;
        Student a = new Student();
        ComboBox b = new();
        Label c = new Label();
        public Form3(Form2 forma, Student _a, ComboBox _b, Label _c)
        {

            InitializeComponent();
            Forma = forma;
            a = _a;
            b = _b;
            c = _c;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            List<string> dis = new Student().Discipline(a);
            BindingSource bindingSource2 = new BindingSource(dis, null);
            comboBox1.DataSource = bindingSource2;
            if(dis.Count == 0 )
            {
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {


            if (radioButton3.Checked) //Редагування
            {
                comboBox1.Visible = true;
                textBox1.Location = new Point(215, 63);
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox2.Enabled = true;
                textBox1.Enabled = true;
                label1.Location = new Point(127, 68);
                textBox2.Location = new Point(127, 36);
                textBox2.Text = a.discplines[comboBox1.SelectedIndex].Disc;
                textBox1.Text = a.discplines[comboBox1.SelectedIndex].marks.ToString();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) //Видалення
            {
                comboBox1.Visible = true;
                textBox2.Visible = false;
                textBox1.Enabled = false;
                textBox1.Location = new Point(215, 36);
                label1.Location = new Point(127, 41);
                textBox2.Location = new Point(127, 11);
                textBox2.Text = a.discplines[comboBox1.SelectedIndex].Disc;
                textBox1.Text = a.discplines[comboBox1.SelectedIndex].marks.ToString();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) //Додавання
            {
                comboBox1.Visible = false;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox2.Enabled = true;
                textBox1.Enabled = true;
                textBox1.Location = new Point(215, 36);
                label1.Location = new Point(127, 41);
                textBox2.Location = new Point(127, 11);
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Forma.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (a.discplines.Count > 0 && !radioButton1.Checked)
            {
                textBox2.Text = a.discplines[comboBox1.SelectedIndex].Disc;
                textBox1.Text = a.discplines[comboBox1.SelectedIndex].marks.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked) //add
            {
                a.AddDiscp(textBox2.Text, Convert.ToInt32(textBox1.Text));

            }
            if (radioButton2.Checked) //rm
            {
                a.discplines.Remove(a.discplines[comboBox1.SelectedIndex]);

            }
            if (radioButton3.Checked) //edit
            {
                a.discplines[comboBox1.SelectedIndex].Disc = textBox2.Text;
                a.discplines[comboBox1.SelectedIndex].marks = Convert.ToInt32(textBox1.Text);

            }
            List<string> dis = new Student().Discipline(a);
            BindingSource bindingSource2 = new BindingSource(dis, null);
            c.Text = " ";
            b.DataSource = bindingSource2;
            Close();
        }
    }
}
