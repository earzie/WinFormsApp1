using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        Form1 Forma = new();
        int mode = new int();
        Student a = new Student();
        DataGridView data = new DataGridView();
        List<Student> la = new List<Student>();
        List<string> dis = new List<string>();
        ComboBox b = new ComboBox();
        public Form2(Student _a, Form1 form, int mode, ComboBox _b, List<Student> _la)
        {
            InitializeComponent();
            a = _a;
            la = _la;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox1.Text = Convert.ToString(a.Surname);
            textBox2.Text = Convert.ToString(a.Name);
            Text = String.Format("Перегляд студента {0}", a.ID);
            Forma = form;
            this.mode = mode;
            b = _b;
            dis = new Student().Discipline(a);
            BindingSource bindingSource2 = new BindingSource(dis, null);
            comboBox1.DataSource = bindingSource2;
        }

        public Form2(List<Student> _a, Form1 form, int mode, DataGridView _data, ComboBox _b)
        {
            InitializeComponent();
           
            la = _a;
            Forma = form;
            this.mode = mode;
            data = _data;
            b = _b;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3(this, a, comboBox1, label5);
            form.Show();
            Hide();
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            button1.Text = "Зберегти";
            mode = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mode == 2)
            {
                a.Name = textBox2.Text;
                a.Surname = textBox1.Text;
                Close();
            }
            if (mode == 0)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button1.Text = "Зберегти";
                mode = 2;
            }
            if (mode == 1)
            {
                a.Name = textBox2.Text;
                a.Surname = textBox1.Text;
                if (la.Count != 0)
                    a.ID = la[la.Count - 1].ID + 1;
                else a.ID = 0;
                la.Add(a); 
                Close();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button1.Text = "Додати";
                button2.Text = "Закрити";
            }
            List<string> dis = new Student().Discipline(a);
            BindingSource bindingSource2 = new BindingSource(dis, null);
            comboBox1.DataSource = bindingSource2;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(mode == 1)
            {
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = la;
                data.DataSource = bindingSource;
                BindingSource bindingSource2 = new BindingSource(new Student().Discipline(la), null);
                b.DataSource = bindingSource2;
            }
            if(mode == 2)
            {
                BindingSource bindingSource2 = new BindingSource(new Student().Discipline(la), null);
                b.DataSource = bindingSource2;
            }
            Forma.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dis = new Student().Discipline(a);
            if (dis.Count > 0)
            {
                label5.Text = Convert.ToString(a.discplines[comboBox1.SelectedIndex].marks);
            }
            else
            {
                label5.Text = " ";
            }
        }
    }
}
