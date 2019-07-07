//
// Created by yurik_322 on 06/03/19.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab1AI.Functions;


namespace Lab1AI
{
    class Program
    {
        readonly static int[] dx = { -1, 1, -1, 1 };
        readonly static int[] dy = { -1, 1, 1, -1 };


        static void Main(string[] args)
        {
            if (Constants.SIZE_CHESSBOARD <= 3)
            {
                MemberType current = new MemberType(), working = new MemberType(), best = new MemberType();

                Solution.InitializeSolution(ref current);
                Solution.ComputeEnergy(ref current);

                int timer = 0, accepted = 0;
                bool isSolution = false, useNewSolution = false;
                double temperature = Constants.START_TEMPERATURE;


                // (7):
                // Безпосередня реалізація алгоритму відпалу:


                best.energy = current.energy;
                // до (5) Допоміжна функція копіювання одного розв’язку в інший:
                Solution.CopySolution(ref working, current);


                while (temperature > Constants.FINISH_TEMPERATURE)
                {
                    Console.WriteLine("Temperature : " + temperature);
                    // Рандомна вибірка:
                    for (int step = 0; step < Constants.COUNT_ITERATIONS; step++)
                    {
                        // до (3) Випадкова зміна розв’язку та початкова ініціалізація:
                        Solution.RandomSolution(ref working);
                        // до (4) Допоміжна функція для оцінки розв’язку:
                        Solution.ComputeEnergy(ref working);

                        if (working.energy <= current.energy)
                        {
                            useNewSolution = true;
                        }
                        else
                        {
                            double test = Solution.GetRand.NextDouble();
                            double delta = working.energy - current.energy;
                            double calc = Math.Exp(-(delta / temperature));

                            if (calc > test)
                            {
                                accepted++;
                                useNewSolution = true;
                            }
                        }
                        if (useNewSolution)
                        {
                            useNewSolution = false;
                            // до (5) Допоміжна функція копіювання одного розв’язку в інший:
                            Solution.CopySolution(ref current, working);

                            if (current.energy < best.energy)
                            {
                                // до (5) Допоміжна функція копіювання одного розв’язку в інший:
                                Solution.CopySolution(ref best, current);
                                isSolution = true;
                            }
                        }
                        else
                        {
                            // до (5) Допоміжна функція копіювання одного розв’язку в інший:
                            Solution.CopySolution(ref working, current);
                        }
                    }
                    Console.WriteLine("Timer: " + (timer++) + " Temperature: " + temperature + " Best energy: " + best.energy + " Accepted " + accepted);

                    Console.WriteLine("Best energy = " + best.energy);

                    if (best.energy == 0)
                        break;  //

                        temperature *= Constants.ALPHA;
                }

                if (isSolution)
                {
                    Console.WriteLine("Best energy = " + best.energy);
                    // до (6) Допоміжна функція виводу результату на екран у вигляді шахової дошки:
                    Solution.PrintSolution(best);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR, CHESSBOARD is too small!");
                Console.ReadKey();
            }

            else
            {
                MemberType current = new MemberType(), working = new MemberType(), best = new MemberType();

                Solution.InitializeSolution(ref current);
                Solution.ComputeEnergy(ref current);

                int timer = 0, accepted = 0;
                bool isSolution = false, useNewSolution = false;
                double temperature = Constants.START_TEMPERATURE;


                // (7):
                // Безпосередня реалізація алгоритму відпалу:


                best.energy = current.energy;
                // до (5) Допоміжна функція копіювання одного розв’язку в інший:
                Solution.CopySolution(ref working, current);


                while (temperature > Constants.FINISH_TEMPERATURE)
                {
                    Console.WriteLine("Temperature : " + temperature);
                    // Рандомна вибірка:
                    for (int step = 0; step < Constants.COUNT_ITERATIONS; step++)
                    {
                        // до (3) Випадкова зміна розв’язку та початкова ініціалізація:
                        Solution.RandomSolution(ref working);
                        // до (4) Допоміжна функція для оцінки розв’язку:
                        Solution.ComputeEnergy(ref working);

                        if (working.energy <= current.energy)
                        {
                            useNewSolution = true;
                        }
                        else
                        {
                            double test = Solution.GetRand.NextDouble();
                            double delta = working.energy - current.energy;
                            double calc = Math.Exp(-(delta / temperature));

                            if (calc > test)
                            {
                                accepted++;
                                useNewSolution = true;
                            }
                        }
                        if (useNewSolution)
                        {
                            useNewSolution = false;
                            // до (5) Допоміжна функція копіювання одного розв’язку в інший:
                            Solution.CopySolution(ref current, working);

                            if (current.energy < best.energy)
                            {
                                // до (5) Допоміжна функція копіювання одного розв’язку в інший:
                                Solution.CopySolution(ref best, current);
                                isSolution = true;
                            }
                        }
                        else
                        {
                            // до (5) Допоміжна функція копіювання одного розв’язку в інший:
                            Solution.CopySolution(ref working, current);
                        }
                    }
                    Console.WriteLine("Timer: " + (timer++) + " Temperature: " + temperature + " Best energy: " + best.energy + " Accepted " + accepted);

                    Console.WriteLine("Best energy = " + best.energy);

                    if (best.energy == 0)
                        break;  //

                        temperature *= Constants.ALPHA;
                }

                if (isSolution)
                {
                    Console.WriteLine("Best energy = " + best.energy);
                    // до (6) Допоміжна функція виводу результату на екран у вигляді шахової дошки:
                    Solution.PrintSolution(best);
                }
                Console.ReadKey();
            }
        }
    }
}