using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_management_system
{
    internal class Person
    {
        public int id;
        public string name;
        public string password; 
        public string username;
    public Person()
        {

        }

        public Person(string name,string username,string password,int id)
        {
            this.name = name;
            this.password = password;
            this.id = id;
            this.username = username;
        }


       public bool checkpass(string username,string password)
        {
            return (this.username == username && this.password == password);
        }
        
    }
}
