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
    public partial class SelectStructuralFloorsForm : Form
    {
        // Document Info
        public ExternalCommandData CommandData { get; set; }
        public UIDocument UIDocument { get; }
        public Document Document { get; }

        // Project Info
        public static List<Floor> StructuralFloors { get; private set; }
        public static List<FloorType> StructuralFloorTypes { get; private set; }
        public MaterialSelectionScenario Scenario { get; set; }
        public List<FloorType> SelectedFloorTypes { get; }
        public FloorType SelectedFloorType { get; private set; }

        // Constructor
        public SelectStructuralFloorsForm(ExternalCommandData commandData)
        {
            CommandData = commandData;
            UIDocument = commandData.Application.ActiveUIDocument;
            Document = UIDocument.Document;
            StructuralFloors = GetElements.GetStructuralFloors(Document);
            StructuralFloorTypes = GetElements.GetStructuralFloorTypes(Document);
            SelectedFloorTypes = new List<FloorType>();

            InitializeComponent();
            ComboBox_ScenarioMode.DataSource = Enum.GetValues(typeof(ScenarioMode));
        }

        /////////////////////////// Button Methods ///////////////////////////
        private void SelectStructuralFloorsForm_Load(object sender, EventArgs e)
        {
            foreach (var floorType in StructuralFloorTypes)
            {
                if(!floorType.Name.Contains("Generic")) 
                    listBox_AllTypes.Items.Add(floorType.Name);
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
            SelectedFloorTypes.Clear();
            // Getting the user selected floor types
            foreach (var item in listBox_SelectedTypes.Items)
            {
                foreach (var floorType in StructuralFloorTypes)
                {
                    if(floorType.Name == item.ToString())
                        SelectedFloorTypes.Add(floorType);
                }
                
            }

            // Clearing the Result lists

            dataGridView_Result.Rows.Clear();

            var floorDetails = SelectionTool.CalculateFloors(SelectedFloorTypes, StructuralFloors);
            var floorsRanked = SelectionTool.RankFloors(Scenario, floorDetails);

            var rankCounter = 1;
            foreach (var d in floorsRanked)
            {
                dataGridView_Result.Rows.Add
                    (rankCounter, d.Key.Name,
                    floorDetails[d.Key]["Price"],
                    floorDetails[d.Key]["Embodied Energy"],
                    floorDetails[d.Key]["Social Score"],
                    d.Value);
                rankCounter += 1;
            }
        }

        // Select Element Type in the Model
        private void Button_SelectType_Click(object sender, EventArgs e)
        {
            string floorTypeName = "";

            if (dataGridView_Result.SelectedCells.Count>0)
            {
                int selectedRowIndex = dataGridView_Result.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView_Result.Rows[selectedRowIndex];
                floorTypeName = selectedRow.Cells["FloorType"].Value.ToString();
            }


            foreach (var floorType in StructuralFloorTypes)
            {
                if (floorType.Name == floorTypeName)
                {
                    SelectedFloorType = floorType;
                    break;
                }
            }

            foreach (var floor in StructuralFloors)
            {
                floor.FloorType = SelectedFloorType;
            }

            TaskDialog.Show("Result",
                $"{StructuralFloors.Count} structural floors detected and changed to {floorTypeName} floor type");
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
