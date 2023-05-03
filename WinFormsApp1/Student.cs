using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    [Serializable]
    public class Discplines
    {
        public string Disc { get; set; }
        public int marks { get; set; }
        public Discplines() { }
        public Discplines(string _disc, int _mark) {
            Disc = _disc;
            marks = _mark;
        }

    }
    public class Discp
    { 
        public string Name { get; set; }
        public Discp(string _name) {
            Name = _name;
        }
        public Discp() {
            Name = string.Empty;
        }
    }
    [Serializable]
    public class Student
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public List<Discplines> discplines = new();
        public Student() 
        { 
            Name = string.Empty;
            Surname = string.Empty;
        }
        public Student(string _surname, string _name, int _id)
        {
            Name = _name;
            Surname = _surname;
            ID = _id;
        }
        public void AddDiscp(string _name, int mark)
        {
            bool en = true;
            for (int i = 0; i < discplines.Count; i++)
            {
                if(_name == discplines[i].Disc)
                {
                    discplines[i].marks = mark;
                    en = false;
                }
            }
            if (en)
            {
                discplines.Add(new Discplines(_name, mark));
            }
        }
        public List<string> Discipline(List<Student> a)
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < a[i].discplines.Count; j++)
                {
                    Discp temp = new Discp(a[i].discplines[j].Disc);
                    if (strings.Count != 0)
                    {
                        bool Enabled = true;
                        for (int k = 0; k < strings.Count; k++)
                        {
                            if (strings[k] == temp.Name)
                            {
                                Enabled = false;
                                break;
                            }
                        }
                        if (Enabled) strings.Add(temp.Name);
                    }
                    else
                    {
                        strings.Add(temp.Name);
                    }
                }
            }
            return strings;
        }

        public List<string> Discipline(Student a)
        {
            List<string> strings = new List<string>();
            for (int j = 0; j < a.discplines.Count; j++)
            {
                Discp temp = new Discp(a.discplines[j].Disc);
                if (strings.Count != 0)
                {
                    bool Enabled = true;
                    for (int k = 0; k < strings.Count; k++)
                    {
                        if (strings[k] == temp.Name)
                        {
                            Enabled = false;
                            break;
                        }
                    }
                    if (Enabled) strings.Add(temp.Name);
                }
                else
                {
                    strings.Add(temp.Name);
                }
            }
            return strings;
        }

        public List<Student> Filtered(List<Student> a, string disc) {
            List<Student> temp = new();
            int i = 0;
            while (i < a.Count)
            {
                int j = 0;
                while (j < a[i].discplines.Count)
                {
                    if (a[i].discplines[j].Disc == disc)
                    {
                        temp.Add(a[i]);
                        break;
                    }
                    j++;
                }
                i++;
            }
            return temp;
        }
    }

}

