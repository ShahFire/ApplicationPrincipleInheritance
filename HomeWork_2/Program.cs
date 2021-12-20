using System;
using System.Collections.Generic;

namespace HomeWork_2
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonnelOfficer personnelOfficer = new PersonnelOfficer("Светлана", "Татаринова", "Борисовна");
            Student stud1 = personnelOfficer.MakeStudent("Артур", "Шодиев", "Хушвахтович");
            Student stud2 = personnelOfficer.MakeStudent("Иван", "Чесновков", "Александравич");
            Student stud3 = personnelOfficer.MakeStudent("Дана", "Шишова", "Ивановна");
            Teacher teacher = personnelOfficer.MakeTeacher("Илья", "Климов", "Витальевич");

            Group group = personnelOfficer.MakeGroup();
            group.SetTeacher(teacher);
            teacher.Lecture = 23;
            teacher.Lectured();
            group.NameGroup = "3-1П9";
            group.AddStudent(stud1);
            group.AddStudent(stud2);
            group.AddStudent(stud3);

            Console.WriteLine(personnelOfficer.GetAllFIO());
            Console.WriteLine(teacher.GetAllFIO());
            Console.WriteLine(stud3.GetAllFIO());

            Console.WriteLine(stud3.GetGroup());
            Console.WriteLine(personnelOfficer.Post);
            Console.WriteLine(teacher.Post);
            Console.WriteLine(group.GetNumber());
            stud2.Deducted();
            Console.WriteLine(group.GetNumber());
            Console.WriteLine(teacher.Lecture);
            teacher.Lectured();
            Console.WriteLine(teacher.Lecture);
        }
    }
    public class Group
    {
        private string namegroup;
        private Teacher teacher;
        private List<Student> students = new List<Student>();

        public void SetTeacher(Teacher teacher)
        {
            teacher.SetGroup(this);
            this.teacher = teacher;
        }
        public void AddStudent(Student student)
        {
            this.students.Add(student);
            student.group = this;
        }

        public string NameGroup
        {
            get
            {
                return (namegroup);
            }
            set
            {
                this.namegroup = value;
                foreach (Student student in students) student.group = this;
            }
        }
        public int GetNumber()
        {
            return (this.students.Count);
        }
        public void UpData()
        {
            try
            {
                foreach (Student student in students)
                {
                    if (student.studies == false) students.Remove(student);
                }
            }
            catch { }
        }
        public List<Student> GroupList()
        {
            return (this.students);
        }
    }

    public abstract class Human
    {
        private string name;
        private string middlename;
        private string surname;

        public Human(string name, string middlename, string surname)
        {
            this.name = name;
            this.surname = surname;
            this.middlename = middlename;
        }
        public string GetAllFIO()
        {
            string[] fio = new string[] { this.middlename, this.surname };
            string allfio = name;
            foreach (string i in fio)
            {
                if (i != null)
                {
                    allfio = allfio + " " + i;
                }
            }
            return (allfio);
        }
    }
    public abstract class Worker : Human
    {
        public Worker(string name, string middlename, string surname, string post) : base(name, middlename, surname)
        {
            this.post = post;
        }
        private string post;
        public string Post
        {
            get
            {
                return (this.post);
            }
        }
    }

    public class PersonnelOfficer : Worker
    {
        public PersonnelOfficer(string name, string middlename, string surname)
            : base(name, middlename, surname, "Кадровик") { }
        public void AddStudent(Group group, Student student)
        {
            group.AddStudent(student);
        }
        public Student MakeStudent(string name, string middlename, string surname)
        {
            return (new Student(name, middlename, surname));
        }
        public Teacher MakeTeacher(string name, string middlename, string surname)
        {
            return (new Teacher(name, middlename, surname));
        }
        public Group MakeGroup()
        {
            return (new Group());
        }
    }

    public class Teacher : Worker
    {
        public Teacher(string name, string middlename, string surname)
            : base(name, middlename, surname, "Препод") { }
        private int lecture;
        private Group group = null;
        public int Lecture
        {
            set
            {
                if (group != null) this.lecture = value;
            }
            get
            {
                return (this.lecture);
            }
        }
        public void Lectured()
        {
            this.lecture--;
        }
        public void SetGroup(Group group)
        {
            this.group = group;
        }
    }
    public class Student : Human
    {
        public Student(string name, string middlename, string surname) : base(name, middlename, surname) { }
        public Group group;
        public bool studies = true;

        public string GetGroup()
        {
            return (this.group.NameGroup);
        }
        public void Deducted()
        {
            this.studies = false;
            group.UpData();
        }
    }
}
