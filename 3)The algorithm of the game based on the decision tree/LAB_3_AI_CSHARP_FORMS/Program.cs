//
// Created by yurik_322 on 30/03/19.
//
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Lab3AI
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public struct Move
        {
            public int score;
            public char index;
            public Move(int Score, char Index = '\0')
            {
                score = Score;
                index = Index;
            }
        }

            // Наш символ:
            public static readonly char huPlayer = 'X';

            // Штучний інтелект:
            public static readonly char aiPlayer = 'O';

            // повертаємо індекс пустих квадртів:
            static List<int> EmptyIndices(char[] board)
            {
                var list = new List<int>();
                for (int i = 0; i < board.Length; ++i)
                {
                    if (board[i] != 'O' && board[i] != 'X')
                        list.Add(i);
                }
                return list;
            }


                //Дерево рішень ?
                
                    // (2):
                // Структура даних для зберігання дерева рішень:
                // виграшні комбінації з урахуванням показників:
            static bool Winning(char[] board, char player)
            {
                if (
                      (board[0] == player  &&  board[1] == player  &&  board[2] == player) ||
                      (board[3] == player  &&  board[4] == player  &&  board[5] == player) ||
                      (board[6] == player  &&  board[7] == player  &&  board[8] == player) ||
                      (board[0] == player  &&  board[3] == player  &&  board[6] == player) ||
                      (board[1] == player  &&  board[4] == player  &&  board[7] == player) ||
                      (board[2] == player  &&  board[5] == player  &&  board[8] == player) ||
                      (board[0] == player  &&  board[4] == player  &&  board[8] == player) ||
                      (board[2] == player  &&  board[4] == player  &&  board[6] == player)
                  )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


                    // (3):
                // Розробити дерево рішень для гри «хрестики-нулики»
                // Основна міні-макс функція:
            public static Move Minimax(char[] newBoard, char player)
            {
                //доступні місця:
                var availSpots = EmptyIndices(newBoard);
                
                // Перевірка на перемогу і відповідно повертає значення:
                if (Winning(newBoard, huPlayer))
                {
                    return new Move(-10);
                }
                else if (Winning(newBoard, aiPlayer))
                {
                    return new Move(10);
                }
                else if (availSpots.Count==0)
                {
                    return new Move(0);
                }

                // Масив для збереження всіх об'єктів:
                List<Move> moves = new List<Move>();

                // Для вільного місця:
                for (var i = 0; i < availSpots.Count; i++)
                {
                    // Створити об'єкт для кожного і зберегти індекс цього місця: 
                    Move move;
                    move.index = newBoard[availSpots[i]];

                    // Рух для поточного гравця:
                    newBoard[availSpots[i]] = player;


                    // Отримати результат:
                    if (player == aiPlayer)
                    {
                        var result = Minimax(newBoard, huPlayer);
                        move.score = result.score;
                    }
                    else
                    {
                        var result = Minimax(newBoard, aiPlayer);
                        move.score = result.score;
                    }

                    // Очистити:
                    newBoard[availSpots[i]] = move.index;

                    // Додати об"єкт в масив:
                    moves.Add(move);
                }

                // Якщо хід штучного інтелекту, знайти максимальну кількість балів:
                int bestMove = 0;
                if (player == aiPlayer)
                {
                    var bestScore = -10000;
                    for (var i = 0; i < moves.Count; i++)
                    {
                        if (moves[i].score > bestScore)
                        {
                            bestScore = moves[i].score;
                            bestMove = i;
                        }
                    }
                }
                else
                {
                    // Інакше знайдіть мінімальну кількість балів:
                    var bestScore = 10000;
                    for (var i = 0; i < moves.Count; i++)
                    {
                        if (moves[i].score < bestScore)
                        {
                            bestScore = moves[i].score;
                            bestMove = i;
                        }
                    }
                }

                // Повернення переміщення цілі:
                return moves[bestMove];
            }


        // Вивід:
        public static void Print(char[] list)
        {
            for (int i = 0; i < list.Length; ++i)
            {
                if ( i% 3==0  &&  i!=0 )
                    Console.WriteLine();
                Console.Write(list[i]);

            }
            Console.WriteLine();
        }
    }
}