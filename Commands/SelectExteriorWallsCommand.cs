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
    class SelectExteriorWallsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDocument = commandData.Application.ActiveUIDocument;
            Document document = uiDocument.Document;
            SelectExteriorWallsForm exteriorWallsForm = new SelectExteriorWallsForm(commandData);

            try
            {
                using (Transaction trans = new Transaction(document, "Select Internal Walls"))
                {
                    trans.Start();

                    // set element properties from excel file
                    Excel excel = new Excel(@"F:\پایان نامه\Database\Material Options.xlsx", 1);
                    var excelData = excel.ReadRange();
                    excel.Close();


                    var exteriorWallTypes = new List<WallType>();
                    foreach (var wallType in GetElements.GetExteriorWallTypes(document))
                    {
                        if (!wallType.Name.Contains("Generic"))
                        {
                            exteriorWallTypes.Add(wallType);
                        }
                    }
                    SetParametersFromDatabase(excelData, new List<string>() { "Unit Price", "Embodied Energy", "Social Score" }, exteriorWallTypes);


                    // open form
                    exteriorWallsForm.ShowDialog();

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

        public void SetParametersFromDatabase(string[,] databaseStrArray, List<string> parameterNamesList, List<WallType> wallTypes)
        {

            for (int i = 0; i < wallTypes.Count; i++)
            {
                var paramList = new List<Parameter>();
                var row = Excel.GetRowNumber(databaseStrArray, wallTypes[i].Name);
                for (int j = 0; j < parameterNamesList.Count; j++)
                {
                    var column = Excel.GetColumnNumber(databaseStrArray, parameterNamesList[j]);

                    paramList.Add(wallTypes[i].LookupParameter(parameterNamesList[j]));
                    paramList[j].Set(double.Parse(databaseStrArray[row, column]));
                }
                paramList.Clear();
            }
        }
    
    }
}