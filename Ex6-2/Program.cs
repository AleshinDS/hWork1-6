using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

//Модифицировать программу нахождения минимума функции так, чтобы можно было
//передавать функцию в виде делегата.
//а) Сделать меню с различными функциями и представить пользователю выбор, для какой
//функции и на каком отрезке находить минимум. Использовать массив (или список) делегатов,
//в котором хранятся различные функции.
//б) *Переделать функцию Load, чтобы она возвращала массив считанных значений. Пусть она
//возвращает минимум через параметр (с использованием модификатора out).

namespace Ex6_2
{
    public delegate double Fun(double x);
    class Program
    {
        public static double F(double x)
        {
            return x * x - 50 * x + 10;
        }
        public static void SaveFunc(string fileName, double a, double b, double h, Fun F)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            while (a <= b)
            {
                bw.Write(F(a));
                a += h;// a=a+h;
            }
            bw.Close();
            fs.Close();
        }
        public static double[] Load(string fileName, out double min)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double[] array = new double[fs.Length / sizeof(double)];
            min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                array[i] = d;
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return array;
        }

        static double SUM(double x)
        {
            return x + x;
        }

        static double SQR(double x)
        {
            return x * x;
        }

        static double SQRT(double x)
        {
            return Math.Sqrt(x);
        }
        static double myFun(double x)
        {
            return x * x * x - 4 * x;
        }

        static int GetInt(int max)
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int x) || x > max)
                    Console.Write($"Некорректный ввод, принимаются числовые значения от 0 до {max}. Повторите ввод: ");
                else return x;
        }

        static void GetInterval(out double a, out double b)
        {
            string[] interval = Console.ReadLine().Split(' ');
            a = double.Parse(interval[0], CultureInfo.InvariantCulture);
            b = double.Parse(interval[1], CultureInfo.InvariantCulture);
        }


        static void Print(double a, double b, double h, double[] values)
        {
            Console.WriteLine("------- X ------- Y -----");
            int index = 0;
            while (a <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} ", a, values[index]);
                a += h;
                index++;
            }
        }
        static void Main(string[] args)
                {
                    Console.WriteLine("Вас приветсвует программа нахождения минимума функции!");
                    List<Fun> functions = new List<Fun> { new Fun(SUM), new Fun(SQR), new Fun(SQRT), new Fun(myFun) };
                    Console.WriteLine("Выберите функцию для дальнейшей работы.");
                    Console.WriteLine("1) f(x)=y+y");
                    Console.WriteLine("2) f(x)=y*y");
                    Console.WriteLine("3) f(x)=y^1/2");
                    Console.WriteLine("4) f(x)=y^3-4y");
                    int userChoose = GetInt(functions.Count);

                    Console.WriteLine("Задайте отрезок для нахождения минимума в формате 'х1 х2':");

                    double a = 0;
                    double b = 0;
                    GetInterval(out a, out b);

                    Console.WriteLine("Задайте величинау шага:");
                    double h = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    SaveFunc("data.bin", a, b, h, functions[userChoose - 1]);
                    double min = double.MaxValue;
                    Console.WriteLine("Получены следующие значения функции: ");
                    Print(a, b, h, Load("data.bin", out min));
                    Console.WriteLine("Минимальное значение функции равняется: {0:0.00}", min);
                    Console.ReadKey();
                }
        
        }
    }



        

