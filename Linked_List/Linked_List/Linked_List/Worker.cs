using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linked_List
{
    public class Worker : IComparable<Worker>
    {
        private string name;
        private double salary;

        public Worker(string name, double salary)
        {
            this.name = name;
            this.salary = salary;
        }

        public string GetName() { return name; }



        //העמסה על המתודה compareTo 
        public int CompareTo(Worker worker)
        {
            return this.name.CompareTo(worker.name);           
        }

        public override bool Equals(object w)
        {
            if (w is Worker)
            {
                Worker worker = (Worker)w;
                return this.name.Equals(worker.name) && this.salary.Equals(worker.salary);
            }
            return false;
        }
        public override string ToString()
        {
            return this.name + " " + this.salary;
        }
    }
}
