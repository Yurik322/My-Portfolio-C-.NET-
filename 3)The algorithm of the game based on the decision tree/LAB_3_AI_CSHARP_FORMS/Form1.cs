using System;
using System.Drawing;
using System.Windows.Forms;


namespace Lab3AI
{
    public partial class Form1 : Form
    {
        private Game game;
        private PictureBox[,] pbs = new PictureBox[3, 3];

        private Image Cross;
        private Image Circle;


        // Те, що є виводом на екран:
        public Form1()
        {
            InitializeComponent();

            Init();

            game = new Game();
            Build(game);
        }


            // (5):
        // Допоміжна функція для візуалізації поля гри:
        void Init()
        {
            // підкачуємо кртинки:
            Cross = Image.FromFile(@"cross.png");
            Circle = Image.FromFile(@"circle.png");

            // реалізація відтворення при натисканні на клітинку:
            // заповнення картинкою області клітинки
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    pbs[i, j] = new PictureBox { Parent = this, Size = new Size(200, 200), Top = i * 200, Left = j * 200, BorderStyle = BorderStyle.FixedSingle, Tag = new Point(i, j), Cursor = Cursors.Hand, SizeMode = PictureBoxSizeMode.StretchImage };

                    pbs[i, j].Click += Pb_Click;
                }
            // Оновити:
            new Button { Parent = this, Top = 600, Text = "Reset" }.Click += delegate { game = new Game(); Build(game); };
        }


        // через тернарну операцію перевіряю чи пересікаються елементи:
        private void Build(Game game)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)

                    pbs[i, j].Image = game.Items[i, j] == FieldState.Cross ? Cross : (game.Items[i, j] == FieldState.Circle ? Circle : null);
        }


        // Побудова дошки для гри:
        char[] BuildCharBoard(FieldState[,] board)
        {
            char[] charBoard = new char[9];
            int k = 0;
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                {
                    if (board[i, j] == FieldState.Cross)
                        charBoard[k] = 'X';

                    else if (board[i, j] == FieldState.Circle)
                        charBoard[k] = 'O';

                    else charBoard[k] = Convert.ToChar(k.ToString());
                    ++k;
                }
            return charBoard;
        }


        // Для пустих клітинок:
        int ContainNull(FieldState[,] list)
        {
            int countNull=0;

            foreach (var el in list)
                if (el == FieldState.Empty)
                    countNull++;
            return countNull;
        }


            // (4):
        // Реалізую допоміжну функцію для вибору комп’ютером рішення з дерева рішень:
        void Pb_Click(object sender, EventArgs e)
        {
            var p = (Point)(sender as Control).Tag;

            if ( game.Items[p.X, p.Y]  !=  FieldState.Empty )
                return;

            // гра передає естафету між людиною та штучним інтелектом:
            game.MakeMove(p);
            char[] origBoard = BuildCharBoard(game.Items);

            if ( !game.Winned  &&  ContainNull(game.Items)>1 )
            {
                int minimaxNum = Convert.ToInt32(Program.Minimax(origBoard, Program.aiPlayer).index.ToString());
                game.MakeMove( new Point(minimaxNum / 3, minimaxNum % 3) );
            }
            Build(game);

            // Фінальне повідомлення:
            if (game.Winned)
                MessageBox.Show(string.Format("{0} is winner!", game.CurrentPlayer == 0 ? "Cross" : "Circle"));
        }
    }


        // (6):
    // Безпосередньо реалізую гру «хрестики-нулики» для гри людини з комп’ютером:
    class Game
    {
        public FieldState[,] Items = new FieldState[3, 3];
        public int CurrentPlayer = 0;
        public bool Winned;


        public void MakeMove(Point p)
        {
            if (Items[p.X, p.Y] != FieldState.Empty)
                return;

            if (Winned)
                return;

            Items[p.X, p.Y] = CurrentPlayer == 0 ? FieldState.Cross : FieldState.Circle;
            if (CheckWinner(FieldState.Cross) || CheckWinner(FieldState.Circle))
            {
                Winned = true;
                return;
            }
            CurrentPlayer ^= 1;
        }


        // Перевірка на перемогу:
        private bool CheckWinner(FieldState state)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Items[i, 0] == state && Items[i, 1] == state && Items[i, 2] == state)
                    return true;
                if (Items[0, i] == state && Items[1, i] == state && Items[2, i] == state)
                    return true;
            }

            if (Items[0, 0] == state && Items[1, 1] == state && Items[2, 2] == state)
                return true;

            if (Items[0, 2] == state && Items[1, 1] == state && Items[2, 0] == state)
                return true;

            return false;
        }
    }


    // Перечислення набору всіх видів клітинок:
    enum FieldState
    {
        Empty,
        Cross,
        Circle
    }
}