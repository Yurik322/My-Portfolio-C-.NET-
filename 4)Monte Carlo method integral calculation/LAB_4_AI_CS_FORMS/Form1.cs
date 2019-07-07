using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//!!! Доробити (від пункту 3 до 6)

namespace Lab4AI
{
    public partial class Integration : Form
    {
        public static uint N;
        public static double a;
        public static double b;
        public static double eps = 0.01;


        double I = 0;
        double I1 = 0;

        List<double>  arg = new List<double>();
        List<double> y1 = new List<double>();
        List<double> y2 = new List<double>();
        List<double> y = new List<double>();


        public Integration()
        {
            InitializeComponent();
        }


        // Чомусь нижче не можу змінити arg i 0::::

        // основна функція:
        static double F1(double arg)
        {
            return Math.Exp( Math.Pow(arg, 2) );    // return arg;      //// e^x^2 треба сюди засунути
        }


        // тестова функція:
        static double F2(double arg)
        {
            return arg; // return 0;
                        // return Math.Exp(Math.Pow(6, 2));     //// x^2 треба сюди засунути

        }


        delegate double f(double arg);

        // (6):
        // Значення тестової функції, значення основної функції:
        void Draw(f func, int key, double delta = 0.01)
        {
            for (double i = a + delta; i <= b; i += delta)
            {
                chart1.Series[key].Points.AddXY(i, func(i));
            }
        }


        // (5):
        // Допоміжна функція для генерування випадкової точки на координатній 
        // площині – пара рівномірно розподілених випадкових значень(x, y):
        void Draw(List<double> y, List<double> arg, int key)
        {
            for (int i = 0; i < (arg.Count > 1000 ? 1000 : arg.Count); ++i)
            {
                chart1.Series[key].Points.AddXY(arg[i], y[i]);
            }
        }


        static double Max(List<double> y1, List<double> y2)
        {
            double y1Max = y1.Max();
            double y2Max = y2.Max();

                return y1Max > y2Max ? y1Max : y2Max;
        }


        static double Min(List<double> y1, List<double> y2)
        {
            double y1Min = y1.Min();
            double y2Min = y2.Min();

                return y1Min < y2Min ? y1Min : y2Min;
        }


        static Random rnd = new Random();

        public double I11 { get => I12; set => I12 = value; }
        public double I12 { get => I1; set => I1 = value; }
        public double I13 { get => I1; set => I1 = value; }


            // (3):
        // Обчислення значення визначеного інтегралу від деякої додатно визначеної 
        // у заданому інтервалі функції, інтеграл від якої не можна порахувати 
        // аналітично, проте яка є точно визначеною на цьому інтервалі [1, 2]:
        private void Button1_Click(object sender, EventArgs e)
        {
            ClearAll();
            // блок для виключень:
            try
            {
                a = Convert.ToDouble(aTextBox.Text);
                b = Convert.ToDouble(bTextBox.Text);
                N = Convert.ToUInt32(NTextBox.Text);

                if (a > b)
                {
                    aTextBox.Text = b.ToString();
                }

                do
                {
                    I=MonteCarlo(I);
                    I1=MonteCarlo(I1);    // розкоментувати - показати
                    N += 1000;
                }
                while ( Math.Abs(I - 4) > eps);


                label4.Text = N.ToString();


                // Викликаємо наші ГРАФІКИ:
                Draw(F1, 0);    // 0
                Draw(F2, 0);
                Draw(y, arg, 2);  // якщо змінити на 3 - синій
            }
            catch (Exception)
            {
                if (String.IsNullOrEmpty(NTextBox.Text)) NTextBox.Text = N.ToString();
                if (String.IsNullOrEmpty(aTextBox.Text)) aTextBox.Text = a.ToString();
                if (String.IsNullOrEmpty(bTextBox.Text)) bTextBox.Text = b.ToString();
            }
        }


        void ClearAll()
        {
            arg.Clear();
            y1.Clear();
            y2.Clear();
            y.Clear();

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
        }


            // (7):
        // Безпосередня реалізація алгоритм Монте-Карло для обчислення
        // значення визначеного інтегралу від підінтегральної функції:
        double MonteCarlo(double I)
        {
            for (int i = 0; i < N; ++i)
                arg.Add( rnd.NextDouble()* (b - a) + a );

            for (int i = 0; i < N; ++i)
            {
                y1.Add( F1(arg[i]) );
                y2.Add( F2(arg[i]) );
            }




            double maxy = Max(y1, y2);
            double miny = Min(y1, y2);

            // Обчислення визначеного інтеграла:
            double S = (b - a) * (maxy - miny);

            for (int i = 0; i < N; ++i)
                y.Add( miny + (maxy - miny)* rnd.NextDouble() );


            List<bool> L1 = new List<bool>();
            for (int i = 0; i < N; ++i)
            {
                if ( y1[i] <= y[i] )
                    L1.Add(true);
                else
                    L1.Add(false);
            }


                // (6):
            // Реалізація допоміжної функції, що повертає точне значення
            // підінтегральної функції в заданій точці:
            int n1 = L1.Where( s => s == true ).Count();
            List<bool> L2 = new List<bool>();

            for (int i = 0; i < N; ++i)
            {
                if ( y2[i] <= y[i] )
                    L2.Add(true);

                else
                    L2.Add(false);
            }

            int n2 = L2.Where( s => s == true ).Count();
            //Console.WriteLine(n1);  //
            //Console.WriteLine(n2);  //


                // (8):
            // Розрахувати похибки(абсолютна, відносна) у тестовому прикладі та
            // провести оцінку похибок основної задачі:
            double m = Math.Abs( n1 - n2 );
            I = m / N * S;
            IValue.Text = I.ToString();

            double _I = 2;

            double delta = Math.Abs( I - _I );
            double deltaPer = delta / _I * 100;

            return I;
        }


        // Це для Forms - вивід на графік:
        private void Integration_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;


            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;

            NTextBox.Text = Constant.N.ToString();
            aTextBox.Text = Constant.a.ToString();
            bTextBox.Text = Constant.b.ToString();

            N = Constant.N;
            a = Constant.a;
            b = Constant.b;


            do
            {
                I=MonteCarlo(I);
                N += 1000;
                
                //для тесту:
                //I1 =MonteCarlo(I1);   // розкоментувати - показати

            }
            while (Math.Abs(I - 4) > eps);


            label4.Text = N.ToString();


            // Викликаємо наші ГРАФІКИ:
            Draw(F1, 1);    // 0
            Draw(F2, 0);
            Draw(y, arg, 3);  // якщо змінити на 3 - синій
        }


        private void Chart1_Click(object sender, EventArgs e)
        {

        }
            

        // результат - значеня інтеграла:
        private void IValue_Click(object sender, EventArgs e)
        {

        }
    }
}