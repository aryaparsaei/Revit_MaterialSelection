using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitAddIn
{
    public partial class FormPairwiseComparison : Form
    {

        // In weights array, the indexes 0 to 2 relate to Environment, Economy, and Society respectively. 
        public double[] Weights { get; }
        public double[,] PairwiseComparisonMatrix { get; set; }
        public double[,] NormalWeightedMatrix { get; set; }
        public FormPairwiseComparison()
        {
            InitializeComponent();
            Weights = new double[3];
            PairwiseComparisonMatrix = new double[3, 3]
            {
                {1, 0, 0},
                {0, 1, 0},
                {0, 0, 1}
            };
            NormalWeightedMatrix = new double[3, 3];

        }

        private void button_FinishComparison_Click(object sender, EventArgs e)
        {
            CalculatePairwiseComparisonMatrix();

            CalculateWieghtsAHP();

            dataGridView_PairwiseComparison.Rows.Clear();
            dataGridView_PairwiseComparison.Rows.Add
                (PairwiseComparisonMatrix[0, 0], PairwiseComparisonMatrix[0, 1], PairwiseComparisonMatrix[0, 2]);
            dataGridView_PairwiseComparison.Rows.Add
                (PairwiseComparisonMatrix[1, 0], PairwiseComparisonMatrix[1, 1], PairwiseComparisonMatrix[1, 2]);
            dataGridView_PairwiseComparison.Rows.Add
                (PairwiseComparisonMatrix[2, 0], PairwiseComparisonMatrix[2, 1], PairwiseComparisonMatrix[2, 2]);

            dataGridView_NormalMatrix.Rows.Clear();
            dataGridView_NormalMatrix.Rows.Add
                (NormalWeightedMatrix[0, 0], NormalWeightedMatrix[0, 1], NormalWeightedMatrix[0, 2]);
            dataGridView_NormalMatrix.Rows.Add
                (NormalWeightedMatrix[1, 0], NormalWeightedMatrix[1, 1], NormalWeightedMatrix[1, 2]);
            dataGridView_NormalMatrix.Rows.Add
                (NormalWeightedMatrix[2, 0], NormalWeightedMatrix[2, 1], NormalWeightedMatrix[2, 2]);

            dataGridView_Weights.Rows.Clear();
            dataGridView_Weights.Rows.Add
                ("Environmental",Weights[0]);
            dataGridView_Weights.Rows.Add
                ("Economic", Weights[1]);
            dataGridView_Weights.Rows.Add
                ("Social", Weights[2]);

        }

        /////////////////////////////////////// AHP Process ///////////////////////////////////////
        private void CalculatePairwiseComparisonMatrix()
        {
            // Convert user weights to pairwise comparison matrix
            var envEco = ConvertToAHPValue(trackBar_EnvEco);
            var envSoc = ConvertToAHPValue(trackBar_EnvSoc);
            var ecoSoc = ConvertToAHPValue(trackBar_EcoSoc);
            if (trackBar_EnvEco.Value < 0)
            {
                PairwiseComparisonMatrix[0, 1] = envEco;
                PairwiseComparisonMatrix[1, 0] = 1 / (double)envEco;
            }
            else
            {
                PairwiseComparisonMatrix[0, 1] = 1 / (double)envEco;
                PairwiseComparisonMatrix[1, 0] = envEco;
            }

            if (trackBar_EnvSoc.Value < 0)
            {
                PairwiseComparisonMatrix[0, 2] = envSoc;
                PairwiseComparisonMatrix[2, 0] = 1 / (double)envSoc;
            }
            else
            {
                PairwiseComparisonMatrix[0, 2] = 1 / (double)envSoc;
                PairwiseComparisonMatrix[2, 0] = envSoc;
            }

            if (trackBar_EcoSoc.Value < 0)
            {
                PairwiseComparisonMatrix[1, 2] = ecoSoc;
                PairwiseComparisonMatrix[2, 1] = 1 / (double)ecoSoc;
            }
            else
            {
                PairwiseComparisonMatrix[1, 2] = 1 / (double)ecoSoc;
                PairwiseComparisonMatrix[2, 1] = ecoSoc;
            }
        }
        
        private void CalculateWieghtsAHP()
        {
            // Calculate Normal Matrix
            for (int i = 0; i < 3; i++)
            {
                double columnSum = 0;
                for (int j = 0; j < 3; j++)
                {
                    columnSum += PairwiseComparisonMatrix[j, i];
                }

                for (int j = 0; j < 3; j++)
                {
                    NormalWeightedMatrix[j, i] = PairwiseComparisonMatrix[j, i] / columnSum;
                }
            }

            // Calculate Row Weights
            for (int i = 0; i < 3; i++)
            {
                double rowSum = 0;
                for (int j = 0; j < 3; j++)
                {
                    rowSum += NormalWeightedMatrix[i, j];
                }

                Weights[i] = rowSum / 3;
            }
        }

        // Check Results Consistency
        private void button_CR_Click(object sender, EventArgs e)
        {
            var WSV = new double[3];
            for (int i = 0; i < 3; i++)
            {
                double sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum+=PairwiseComparisonMatrix[i, j] * Weights[j];
                }
                WSV[i] = sum;
            }

            var CV = new double[3];
            for (int i = 0; i < 3; i++)
            {
                CV[i] = WSV[i] / Weights[i];
            }

            double sumCV = 0;
            for (int i = 0; i < 3; i++)
            {
                sumCV += CV[i];
            }
            var LandaMAX = sumCV / 3;

            var CI = (LandaMAX - 3) / 2;

            var CR = (CI / 0.58);

            if (CR<=0.1)
            {
                MessageBox.Show(@"Results are OK! Weightings are consistent!");
            }
            else
            {
                MessageBox.Show(@"Weightings ar not consistent. Please repeat the pairwise comparison more carefully");
                dataGridView_PairwiseComparison.Rows.Clear();
                dataGridView_NormalMatrix.Rows.Clear();
                dataGridView_Weights.Rows.Clear();
            }
            textBox_CR.Text = CR.ToString();
        }

        private int ConvertToAHPValue(TrackBar trackbar)
        {
            return Math.Abs(trackbar.Value) + 1;
        }
    }
}
