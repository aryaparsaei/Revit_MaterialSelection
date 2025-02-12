using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.UI;

namespace RevitAddIn
{
    class SelectionTool
    {
        /////////////////////////////////////// Calculate Area ///////////////////////////////////////

        public static double TotalArea(List<Wall> walls)
        {
            double sumAreaSqf = 0;
            foreach (var wall in walls)
            {
                sumAreaSqf += wall.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED).AsDouble();
            }
            // Converting sqf to sqm
            var sumAreaSqm = sumAreaSqf * 0.092903;

            return sumAreaSqm;
        }

        public static double TotalArea(List<Floor> floors)
        {
            double sumAreaSqf = 0;
            foreach (var floor in floors)
            {
                sumAreaSqf += floor.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED).AsDouble();
            }
            // Converting sqf to sqm
            var sumAreaSqm = sumAreaSqf * 0.092903;

            return sumAreaSqm;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        ////////////////////////////////////////////////// Calculate Wall Types //////////////////////////////////////////////////

        // Calculate Walls' Parameter Values
        public static Dictionary<WallType, Dictionary<string, double>> CalculateWalls(List<WallType> wallTypes, List<Wall> walls)
        {
            Dictionary<WallType, Dictionary<string,double>> wallTypeAttributes = new Dictionary<WallType, Dictionary<string,double>>();
            if (wallTypes.Count > 0)
            {
                foreach (var wallType in wallTypes)
                {
                    var attributes = new Dictionary<string, double>
                    {
                        {"Price", wallType.LookupParameter("Unit Price").AsDouble() * TotalArea(walls)},
                        {"Embodied Energy", wallType.LookupParameter("Embodied Energy").AsDouble() * TotalArea(walls)},
                        {"Social Score", wallType.LookupParameter("Social Score").AsDouble()}
                    };


                    wallTypeAttributes.Add
                        (wallType, attributes);
                    
                }

                return wallTypeAttributes;
            }
            TaskDialog.Show("Calculation Error", "There are no available wall types!");
            return wallTypeAttributes;
        }

        // Rank Wall Types Based on the Multi Criteria Decision Making method
        public static Dictionary<WallType, double> RankWalls(MaterialSelectionScenario scenario,
            Dictionary<WallType, Dictionary<string, double>> wallTypeAttributes)
        {
            var decisionMatrix = new double[wallTypeAttributes.Count, 3];
            var rowCounter = 0;
            var columnCounter = 0;
            foreach (var wallTypeAttribute in wallTypeAttributes)
            {
                foreach (var kvp in wallTypeAttribute.Value)
                {
                    decisionMatrix[rowCounter, columnCounter] = kvp.Value;
                    columnCounter++;
                }

                columnCounter = 0;
                rowCounter++;
            }

            var weights = new double[3]{scenario.EconomicalFactor, scenario.EnvironmentalFactor, scenario.SocialFactor};
            var topsis = new TOPSIS(decisionMatrix,weights,new[]{false,false,true});

            var wallTypeScore = new Dictionary<WallType, double>();

            var counter = 0;
            foreach (var item in wallTypeAttributes)
            {
                double score = topsis.RelativeClosenessToIdealSolution[counter];
                counter++;

                wallTypeScore.Add(item.Key, score);
            }
            var sorted = from entry in wallTypeScore orderby entry.Value descending select entry;
            var dict = sorted.ToDictionary(pair => pair.Key, pair => pair.Value);
            
            return dict;
        }

        ////////////////////////////////////////////////// Calculate Floor Types //////////////////////////////////////////////////

        // Calculate Floors' Parameter Values
        public static Dictionary<FloorType, Dictionary<string, double>> CalculateFloors(List<FloorType> floorTypes, List<Floor> floors)
        {
            Dictionary<FloorType, Dictionary<string, double>> floorTypeAttributes = new Dictionary<FloorType, Dictionary<string, double>>();
            if (floorTypes.Count > 0)
            {
                foreach (var floorType in floorTypes)
                {
                    var attributes = new Dictionary<string, double>
                    {
                        {"Price", floorType.LookupParameter("Unit Price").AsDouble() * TotalArea(floors)},
                        {"Embodied Energy", floorType.LookupParameter("Embodied Energy").AsDouble() * TotalArea(floors)},
                        {"Social Score", floorType.LookupParameter("Social Score").AsDouble()}
                    };


                    floorTypeAttributes.Add
                        (floorType, attributes);

                }

                return floorTypeAttributes;
            }
            TaskDialog.Show("Calculation Error", "There are no available floor types!");
            return floorTypeAttributes;
        }

