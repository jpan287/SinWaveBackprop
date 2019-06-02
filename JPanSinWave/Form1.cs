using JPanBackprop;
using MachineLearning;
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

        Network gdnetwork = new Network(a => (Math.Pow(Math.E, a) - Math.Pow(Math.E, -a)) / (Math.Pow(Math.E, a) + Math.Pow(Math.E, -a)), 
            a => (1 - Math.Pow((Math.Pow(Math.E, a) - Math.Pow(Math.E, -a)) / (Math.Pow(Math.E, a) + Math.Pow(Math.E, -a)), 2)), 1, 16, 1);
        Network geneticNetwork = new Network(a => (Math.Pow(Math.E, a) - Math.Pow(Math.E, -a)) / (Math.Pow(Math.E, a) + Math.Pow(Math.E, -a)),
            a => (1 - Math.Pow((Math.Pow(Math.E, a) - Math.Pow(Math.E, -a)) / (Math.Pow(Math.E, a) + Math.Pow(Math.E, -a)), 2)), 1, 8, 1);

        SolidBrush brush = new SolidBrush(Color.Black);
        SolidBrush gdBrush = new SolidBrush(Color.Red);
        SolidBrush geneticBrush = new SolidBrush(Color.Green);

        Trainer trainer;
        GeneticTrainer genetic;
        Graphics graphics;
        Graph graph;

        double[][] inputs;
        double[][] outputs;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();

            trainer = new Trainer(gdnetwork);
            genetic = new GeneticTrainer();

            gdnetwork.Randomize(rand);
            geneticNetwork.Randomize(rand);

            inputs = new double[20][];
            outputs = new double[20][];
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = new double[1];
                outputs[i] = new double[1];
            }

            graph = new Graph(Width, Height, 2 * -Math.PI, 2 * Math.PI, 2, 2, 8, 5);

            int count = 0;
            for (double x = -Math.PI; x < Math.PI; x += 2 * Math.PI / outputs.Length)
            {
                inputs[count][0] = x;
                outputs[count][0] = Math.Sin(x);
                count++;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            graph.DrawGraph(graphics, brush);
            label1.Text = gdnetwork.MAE(inputs, outputs).ToString();
            graph.DrawSineWave(graphics, brush);

            graph.DrawAIWave(graphics, gdBrush, gdnetwork);
            for (int i = 0; i < 2000; i++)
            {
                trainer.SGD(inputs, outputs, 1);
            }
            graph.DrawAIWave(graphics, geneticBrush, geneticNetwork);
            for (int i = 0; i < 200; i++)
            {
                genetic.Train((geneticNetwork, ))
            }

            graphics.Clear(Color.White);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }
    }
}
