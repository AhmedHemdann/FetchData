using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Linq;
using System.Text;


namespace FetchData.Applications.RevitCommand
{
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class FetchData : IExternalCommand
    {

        #region Public method
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uIDocument = commandData.Application.ActiveUIDocument;
            Document document = uIDocument.Document;

            // get all data in model
            var filterElement = new FilteredElementCollector(document)
               .WhereElementIsNotElementType()
               .WhereElementIsViewIndependent()
               .Where(x => (x.Category != null) && 
               (x.get_BoundingBox(null)) != null && 
               (x.get_Geometry(new Options() { ComputeReferences = true })) != null);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (Element element in filterElement)
            {
                if (element.IsValidObject)
                {
                    stringBuilder.AppendLine("Element Category : "
                         + element.Category.Name + ", Name : " + element.Name + ", Id : " + element.Id + Environment.NewLine);
                }
            }

            TaskDialog.Show("Info", "successful Fetch data");

            
            File.AppendAllText(@"D:\ITI\Projects BIMAD\PurgeFetchHighlight\Data.txt", stringBuilder.ToString());

            return Result.Succeeded;

        }

        #endregion

    }
}
