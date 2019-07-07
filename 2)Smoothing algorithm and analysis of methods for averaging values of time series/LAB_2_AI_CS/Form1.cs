using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Lab2AI
{
    public partial class AverageValues : Form
    {
        private List<double> x_n = new List<double>();

        public List<double> X_n => x_n;

        //private List<double> x1_n = new List<double>();
        //public List<double> X1_n => x1_n;
        double x1 = 0;
        double x2 = Const.N;


        // (2):
        // Реалізовую другий пункт:
        double Asin(double x, double phi, int A)
        {
            return A * Math.Sin(x + phi);
        }


        // (7):
        // Алгоритм для проведення «згладжування» тестової послідовності значень:
        void MovingAverage()
        {
            if ( !String.IsNullOrEmpty(periodText.Text) )
                try
                {
                    // Просто рух середніх ліній:
                    for (int i=0; i<x_n.Count; ++i)
                    {
                        chart1.Series[4].Points.AddXY( i, SimpleMovingAverage(i, Convert.ToInt32(periodText.Text)) );
                    }
                    //***************************
                    // Зважені лінії середньої швидкості:
                    for (int i=Convert.ToInt32(periodText.Text); i<x_n.Count; ++i)
                    {
                        chart1.Series[5].Points.AddXY( i, WeightedMovingAverage(i, Convert.ToInt32(periodText.Text)) );
                    }
                    //***************************
                    // Експонентні лінії ковзних середніх:
                    for (int i=0; i<x_n.Count; ++i)
                    {
                        chart1.Series[6].Points.AddXY( i, ExponentialMovingAverage(i, 2.0/(Convert.ToInt32(periodText.Text) + 1)) );
                    }
                    //********************************
                }
                catch (Exception)
                {

                }
        }


        void RefreshGraph()
        {
            try
            {
                if ( x_n.Count != Convert.ToDouble(nElementText.Text) || (!String.IsNullOrEmpty(aElementText.Text)
                       || !String.IsNullOrEmpty(phiElementText.Text)) )
                {
                    double x = 0;

                    Random rnd = new Random();
                    x_n.Clear();

                    foreach (var series in chart1.Series)
                    {
                        series.Points.Clear();
                    }

                    for ( int i=0; i<Convert.ToInt32(nElementText.Text); ++i )
                    {
                        x_n.Add( Asin(x, Const.Phi, Convert.ToInt32(aElementText.Text) )
                                    + rnd.NextDouble() % (Convert.ToInt32(aElementText.Text) * 0.05) - rnd.NextDouble() % (Convert.ToInt32(aElementText.Text) * 0.05));
                        chart1.Series[0].Points.AddXY(i, x_n[i]);
                        x += Const.period;
                    }
                    x = 0;

                    // do fMean()
                    // Арифметична середня лінія:
                    aMeanValue.Text = Convert.ToString(ArithmeticMean());
                    chart1.Series[1].Points.AddXY(0, ArithmeticMean());
                    chart1.Series[1].Points.AddXY( Convert.ToInt32(nElementText.Text) - 1, ArithmeticMean() );
                    //***************
                    // Геометрична середня лінія:
                    gMeanValue.Text = Convert.ToString(GeometricMean());
                    if ( !Double.IsInfinity(GeometricMean()) )
                    {
                        chart1.Series[2].Points.AddXY(0, GeometricMean());
                        chart1.Series[2].Points.AddXY( Convert.ToInt32(nElementText.Text) - 1, GeometricMean() );
                    }
                    //*******************
                    // Гармонійна середня лінія:
                    hMeanValue.Text = Convert.ToString(HarmonicMean());
                    chart1.Series[3].Points.AddXY(0, HarmonicMean());
                    chart1.Series[3].Points.AddXY( Convert.ToInt32(nElementText.Text) - 1, HarmonicMean() );
                    //******************
                    MovingAverage();    // проста вага
                }
            }
            catch (Exception)
            {

            }

        }


        // (3):
        // Реалізовую допоміжні функції для обчислення: сер. арифметичного, сер гармонійного, сер. геометричного:
        double ArithmeticMean()
        {
            double summ = 0;
            for (int i=0; i<x_n.Count; ++i)
                summ += x_n[i];
            return summ/= x_n.Count;
        }


        double GeometricMean()
        {
            double multiple = 1;

            for (int i=0; i<x_n.Count; ++i)
            {
                multiple *= x_n[i];
            }
            if (multiple < 0)
            {
                return (-1) * Math.Pow(Math.Abs(multiple),  1.0/(x_n.Count));
            }
            else
                return Math.Pow(multiple, 1.0/(x_n.Count));
        }


        double HarmonicMean()
        {
            double summ = 0;
            for (int i=0; i<x_n.Count; ++i)
                summ += 1.0/x_n[i];
            if (summ < 0)
                summ = (-1) * Math.Pow( Math.Abs(summ), -1 );
            else
                summ = Math.Pow(summ, -1);
            return x_n.Count * summ;
        }


        // (4):
        // Допоміжні функції для обчислення біжучого середнього значення:
        // просте, зважене, експотенціальне та модифіковане
        double SimpleMovingAverage(int t, int n)
        {
            int i = t - n;
            double summ = 0;
            if (i < 0)
                i = 0;
            if ( t == 0  ||  t == x_n.Count )
                return summ = x_n[i];
            else
            {
                while (i < t + n)
                {
                    summ += x_n[i];
                    ++i;
                    if (i > t)
                        break;
                }
                return summ /= (n + 1);
            }
        }


        double WeightedMovingAverage(int t, int n)
        {
            int i = t - n;
            if (i < 0)
                i = 0;
            double summ = 0;
            if ( t == 0  ||  t == x_n.Count )
                return summ = x_n[i];
            for (i=0; i<=n-1; ++i)
                summ += (n-i) * x_n[t - i];
            return 2.0 / (n * (n+1)) * summ;
        }


        double ExponentialMovingAverage(int t, double alpha)
        {
            if (t >= 1)
                return x_n[t] * alpha + (1 - alpha) * ExponentialMovingAverage(t - 1, alpha);
            else
                return x_n[t];
        }


        public AverageValues()
        {
            InitializeComponent();
        }


        // (5):
        // Допоміжна функція виводу результату на екран у вигляді графіку функцій:
        private void AverageValues_Load(object sender, EventArgs e)
        {
            // Ініціалізація констант:
            nElementText.Text = Convert.ToString(Const.N);
            aElementText.Text = Convert.ToString(Const.A);
            phiElementText.Text = Convert.ToString(Const.Phi);
            periodText.Text = Convert.ToString(Const.n);
            //****************
            double x = 0;


            Random rnd = new Random();
            for (int i=0; i<Const.N; ++i)
            {
                x_n.Add( Asin(x, Const.Phi, Convert.ToInt32(aElementText.Text)) +
                            rnd.Next(-100, 100) % (Convert.ToInt32(aElementText.Text) * 0.05) );
                chart1.Series[0].Points.AddXY(i, x_n[i]);
                x += Const.period;
            }

            chart1.ChartAreas[0].AxisY.Title = "Y";
            chart1.ChartAreas[0].AxisX.Title = "X";

            // chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, Const.N / (Const.N / 100));
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;


            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-Const.A, Const.A);
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;

            // do fMean()
            aMeanValue.Text = Convert.ToString(ArithmeticMean());
            // Арифметична лінія:
            chart1.Series[1].Points.AddXY(0, ArithmeticMean());
            chart1.Series[1].Points.AddXY(Const.N -1, ArithmeticMean());
            //***************
            // Геометрична середня лінія:
            gMeanValue.Text = Convert.ToString(GeometricMean());
            if ( !Double.IsInfinity(GeometricMean()) )
            {
                chart1.Series[2].Points.AddXY(0, GeometricMean());
                chart1.Series[2].Points.AddXY(Const.N - 1, GeometricMean());
            }
            //**************
            // Гармонійна середня лінія:
            hMeanValue.Text = Convert.ToString(HarmonicMean());
            chart1.Series[3].Points.AddXY(0, HarmonicMean());
            chart1.Series[3].Points.AddXY( Convert.ToInt32(nElementText.Text) - 1, HarmonicMean() );
            //******************
            // Прості середні лінії:
            for (int i=0; i<x_n.Count; ++i)
            {
                chart1.Series[4].Points.AddXY(i, SimpleMovingAverage(i, Const.n));
            }
            //***************************
            // Зважені лінії середньої швидкості:
            for (int i=Const.n; i<x_n.Count; ++i)
            {
                chart1.Series[5].Points.AddXY(i, WeightedMovingAverage(i, Const.n));
            }
            //***************************
            // Експонентні лінії ковзних середніх:
            for (int i=0; i<x_n.Count; ++i)
            {
                chart1.Series[6].Points.AddXY( i, ExponentialMovingAverage(i, 2.0 / (Const.n + 1)) );
            }
            //********************************
        }


        // (6):
        // Для обчислення точного значення - фіксований інтервал дельта x (для вимірування та порівняння значень):
        private void deltaXText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                x1 = Convert.ToDouble(deltaXText.Text);
                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(x1, x2);
            }
            catch (Exception ex)
            {
                if ( (String.IsNullOrEmpty(deltaXText.Text)) )
                {
                    x1 = 0;
                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(x1, x2);
                }
            }
        }


        private void deltaX1Text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                x2 = Convert.ToDouble(deltaX1Text.Text);
                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(x1, x2);
            }
            catch (Exception ex)
            {
                if ( String.IsNullOrEmpty(deltaX1Text.Text) )
                {
                    x2 = Const.N;
                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(x1, x2);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            RefreshGraph();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var el in chart1.Series)
                el.Points.Clear();
        }


        private void periodText_TextChanged(object sender, EventArgs e)
        {
            chart1.Series[4].Points.Clear();// очищую простий рух середнього
            chart1.Series[5].Points.Clear();// очищую зважений рух сережнього
            chart1.Series[6].Points.Clear();// очищую експоненціальний рух середнього
            MovingAverage();
        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }


        private void nElementText_TextChanged(object sender, EventArgs e)
        {

        }

        private void periondLabel_Click(object sender, EventArgs e)
        {

        }
    }
}