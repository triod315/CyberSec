using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Lab1_part1
{
    public partial class FrequencyHistogram : Form
    {
        Dictionary<string, double> frequency;
        public FrequencyHistogram(Dictionary<string, double> freq)
        {
            frequency = freq;

            InitializeComponent();
        }

        string alphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";

        public int LinearInterp(int start, int end, double percentage) => start + (int)Math.Round(percentage * (end - start));
        public Color ColorInterp(Color start, Color end, double percentage) =>
            Color.FromArgb(LinearInterp(start.A, end.A, percentage),
                           LinearInterp(start.R, end.R, percentage),
                           LinearInterp(start.G, end.G, percentage),
                           LinearInterp(start.B, end.B, percentage));
        public Color GradientPick(double percentage, Color Start, Color Center, Color End)
        {
            if (percentage < 0.5)
                return ColorInterp(Start, Center, percentage / 0.5);
            else if (percentage == 0.5)
                return Center;
            else
                return ColorInterp(Center, End, (percentage - 0.5) / 0.5);
        }
        private void FrequencyHistogram_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < alphabet.Length; i++) 
            {
                dataGridView1.Columns.Add(alphabet[i].ToString(),alphabet[i].ToString());
                dataGridView1.Columns[i].Width = 20;
            }
            for (int i = 0; i < alphabet.Length; i++)
            {

                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell = new DataGridViewRowHeaderCell();
                dataGridView1.Rows[i].HeaderCell.Value = $"{alphabet[i]}";
                dataGridView1.Rows[i].Height = 18;
            }

            var Start = Color.Blue;
            var Center = Color.Green;
            var End = Color.Red;

            var maxFrequency = frequency.Values.Max();
            

            foreach (var item in frequency) 
            {
                var i = alphabet.IndexOf(item.Key[0]);
                var j = alphabet.IndexOf(item.Key[1]);
                //dataGridView1.Rows[i].Cells[j].Value = item.Value;
                dataGridView1.Rows[i].Cells[j].ToolTipText = item.Value.ToString("#0.00000");
                var Pick = GradientPick(item.Value/maxFrequency, Start, Center, End);
                dataGridView1.Rows[i].Cells[j].Style.BackColor = Pick;
            }
            label1.Text = maxFrequency.ToString("#0.00000");
            label2.Text = "0";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            /*base.OnPaint(e);

            Graphics graphicsObject = e.Graphics;

            using (Brush aGradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, pictureBox1.Height), Color.Blue, Color.Red))
            {
                using (Pen aGradientPen = new Pen(aGradientBrush,15))
                {
                    
                    graphicsObject.DrawLine(aGradientPen, new Point(0, 0), new Point(0, pictureBox1.Height));
                }
            }*/

            var rect = new Rectangle(0, 0, 25, pictureBox1.Height);
            using (LinearGradientBrush br = new LinearGradientBrush(
                rect, Color.Blue, Color.White, 270f))
            {
                // Create a ColorBlend object. Note that you
                // must initialize it before you save it in the
                // brush's InterpolationColors property.
                ColorBlend colorBlend = new ColorBlend();
                colorBlend.Colors = new Color[]
                {
                    Color.Blue,
                    Color.Green,
                    Color.Red
                };
                colorBlend.Positions = new float[]
                {
                    0f, 0.5f, 1f
                };
                br.InterpolationColors = colorBlend;

                e.Graphics.FillRectangle(br, rect);
                e.Graphics.DrawRectangle(Pens.Black, rect);
            }
        }
    }
}
