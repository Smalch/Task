using System;
using System.IO;
using System.Collections.Generic;

namespace Task2
{

    public abstract class Figure
    {
        public Figure(string sides) 
        {
            this.sides = new List<double>();
            foreach (var side in sides.Split(' '))
            {
                this.sides.Add(Convert.ToDouble(side));
            }
        }
        protected List<double> sides;
        /// <summary>
        /// Должен считать площадь фигуры
        /// </summary>
        /// <returns> Возврящает площадь фигуры</returns>
        protected abstract double Area();
        /// <summary>
        /// Должен считать периметр фигуры
        /// </summary>
        /// <returns>Возврящает периетр фигуры</returns>
        protected abstract double Perimeter();
        /// <summary>
        /// Вывод информации о фигуре
        /// </summary>
        public void Info() 
        {
            Console.WriteLine("Количество сторон = " + sides.Count);
            Console.WriteLine("Количество углов = " + (sides.Count-1));
            Console.WriteLine("Площадь = " + Area());
            Console.WriteLine("Периметр = " + Perimeter());
        }

       
    }


    public class Triangle : Figure
    {
        public Triangle(string sides):base(sides){}

        /// <summary>
        /// Считает площадь треугольника
        /// </summary>
        /// <returns> Возвращает площадь треугольника</returns>
        protected override double Area()
        {
            return Math.Sqrt(Perimeter() * (Perimeter() - sides[0]) * (Perimeter() - sides[1]) * (Perimeter() - sides[2]));
        }
        /// <summary>
        /// Считает периметр треугольника
        /// </summary>
        /// <returns>Возвращает периметр треугольника</returns>
        protected override double Perimeter()
        {
            return sides[0] + sides[1] + sides[2];
        }
    }

    public class Circle : Figure
    {
        public Circle(string sides) : base(sides) { }
        /// <summary>
        /// Считает площадь круга
        /// </summary>
        /// <returns>Возвращает площадь круга</returns>
        protected override double Area()
        {
            return sides[0] * sides[0] * Math.PI;
        }
        /// <summary>
        /// Считеат длину окружности
        /// </summary>
        /// <returns>Возврящает длину окружности</returns>
        protected override double Perimeter()
        {
            return 2* sides[0] * Math.PI;
        }
    }
    public class Rectangle : Figure
    {
        public Rectangle(string sides) : base(sides) { }
        /// <summary>
        /// Считает площадь прямоугольника
        /// </summary>
        /// <returns>Возвращает площадб прямоугольника</returns>
        protected override double Area()
        {
            return sides[0] * sides[1] ;
        }
        /// <summary>
        /// Считает периметр прямоугольника
        /// </summary>
        /// <returns>Возвращает периметр прямоугольника</returns>
        protected override double Perimeter()
        {
            return (sides[0] + sides[1])*2;
        }
    }









    class Task2
    {
        static void Main(string[] args)
        {
            string big_str=Read_from_file();
            List<Figure> figures = Create_figures_list(big_str);
            foreach(Figure figure in figures)
            {
                figure.Info();
            }
        }
        /// <summary>
        /// Разбивает строку с данными из файла на строки, содержащие информацию об одной фигуре
        /// Создает и заполняет лист фигур на основе данных из полученной строки
        /// </summary>
        /// <param name="big_str">Строка, содержащая все данные из файла</param>
        /// <returns>Лист в котором записаны все фигуры на основе полученных из файла данных</returns>
        static List<Figure> Create_figures_list(string big_str)
        {
            string figure;
            int i = 0;
            int n = 0;
            List<Figure> figures = new List<Figure>();
            foreach (string figure_pr in big_str.Split('\n'))
            {
                figure= figure_pr.Trim(' ','\n','\r');
                if (i != 0 && n > 0)
                {
                    Figure figuree;
                    switch (big_str.Split(' ').Length)
                    {
                        case 2:
                            figuree = new Rectangle(figure);
                            break;
                        case 3:
                            figuree = new Triangle(figure);
                            break;
                        default:
                            figuree = new Circle(figure);
                            break;
                    }
                    figures.Add(figuree);
                    n--;
                }
                if (i == 0)
                {
                    n = Convert.ToInt32(figure);
                    i++;
                }
            }

            return (figures);
        }

        /// <summary>
        /// Читает данные из файла в строку
        /// </summary>
        /// <returns>Возвращает строку со всеми данными из файла</returns>
        static string Read_from_file()
        {
            string big_str="";
            Console.WriteLine("Введите адрес файла источника");
            string start_address = @"D:\test.txt";//Console.ReadLine();
            try
            {
                using (StreamReader sr = new StreamReader(start_address))
                {
                    big_str = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Read_from_file();
            }
            return (big_str);
        }
    }
}
