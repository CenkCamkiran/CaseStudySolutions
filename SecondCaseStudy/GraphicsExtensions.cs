﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCaseStudy
{
    public static class GraphicsExtensions
    {
        public static void DrawStringInside(this Graphics graphics, Rectangle rect, Font font, Brush brush, string text)
        {
            File.AppendAllText("cenk.txt", text);
            var textSize = graphics.MeasureString(text, font);
            var state = graphics.Save();
            graphics.TranslateTransform(rect.Left, rect.Top);
            graphics.ScaleTransform(rect.Width / textSize.Width, rect.Height / textSize.Height);
            graphics.DrawString(text, font, brush, PointF.Empty);
            graphics.Restore(state);
        }
    }
}
