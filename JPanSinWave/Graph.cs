using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPanSinWave
{
    public class Graph
    {
        Point origin;
        int windowWidth;
        int windowHeight;
        double leftBound;
        double rightBound;
        double topBound;
        double bottomBound;
        int xLines;
        int yLines;

        public Graph(int Width, int Height, double Left, double Right, double Top, double Bottom, int XLines, int YLines)
        {
            windowWidth = Width;
            windowHeight = Height;
            origin = new Point(windowWidth / 2, windowHeight / 2);
            leftBound = Left;
            rightBound = Right;
            topBound = Top;
            bottomBound = Bottom;
            xLines = XLines;
            yLines = YLines;
        }

        public void DrawGraph(Graphics graphics, Brush brush)
        {
            graphics.FillRectangle(brush, 0, origin.Y - 2, windowWidth, 3);
            graphics.FillRectangle(brush, origin.X - 2, 0, 3, windowHeight);
            for (int i = 0; i < xLines; i++)
            {
                graphics.FillRectangle(brush, i * windowWidth / xLines, origin.Y - 3, 3, 6); 
            }
            for (int i = 0; i < yLines; i++)
            {
                graphics.FillRectangle(brush, origin.X - 3, i * windowHeight / yLines, 6, 3);
            }
        }
        
        public void DrawSineWave(Graphics graphics, Brush brush)
        {
            for (double x = leftBound; x <= rightBound; x += (rightBound - leftBound) / 750)
            {
                double xPos = origin.X + x * origin.X / Math.Abs(leftBound);
                double yPos = Math.Sin(x);// * origin.Y / topBound;
                graphics.FillRectangle(brush, (float)(Math.Abs(xPos)), (float)(origin.Y + yPos * origin.Y / topBound), 2, 2);
            }
        }
    }
}
