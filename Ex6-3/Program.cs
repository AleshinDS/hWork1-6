using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
//Переделать программу Пример использования коллекций для решения следующих задач:
//а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
//б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся (*частотный
//массив);
//в) отсортировать список по возрасту студента;
//г) *отсортировать список по курсу и возрасту студента;
namespace Ex6_3
{
    class Student
    {
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string univercity { get; set; }
        public string faculty { get; set; }
        public string department { get; set; }
        public int age { get; set; }
        public int course { get; set; }
        public int group { get; set; }
        public string city { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int highCourse = 0;
           
            List<Student> studentList = new List<Student>();
            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "students.csv");
            Dictionary<int, int> cousreFrequency = new Dictionary<int, int>();
            String result = String.Format("{0,-10} {1,-10}\n", "Курс", "Количество студентов");
            while (!reader.EndOfStream)
            {
                string[] studentString = reader.ReadLine().Split(';');
                Student student = new Student();
                student.firstName = studentString[0];
                student.secondName = studentString[1];
                student.univercity = studentString[2];
                student.faculty = studentString[3];
                student.department = studentString[4];
                student.age = int.Parse(studentString[5]);
                student.course = int.Parse(studentString[6]);
                student.group = int.Parse(studentString[7]);
                student.city = studentString[8];

                studentList.Add(student);

                Console.WriteLine($"{studentString[0]} {studentString[1]} {studentString[2]} {studentString[3]}" +
                        $" {studentString[4]} {int.Parse(studentString[5])} {int.Parse(studentString[6])} {int.Parse(studentString[7])} {studentString[8]}");

                if (int.Parse(studentString[6]) == 5 | int.Parse(studentString[6]) == 6)
                {
                    highCourse++;
                }
                if (cousreFrequency.ContainsKey(int.Parse(studentString[6])))
                    cousreFrequency[int.Parse(studentString[6])] += 1;
                else
                    cousreFrequency.Add(int.Parse(studentString[6]), 1);
                ICollection<int> keys = cousreFrequency.Keys;
                foreach (int key in keys)
                    result += String.Format("{0,-10} {1,-10:N0}\n",
                                       key, cousreFrequency[key]);
                
            }

            Console.WriteLine($"\n{result}");
            Console.WriteLine($"всего студентов 5 и 6 курса: {highCourse}");
            
            reader.Close();
            





        }
    }

}
