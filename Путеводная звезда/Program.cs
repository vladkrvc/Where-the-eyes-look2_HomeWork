using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            // Считываем количество городов c консоли
            var cityCounter = Convert.ToInt32(Console.ReadLine());
            // Считываем количество путей с консоли
            var roadCounter = Convert.ToInt32(Console.ReadLine());

            // Cоздаем матрицу смежности
            var matrix = new int[cityCounter, cityCounter];

            // Заполняем матрицу смежности:
            for (var k = 0; k < roadCounter; k++)
            {
                // Считываем данные о дороге из консоли
                // Из какого города идет дорога
                var i = Convert.ToInt32(Console.ReadLine());
                // Куда идет дорога
                var j = Convert.ToInt32(Console.ReadLine());
                // Сколько составляет длинна дороги
                var size = Convert.ToInt32(Console.ReadLine());
                // Записываем эти данные в матрицу смежности
                matrix[i, j] = matrix[j, i] = size;
            }

            // Считываем номер города, с которого начинаем построение маршрута
            var start = Convert.ToInt32(Console.ReadLine());
            // Считываем номер города, куда надо построить маршрут
            var end = Convert.ToInt32(Console.ReadLine());

            // В массиве путей будут храниться объекты класса Путь
            var path = new Path[cityCounter];

            // Заполняем массив объектами с максимальным значением расстояния 
            for (var i = 0; i < path.Length; i++)
            {
                path[i] = new Path(Int32.MaxValue, i);
            }

            // Расстояние до стартового города равно нулю
            path[start].DistanceToTheCity = 0;

            // Очередь с объектами класса Путь
            var list = new Queue<Path>();
            list.Enqueue(path[start]);

            // Цикл будет продолжаться до тех пор, пока очередь не пуста
            while (list.Count != 0)
            {
                var currentCity = list.Dequeue();

                for (int i = 0; i < cityCounter; i++)
                {
                    // Если дорога есть, и если расстояние до этого города меньше текущего минимального
                    if (matrix[currentCity.NumberOfTheCity, i] != 0)
                    {
                        if (path[i].DistanceToTheCity > path[currentCity.NumberOfTheCity].DistanceToTheCity +
                            matrix[currentCity.NumberOfTheCity, i])
                        {
                            // Записываем новое расстояние в массив 
                            path[i].DistanceToTheCity = path[currentCity.NumberOfTheCity].DistanceToTheCity +
                                                        matrix[currentCity.NumberOfTheCity, i];
                            // Сохраняем в следующем ближайшем городе ссылку на текущий город
                            path[i].Previous = currentCity;
                            list.Enqueue(path[i]);
                        }
                    }
                }
            }

            // Выводим на экран путь из стартового города в конечный город
            if (path[end].DistanceToTheCity != Int32.MaxValue)
            {
                // Создаем массив для хранения пути
                var truePath = new List<int>();

                var node = path[end];
                // Пока не дошли до начального города
                while (node != null)
                {
                    // Добавляем в массив номер города
                    truePath.Add(node.NumberOfTheCity);
                    // Переходим к следующему городу
                    node = node.Previous;
                }

                // Выводим на экран путь
                for (var i = truePath.Count - 1; i >= 0; i--)
                {
                    Console.Write(truePath[i] + " ");
                }
            }
            else
            {
                Console.WriteLine("Дороги нет!");
            }
        }
    }

    public class Path
    {
        public int DistanceToTheCity;
        public int NumberOfTheCity;
        public Path Previous;

        public Path(int distance, int number)
        {
            DistanceToTheCity = distance;
            NumberOfTheCity = number;
        }
    }
}