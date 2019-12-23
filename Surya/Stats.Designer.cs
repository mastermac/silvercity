namespace Surya
{
    partial class Stats
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.SimpleDiagram3D simpleDiagram3D1 = new DevExpress.XtraCharts.SimpleDiagram3D();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Doughnut3DSeriesLabel doughnut3DSeriesLabel1 = new DevExpress.XtraCharts.Doughnut3DSeriesLabel();
            DevExpress.XtraCharts.Doughnut3DSeriesView doughnut3DSeriesView1 = new DevExpress.XtraCharts.Doughnut3DSeriesView();
            DevExpress.XtraCharts.SeriesTitle seriesTitle1 = new DevExpress.XtraCharts.SeriesTitle();
            DevExpress.XtraCharts.Doughnut3DSeriesView doughnut3DSeriesView2 = new DevExpress.XtraCharts.Doughnut3DSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.SimpleDiagram3D simpleDiagram3D2 = new DevExpress.XtraCharts.SimpleDiagram3D();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Doughnut3DSeriesLabel doughnut3DSeriesLabel2 = new DevExpress.XtraCharts.Doughnut3DSeriesLabel();
            DevExpress.XtraCharts.Doughnut3DSeriesView doughnut3DSeriesView3 = new DevExpress.XtraCharts.Doughnut3DSeriesView();
            DevExpress.XtraCharts.SeriesTitle seriesTitle2 = new DevExpress.XtraCharts.SeriesTitle();
            DevExpress.XtraCharts.Doughnut3DSeriesView doughnut3DSeriesView4 = new DevExpress.XtraCharts.Doughnut3DSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.xpCollection1 = new DevExpress.Xpo.XPCollection(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
            this.xpCollection2 = new DevExpress.Xpo.XPCollection(this.components);
            this.chartControl3 = new DevExpress.XtraCharts.ChartControl();
            this.metroTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.tabPage1);
            this.metroTabControl1.Controls.Add(this.tabPage2);
            this.metroTabControl1.Controls.Add(this.tabPage3);
            this.metroTabControl1.Controls.Add(this.tabPage4);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 2;
            this.metroTabControl1.Size = new System.Drawing.Size(1338, 700);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chartControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1330, 658);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Stock Stats";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // chartControl1
            // 
            this.chartControl1.AppearanceNameSerializable = "The Trees";
            this.chartControl1.DataSource = this.xpCollection1;
            simpleDiagram3D1.RotationMatrixSerializable = "0.453509747992261;0.716524101949608;-0.530020867326315;0;-0.798901637445061;0.590" +
    "438245247476;0.114624832548207;0;0.395076046051421;0.371451059864447;0.840201778" +
    "123561;0;0;0;0;1";
            this.chartControl1.Diagram = simpleDiagram3D1;
            this.chartControl1.Legend.Visible = false;
            this.chartControl1.Location = new System.Drawing.Point(3, 70);
            this.chartControl1.Name = "chartControl1";
            series1.ArgumentDataMember = "Argument";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            doughnut3DSeriesLabel1.TextPattern = "{V} {A}";
            series1.Label = doughnut3DSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.Name = "Series 1";
            series1.ValueDataMembersSerializable = "Value";
            doughnut3DSeriesView1.HoleRadiusPercent = 35;
            doughnut3DSeriesView1.SizeAsPercentage = 100D;
            doughnut3DSeriesView1.SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Counterclockwise;
            seriesTitle1.Text = "";
            doughnut3DSeriesView1.Titles.AddRange(new DevExpress.XtraCharts.SeriesTitle[] {
            seriesTitle1});
            series1.View = doughnut3DSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.SeriesSorting = DevExpress.XtraCharts.SortingMode.Descending;
            this.chartControl1.SeriesTemplate.ArgumentDataMember = "Argument";
            this.chartControl1.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.chartControl1.SeriesTemplate.ValueDataMembersSerializable = "Value";
            doughnut3DSeriesView2.SizeAsPercentage = 100D;
            doughnut3DSeriesView2.SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Counterclockwise;
            this.chartControl1.SeriesTemplate.View = doughnut3DSeriesView2;
            this.chartControl1.Size = new System.Drawing.Size(499, 517);
            this.chartControl1.TabIndex = 0;
            chartTitle1.Text = "STOCK AVAILABILITY CHART";
            this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // xpCollection1
            // 
            this.xpCollection1.ObjectType = typeof(Surya.Stats.SeriesRecord);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.chartControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1330, 658);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Buyer Stats";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.chartControl3);
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1330, 658);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sold-Out Stock Stats";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Location = new System.Drawing.Point(4, 38);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1330, 658);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Invoice Stats";
            // 
            // chartControl2
            // 
            this.chartControl2.AppearanceNameSerializable = "The Trees";
            this.chartControl2.DataSource = this.xpCollection2;
            simpleDiagram3D2.RotationMatrixSerializable = "0.453509747992261;0.716524101949608;-0.530020867326315;0;-0.798901637445061;0.590" +
    "438245247476;0.114624832548207;0;0.395076046051421;0.371451059864447;0.840201778" +
    "123561;0;0;0;0;1";
            simpleDiagram3D2.ZoomPercent = 90;
            this.chartControl2.Diagram = simpleDiagram3D2;
            this.chartControl2.Legend.Visible = false;
            this.chartControl2.Location = new System.Drawing.Point(10, 10);
            this.chartControl2.Name = "chartControl2";
            this.chartControl2.PaletteName = "Office 2013";
            series2.ArgumentDataMember = "Argument";
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            doughnut3DSeriesLabel2.TextPattern = "{A} = {V}";
            series2.Label = doughnut3DSeriesLabel2;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Name = "Series 1";
            series2.ValueDataMembersSerializable = "Value";
            doughnut3DSeriesView3.HoleRadiusPercent = 35;
            doughnut3DSeriesView3.SizeAsPercentage = 100D;
            doughnut3DSeriesView3.SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Counterclockwise;
            seriesTitle2.Text = "";
            doughnut3DSeriesView3.Titles.AddRange(new DevExpress.XtraCharts.SeriesTitle[] {
            seriesTitle2});
            series2.View = doughnut3DSeriesView3;
            this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControl2.SeriesSorting = DevExpress.XtraCharts.SortingMode.Descending;
            this.chartControl2.SeriesTemplate.ArgumentDataMember = "Argument";
            this.chartControl2.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.chartControl2.SeriesTemplate.ValueDataMembersSerializable = "Value";
            doughnut3DSeriesView4.SizeAsPercentage = 100D;
            doughnut3DSeriesView4.SweepDirection = DevExpress.XtraCharts.PieSweepDirection.Counterclockwise;
            this.chartControl2.SeriesTemplate.View = doughnut3DSeriesView4;
            this.chartControl2.Size = new System.Drawing.Size(1306, 645);
            this.chartControl2.TabIndex = 1;
            chartTitle2.Text = "";
            this.chartControl2.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle2});
            // 
            // xpCollection2
            // 
            this.xpCollection2.ObjectType = typeof(Surya.Stats.SeriesRecord1);
            // 
            // chartControl3
            // 
            this.chartControl3.AppearanceNameSerializable = "Nature Colors";
            this.chartControl3.DataSource = this.xpCollection1;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl3.Diagram = xyDiagram1;
            this.chartControl3.Location = new System.Drawing.Point(3, 3);
            this.chartControl3.Name = "chartControl3";
            series3.Name = "Series 1";
            this.chartControl3.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.chartControl3.SeriesTemplate.ArgumentDataMember = "Argument";
            this.chartControl3.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            this.chartControl3.SeriesTemplate.ValueDataMembersSerializable = "Value";
            this.chartControl3.Size = new System.Drawing.Size(543, 411);
            this.chartControl3.TabIndex = 0;
            // 
            // Stats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 780);
            this.Controls.Add(this.metroTabControl1);
            this.Name = "Stats";
            this.Text = "STATISTICS";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Stats_Load);
            this.metroTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(simpleDiagram3D2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.Xpo.XPCollection xpCollection1;
        private DevExpress.XtraCharts.ChartControl chartControl2;
        private DevExpress.Xpo.XPCollection xpCollection2;
        private DevExpress.XtraCharts.ChartControl chartControl3;
    }
}