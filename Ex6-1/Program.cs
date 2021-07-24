using System;
//Изменить программу вывода таблицы функции так, чтобы можно было передавать функции
//типа double (double, double). Продемонстрировать работу на функции с функцией a* x^2 и
//функцией a* sin(x).
namespace Ex6_1
{
    public delegate double Fun(double x, double a);
    class Program
    {
        // Создаем метод, который принимает делегат
        // На практике этот метод сможет принимать любой метод
        // с такой же сигнатурой, как у делегата
        public static void Table(Fun F, double a, double x, double b)
        {
            Console.WriteLine("----- A ------- X -------- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} | {2,8:0.000} |", a, x, F(a,x));
                x += 1;
            }
            Console.WriteLine("---------------------");
        }
        // Создаем метод для передачи его в качестве параметра в Table
        public static double MyFunc(double a, double x)
        {
            return a * x * x;
        }

        public static double Sinus(double a, double x)
        {
            return a * Math.Sin(x);
        }
        static void Main()
        {
            // Создаем новый делегат и передаем ссылку на него в метод Table
            Console.WriteLine("Таблица функции a*x^2:");
            // Параметры метода и тип возвращаемого значения, должны совпадать с делегатом
            Table(new Fun(MyFunc), -2, -2, 2);
            Console.WriteLine("Таблица функции a*sin(x):");
            Table(new Fun(Sinus), 1, -2, 2);
        }
        
}
}

