using System;
using System.IO;
using System.Collections.Generic;

namespace DisModels3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new JourneyPerson();

            person.Execute();
        }

        public class JourneyPerson
        {
            public void Execute()
            {
                int[,] matrix;
                int[] optimalRoute;
                int optimalDistance = int.MaxValue;

                StreamReader reader = new StreamReader("C:/Users/admin/Desktop/DM/files/dm3.txt");
                string dimension = reader.ReadLine();
                int numRows = int.Parse(dimension);
                matrix = new int[numRows, numRows];

                for (int i = 0; i < numRows; i++)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(' ');
                    for (int j = 0; j < numRows; j++)
                    {
                        matrix[i, j] = int.Parse(values[j]);
                    }
                }
                reader.Close();

                int[] Reduce(int[] arr)
                {
                    int[] target = new int[arr.Length - 1];
                    for (int i = 0; i < target.Length; i++)
                    {
                        target[i] = arr[i + 1];
                    }
                    return target;
                }

                void Search(int[] route, int distance, List<int> remaining)
                {
                    if (remaining.Count == 0)
                    {
                        distance += matrix[route[route.Length - 1], route[0]];
                        if (distance < optimalDistance)
                        {
                            optimalDistance = distance;
                            route = Reduce(route);
                            Array.Copy(route, optimalRoute, route.Length - 1);
                        }
                        return;
                    }

                    if (distance >= optimalDistance)
                    {
                        return;
                    }

                    int lastCity = route[route.Length - 1];
                    foreach (int city in remaining)
                    {
                        int newDistance = distance + matrix[lastCity, city];
                        if (newDistance < optimalDistance)
                        {
                            int[] newPath = new int[route.Length + 1];
                            Array.Copy(route, newPath, route.Length);
                            newPath[route.Length] = city;
                            List<int> newRemaining = new List<int>(remaining);
                            newRemaining.Remove(city);
                            Search(newPath, newDistance, newRemaining);
                        }
                    }
                }

                int n = matrix.GetLength(0);
                optimalRoute = new int[n];
                for (int i = 0; i < n; i++)
                {
                    optimalRoute[i] = i;
                }

                Search(new int[] { 0 }, 0, new List<int>(optimalRoute));
                Console.WriteLine("Shortest path: " + string.Join(" -> ", optimalRoute) + " -> " + optimalRoute[0]);
            }
        }
    }
}