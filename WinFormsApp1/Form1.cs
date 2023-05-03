using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{

    public partial class Form1 : Form
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
        List<Student> students = new();
        int a = 0;
        List<Student> Filter = new();
        List<string> dis = new();
        bool init = true;
        BindingSource bindingSource3 = new();
        int mode = 0;
        BindingSource bindingSource1 = new BindingSource();

        public Form1()
        {
            students.Add(new Student("Рєпін", "Ілля", students.Count));
            students.Add(new Student("Рєпін", "Ілля", students.Count));
            students.Add(new Student("Aєпін", "Ілля", students.Count));
            students.Add(new Student("Рєпін", "Ілля", students.Count));
            students.Add(new Student("Рєпін", "Ілля", students.Count));
            students[3].AddDiscp("Математика", 90);
            students[3].AddDiscp("Математика", 91);
            students[4].AddDiscp("Математика", 90);
            students[1].AddDiscp("Матема", 90);

            students[3].AddDiscp("Матема", 89);
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;



            dis = new Student().Discipline(students);
            BindingSource bindingSource2 = new BindingSource(dis, null);
            comboBox1.DataSource = bindingSource2;

            bindingSource1.DataSource = students;
            dataGridView1.DataSource = bindingSource1;

            List<Student> Filter = new Student().Filtered(students, dis[a]);
            BindingSource bindingSource3 = new BindingSource();
            bindingSource3.DataSource = Filter;
            dataGridView2.DataSource = bindingSource3;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                mode = 0;
                Hide();
                Form2 form2 = new Form2(students[e.RowIndex], this, mode, comboBox1, students);
                form2.Show();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!init)
            {
                a = comboBox1.SelectedIndex;
                dis = new Student().Discipline(students);
                if (dis.Count != 0)
                {
                    Filter = new Student().Filtered(students, dis[a]);
                    bindingSource3.DataSource = Filter;
                    dataGridView2.DataSource = bindingSource3;
                }
                else
                {
                    dataGridView2.DataSource = new List<Student>();
                }
            }
            Update();
            init = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int mode = 1;
            Form2 form = new(students, this, mode, dataGridView1, comboBox1);
            form.Text = "Додавання";
            Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (students.Count > 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y]);
                dis = new Student().Discipline(students);
                if (dis.Count > 0)
                {
                    a = comboBox1.SelectedIndex;
                    Filter = new Student().Filtered(students, dis[a]);
                    bindingSource3.DataSource = Filter;
                    dataGridView2.DataSource = bindingSource3;
                    dis = new Student().Discipline(students);
                    BindingSource bindingSource2 = new BindingSource(dis, null);
                    comboBox1.DataSource = bindingSource2;
                }
                else
                {
                    dataGridView2.DataSource = null;
                    comboBox1.DataSource = null;
                }
            }
            else
            {
                dataGridView1 = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            students.Sort(delegate (Student x, Student y)
            {
                if (x.ID == null && y.ID == null) return 0;
                else if (x.ID == null) return -1;
                else if (y.ID == null) return 1;
                else return x.ID.CompareTo(y.ID);
            });
            dataGridView1.Refresh();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
                students.Sort(delegate (Student x, Student y)
                {
                    if (x.Surname == null && y.Surname == null) return 0;
                    else if (x.Surname == null) return -1;
                    else if (y.Surname == null) return 1;
                    else return x.Surname.CompareTo(y.Surname);
                });
            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("Users.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, students);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("Users.xml", FileMode.OpenOrCreate))
            {
                students = (List<Student>)serializer.Deserialize(fs);
            }
            init = true;
            BindingSource bindingSource2 = new BindingSource(new Student().Discipline(students), null);
            comboBox1.DataSource = bindingSource2;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            bindingSource1.DataSource = students;
            dataGridView1.DataSource = bindingSource1;
            a = comboBox1.SelectedIndex;
            BindingSource bindingSource3 = new BindingSource();
            bindingSource3.DataSource = new Student().Filtered(students, new Student().Discipline(students)[a]);
            dataGridView2.DataSource = bindingSource3;
            dataGridView1.Refresh();
            dataGridView2.Refresh();
        }
    }
}