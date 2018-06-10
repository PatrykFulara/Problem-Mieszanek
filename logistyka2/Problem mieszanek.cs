using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;

namespace logistyka2
{

    public partial class Form1 : Form
    {
        double[] pierwiastki = new double[16];
        double[] minimum = new double[4];
        double[] ceny = new double[4];

       

        public Form1()
        {
            
            InitializeComponent();
            wynik1.ForeColor = Color.Red;
            wynik2.ForeColor = Color.Red;
            wynik3.ForeColor = Color.Red;
            wynik4.ForeColor = Color.Red;
            wynik5.ForeColor = Color.Red;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void pobierzDane()
        {
            double.TryParse(z1.Text, out pierwiastki[0]);
            double.TryParse(z2.Text, out pierwiastki[1]);
            double.TryParse(z3.Text, out pierwiastki[2]);
            double.TryParse(z4.Text, out pierwiastki[3]);
            double.TryParse(z5.Text, out pierwiastki[4]);
            double.TryParse(z6.Text, out pierwiastki[5]);
            double.TryParse(z7.Text, out pierwiastki[6]);
            double.TryParse(z8.Text, out pierwiastki[7]);
            double.TryParse(z9.Text, out pierwiastki[8]);
            double.TryParse(z10.Text, out pierwiastki[9]);
            double.TryParse(z11.Text, out pierwiastki[10]);
            double.TryParse(z12.Text, out pierwiastki[11]);
            double.TryParse(z13.Text, out pierwiastki[12]);
            double.TryParse(z14.Text, out pierwiastki[13]);
            double.TryParse(z15.Text, out pierwiastki[14]);
            double.TryParse(z16.Text, out pierwiastki[15]);

            for (int i = 0; i < 16; i++)
                pierwiastki[i] /= 100;

            double.TryParse(najmniej1.Text, out minimum[0]);
            double.TryParse(najmniej2.Text, out minimum[1]);
            double.TryParse(najmniej3.Text, out minimum[2]);
            double.TryParse(najmniej4.Text, out minimum[3]);

            double.TryParse(cena1.Text, out ceny[0]);
            double.TryParse(cena2.Text, out ceny[1]);
            double.TryParse(cena3.Text, out ceny[2]);
            double.TryParse(cena4.Text, out ceny[3]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pobierzDane();

            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();

            Decision z1 = new Decision(Domain.RealNonnegative, "z1");
            Decision z2 = new Decision(Domain.RealNonnegative, "z2");
            Decision z3 = new Decision(Domain.RealNonnegative, "z3");
            Decision z4 = new Decision(Domain.RealNonnegative, "z4");
            model.AddDecisions(z1, z2, z3, z4);
        
            model.AddConstraints("limits",
                z1 >= 0,
                z2 >= 0,
                z3 >= 0,
                z4 >= 0
            );

            model.AddConstraints("production",
                pierwiastki[0] * z1 + pierwiastki[1] * z2 + pierwiastki[2] * z3 + pierwiastki[3] * z4 >= minimum[0],
                pierwiastki[4] * z1 + pierwiastki[5] * z2 + pierwiastki[6] * z3 + pierwiastki[7] * z4 >= minimum[1],
                pierwiastki[8] * z1 + pierwiastki[9] * z2 + pierwiastki[10] * z3 + pierwiastki[11] * z4 >= minimum[2],
                pierwiastki[12] * z1 + pierwiastki[13] * z2 + pierwiastki[14] * z3 + pierwiastki[15] * z4 >= minimum[3]
                );
            
            Goal g = model.AddGoal("cost", GoalKind.Minimize,
                ceny[0] * z1 + ceny[1] * z2 + ceny[2] * z3 + ceny[3] * z4
            );

            Solution solution = context.Solve(new SimplexDirective());

            Report report = solution.GetReport();
            //Console.WriteLine("vz: {0}, sa: {1}", z1, sa);
            wynik1.Text = z1.ToDouble().ToString();
            wynik2.Text = z2.ToDouble().ToString();
            wynik3.Text = z3.ToDouble().ToString();
            wynik4.Text = z4.ToDouble().ToString();

            wynik5.Text = g.ToString();

            Console.Write("{0}", report);
            Console.ReadLine();

           
        }

        private void wynik1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
