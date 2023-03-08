using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linked_List
{
    public class Course : IComparable<Course>
    {
        private string code;
        private int grade;

        public Course(string code,int grade)
        {
            this.code = code;
            this.grade = grade;
        }

        public Course(string code)
        {
            this.code = code;          
        }

        public int CompareTo(Course c)
        {
            return this.code.CompareTo(c.code);

        }


       

        public void SetGrade(int grade) {  this.grade = grade; }
        public int GetGrade() { return grade; }

        public bool IsPassingGrade()
        {
            return grade >= 55;//passing grade can be defined as a class static variable 
        }

        public override string ToString()
        {
            return $"Course: {code}, grade ={grade}.";
        }
    }
}
