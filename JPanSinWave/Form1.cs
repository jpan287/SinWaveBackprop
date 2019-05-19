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
        Network network = new Network(a => (Math.Exp(a) - Math.Exp(-a)) / (Math.Exp(a) + Math.Exp(-a)), 1, 3, 1);
        SolidBrush brush = new SolidBrush(Color.Black);
        Trainer trainer;
        Graphics graphics;
        double[][] inputs;
        double[][] outputs;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            trainer = new Trainer(network);
            graphics = this.CreateGraphics();
            network.Randomize(rand);
            inputs = new double[100][];
            outputs = new double[100][];
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = new double[1];
                outputs[i] = new double[1];
            }
            for (int i = 0; i < outputs.Length; i++)
            {
                inputs[i][0] = i / 5 * Math.PI;
                outputs[i][0] = Math.Sin(i / 5 * Math.PI);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = network.MAE(inputs, outputs).ToString();
            for (float x = 0; x < Width; x += 0.5f)
            {
                graphics.FillRectangle(brush, new RectangleF(x, Height / 2 + 100 * (float)Math.Sin(x / 50), 2, 2));
            }
            for (float x = 0; x < Width; x += 0.5f)
            {
                double calcY = network.Compute(new double[] { x / 50 })[0];
                graphics.FillRectangle(brush, new RectangleF(x, Height / 2 + 100 * (float)calcY, 2, 2));
            }
            for (int i = 0; i < 5000; i++)
            {
                trainer.GradientDescent(inputs, outputs);
            }

            graphics.Clear(Color.White);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }
    }
}
