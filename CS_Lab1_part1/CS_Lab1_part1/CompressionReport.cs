using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Lab1_part1
{
    public partial class CompressionReport : Form
    {
        string fileName;
        public CompressionReport()
        {
            InitializeComponent();
        }

        public CompressionReport(string filename)
        {
            this.fileName = filename;
            InitializeComponent();
            fillChart();
        }

        void fillChart()
        {
            chart1.Series["Sizes"].Points.Clear();
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.Titles.Add($"file {fileName}");

            foreach (var elem in getSizes())
            {
                chart1.Series["Sizes"].Points.AddXY(elem.Key, elem.Value);
            }
        }

        Dictionary<string, long> getSizes()
        {
            Dictionary<string, long> result=new Dictionary<string, long>();
            result.Add("bzip2",new FileInfo(fileName+".bzip2").Length);
            result.Add("gzip", new FileInfo(fileName + ".gzip").Length);
            result.Add("lzf4", new FileInfo(fileName + ".lzf4").Length);
            result.Add("lzma", new FileInfo(fileName + ".lzma").Length);
            result.Add("zlib", new FileInfo(fileName + ".zlib").Length);
            result.Add("original", new FileInfo(fileName + ".txt").Length);

            return result;
        }

        private void CompressionReport_Load(object sender, EventArgs e)
        {

        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                chart1.SaveImage(saveFileDialog.FileName + ".png", System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
            }
        }
    }
}
