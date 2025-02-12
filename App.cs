using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace RevitAddIn
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // create ribbon panel
            RibbonPanel panel = application.CreateRibbonPanel("MaterialSelectionTool");
            
            PushButtonData button1 = new PushButtonData("SelectInteriorWall","Select Interior\nWall Type",Assembly.GetExecutingAssembly().Location,"RevitAddIn.SelectInteriorWallsCommand");
            PushButtonData button2 = new PushButtonData("SelectExteriorWall","Select Exterior\nWall Type",Assembly.GetExecutingAssembly().Location,"RevitAddIn.SelectExteriorWallsCommand");
            PushButtonData button3 = new PushButtonData("SelectStructuralFloor","Select Structural\nFloor Type",Assembly.GetExecutingAssembly().Location,"RevitAddIn.SelectStructuralFloorsCommand");
            PushButtonData button4 = new PushButtonData("SelectArchitecturalFloor","Select Architectural\nFloor Type",Assembly.GetExecutingAssembly().Location, "RevitAddIn.SelectArchitecturalFloorsCommand");

            panel.AddItem(button1);
            panel.AddItem(button2);
            panel.AddItem(button3);
            panel.AddItem(button4);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
