using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_management_system
{
    internal class Course
    {
       public string name;
        public string code;
       public int id;
        public Doctor taught_by;
        public ArrayList Students = new ArrayList();
        public ArrayList Assignments = new ArrayList();
        public Course()
        {
            Assignment ass = new Assignment(1);
            Assignments.Add(ass);
            ass= new Assignment(2);
            Assignments.Add(ass);
        }

        public Course(string name,string code,int id)
        {
            Assignment ass = new Assignment(1);
            Assignments.Add(ass);
            ass = new Assignment(2);
            Assignments.Add(ass);
            this.name = name;
            this.code = code;
            this.id = id;
        }

        public void Add_Dcotor(Doctor doctor)
        {
            this.taught_by = doctor;
        }

        public void Add_Student(Student student)
        {
            this.Students.Add(student);
        }

      public void remove_student(Student ss)
 {
     
             Students.Remove(ss);
     foreach (Assignment ass in this.Assignments)
     {
         foreach (Solution sol in ass.solutions)
         {
             if (sol.student.id == ss.id)
             {
                 ass.solutions.Remove(sol);
                 break;

             }
         }
     } 
 }
      
        public void Course_view()
        {
            Console.WriteLine($"ID : {this.id} - Course : {this.name} with Code : {this.code} is taught by : {this.taught_by.name}");
        }

    }
}
