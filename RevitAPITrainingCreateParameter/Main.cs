using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAPITrainingSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingCreateParameter
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new WallFilter(), "Выберите стены");
            double Sum = 0;
            foreach (var selectedElement in selectedElementRefList)
            {
                Wall oWall = doc.GetElement(selectedElement) as Wall;
                Parameter volumeParameter = oWall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);
                if (volumeParameter.StorageType == StorageType.Double)
                {
                    double volumeValue = UnitUtils.ConvertFromInternalUnits(volumeParameter.AsDouble(), UnitTypeId.CubicMeters);
                    Sum += volumeValue;
                    
                }
                

            }
            TaskDialog.Show("volume", Sum.ToString());
            return Result.Succeeded;
        }

       
    }
}
