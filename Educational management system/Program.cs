using System.Collections;
using System.IO;
using System.Numerics;
using System.Xml.Serialization;
namespace Educational_management_system
{

    internal class Program
    {
        static void BuildCourses(ref ArrayList AvailableCourses)
        {
            string file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Courses data\\Course data.txt";
            using (StreamReader sr=File.OpenText (file))
            {
                string line = sr.ReadLine();
                int cnt = 1;
                while(line != null)
                {// name code id
                    string[] data = line.Split(' ');
                    Course c = new Course($"{data[0]} {data[1]}", data[2],cnt);
                    AvailableCourses.Add(c);
                    cnt++;
                    line = sr.ReadLine();
                }
            }

        }


        static void BuildDoctors(ref ArrayList allDoctorors,ref ArrayList AvailableCourses)
        {
            string file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Doctors data\\Names.txt";
            using(StreamReader sr=File .OpenText (file))
            {
                string line = sr.ReadLine();
                int cnt = 0;
                while (line != null)
                {
                    string[] data = line.Split(" ");
                    Doctor c = new Doctor(data[0], data[0], data[1],cnt);
                    allDoctorors.Add(c);
                    cnt++;
                    line = sr.ReadLine();
                }
            }

            file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Doctors data\\Courses.txt";

            using (StreamReader sr = File.OpenText(file))
            {
                string line = sr.ReadLine();
                int cnt = 0;
                while (line != null)
                {
                    if (line == "0")
                    {
                        cnt++;
                        line = sr.ReadLine();
                        continue;
                    }
                    int cc = 0;
                    foreach(Doctor d in allDoctorors)
                    {
                        if (cc == cnt)
                        {
                            string[] data = line.Split(' ');
                            for(int i = 0; i < data?.Length; i++)
                            {
                                foreach(Course course in AvailableCourses)
                                {
                                    if (course.code == data[i])
                                    {
                                        d.Add_course(course);
                                        course.Add_Dcotor(d);
                                        break;
                                    }
                                   
                                }
                            } break;

                        }
                        cc++;
                    }
                    line = sr.ReadLine();
                    cnt++;
                }
            }
        }
        
        static void BuildStudents(ref ArrayList allStudents,ref ArrayList AvailableCourses)
        {
            string file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Students data\\Names.txt";
            using (StreamReader sr = File.OpenText(file))
            {
                string line = sr.ReadLine();
                int cnt = 0;
                while (line != null)
                {
                    string[] data = line.Split(' ');
                    Student c = new Student($"{data[0]} {data[1]}", data[0], data[2],cnt);
                    allStudents.Add(c);
                    cnt++;
                    line = sr.ReadLine();
                }
            }

            file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Students data\\Courses.txt";

            using (StreamReader sr = File.OpenText(file))
            {
                string line = sr.ReadLine();
                int cnt = 0;
                while (line != null)
                {
                    
                    int cc = 0;
                    foreach (Student d in allStudents)
                    {
                        if (cc == cnt)
                        {
                            string[] data = line.Split(' ');
                            for (int i = 0; i < data?.Length; i++)
                            {
                                foreach (Course course in AvailableCourses)
                                {
                                    if (course.code == data[i])
                                    {
                                        d.Add_course(course);
                                        course.Add_Student(d);
                                        break;
                                    }

                                }
                            }
                            break;

                        }
                        cc++;
                    }
                    line = sr.ReadLine();
                    cnt++;
                }

            }
                
            BuildAssignments(ref allStudents,ref AvailableCourses);

        }

