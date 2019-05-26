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
        Graph graph;
        double[][] inputs;
        double[][] outputs;

        double leftTrainBound;
        double rightTrainBound;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            leftTrainBound = 0;
            rightTrainBound = 300;
            trainer = new Trainer(network);
            graphics = this.CreateGraphics();
            network.Randomize(rand);
            inputs = new double[20][];
            outputs = new double[20][];
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = new double[1];
                outputs[i] = new double[1];
            }
            for (int i = 0; i < outputs.Length; i++)
            {
                inputs[i][0] = i * (rightTrainBound - leftTrainBound / outputs.Length);
                outputs[i][0] = Math.Sin(i * (rightTrainBound - leftTrainBound / outputs.Length));
            }
            graph = new Graph(Width, Height, 2 * -Math.PI, 2 * Math.PI, 2, 2, 9, 5);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            graph.DrawGraph(graphics, brush);
            label1.Text = network.MAE(inputs, outputs).ToString();

            graph.DrawSineWave(graphics, brush);
            /*
            //drawing regular sine wave
            for (float x = 0; x < Width; x += 0.5f)
            {
                graphics.FillRectangle(brush, new RectangleF(x, Height / 2 + 100 * (float)Math.Sin(x / 50), 2, 2));
            }

            //ai drawing sine wave
            for (float x = 0; x < Width; x += 0.5f)
            {
                double calcY = network.Compute(new double[] { x / 50 })[0];
                graphics.FillRectangle(brush, new RectangleF(x, Height / 2 + 100 * (float)calcY, 2, 2));
            }
            //for (int i = 0; i < 5000; i++)
            //{
            //    trainer.GradientDescent(inputs, outputs);
            //*/

            //graphics.Clear(Color.White);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }
    }
}
