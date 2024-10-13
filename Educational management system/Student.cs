using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_management_system
{
    internal class Student:Person
    {
       public ArrayList Registered_Courses = new ArrayList();
        public Student() : base()
        {

        }
    public Student(string name,string username,string pass,int id ) : base(name,username,pass,id)
        {

        }

        public void Add_course(Course course)
        {
            this.Registered_Courses.Add(course);
        }

        public void view_courses()
        {
            foreach(Course course in this.Registered_Courses)
            {
                Console.WriteLine($"ID : {course.id} - Course {course.name} with code {course.code}");
            }
        }


        public bool view_course(int id)
        {
            foreach(Course course in this.Registered_Courses)
            {
                if (course.id == id)
                {
                    Console.Clear();
                    Console.WriteLine($"Course Name :{course.name}");
                    Console.WriteLine($"Course Code :{course.code}");
                    Console.WriteLine($"Taught By :{course.taught_by.name}");
                    Console.WriteLine($"Registered Student :{course.Students.Count}");
                    Console.WriteLine($"This Course has {course.Assignments.Count} Assignment");
                    foreach(Assignment ass in course.Assignments)
                    {
                        Console.Write($"Assignment : {ass.id} - ");
                        bool flag = false;
                        foreach(Solution sol in ass.solutions)
                        {
                            if (sol.student == this)
                            {
                                flag = true;
                                Console.Write("Submitted - ");
                                if (sol.grade == 0)
                                {
                                    Console.Write("NA / 100");
                                }
                                else
                                {
                                    Console.Write($"{sol.grade} / 100");
                                }
                                Console.WriteLine();
                                break;
                            }
                        }
                        if (!flag)
                        {
                            Console.WriteLine("NOT Submitted - NA / 100");
                        }
                    }
                    return true;

                }
            }
            return false;
        }


        public void report()
        {
            foreach(Course c in this.Registered_Courses)
            {
                int sum = 0, cnt = 0;
                Console.WriteLine($"Course {c.name} with Code {c.code} has {c.Assignments.Count} Assignments");
                foreach(Assignment ass in c.Assignments)
                {
                    Console.Write($"Assignment {ass.id} ");
                    
                    bool flag = false; 
                    foreach(Solution sol in ass.solutions)
                    {
                        if(sol.student == this)
                        {
                            Console.Write("Submited - ");
                            if(sol.grade == 0)
                            {
                                Console.Write("NA / 100");
                            }
                            else
                            {
                                Console.Write($"{sol.grade} / 100");
                                sum += sol.grade;
                                cnt++;

                            }
                            flag = true;
                            Console.WriteLine();
                            break;
                        }
                    }
                    if(!flag)
                    {
                        Console.Write("NOT Submited - NA / 100");
                    }

                }
                Console.WriteLine($"Your total grade in this course is {sum} / {cnt * 100}\n");
            } 
        }

        public Course unRegister(int id)
        {
            Course rm=new Course();
            foreach(Course course in this.Registered_Courses)
            {
                if (course.id == id)
                {
                    rm = course;
                    this.Registered_Courses.Remove(rm);
                    break;
                }
            }
            return rm;
        }

        public bool SBSOL(int cid,int assid,string sol)
        {
            foreach(Course c in this.Registered_Courses) {
                if (c.id == cid)
                {
                    foreach(Assignment ass in c.Assignments)
                    {
                        if (ass.id == assid)
                        {
                            Solution s = new Solution(sol, this);
                            foreach(Solution solution in ass.solutions)
                            {
                                if (solution.student == this)
                                {
                                    Console.WriteLine($"You actually have submitted a solution to this assignment ");
                                    Console.WriteLine($"Do you want to replace it ?");
                                    Console.WriteLine("1 - YES");
                                    Console.WriteLine("2 - NO");
                                    int ch = int.Parse("" + Console.ReadLine());
                                   while(ch!=1 && ch != 2)
                                    {
                                        Console.Write("Please enter a valid choice : ");
                                        ch = int.Parse("" + Console.ReadLine());
                                    }
                                    if (ch == 1)
                                    {
                                        ass.solutions.Remove(solution);
                                        break;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }ass.solutions.Add(s);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }

}
