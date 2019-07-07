namespace Lab2AI
{
    partial class AverageValues
    {
        /// <summary>
        /// Обов"язкова змінна конструктора
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Звільнити всі затрачені ресурси
        /// </summary>
        /// <param name="disposing">True, якщо керуючий ресурс має бути видалений; else False</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматично створений конструктором форм Windows

        /// <summary>
        /// Потрібен метод для підтримки конструктора — його не можна міняти 
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.deltaXText = new System.Windows.Forms.TextBox();
            this.deltaX1Text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.arimMean = new System.Windows.Forms.Label();
            this.aMeanValue = new System.Windows.Forms.Label();
            this.gMean = new System.Windows.Forms.Label();
            this.gMeanValue = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.nElementLabel = new System.Windows.Forms.Label();
            this.nElementText = new System.Windows.Forms.TextBox();
            this.aElementLabel = new System.Windows.Forms.Label();
            this.aElementText = new System.Windows.Forms.TextBox();
            this.phiElementLabel = new System.Windows.Forms.Label();
            this.phiElementText = new System.Windows.Forms.TextBox();
            this.hMeanLabel = new System.Windows.Forms.Label();
            this.hMeanValue = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.periodText = new System.Windows.Forms.TextBox();
            this.periondLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(16, 15);
            this.chart1.Margin = new System.Windows.Forms.Padding(4);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;   // Задаю колір для виводу
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.LegendText = "f(x)";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.LegendText = "Arithmetic mean ";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.LegendText = "Geometric mean";
            series3.Name = "Series3";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.LabelForeColor = System.Drawing.Color.Gold;
            series4.Legend = "Legend1";
            series4.LegendText = "Harmonic mean ";
            series4.Name = "Series4";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.LegendText = "Simple moving mean";
            series5.Name = "Series5";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Maroon;
            series6.Legend = "Legend1";
            series6.LegendText = "Weighted moving mean";
            series6.Name = "Series6";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Legend = "Legend1";
            series7.LegendText = "ExponentialMovingAverage";
            series7.Name = "Series7";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Size = new System.Drawing.Size(1352, 715);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // deltaXText
            // 
            this.deltaXText.Location = new System.Drawing.Point(141, 241);
            this.deltaXText.Margin = new System.Windows.Forms.Padding(4);
            this.deltaXText.Name = "deltaXText";
            this.deltaXText.Size = new System.Drawing.Size(55, 22);
            this.deltaXText.TabIndex = 1;
            this.deltaXText.TextChanged += new System.EventHandler(this.deltaXText_TextChanged);
            // 
            // deltaX1Text
            // 
            this.deltaX1Text.Location = new System.Drawing.Point(291, 241);
            this.deltaX1Text.Margin = new System.Windows.Forms.Padding(4);
            this.deltaX1Text.Name = "deltaX1Text";
            this.deltaX1Text.Size = new System.Drawing.Size(52, 22);
            this.deltaX1Text.TabIndex = 3;
            this.deltaX1Text.TextChanged += new System.EventHandler(this.deltaX1Text_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 241);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "X1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 241);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "X2";
            // 
            // arimMean
            // 
            this.arimMean.AutoSize = true;
            this.arimMean.Location = new System.Drawing.Point(138, 107);
            this.arimMean.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.arimMean.Name = "arimMean";
            this.arimMean.Size = new System.Drawing.Size(109, 17);
            this.arimMean.TabIndex = 6;
            this.arimMean.Text = "Arithmetic mean";
            // 
            // aMeanValue
            // 
            this.aMeanValue.AutoSize = true;
            this.aMeanValue.Location = new System.Drawing.Point(288, 107);
            this.aMeanValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aMeanValue.Name = "aMeanValue";
            this.aMeanValue.Size = new System.Drawing.Size(16, 17);
            this.aMeanValue.TabIndex = 7;
            this.aMeanValue.Text = "0";
            // 
            // gMean
            // 
            this.gMean.AutoSize = true;
            this.gMean.Location = new System.Drawing.Point(138, 145);
            this.gMean.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gMean.Name = "gMean";
            this.gMean.Size = new System.Drawing.Size(112, 17);
            this.gMean.TabIndex = 8;
            this.gMean.Text = "Geometric mean";
            // 
            // gMeanValue
            // 
            this.gMeanValue.AutoSize = true;
            this.gMeanValue.Location = new System.Drawing.Point(288, 145);
            this.gMeanValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gMeanValue.Name = "gMeanValue";
            this.gMeanValue.Size = new System.Drawing.Size(16, 17);
            this.gMeanValue.TabIndex = 9;
            this.gMeanValue.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(64, 287);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 42);
            this.button1.TabIndex = 10;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nElementLabel
            // 
            this.nElementLabel.AutoSize = true;
            this.nElementLabel.Location = new System.Drawing.Point(72, 9);
            this.nElementLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nElementLabel.Name = "nElementLabel";
            this.nElementLabel.Size = new System.Drawing.Size(18, 17);
            this.nElementLabel.TabIndex = 11;
            this.nElementLabel.Text = "N";
            // 
            // nElementText
            // 
            this.nElementText.Location = new System.Drawing.Point(125, 7);
            this.nElementText.Margin = new System.Windows.Forms.Padding(4);
            this.nElementText.Name = "nElementText";
            this.nElementText.Size = new System.Drawing.Size(71, 22);
            this.nElementText.TabIndex = 12;
            this.nElementText.TextChanged += new System.EventHandler(this.nElementText_TextChanged);
            // 
            // aElementLabel
            // 
            this.aElementLabel.AutoSize = true;
            this.aElementLabel.Location = new System.Drawing.Point(72, 42);
            this.aElementLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aElementLabel.Name = "aElementLabel";
            this.aElementLabel.Size = new System.Drawing.Size(17, 17);
            this.aElementLabel.TabIndex = 13;
            this.aElementLabel.Text = "A";
            // 
            // aElementText
            // 
            this.aElementText.Location = new System.Drawing.Point(125, 37);
            this.aElementText.Margin = new System.Windows.Forms.Padding(4);
            this.aElementText.Name = "aElementText";
            this.aElementText.Size = new System.Drawing.Size(71, 22);
            this.aElementText.TabIndex = 14;
            // 
            // phiElementLabel
            // 
            this.phiElementLabel.AutoSize = true;
            this.phiElementLabel.Location = new System.Drawing.Point(72, 73);
            this.phiElementLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.phiElementLabel.Name = "phiElementLabel";
            this.phiElementLabel.Size = new System.Drawing.Size(28, 17);
            this.phiElementLabel.TabIndex = 15;
            this.phiElementLabel.Text = "Phi";
            // 
            // phiElementText
            // 
            this.phiElementText.Location = new System.Drawing.Point(125, 68);
            this.phiElementText.Margin = new System.Windows.Forms.Padding(4);
            this.phiElementText.Name = "phiElementText";
            this.phiElementText.Size = new System.Drawing.Size(71, 22);
            this.phiElementText.TabIndex = 16;
            // 
            // hMeanLabel
            // 
            this.hMeanLabel.AutoSize = true;
            this.hMeanLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hMeanLabel.Location = new System.Drawing.Point(138, 179);
            this.hMeanLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.hMeanLabel.Name = "hMeanLabel";
            this.hMeanLabel.Size = new System.Drawing.Size(111, 17);
            this.hMeanLabel.TabIndex = 17;
            this.hMeanLabel.Text = "Harmonic mean ";
            // 
            // hMeanValue
            // 
            this.hMeanValue.AutoSize = true;
            this.hMeanValue.Location = new System.Drawing.Point(288, 179);
            this.hMeanValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.hMeanValue.Name = "hMeanValue";
            this.hMeanValue.Size = new System.Drawing.Size(16, 17);
            this.hMeanValue.TabIndex = 18;
            this.hMeanValue.Text = "0";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(291, 287);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 42);
            this.button2.TabIndex = 19;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.periodText);
            this.panel1.Controls.Add(this.periondLabel);
            this.panel1.Controls.Add(this.hMeanLabel);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.deltaXText);
            this.panel1.Controls.Add(this.hMeanValue);
            this.panel1.Controls.Add(this.deltaX1Text);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.phiElementText);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.phiElementLabel);
            this.panel1.Controls.Add(this.arimMean);
            this.panel1.Controls.Add(this.aElementText);
            this.panel1.Controls.Add(this.aMeanValue);
            this.panel1.Controls.Add(this.aElementLabel);
            this.panel1.Controls.Add(this.gMean);
            this.panel1.Controls.Add(this.nElementText);
            this.panel1.Controls.Add(this.gMeanValue);
            this.panel1.Controls.Add(this.nElementLabel);
            this.panel1.Location = new System.Drawing.Point(1085, 340);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 360);
            this.panel1.TabIndex = 20;
            // 
            // periodText
            // 
            this.periodText.Location = new System.Drawing.Point(291, 9);
            this.periodText.Margin = new System.Windows.Forms.Padding(4);
            this.periodText.Name = "periodText";
            this.periodText.Size = new System.Drawing.Size(71, 22);
            this.periodText.TabIndex = 21;
            this.periodText.TextChanged += new System.EventHandler(this.periodText_TextChanged);
            // 
            // periodLabel
            // 
            this.periondLabel.AutoSize = true;
            this.periondLabel.Location = new System.Drawing.Point(226, 14);
            this.periondLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.periondLabel.Name = "periondLabel";
            this.periondLabel.Size = new System.Drawing.Size(49, 17);
            this.periondLabel.TabIndex = 20;
            this.periondLabel.Text = "Period";
            this.periondLabel.Click += new System.EventHandler(this.periondLabel_Click);
            // 
            // AverageValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1782, 853);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AverageValues";
            this.Text = "Average values";
            this.Load += new System.EventHandler(this.AverageValues_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox deltaXText;
        private System.Windows.Forms.TextBox deltaX1Text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label arimMean;
        private System.Windows.Forms.Label aMeanValue;
        private System.Windows.Forms.Label gMean;
        private System.Windows.Forms.Label gMeanValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label nElementLabel;
        private System.Windows.Forms.TextBox nElementText;
        private System.Windows.Forms.Label aElementLabel;
        private System.Windows.Forms.TextBox aElementText;
        private System.Windows.Forms.Label phiElementLabel;
        private System.Windows.Forms.TextBox phiElementText;
        private System.Windows.Forms.Label hMeanLabel;
        private System.Windows.Forms.Label hMeanValue;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox periodText;
        private System.Windows.Forms.Label periondLabel;
    }
}