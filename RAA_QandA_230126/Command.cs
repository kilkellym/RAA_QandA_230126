#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;

#endregion

namespace RAA_QandA_230126
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // put any code needed for the form here
            List<PointData> points = new List<PointData>();
            points.Add(new PointData(100, 100, 0));
            points.Add(new PointData(1, 100, 0));
            points.Add(new PointData(1, 10, 0));
            points.Add(new PointData(5, 15, 0));
            points.Add(new PointData(105, 100, 0));
            points.Add(new PointData(110, 100, 0));
            points.Add(new PointData(120, 100, 0));
            points.Add(new PointData(120, 99, 0));
            points.Add(new PointData(1, 11, 0));
            points.Add(new PointData(5, 5, 0));
            points.Add(new PointData(20, 100, 0));

            List<PointData> sortedList = points.OrderBy(p => p.X).ThenBy(q => q.Y).ThenBy(t => t.Multiply()).ToList();

            GetAllWalls(doc);

            // get form data and do something

            return Result.Succeeded;
        }

        private static List<Wall> GetAllWalls(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Walls);
            collector.WhereElementIsNotElementType();

            List<Wall> wallList = collector.Cast<Wall>().ToList();
            
            return wallList;
        }

        private static List<Wall> GetAllWallsSortedByName(Document doc)
        {
            List<Wall> walls = GetAllWalls(doc);
            List<Wall> sortedWalls = walls.OrderBy(x => x.Name).ToList();
            return sortedWalls;
        }



        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }

        
    }
    public class PointData
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Number { get; set; }

        public PointData(double x, double y, double number)
        {
            X = x;
            Y = y;
            Number = number;
        }
        public double Multiply()
        {
            return X * Y;
        }
    }

    public class WallData
    {
        public Wall WallObject { get; set; }
        public double WallArea { get; set;}

        public WallData(Wall wall)
        {
            WallObject = wall;

        }
    }
}
