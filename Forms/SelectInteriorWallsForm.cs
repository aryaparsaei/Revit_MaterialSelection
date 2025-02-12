using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ComboBox = System.Windows.Forms.ComboBox;
using Form = System.Windows.Forms.Form;

namespace RevitAddIn
{
    public partial class SelectInteriorWallsForm : Form
    {
        // Document Info
        public ExternalCommandData CommandData { get; set; }
        public UIDocument UIDocument { get; }
        public Document Document { get; }

        // Project Info
        public static List<Wall> InteriorWalls { get; private set; }
        public static List<WallType> InteriorWallTypes { get; private set; }
        public MaterialSelectionScenario Scenario { get; set; }
        public List<WallType> SelectedWallTypes { get; }
        public WallType SelectedWallType { get; private set; }

        // Constructor
        public SelectInteriorWallsForm(ExternalCommandData commandData)
        {
            CommandData = commandData;
            UIDocument = commandData.Application.ActiveUIDocument;
            Document = UIDocument.Document;
            InteriorWalls = GetElements.GetInteriorWalls(Document);
            InteriorWallTypes = GetElements.GetInteriorWallTypes(Document);
            SelectedWallTypes = new List<WallType>();

            InitializeComponent();
            ComboBox_ScenarioMode.DataSource = Enum.GetValues(typeof(ScenarioMode));
        }

        /////////////////////////// Button Methods ///////////////////////////
        private void SelectInteriorWallsForm_Load(object sender, EventArgs e)
        {
            foreach (var wallType in InteriorWallTypes)
            {
                if(!wallType.Name.Contains("Generic")) 
                    listBox_AllTypes.Items.Add(wallType.Name);
            }
        }

        private void Button_AddTypes_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(listBox_AllTypes, listBox_SelectedTypes);
        }

        private void Button_AddAll_Click(object sender, EventArgs e)
        {
            MoveAllItems(listBox_AllTypes, listBox_SelectedTypes);
        }

        private void Button_RemoveSelected_Click(object sender, EventArgs e)
        {
            while (listBox_SelectedTypes.SelectedItems.Count > 0)
            {
                listBox_SelectedTypes.Items.Remove(listBox_SelectedTypes.SelectedItems[0]);
            }

        }

        private void Button_RemoveAll_Click(object sender, EventArgs e)
        {
            listBox_SelectedTypes.Items.Clear();
        }

        private void ComboBox_ScenarioMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((int)(ScenarioMode)ComboBox_ScenarioMode.SelectedItem == 5)
            {
                Button_PairwiseComparison.Enabled = true;
            }
            else
            {
                Button_PairwiseComparison.Enabled = false;
                Scenario = new MaterialSelectionScenario((ScenarioMode) ComboBox_ScenarioMode.SelectedItem);
            }
        }

        private void Button_PairwiseComparison_Click(object sender, EventArgs e)
        {
            var formAHP = new FormPairwiseComparison();
            formAHP.ShowDialog();
            Scenario = new MaterialSelectionScenario
            {
                EnvironmentalFactor = formAHP.Weights[0],
                EconomicalFactor = formAHP.Weights[1],
                SocialFactor = formAHP.Weights[2]
            };

        }


        // Calculate
        private void Button_Calculate_Click(object sender, EventArgs e)
        {
            SelectedWallTypes.Clear();
            // Getting the user selected wall types
            foreach (var item in listBox_SelectedTypes.Items)
            {
                foreach (var wallType in InteriorWallTypes)
                {
                    if(wallType.Name == item.ToString())
                        SelectedWallTypes.Add(wallType);
                }
                
            }

            // Clearing the Result lists

            dataGridView_Result.Rows.Clear();
            //listBox_Result.Items.Clear();
            //listBox_Price.Items.Clear();
            //listBox_EmbodiedEnergy.Items.Clear();
            //listBox_SocialScore.Items.Clear();

            var wallDetails = SelectionTool.CalculateWalls(SelectedWallTypes, InteriorWalls);
            var wallsRanked = SelectionTool.RankWalls(Scenario, wallDetails);


            //foreach (var item in wallsRanked)
            //{
            //    listBox_Result.Items.Add(item.Key.Name);
            //    listBox_Price.Items.Add(wallDetails[item.Key]["Price"]);
            //    listBox_EmbodiedEnergy.Items.Add(wallDetails[item.Key]["Embodied Energy"]);
            //    listBox_SocialScore.Items.Add(wallDetails[item.Key]["Social Score"]);
            //}

            var rankCounter = 1;
            foreach (var d in wallsRanked)
            {
                dataGridView_Result.Rows.Add
                    (rankCounter, d.Key.Name,
                    wallDetails[d.Key]["Price"],
                    wallDetails[d.Key]["Embodied Energy"],
                    wallDetails[d.Key]["Social Score"],
                    d.Value);
                rankCounter += 1;
            }
        }

        // Select Element Type in the Model
        private void Button_SelectType_Click(object sender, EventArgs e)
        {
            string wallTypeName = "";

            if (dataGridView_Result.SelectedCells.Count>0)
            {
                int selectedRowIndex = dataGridView_Result.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView_Result.Rows[selectedRowIndex];
                wallTypeName = selectedRow.Cells["WallType"].Value.ToString();
            }


            //if (listBox_Result.SelectedItem != null)
            //    wallTypeName = listBox_Result.SelectedItem.ToString();

            foreach (var wallType in InteriorWallTypes)
            {
                if (wallType.Name == wallTypeName)
                {
                    SelectedWallType = wallType;
                    break;
                }
            }

            foreach (var wall in InteriorWalls)
            {
                wall.WallType = SelectedWallType;
            }

            TaskDialog.Show("Result",
                $"{InteriorWalls.Count} interior walls detected and changed to {wallTypeName} wall type");
        }

        /////////////////////////// Logic Methods ///////////////////////////

        // Move selected items from one ListBox to another.
        private void MoveSelectedItems(ListBox lstFrom, ListBox lstTo)
        {
            foreach (var item in lstFrom.SelectedItems)
            {
                if (!lstTo.Items.Contains(item))
                {
                    lstTo.Items.Add(item);
                }
            }
        }

        // Move all items from one ListBox to another.
        private void MoveAllItems(ListBox lstFrom, ListBox lstTo)
        {
            foreach (var item in lstFrom.Items)
            {
                if (!lstTo.Items.Contains(item))
                    lstTo.Items.Add(item);
            }
        }
    }
}
