using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лопатки_с_номерами
{
    internal class Program
    {
        //Проверка балансировки массива
        static void RLCheckArr(float[,] balanced)
        {
            float RightHalfSum = 0;
            float LeftHalfSum = 0;

            if (balanced.GetLength(1) % 2 != 0)
            {
                LeftHalfSum = balanced[1, balanced.GetLength(1) - 1] / 2;
                RightHalfSum = balanced[1, balanced.GetLength(1) - 1] / 2;

                for (int i = 0; i < balanced.GetLength(1) / 2; i++)
                {
                    LeftHalfSum += balanced[1, i];
                }

                for (int i = balanced.GetLength(1) / 2; i < balanced.GetLength(1) - 1; i++)
                {
                    RightHalfSum += balanced[1, i];
                }
            }
            else
            {
                for (int i = 0; i < balanced.GetLength(1) / 2; i++)
                {
                    LeftHalfSum += balanced[1, i];
                }

                for (int i = balanced.GetLength(1) / 2; i < balanced.GetLength(1); i++)
                {
                    RightHalfSum += balanced[1, i];
                }
            }

            float allSum = RightHalfSum + LeftHalfSum;
            Console.WriteLine($"Масса левой половины диска: {LeftHalfSum}\tМасса правой половины диска: {RightHalfSum}\nСуммарная масса: {allSum}");

        }

        //Сортировка двухмерного массива
        static void DoubleArraySort(float[,] bladesWithNumbers)
        {
            float[] forSort = new float[bladesWithNumbers.GetLength(1)];

            //Создание одномерного массива для сортировки
            for (int i = 0; i <  bladesWithNumbers.GetLength(1); i++)
            {
                forSort[i] = bladesWithNumbers[1,i];
            }

            Array.Sort(forSort);

            //Перенос порядковых номеров с учетом сортировки
            for (int i = 0; i < forSort.Length; i++)
            {
                for (int j = 0; j < forSort.Length; j++)
                {
                    if (forSort[i] == bladesWithNumbers[1,j])
                    {
                        bladesWithNumbers[0, i] = j + 1;
                    }
                }
            }

            //Присвоение значений обратно
            for (int i = 0;i < bladesWithNumbers.GetLength(1); i++)
            {
                bladesWithNumbers[1,i] = forSort[i];
            }
        }

        //Нахождение похожего значения и расстановка лопаток
        static void FindSimilar(float[,] bladesWuthNumbers, float[,] balanced)
        {
            int lastSimilarPosition = 1;
            float differenceBetween;
            float lastSimilarValue = bladesWuthNumbers[1, lastSimilarPosition];
            float lastSimilarNumber = bladesWuthNumbers[0, lastSimilarPosition];

            for (int i = 0; i < bladesWuthNumbers.GetLength(1) / 2; i++)
            {
                differenceBetween = 1;
                balanced[1, i] = bladesWuthNumbers[1, i];
                balanced[0, i] = bladesWuthNumbers[0, i];

                for (int j = i + 1; j < balanced.GetLength(1) - i; j++)
                {
                    if (Math.Abs(bladesWuthNumbers[1, i] - bladesWuthNumbers[1, j]) < differenceBetween)
                    {
                        differenceBetween = Math.Abs(bladesWuthNumbers[1, i] - bladesWuthNumbers[1, j]);
                        balanced[1, bladesWuthNumbers.GetLength(1) / 2 + i] = bladesWuthNumbers[1, j];
                        balanced[0, bladesWuthNumbers.GetLength(1) / 2 + i] = bladesWuthNumbers[0, j];

                        lastSimilarPosition = j;
                        lastSimilarValue = bladesWuthNumbers[1, lastSimilarPosition];
                        lastSimilarNumber = bladesWuthNumbers[0, lastSimilarPosition];
                    }
                }
                //Перенос значений в конец массива, что бы они не использовались повторно
                bladesWuthNumbers[1, lastSimilarPosition] = bladesWuthNumbers[1, bladesWuthNumbers.GetLength(1) - (i + 1)];
                bladesWuthNumbers[1, bladesWuthNumbers.GetLength(1) - (i + 1)] = lastSimilarValue;

                bladesWuthNumbers[0, lastSimilarPosition] = bladesWuthNumbers[0, bladesWuthNumbers.GetLength(1) - (i + 1)];
                bladesWuthNumbers[0, bladesWuthNumbers.GetLength(1) - (i + 1)] = lastSimilarNumber;

            }

            if (bladesWuthNumbers.GetLength(1) % 2 != 0)
            {
                balanced[1, balanced.GetLength(1) - 1] = bladesWuthNumbers[1, bladesWuthNumbers.GetLength(1) / 2];
                balanced[0, balanced.GetLength(1) - 1] = bladesWuthNumbers[0, bladesWuthNumbers.GetLength(1) / 2];
             }

        }

        //Заполнение массива случайными массами лопаток и присвоение им порядкового номера
        static void FillArr(float[,] array)
        {
            Random random = new Random();
            int position = 1;

            for (int i = 0; i < array.GetLength(1); i++,position++)
            {
                array[0, i] = position;
                array[1,i] = (float)Math.Round(random.NextDouble(), 6)*10;
            }
        }

        //Вывод массива
        static void PrintArr(float[,] array)
        {
            for (int i = 0;i < array.GetLength(0);i++)
            {
                for (int j = 0;j < array.GetLength(1);j++)
                {
                    Console.Write(array[i,j] +" ");
                }
                Console.WriteLine();
            }
        }

        //Для вывода только номеров лопаток
        static void PrintNumbers(float[,] array)
        {
            for (int i = 0; i < array.GetLength(1) ; i++) 
            {
                Console.Write(array[0,i] + " ");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите число лопаток (26-100):");
            int bladeValue = 0;

            while (bladeValue <= 25 || bladeValue > 100)
            {
                try
                {
                    bladeValue = int.Parse(Console.ReadLine());
                    if (bladeValue <= 25)
                    {
                        Console.WriteLine("Слишком мало, попробуйте еще раз:");
                    }
                    else if (bladeValue > 100)
                    {
                        Console.WriteLine("Слишком много, попробуйте еще раз:");
                    }
                }
                catch
                {
                    Console.WriteLine("Ввод только целых чисел, попробуйте еще раз:");
                    continue;
                }
            }
            Console.WriteLine();

            float[,] bladesWithNumbers = new float[2, bladeValue];
            float[,] balanced = new float[2, bladeValue];

            FillArr(bladesWithNumbers);

            Console.WriteLine("Номера лопаток в исходном порядке:");
            PrintArr(bladesWithNumbers);
            Console.WriteLine();
            RLCheckArr(bladesWithNumbers);
            Console.WriteLine();

            DoubleArraySort(bladesWithNumbers);
            FindSimilar(bladesWithNumbers, balanced);

            Console.WriteLine("Номера лопаток в отсортированном порядке:");
            PrintArr(balanced); 
            Console.WriteLine();
            RLCheckArr(balanced);
            
        }
    }
}
