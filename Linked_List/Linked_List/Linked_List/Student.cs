using System;

namespace Linked_List
{
    public class Student : IComparable<Student>
    {
        private string name;
        private Node<Course> listOfCourses;

        public Student(string name)
        {
            this.name = name;
        }

        public Student(string name, Node<Course> c)
        {
            this.name = name;
            this.listOfCourses = c;
        }

        public void AddCourse(Course c)
        {
            if (listOfCourses == null)
            {
                listOfCourses = new Node<Course>(c);
                return;
            }

            Node<Course> currentNode = listOfCourses;

            while (currentNode.HasNext())
            {
                currentNode = currentNode.GetNext();
            }
            currentNode.SetNext(new Node<Course>(c));
        }

        public Node<Course> GetCoursesList()
        {
            return listOfCourses;
        }

        public string GetName() { return name; }

        public static string PrintList<T>(Node<T> list)
        {
            if (list == null)
            {
                return "";
            }
            return list.GetValue() + " " + PrintList(list.GetNext());
        }

       
        public int GetAverageGrade()
        {
            int sumGrades = 0, countCourses = 0;
            return GetAverageGrade(listOfCourses, sumGrades, countCourses);
        }

        public int GetAverageGrade(Node<Course> courseNode, int sumGrades, int countCourses)
        {
            if (courseNode == null)
            {
                if (countCourses == 0) return 0; // Avoid an exception if dividing by zero
                return sumGrades / countCourses;
            }
            sumGrades += courseNode.GetValue().GetGrade();
            countCourses++;
            return GetAverageGrade(courseNode.GetNext(), sumGrades, countCourses);
        }


        public int GetAmountOfFailedCourses()
        {
            int numFailedCourses = 0;
            return GetAmountOfFailedCourses(listOfCourses, numFailedCourses);
        }

        public int GetAmountOfFailedCourses(Node<Course> coursesList, int numFailedCourses)
        {
            if (coursesList == null) return numFailedCourses;
            if (!(coursesList.GetValue().IsPassingGrade()))
                numFailedCourses++;
            return GetAmountOfFailedCourses(coursesList.GetNext(),numFailedCourses);
        }


        public int CompareTo(Student s)
        {
            return this.name.CompareTo(s.name);
        }

        public override string ToString()
        {
            string str = $"{name} - {PrintList(listOfCourses)}\n";
            return str;
        }

        public  string DisplayStudentWithAverage()
        {
            string str = $"{name} - Average:{GetAverageGrade()} | {PrintList(listOfCourses)}\n";
            return str;
        }


    }
}