        // Rank Floor Types Based on the Multi Criteria Decision Making method
        public static Dictionary<FloorType, double> RankFloors(MaterialSelectionScenario scenario,
            Dictionary<FloorType, Dictionary<string, double>> floorTypeAttributes)
        {
            var decisionMatrix = new double[floorTypeAttributes.Count, 3];
            var rowCounter = 0;
            var columnCounter = 0;
            foreach (var floorTypeAttribute in floorTypeAttributes)
            {
                foreach (var kvp in floorTypeAttribute.Value)
                {
                    decisionMatrix[rowCounter, columnCounter] = kvp.Value;
                    columnCounter++;
                }

                columnCounter = 0;
                rowCounter++;
            }

            var weights = new double[3] { scenario.EconomicalFactor, scenario.EnvironmentalFactor, scenario.SocialFactor };
            var topsis = new TOPSIS(decisionMatrix, weights, new[] { false, false, true });

            var floorTypeScore = new Dictionary<FloorType, double>();

            var counter = 0;
            foreach (var item in floorTypeAttributes)
            {
                double score = topsis.RelativeClosenessToIdealSolution[counter];
                counter++;

                floorTypeScore.Add(item.Key, score);
            }
            var sorted = from entry in floorTypeScore orderby entry.Value descending select entry;
            var dict = sorted.ToDictionary(pair => pair.Key, pair => pair.Value);

            return dict;
        }


        // Calculate Floors Cost

        //public static Dictionary<FloorType, double> CalculateCost(List<FloorType> floorTypes, List<Floor> floors)
        //{
        //    Dictionary<FloorType, double> floorTypePrices = new Dictionary<FloorType, double>();
        //    if (floorTypes.Count > 0)
        //    {
        //        foreach (var floorType in floorTypes)
        //        {
        //            if (!floorType.Name.Contains("Generic"))
        //            {
        //                foreach (var floor in floors)
        //                {
        //                    floor.FloorType = floorType;
        //                }

        //                floorTypePrices.Add
        //                    (floorType, floorType.LookupParameter("Unit Price").AsDouble() * TotalArea(floors));
        //            }
        //        }
        //        var sorted = from entry in floorTypePrices orderby entry.Value descending select entry;
        //        var dict = sorted.ToDictionary(pair => pair.Key, pair => pair.Value);
        //        var strbuild = new StringBuilder();
        //        foreach (var pair in dict)
        //        {
        //            strbuild.Append($"\nfloor Type {pair.Key.Name}\nPrice: {pair.Value}\n");
        //        }
        //        TaskDialog.Show("Floor Type Prices:", strbuild.ToString());
        //        return dict;
        //    }
        //    TaskDialog.Show("Calculation Error", "There are no available floor types!");
        //    return floorTypePrices;
        //}

        //////////////////////////////// Calculate Different Element Types Embodied Energy ////////////////////////////////

        // Calculate Walls Embodied Energy
        //public static Dictionary<WallType, double> CalculateEmbodiedEnergy(List<WallType> wallTypes, List<Wall> walls)
        //{
        //    Dictionary<WallType, double> wallTypeEmbodiedEnergies = new Dictionary<WallType, double>();
        //    if (wallTypes.Count > 0)
        //    {
        //        foreach (var wallType in wallTypes)
        //        {
        //            if (!wallType.Name.Contains("Generic"))
        //            {
        //                foreach (var wall in walls)
        //                {
        //                    wall.WallType = wallType;
        //                }

        //                wallTypeEmbodiedEnergies.Add
        //                    (wallType, wallType.LookupParameter("Embodied Energy").AsDouble() * TotalArea(walls));
        //            }
        //        }
        //        var sorted = from entry in wallTypeEmbodiedEnergies orderby entry.Value descending select entry;
        //        var dict = sorted.ToDictionary(pair => pair.Key, pair => pair.Value);
        //        var strbuild = new StringBuilder();
        //        foreach (var pair in dict)
        //        {
        //            strbuild.Append($"\nWall Type {pair.Key.Name}\nPrice: {pair.Value}\n");
        //        }
        //        TaskDialog.Show("Wall Type Embodied Energies: ", strbuild.ToString());
        //        return dict;
        //    }
        //    TaskDialog.Show("Calculation Error", "There are no available wall types!");
        //    return wallTypeEmbodiedEnergies;
        //}


        // Calculate Floors Embodied Energy

        //public static Dictionary<FloorType, double> CalculateEmbodiedEnergy(List<FloorType> floorTypes, List<Floor> floors)
        //{
        //    Dictionary<FloorType, double> wallTypeEmbodiedEnergies = new Dictionary<FloorType, double>();
        //    if (floorTypes.Count > 0)
        //    {
        //        foreach (var floorType in floorTypes)
        //        {
        //            if (!floorType.Name.Contains("Generic"))
        //            {
        //                foreach (var floor in floors)
        //                {
        //                    floor.FloorType = floorType;
        //                }

        //                wallTypeEmbodiedEnergies.Add
        //                    (floorType, floorType.LookupParameter("Embodied Energy").AsDouble() * TotalArea(floors));
        //            }
        //        }
        //        var sorted = from entry in wallTypeEmbodiedEnergies orderby entry.Value descending select entry;
        //        var dict = sorted.ToDictionary(pair => pair.Key, pair => pair.Value);
        //        var strbuild = new StringBuilder();
        //        foreach (var pair in dict)
        //        {
        //            strbuild.Append($"\nfloor Type {pair.Key.Name}\nPrice: {pair.Value}\n");
        //        }
        //        TaskDialog.Show("Floor Type Embodied Energies:", strbuild.ToString());
        //        return dict;
        //    }
        //    TaskDialog.Show("Calculation Error", "There are no available floor types!");
        //    return wallTypeEmbodiedEnergies;
        //}

    }

}
