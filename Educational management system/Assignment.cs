using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Educational_management_system
{
    internal class Assignment
    {
       public int id;
       public ArrayList solutions = new ArrayList();
       public Assignment(int id)
        {
            this.id = id;
        }
    }
}
