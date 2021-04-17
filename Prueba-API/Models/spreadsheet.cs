using System.Linq;
using IronXL;
namespace Prueba_API.Models
{
    public class spreadsheet
    {
        private readonly string FileName;
        WorkBook workbook;
        private const string AnswerCell = "A4";

        public spreadsheet(string fileName)
        {
            FileName = fileName;
            IronXL.License.LicenseKey = ""; //Requerido para que funcione la libreria IRONXL
            
            LoadFile();
        }
        private void LoadFile()
        {
            try
            {
                workbook = WorkBook.Load($@"{FileName}.xlsx");

            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public float GetCell(string cell)
        {
            WorkSheet sheet = workbook.WorkSheets.First();
            float cellValue = sheet[cell].FloatValue;
            return cellValue;
        }
        public void SetCell(string cell,float value)
        {
            workbook.WorkSheets.First()[cell].Value = value;
            workbook.Save();
        }
        public void Calculate()
        {
            workbook.WorkSheets.First()[AnswerCell].FormatString = "VNA(G2,A2:F2)";
            float value = workbook.WorkSheets.First()[AnswerCell].FloatValue;
            workbook.Save();
        }
    }
}
