using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.MachineLearning;
using Accord.Statistics.Distributions.DensityKernels;


namespace Lab5AI
{
    public partial class Form1 : Form
    {
        int _countClasters = Const.CountClusters;
        int _N = Const.N;
        int rndI = 0;

        GenerationPoint gp = new GenerationPoint(Const.N);

        // Сигнатури:
        public int CountClasters { get => _countClasters; set => _countClasters = value; }

        public int N { get => _N; set => _N = value; }
        
        public int RndI { get => rndI; set => rndI = value; }

        internal GenerationPoint Gp { get => gp; set => gp = value; }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NElementsTextBox.Text = N.ToString();
            CountClustersTextBox.Text = CountClasters.ToString();

            #region MyRegion
            //chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;


            //chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            //chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            //chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            //chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true; 
            #endregion

            for (int i = 0; i < Gp.TwoDPoint.Count; ++i)
                chart1.Series[0].Points.AddXY(Gp.TwoDPoint[i].x, Gp.TwoDPoint[i].y);
        }





        /// <summary>
        /// (5):-mb
        // Реалізувати допоміжну функцію для виконання алгоритму кластеризації
        // за будь-яким іншим(за вибором) методом(ієрархічний, C-середніх,
        // мінімального покриваючого дерева, по-шарової кластеризації, тощо):
        /// </summary>
        // При натисканні на "ОК":
        void DoCluster()
        {
            Debug.WriteLine("Ok");
            if (checkBox1.Checked)
            {
                RndI = new Random().Next();
            }
            var clustering = ClusterProcess.ClusterKMeans(Gp.TwoDPoint, CountClasters, RndI);


            #region MyRegion
            // Створюю алгоритм середнього зсуву, використовуючи дану смугу пропускання і ядро щільності гаусівської функції як функцію ядра::


            //Debug.WriteLine("Ok");
            //if (checkBox1.Checked)
            //{
            //    RndI = new Random().Next();
            //}
            //var meanShift = new MeanShift()
            //{
            //    Kernel = new GaussianKernel(2),
            //    Bandwidth = 0.005,
            //    Tolerance = 0.05,
            //    MaxIterations = 10
            //};

            //var clustering = meanShift.Learn(Point.ConvertToDouble(gp.TwoDPoint)).Decide(Point.ConvertToDouble((gp.TwoDPoint)));

            #endregion
            
            Debug.WriteLine("Ok");

            Random rnd = new Random();

            // Рандомний колір:
            int[] randomRGBColor = new int[3];
            for (int i = 1; i <= CountClasters; i++)
            {
                // тут можна вручну задати палітру кольорів:
                randomRGBColor[0] = rnd.Next(0, 255);
                randomRGBColor[1] = rnd.Next(0, 255);
                randomRGBColor[2] = rnd.Next(0, 255);
                chart1.Series.Add(Const.NameOfSeries + i.ToString());
                //chart1.Series[Const.NameOfSeries + i.ToString()] = 3;
                chart1.Series[Const.NameOfSeries + i.ToString()].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                chart1.Series[Const.NameOfSeries + i.ToString()].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                chart1.Series[Const.NameOfSeries + i.ToString()].Color = Color.FromArgb(randomRGBColor[0], randomRGBColor[1], randomRGBColor[2]);
                chart1.Series[Const.NameOfSeries + i.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                //Debug.WriteLine(chart1.Series[i].Points.Count);
            }
            for (int j = 0; j < Gp.TwoDPoint.Count; ++j)
            {
                chart1.Series[Const.NameOfSeries + (clustering[j] + 1).ToString()].Points.AddXY(Gp.TwoDPoint[j].x, Gp.TwoDPoint[j].y);
            }
        }




        void ClearClusterSeries()
        {
            var ser = chart1.Series[0];
            chart1.Series.Clear();

            chart1.Series.Add("Point");
            chart1.Series[0] = ser;
        }


        // При натисканні на кнопку "ОК":
        private void OKButton_Click(object sender, EventArgs e)
        {
            //N = Convert.ToInt32(NElementsTextBox.Text);
            CountClasters = Convert.ToInt32(CountClustersTextBox.Text);

            ClearClusterSeries();
            DoCluster();
            /////////////////////////////// TUT:
            ///
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}