        static void BuildAssignments(ref ArrayList allStudents, ref ArrayList AvailableCourses)
        {
          string file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Courses data\\Assignments.txt";
            using (StreamReader sr = File.OpenText(file))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] data = line.Split(' ');
                    foreach(Course c in AvailableCourses)
                    {
                        if (c.code == data[0])
                        {
                            foreach(Assignment ass in c.Assignments)
                            {
                                if (ass.id == int.Parse(data[1]))
                                {
                                    for(int i = 2; i < data?.Length; i+=3)
                                    {
                                        foreach(Student s in allStudents)
                                        {
                                            if (s.id == int.Parse(data[i]))
                                            {
                                                Solution sol = new Solution(data[i + 1], s);
                                                sol.grade = int.Parse(data[i + 2]);
                                                ass.solutions.Add(sol);
                                                break;


                                            }

                                        }
                                    }
                                    break;

                                }
                            }
                            break;
                        }

                    }
                    line = sr.ReadLine();
                }
            }
        }

        static void StoreCourses(ref ArrayList courses)
        {
            string file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Courses data\\Course data.txt";
            StreamWriter writer = new StreamWriter(file);
            foreach(Course c in courses)
            {
                writer.WriteLine($"{c.name} {c.code}");
            }
            writer.Close();
            file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Courses data\\Assignments.txt";
            writer = new StreamWriter(file);
            foreach(Course c in courses)
            {
                foreach(Assignment ass in c.Assignments)
                {
                    writer.Write($"{c.code} {ass.id}");
                    foreach(Solution sol in ass.solutions)
                    {
                        writer.Write($"{sol.student.id} {sol.solution} {sol.grade}");
                    }writer.Write("\n");
                }

            }
            writer.Close();
        }

        static void StoreStudents(ref ArrayList allStudents)
        {
            string file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Students data\\Names.txt";
            StreamWriter writer = new StreamWriter(file);
            foreach(Student s in allStudents)
            {
                writer.WriteLine($"{s.name} {s.password}");
            }
            writer.Close();
            file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Students data\\Courses.txt";
            writer = new StreamWriter(file);
            foreach(Student s in allStudents)
            {
                foreach(Course c in s.Registered_Courses)
                {
                    writer.Write($"{c.code} ");
                }
                writer.Write($"\n");
            }
            writer.Close();
        }

        static void StoreDoctors(ref ArrayList allDoctors)
        {
            string file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Doctors data\\Names.txt";

            StreamWriter writer = new StreamWriter(file);
            foreach (Doctor d in allDoctors)
            {
                writer.WriteLine($"{d.name} {d.password}");
            }
            writer.Close();

            file = "C:\\Users\\pc1\\source\\repos\\Educational management system\\Dummy data\\Doctors data\\Courses.txt"; 

             writer = new StreamWriter(file);
            foreach (Doctor d in allDoctors)
            {
                if (d.courses.Count == 0)
                {
                    writer.Write("0");

                }
                else
                {
                    foreach(Course c in d.courses)
                    {
                        writer.Write($"{c.code} ");
                    }
                }
                writer.Write("\n");
            }
            writer.Close();

        }

        static void Main(string[] args)
        {
            ArrayList Availablecourses=new ArrayList();
            ArrayList allStudents = new ArrayList();
            ArrayList allDoctors = new ArrayList();
            BuildCourses(ref Availablecourses);
           BuildStudents(ref allStudents, ref Availablecourses);
            BuildDoctors(ref allDoctors, ref Availablecourses);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please make a choice : ");
                Console.WriteLine("  1-Login\n  2-Sign Up\n  3-Shutdown system\n");
                int choice = int.Parse("" + Console.ReadLine());
                if (choice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter username and password : ");
                    bool st = false, doc = false;
                    Student student = new Student();
                    Doctor doctor = new Doctor();
                    while(!st && !doc)
                    {
                        Console.WriteLine(" Username : ");
                        string username = Console.ReadLine();
                        Console.WriteLine(" Password : ");
                        string pass = Console.ReadLine();
                        foreach(Student s in allStudents)
                        {
                            if(s.checkpass(username, pass))
                            {
                                st = true;
                                student = s;
                                break;
                            }
                        }

                        foreach(Doctor d in allDoctors)
                        {
                            if(d.checkpass (username, pass))
                            {
                                doc = true;
                                doctor = d;
                                break;
                            }
                        }

                        if(!st && !doc)
                        {
                            Console.WriteLine("\n Username and Password are incorrect\n  Please enter a valid data :\n");
                        }
                    }

                    if (st)
                    {
                        Console.Clear();
                        Console.WriteLine($"Welcome {student.name} . You are logged in : ");
                        while (true)
                        {
                            Console.WriteLine("Please make a choice : ");
                            Console.WriteLine(" 1 - Register in a Course");
                            Console.WriteLine(" 2 - List my courses");
                            Console.WriteLine(" 3 - View Course");
                            Console.WriteLine(" 4 - Grades Report");
                            Console.WriteLine(" 5 - Log out");
                            choice = int.Parse("" + Console.ReadLine());
                            while (choice < 1 || choice > 5)
                            {
                                Console.Write("Please enter a valid choice");
                                choice = int.Parse("" + Console.ReadLine());

                            }
                            if (choice == 1)
                            {
                                Console.Clear();
                                ArrayList ids = new ArrayList();
                                foreach (Course c in Availablecourses)
                                {
                                    if (c.Students.Contains(student))
                                    {
                                        continue;
                                    }
                                    c.Course_view();
                                    ids.Add(c.id);
                                }
                                if (ids.Count == 0)
                                {
                                    Console.WriteLine("You are registered in all our available courses .");
                                    Console.WriteLine("\n Press any key to go back");
                                    Console.ReadLine();
                                    Console.Clear();
                                    continue;
                                }
                                Console.WriteLine("Which course id to register in : ");
                                int id = int.Parse("" + Console.ReadLine());
                                while (!ids.Contains(id))
                                {
                                    Console.WriteLine("Please enter a valid ID : ");
                                    id = int.Parse("" + Console.ReadLine());
                                }
                                foreach (Course c in Availablecourses)
                                {
                                    if (c.id == id)
                                    {
                                        c.Add_Student(student);
                                        student.Add_course(c);
                                        Console.WriteLine("You are registered in successfully\n");
                                        Console.WriteLine("  To go back enter any key");
                                        string s = Console.ReadLine();
                                        break;
                                    }
                                }
                            }

                            else if (choice == 2)
                            {
                                Console.Clear();
                                student.view_courses();
                                Console.WriteLine("  To go back enter any key");
                                string s = Console.ReadLine();
                            }

                            else if (choice == 3)
                            {
                                Console.Clear();
                                student.view_courses();
                                Console.WriteLine("Which course ID to view ?");
                                int id = int.Parse("" + Console.ReadLine());
                                while (!student.view_course(id))
                                {
                                    Console.WriteLine("Please enter a valid id :");
                                    id = int.Parse("" + Console.ReadLine());
                                }
                                Console.WriteLine("\n Please make a choice : ");
                                Console.WriteLine(" 1 - UnRegister from a course");
                                Console.WriteLine(" 2 - Submit Solution");
                                Console.WriteLine(" 3 - Back");
                                choice = int.Parse("" + Console.ReadLine());
                                while (choice < 1 || choice > 3)
                                {
                                    Console.Write("Please enter a valid choice");
                                    choice = int.Parse("" + Console.ReadLine());

                                }
                                if (choice == 1)
                                {
                                    Course course = student.unRegister(id);
                                    course.remove_student(student.id);
                                    Console.Clear();
                                    continue;

                                }
                                else if (choice == 2)
                                {
                                    Console.WriteLine("Which Assignment to submit ?");
                                    int i = int.Parse(""+ Console.ReadLine());
                                    Console.WriteLine("Please enter your solution :");
                                    string sol = Console.ReadLine();
                                    while (!student.SBSOL(id, i, sol))
                                    {
                                        Console.WriteLine("Please enter a valid Assignment id :");
                                        i = int.Parse("" + Console.ReadLine());
                                    }
                                }
                                else if (choice == 3)
                                {
                                    Console.Clear();
                                    continue;
                                }

                            }

                            else if (choice == 4)
                            {
                                Console.Clear();
                                student.report();
                                Console.WriteLine("  To go back press any key");
                                string s = Console.ReadLine();
                            }

                            else if (choice == 5)
                            {
                                Console.Write("Logging out");
                                for (int i = 0; i < 8; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(500);
                                }
                                break;
                            }
                            Console.Clear();

                        }

                    }

                    else if (doc)
                    {
                        Console.Clear();
                        Console.WriteLine($"Welcome Dr: {doctor.name} . You are logged in .");
                        while (true)
                        {
                            Console.WriteLine("Please make a choice");
                            Console.WriteLine(" 1 - List Courses");
                            Console.WriteLine(" 2 - Create Course");
                            Console.WriteLine(" 3 - View Course");
                            Console.WriteLine(" 4 - Log out");
                            choice = int.Parse("" + Console.ReadLine());
                            while (choice > 4 || choice < 1)
                            {
                                Console.Write("please enter a valid choice : ");
                                choice = int.Parse(Console.ReadLine());
                            }

                            if(choice == 1)
                            {
                                Console.Clear();
                                Console.WriteLine("All available courses are : \n");
                                foreach(Course c in Availablecourses)
                                {
                                    c.Course_view();
                                    Console.WriteLine();
                                }
                                Console.WriteLine("  To go back press any key");
                                string s = Console.ReadLine();
                            }

                            else if(choice == 2)
                            {
                                Course course = new Course();
                                Console.Write("Course name? (Two words only)");
                                course.name = Console.ReadLine();
                                Console.Write("Course code?");
                                course.code = Console.ReadLine();
                                course.id = Availablecourses.Count + 1;
                                course.Add_Dcotor(doctor);
                                Availablecourses.Add(course);
                                doctor.Add_course(course);
                                Console.WriteLine("Course is created successfully");
                                Console.WriteLine("  To go back press any key");
                                string s = Console.ReadLine();
                                Console.Clear();
                            }

                            else if(choice == 3)
                            {
                                Console.Clear();
                                if (!doctor.view_courses())
                                {
                                    Console.WriteLine("You are not teaching any courses .");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Which course ID to view");
                                    int id = int.Parse("" + Console.ReadLine());
                                    while (!doctor.view_course(id))
                                    {
                                        Console.Write("Please enter a valid id :");
                                        id = int.Parse("" + Console.ReadLine());

                                    }
                                    Console.WriteLine("  To go back press any key");
                                    string s = Console.ReadLine();
                                }

                            }

                            else if(choice == 4)
                            {
                                Console.Write("Logging out");
                                for (int i = 0; i < 8; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(500);
                                }
                                break;

                            }
                            Console.Clear();

                        }
                    }

                }

                else if(choice == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Please make a choice : ");
                    Console.WriteLine(" 1 - Doctor");
                    Console.WriteLine(" 2 - Student");
                    choice = int.Parse("" + Console.ReadLine());
                    while(choice<1 || choice > 2)
                    {
                        Console.Write("Please enter a valid choice");
                        choice = int.Parse("" + Console.ReadLine());
                    }

                    if (choice == 1)
                    {
                        Doctor doctor = new Doctor();
                        Console.WriteLine("Please enter your name (username) .");
                        doctor.username = Console.ReadLine();
                        doctor.name = doctor.username;
                        Console.WriteLine("Please enter your password .");
                        doctor.password = Console.ReadLine();
                        doctor.id = allDoctors.Count + 1;
                        allDoctors.Add(doctor);
                        Console.WriteLine("Your account is created successfully please try to login");
                        Console.ReadLine();
                    }
                    else
                    {

                        Student student = new Student();

                        Console.WriteLine("Please enter your first name (username) .");
                       student.username = Console.ReadLine();
                        Console.WriteLine("Please enter your second name (username) .");
                        student.name =$"{student.username} {Console.ReadLine()}";
                        Console.WriteLine("Please enter your password .");
                        student.password = Console.ReadLine();
                        student.id = allStudents.Count + 1;
                        allStudents.Add(student);
                        Console.WriteLine("Your account is created successfully please try to login");
                        Console.ReadLine();

                    }
                }

                else if(choice == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Your Application is shut down");
                    break;

                }
                else
                {
                    Console.WriteLine(" Please enter a valid choie : ");
                    Thread.Sleep(3000);
                    Console.Clear();
                }
            }

            StoreCourses(ref Availablecourses);
            StoreDoctors(ref allDoctors);
            StoreStudents(ref allStudents);

        }
    }
}
