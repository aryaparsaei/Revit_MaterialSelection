using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Microsoft.Office.Interop.Excel;
using Floor = Autodesk.Revit.DB.Floor;

public class GetElements
{

    ///////////////////////////////////////////// GetElementTypes /////////////////////////////////////////////
    
    // Get All Available Interior Wall Types
    public static List<WallType> GetInteriorWallTypes(Document document)
    {
        var interiorWallCollector = new FilteredElementCollector(document);
        // collecting all walls
        var walls = interiorWallCollector.OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsElementType().ToElements();

        // collect interior walls
        var listWalls = new List<WallType>();

        foreach (var wall in walls)
        {
            if((wall as WallType).Function.ToString() == "Interior" && (wall as WallType).FamilyName == "Basic Wall")
                listWalls.Add(wall as WallType);
        }
        
        return listWalls;
    }

    // Get All Available Exterior Wall Types
    public static List<WallType> GetExteriorWallTypes(Document document)
    {
        var exteriorWallCollector = new FilteredElementCollector(document);
        // collecting all walls
        var walls = exteriorWallCollector.OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsElementType().ToElements();

        // collect exterior walls
        var listWalls = new List<WallType>();

        foreach (var wall in walls)
        {
            if ((wall as WallType).Function.ToString() == "Exterior" && (wall as WallType).FamilyName == "Basic Wall")
                listWalls.Add(wall as WallType);
        }

        return listWalls;
    }

    // Get All Available Structural Floor Types
    public static List<FloorType> GetStructuralFloorTypes(Document document)
    {
        var floorCollector = new FilteredElementCollector(document);
        // collect all floors
        var floors = floorCollector.OfCategory(BuiltInCategory.OST_Floors)
            .WhereElementIsElementType().ToElements();
        
        // collect structural floors
        var listFloors = new List<FloorType>();

        
        foreach (var floor in floors)
        {
            var param = floor.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_COMMENTS);
            if(param.AsString() == "Structural")
                listFloors.Add(floor as FloorType);
        }
        return listFloors;
    }

    // Get All Available Architectural Floor Types
    public static List<FloorType> GetArchitecturalFloorTypes(Document document)
    {
        var floorCollector = new FilteredElementCollector(document);
        // collect all floors
        var floors = floorCollector.OfCategory(BuiltInCategory.OST_Floors)
            .WhereElementIsElementType().ToElements();

        // collect Architectural floors
        var listFloors = new List<FloorType>();


        foreach (var floor in floors)
        {
            var param = floor.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_COMMENTS);
            if (param.AsString() == "Architectural")
                listFloors.Add(floor as FloorType);
        }
        return listFloors;
    }

    ///////////////////////////////////////////// GetElementInstances /////////////////////////////////////////////
    public static List<Wall> GetInteriorWalls(Document document)
    {
        var interiorWallCollector = new FilteredElementCollector(document);
        // collecting all walls
        var walls = interiorWallCollector.OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsNotElementType().ToElements();

        // collect interior walls
        var listWalls = new List<Wall>();

        foreach (var wall in walls)
        {
            var wallTypeId = wall.GetTypeId();
            var wallType = document.GetElement(wallTypeId) as WallType;
            if (wallType.Function.ToString() == "Interior" && wallType.FamilyName == "Basic Wall")
                listWalls.Add(wall as Wall);
        }

        return listWalls;
    }


    public static List<Wall> GetExteriorWalls(Document document)
    {
        var exteriorWallCollector = new FilteredElementCollector(document);
        // collecting all walls
        var walls = exteriorWallCollector.OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsNotElementType().ToElements();

        // collect exterior walls
        var listWalls = new List<Wall>();

        foreach (var wall in walls)
        {
            var wallTypeId = wall.GetTypeId();
            var wallType = document.GetElement(wallTypeId) as WallType;
            if (wallType.Function.ToString() == "Exterior" && (wallType).FamilyName == "Basic Wall")
                listWalls.Add(wall as Wall);
        }

        return listWalls;
    }

    public static List<Floor> GetStructuralFloors(Document document)
    {
        var floorCollector = new FilteredElementCollector(document);
        // collect all floors
        var floors = floorCollector.OfCategory(BuiltInCategory.OST_Floors)
            .WhereElementIsNotElementType().ToElements();

        // collect structural floors
        var listFloors = new List<Floor>();


        foreach (var floor in floors)
        {
            var floorTypeId = floor.GetTypeId();
            var floorType = document.GetElement(floorTypeId) as FloorType;
            var param = floorType.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_COMMENTS);
            if (param.AsString() == "Structural")
                listFloors.Add(floor as Floor);
        }
        return listFloors;
    }


    public static List<Floor> GetArchitecturalFloors(Document document)
    {
        var floorCollector = new FilteredElementCollector(document);
        // collect all floors
        var floors = floorCollector.OfCategory(BuiltInCategory.OST_Floors)
            .WhereElementIsNotElementType().ToElements();

        // collect Architectural floors
        var listFloors = new List<Floor>();


        foreach (var floor in floors)
        {
            var floorTypeId = floor.GetTypeId();
            var floorType = document.GetElement(floorTypeId) as FloorType;
            var param = floorType.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_COMMENTS);
            if (param.AsString() == "Architectural")
                listFloors.Add(floor as Floor);
        }
        return listFloors;
    }

}