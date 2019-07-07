using System;


namespace Lab1AI
{
    public class Functions
    {
        readonly static int[] dx = { -1, 1, -1, 1 };
        readonly static int[] dy = { -1, 1, 1, -1 };


        public class MemberType
        {
            public MemberType()
            {

            }

            public int[] solution = new int[Constants.SIZE_CHESSBOARD];
            public double energy = 0;
        }



        public class Solution
        {
            public static Random GetRand = new Random();


                // (5):
            // Допоміжна функція копіювання одного розв’язку в інший:
            static public void CopySolution(ref MemberType copy, MemberType src)
            {
                for ( int i=0; i< Constants.SIZE_CHESSBOARD; i++ )
                {
                    copy.solution[i] = src.solution[i];
                }
                copy.energy = src.energy;
            }


                // (3):
            // Випадкова зміна розв’язку та початкова ініціалізація:
            static public void RandomSolution(ref MemberType member)
            {
                int temp, x = 0, y = 0;

                x = GetRand.Next(0, (Constants.SIZE_CHESSBOARD));
                do
                {
                    y = GetRand.Next(0, (Constants.SIZE_CHESSBOARD));
                } while (x == y);

                temp = member.solution[x];
                member.solution[x] = member.solution[y];
                member.solution[y] = temp;
                //Console.WriteLine(x + " " + y); // Оце можна закоментувати, щоб швидше обчислити.
            }


                // (3):
            static public void InitializeSolution(ref MemberType member)
            {
                int i;
                // Початкова ініціалізація:
                for ( i=0; i< Constants.SIZE_CHESSBOARD; i++ )
                {
                    member.solution[i] = i;
                }

                // Випадкова зміна розв’язку:
                for ( i=0; i< Constants.SIZE_CHESSBOARD; i++ )
                {
                    RandomSolution(ref member);
                }
            }


                // (4):
            // Допоміжна функція для оцінки розв’язку:
            static public void ComputeEnergy(ref MemberType member)
            {
                int i, j, x, y, tempx, tempy;

                char[,] board = new char[Constants.SIZE_CHESSBOARD, Constants.SIZE_CHESSBOARD];

                int conflicts;

                for ( i=0; i< Constants.SIZE_CHESSBOARD; i++ )
                {
                    board[ i, member.solution[i] ] = 'E';
                }
                // Лічильний, який рахує конфлікти:
                conflicts = 0;

                for ( i=0; i< Constants.SIZE_CHESSBOARD; i++ )
                {
                    x = i;
                    y = member.solution[i];

                    for (j=0; j<3; j++)
                    {
                        tempx = x;
                        tempy = y;

                        while (true)
                        {
                            tempx += dx[j];
                            tempy += dy[j];

                            if ( (tempx < 0)  ||  (tempx >= Constants.SIZE_CHESSBOARD)  ||
                                 (tempy < 0)  ||  (tempy >= Constants.SIZE_CHESSBOARD) )
                                break;
                            if ( board[tempx, tempy] == 'E' )
                                conflicts++;
                        }
                    }
                }
                member.energy = conflicts;
            }


                // (6):
            // Допоміжна функція виводу результату на екран у вигляді шахової дошки:
            static public void PrintSolution(MemberType member)
            {
                char[,] board = new char[Constants.SIZE_CHESSBOARD, Constants.SIZE_CHESSBOARD];

                for ( int x=0; x< Constants.SIZE_CHESSBOARD; x++ )
                {
                    board[x, member.solution[x]] = 'E';
                }
                for ( int y=0; y< Constants.SIZE_CHESSBOARD; y++ )
                {
                    for ( int x=0; x< Constants.SIZE_CHESSBOARD; x++ )
                    {
                        if ( board[x, y] == 'E' )
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // Color
                            Console.Write("    " + "E");
                            Console.ResetColor();   // Color
                        }
                        else
                        {
                            Console.Write("    " + "0");
                        }
                    }
                    Console.WriteLine(); Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}