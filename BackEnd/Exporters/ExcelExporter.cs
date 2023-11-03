using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Excel = Microsoft.Office.Interop.Excel;

namespace BackEnd {
    internal class ExcelExporter : IExporter {
        private Excel.Application _excel = new();
        private Excel._Worksheet _sheet => (Excel.Worksheet)_excel.ActiveSheet;
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        public void SaveTo(List<Employer> inputList, string outputPath) {
            try {
                Init();
                WriteHeaders();
                WriteAllEmployees(inputList);
                AutoWidth();
                _sheet.SaveAs2(outputPath);
            }
            catch {
                _log.Fatal("Při zápisu do Excel souboru nastala chyba.");
                throw;
            }
            finally {
                if (_excel is not null) {
                    _excel.ActiveWorkbook.Saved = true;
                    _excel.ActiveWorkbook.Close();
                    _excel.Quit();
                }
            }
        }

        private void Init() {
            if (_excel is null) {
                _log.Fatal("Excel není nainstalován / dostupný.");
                throw new NullReferenceException("Excel není nainstalován / dostupný.");
            }
            _excel.Workbooks.Add();
        }

        private void WriteHeaders() {
            _sheet.Cells[1, 1] = "CompanyName";
            _sheet.Cells[1, 2] = "EmployeeName";
            _sheet.Cells[1, 3] = "EmployeeNumber";
            _sheet.Cells[1, 4] = "EmployeeAddress";
            _sheet.Cells[1, 5] = "EmployedSince";
        }

        private void WriteAllEmployees(List<Employer> inputList) {
            int row = 2;
            foreach(Employer employer in inputList) {
                foreach (Employee employee in employer.Employees) {
                    WriteEmployee(employee, row);
                    row++;
                }
            }
        }

        private void WriteEmployee(Employee employee, int row) {
            _sheet.Cells[row, 1] = employee.ParentEmployer!.CompanyName;
            _sheet.Cells[row, 2] = employee.GetFullName();
            _sheet.Cells[row, 3] = employee.GetEmployeeNumber();
            _sheet.Cells[row, 4] = employee.Address.GetFullAddress();
            _sheet.Cells[row, 5] = employee.GetEmployedSince();
        }

        private void AutoWidth() {
            //tohle je hodně špatné
            for(int i = 1; i <= 6; i++)
                _sheet.Columns[i].AutoFit();
        }
    }
}
