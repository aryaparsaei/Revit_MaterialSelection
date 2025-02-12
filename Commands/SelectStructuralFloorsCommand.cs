using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddIn
{
    [Transaction(TransactionMode.Manual)]
    class SelectStructuralFloorsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDocument = commandData.Application.ActiveUIDocument;
            Document document = uiDocument.Document;
            SelectStructuralFloorsForm structuralFloorsForm = new SelectStructuralFloorsForm(commandData);

            try
            {
                using (Transaction trans = new Transaction(document, "Select Structural Floors"))
                {
                    trans.Start();

                    // set element properties from excel file
                    Excel excel = new Excel(@"F:\پایان نامه\Database\Material Options.xlsx", 1);
                    var excelData = excel.ReadRange();
                    excel.Close();


                    var structuralFloorTypes = new List<FloorType>();
                    foreach (var floorType in GetElements.GetStructuralFloorTypes(document))
                    {
                        if (!floorType.Name.Contains("Generic"))
                        {
                            structuralFloorTypes.Add(floorType);
                        }
                    }
                    SetParametersFromDatabase(excelData, new List<string>() { "Unit Price", "Embodied Energy", "Social Score" }, structuralFloorTypes);


                    // open form
                    structuralFloorsForm.ShowDialog();

                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        public void SetParametersFromDatabase(string[,] databaseStrArray, List<string> parameterNamesList, List<FloorType> floorTypes)
        {

            for (int i = 0; i < floorTypes.Count; i++)
            {
                var paramList = new List<Parameter>();
                var row = Excel.GetRowNumber(databaseStrArray, floorTypes[i].Name);
                for (int j = 0; j < parameterNamesList.Count; j++)
                {
                    var column = Excel.GetColumnNumber(databaseStrArray, parameterNamesList[j]);

                    paramList.Add(floorTypes[i].LookupParameter(parameterNamesList[j]));
                    paramList[j].Set(double.Parse(databaseStrArray[row, column]));
                }
                paramList.Clear();
            }
        }
    }
}