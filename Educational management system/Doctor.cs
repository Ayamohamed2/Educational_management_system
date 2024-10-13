using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_management_system
{
    internal class Doctor:Person
    {
        public ArrayList courses = new ArrayList();
        public Doctor():base()
        {

        }

        public Doctor(string name, string username, string pass, int id) : base( name,  username, pass,  id)
        {

        }
        
        public void Add_course(Course course)
        {
            courses.Add(course);
        }

        public bool view_courses()
        {
            if (this.courses.Count == 0) return false;
            foreach(Course c in courses)
            {
                Console.WriteLine($"ID : {c.id} Course {c.name}");
            }
            return true;
        }

        public bool view_course(int id)
        {
            foreach (Course c in courses)
            {
                if (c.id == id)
                {
                    Console.Clear();
                    Console.WriteLine($"Course Name : {c.name}\nCourse Code : {c.code}\nRegistered Students : {c.Students.Count}\nTotal Assignments : {c.Assignments.Count}");
                    Console.Write($"\nTo view Assignment solution and mark them enter 1 : ");
                    int ch = int.Parse("" + Console.ReadLine());
                    if (ch == 1)
                    {
                    foreach(Assignment ass in c.Assignments)
                        {
                            Console.Write($"Assignment {ass.id} - has {ass.solutions.Count} Solutions\n");
                            foreach (Solution sol in ass.solutions)
                            {
                                Console.WriteLine($" A Solution [ {sol.solution} ] IS Submited by {sol.student.name}");
                                Console.Write($"Set Grade please");
                                sol.grade = int.Parse("" + Console.ReadLine());

                            }
                        }
                    }
                    return true;
                }

            }
            return false;
        }


    }
}
