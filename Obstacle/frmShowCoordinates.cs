using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Obstacle
{
    public partial class frmShowCoordinates : Form
    {
        public frmShowCoordinates()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void frmShowCoordinates_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void exportToExcel()
        {

            {
                string NewFile = Interaction.InputBox("Give File Name", "File Name", "");
                if (!string.IsNullOrEmpty(NewFile) && NewFile.Length > 0)
                {

                    string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), NewFile + ".Xlsx");

                    SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);

                    // Add a WorkbookPart to the document.
                    WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                    workbookpart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart.
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet(new SheetData());

                    // Add Sheets to the Workbook.
                    Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                        AppendChild<Sheets>(new Sheets());

                    // Append a new worksheet and associate it with the workbook.
                    Sheet sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "Obstacle"
                    };
                    sheets.Append(sheet);
                    Worksheet worksheet = worksheetPart.Worksheet;
                    SheetData sheetData = worksheet.GetFirstChild<SheetData>();

                    string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";
                    using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
                    {

                        try
                        {
                            string getRecords = "SELECT   id, [SL NO], OBJECT, NORTHING, EASTING, LATITUDE, LONGITUDE," +
                " Elevation, Distance,Forward_Bearing,ForwardBDMS,Backward_Bearing,BackwardBDMS FROM ConvCoordinates";

                            OleDbCommand cmd = new OleDbCommand(getRecords,connection);
                            connection.Open();
                            DataTable dt = new DataTable();
                            DataSet ds = new DataSet();
                            Row r = new Row();
                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(getRecords, connection))
                            {
                                adapter.Fill(ds);
                                dt = ds.Tables[0];

                                DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                                List<String> columns = new List<string>();
                                foreach (DataColumn column in dt.Columns)
                                {
                                    columns.Add(column.ColumnName);


                                    Cell c = new Cell()
                                    {
                                        DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                        CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName)
                                    };
                                    headerRow.AppendChild(c);
                                    // r.Append(cell);
                                }
                                sheetData.AppendChild(headerRow);

                                foreach (DataRow item in dt.Rows)
                                {
                                    DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                                    for (int i = 0; i < item.ItemArray.Length; i++)
                                    {
                                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                                        Cell c = new Cell()
                                        {
                                            CellValue = new CellValue(item[i].ToString())
                                            //  DataType = CellValues.String

                                        };
                                        newRow.AppendChild(c);

                                    }
                                    sheetData.AppendChild(newRow);
                                }
                                worksheetPart.Worksheet.Save();
                                spreadsheetDocument.Close();

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }

                // Close the document.
                //   spreadsheetDocument.Close();
            }
        }

        private void ImportData_Click(object sender, EventArgs e)
        {
            exportToExcel();
        }
    }
}
