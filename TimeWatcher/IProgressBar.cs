using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TimeWatcher
{
    class IProgressBar : ProgressBar
    {
        public Font RatioFont = new Font(FontFamily.GenericSansSerif, (float)10);
        public Color[] ProgressBarColor = {Color.FromArgb(86, 163, 108),
            Color.FromArgb(94, 133, 121),
            Color.FromArgb(119, 195, 79),
            Color.FromArgb(46, 104, 170),
            Color.FromArgb(126, 136, 79),
            Color.DarkRed
            };

        public double Ratio;
        SolidBrush Brush;
        Rectangle BarRectangle;
        Rectangle RatioRectangle;
        StringFormat format;

        public IProgressBar ()
        {
            // 由用户处理paint事件
            base.SetStyle(ControlStyles.UserPaint, true);

            format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            format.LineAlignment = StringAlignment.Center;
        }


        public IProgressBar (Rectangle Bound)
            :this()
        {
            SetBounds(Bound.X, Bound.Y, Bound.Width, Bound.Height);
            RatioRectangle = new Rectangle(0, 0, Bound.Width, (int)(Bound.Height * 0.6));
            RatioRectangle.Height -= 4;
            BarRectangle = new Rectangle(0, (int)(Bound.Height * 0.6), Bound.Width, (int)(Bound.Height * 0.4));
            BarRectangle.Height -= 4;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Ratio = (double)Value / (double)Maximum;
            BarRectangle.Width = (int)(base.Width * Ratio) - 4;
            RatioRectangle.Width = base.Width;
            Console.WriteLine(BarRectangle);
            Console.WriteLine(RatioRectangle);
            int colorIndex = Value / 20;
            Brush = new SolidBrush(ProgressBarColor[colorIndex]);
            e.Graphics.FillRectangle(Brush, BarRectangle);
            colorIndex++;
            Brush.Color = ProgressBarColor[colorIndex >= ProgressBarColor.Length ? ProgressBarColor.Length - 1 : colorIndex];

            e.Graphics.DrawString((Ratio * 100).ToString() + "%", RatioFont, Brush, RatioRectangle, format);
            //base.OnPaint(e);
        }
    }
}
