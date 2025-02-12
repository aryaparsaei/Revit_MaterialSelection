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
    public partial class SelectExteriorWallsForm : Form
    {
        // Document Info
        public ExternalCommandData CommandData { get; set; }
        public UIDocument UIDocument { get; }
        public Document Document { get; }

        // Project Info
        public static List<Wall> ExteriorWalls { get; private set; }
        public static List<WallType> ExteriorWallTypes { get; private set; }
        public MaterialSelectionScenario Scenario { get; set; }
        public List<WallType> SelectedWallTypes { get; }
        public WallType SelectedWallType { get; private set; }

        // Constructor
        public SelectExteriorWallsForm(ExternalCommandData commandData)
        {
            CommandData = commandData;
            UIDocument = commandData.Application.ActiveUIDocument;
            Document = UIDocument.Document;
            ExteriorWalls = GetElements.GetExteriorWalls(Document);
            ExteriorWallTypes = GetElements.GetExteriorWallTypes(Document);
            SelectedWallTypes = new List<WallType>();

            InitializeComponent();
            ComboBox_ScenarioMode.DataSource = Enum.GetValues(typeof(ScenarioMode));
        }

        /////////////////////////// Button Methods ///////////////////////////
        private void SelectExteriorWallsForm_Load(object sender, EventArgs e)
        {
            foreach (var wallType in ExteriorWallTypes)
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
                foreach (var wallType in ExteriorWallTypes)
                {
                    if(wallType.Name == item.ToString())
                        SelectedWallTypes.Add(wallType);
                }
                
            }

            // Clearing the Result lists

            dataGridView_Result.Rows.Clear();

            var wallDetails = SelectionTool.CalculateWalls(SelectedWallTypes, ExteriorWalls);
            var wallsRanked = SelectionTool.RankWalls(Scenario, wallDetails);

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


            foreach (var wallType in ExteriorWallTypes)
            {
                if (wallType.Name == wallTypeName)
                {
                    SelectedWallType = wallType;
                    break;
                }
            }

            foreach (var wall in ExteriorWalls)
            {
                wall.WallType = SelectedWallType;
            }

            TaskDialog.Show("Result",
                $"{ExteriorWalls.Count} exterior walls detected and changed to {wallTypeName} wall type");
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
