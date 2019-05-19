using JPanBackprop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JPanSinWave
{
    public partial class Form1 : Form
    {
        Control control = new Control();
        Random rand = new Random();
        Network network = new Network(a => 1 / (1 + Math.Exp(-a)), 1, 3, 1);
        SolidBrush brush = new SolidBrush(Color.Black);
        Trainer trainer;
        Graphics graphics;
        double[] inputs;
        double[] outputs;
        PointF origin;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            trainer = new Trainer(network);
            graphics = this.CreateGraphics();
            network.Randomize(rand);
            inputs = new double[32];
            outputs = new double[32];
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = -2 * Math.PI + i * Math.PI / 8;
                outputs[i] = Math.Sin(-2 * Math.PI + i * Math.PI / 8);
            }
            origin = new PointF(Width / 2, Height / 2);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            for (double x = 0; x < Width; x += 0.001)
            {
                graphics.FillRectangle(brush, new RectangleF((float)x, Height / 2 + 100 * (float)Math.Sin(x / 50), 2, 2));
            }

            //for (double x = 0; x < Width; x += 1)
            //{
            //    for (int i = 0; i < 500; i++)
            //    {
            //        trainer.GradientDescent(new double[][] { inputs }, new double[][] { outputs });
            //    }
            //    graphics.FillRectangle(brush, new RectangleF((float)x, (int)network.Compute(new double[] { x })[0], 2, 2));
            //}

        }

    }
}
