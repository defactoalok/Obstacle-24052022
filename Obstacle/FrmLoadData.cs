using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;

namespace Obstacle
{
    public partial class FrmLoadData : Form
    {
        public FrmLoadData()
        {
            InitializeComponent();
        }

        private void FrmLoadData_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
         
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NewFile = Interaction.InputBox("Give File Name", "File Name", "");


            if (!string.IsNullOrEmpty(NewFile) && NewFile.Length > 0)
            {
                try
                {
                    {
                        string appendMaster = "Insert into Master(Location,H_Northing,H_Easting," +
                            "App1East,App1North,App2East,App2North,HRPElevation,Safety,Diversion,Bearing,ReverseBearing" +
                            ",RotorDia,OEdge,FlatFunnel,Fato,Tolf) " +
                                    "Values(@Location,@H_Northing,@H_Easting,@App1East," +
                                    "@App1North,@App2East,@App2North," +
                                    "@HRPElevation,@Safety, @Diversion,@Bearing,@ReverseBearing," +
                                    "@RotorDia,@OEdge,@FlatFunnel,@Fato,@Tolf)";

                        string GetMaxID = "SELECT Max(Master.ID) AS MaxOfID FROM Master";
                        string updateId = "UPDATE SurveyData SET SurveyData.MasterID =@MaxId";

                        string appendData = " Insert into SurveyDataBank Select * from SurveyData";

                        string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";
                        using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
                        {

                            connection.Open();
                          
                            OleDbCommand cmd = new OleDbCommand(appendMaster, connection);
                            cmd.Parameters.AddWithValue("@Location", NewFile.ToString());
                            cmd.Parameters.AddWithValue("@H_Northing", double.Parse(this.H_Northing.Text));
                            cmd.Parameters.AddWithValue("@H_Easting", double.Parse(this.H_Easting.Text));
                            cmd.Parameters.AddWithValue("@App1East", double.Parse(this.App1East.Text));
                            cmd.Parameters.AddWithValue("@App1North", double.Parse(this.App1North.Text));
                            cmd.Parameters.AddWithValue("@App2East", double.Parse(this.App2East.Text));
                            cmd.Parameters.AddWithValue("@App2North", double.Parse(this.App2North.Text));
                            cmd.Parameters.AddWithValue("@HRPElevation", double.Parse(this.HRPElevation.Text));
                            cmd.Parameters.AddWithValue("@Safety", double.Parse(this.Safety.Text));
                            cmd.Parameters.AddWithValue("@Diversion", double.Parse(this.Diversion.Text));
                            cmd.Parameters.AddWithValue("@Bearing", double.Parse(this.Bearing.Text));
                            cmd.Parameters.AddWithValue("@ReverseBearing", double.Parse(this.ReverseBearing.Text));
                            cmd.Parameters.AddWithValue("@RotorDia", double.Parse(this.RotorDia.Text));
                            cmd.Parameters.AddWithValue("@OEdge", double.Parse(this.OEdge.Text));
                            cmd.Parameters.AddWithValue("@FlatFunnel", double.Parse(this.FlatFunnel.Text));
                            cmd.Parameters.AddWithValue("@Fato", double.Parse(this.Fato.Text));
                            cmd.Parameters.AddWithValue("@Tolf", double.Parse(this.Tolf.Text));
                            cmd.ExecuteNonQuery();

                            cmd = new OleDbCommand(GetMaxID, connection);

                            int Lastid = Convert.ToInt32(cmd.ExecuteScalar());
                            this.MaxId.Text = Lastid.ToString();

                            cmd = new OleDbCommand(updateId, connection);
                            cmd.Parameters.AddWithValue("@MaxId", Lastid);
                            cmd.ExecuteNonQuery();

                            cmd = new OleDbCommand(appendData, connection);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Saved Changes");

                            //   OleDbCommand cmd = new OleDbCommand(appendData, connection);
                            //   cmd.ExecuteNonQuery();


                        }
                        WindowState = FormWindowState.Maximized;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            {
                if (Myrow.IsNewRow) continue;
                if (dataGridView1.Rows.Count > 0 &&
                    !string.IsNullOrEmpty(Myrow.Cells[10].Value.ToString()))
                {
                    string surf = Myrow.Cells[10].Value.ToString();

                    try
                    {

                        if (Convert.ToDouble(Myrow.Cells[15].Value) < 0
                            || Convert.ToDouble(Myrow.Cells[17].Value) < 0
                            || Convert.ToDouble(Myrow.Cells[19].Value) < 0)

                        {
                            if (!string.IsNullOrEmpty(surf) && surf.Substring(0, 2) == "AP")
                            { Myrow.DefaultCellStyle.BackColor = System.Drawing.Color.Pink; }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            {
                string NewFile = Interaction.InputBox("Give File Name", "File Name", "");
                if (!string.IsNullOrEmpty(NewFile) && NewFile.Length > 0)
                {
                    
                    string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), NewFile+".Xlsx");

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
                            OleDbCommand cmd = new OleDbCommand("Select * from SurveyData", connection);
                            connection.Open();
                            DataTable dt = new DataTable();
                            DataSet ds = new DataSet();
                            Row r = new Row();
                            using (OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from SurveyData", connection))
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

        private void editParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ObstaclesData.accdb";
            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
            {

                try
                {
                   
                    string getMaster = "Select * from Master where ID=@MasterID";
                     
                     
                    Form1 frmloadMain = new Form1();

                    OleDbCommand cmd = new OleDbCommand(getMaster, connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@MasterId", int.Parse(this.SelectedID.Text));

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                      if (reader.HasRows) {
                        while (reader.Read())
                        {
                            frmloadMain.Controls["H_Northing"].Text = reader["H_Northing"].ToString();
                            frmloadMain.Controls["H_Easting"].Text = reader["H_Easting"].ToString();
                            frmloadMain.Controls["Bearing"].Text = reader["Bearing"].ToString();
                            frmloadMain.Controls["ReverseBearing"].Text = reader["ReverseBearing"].ToString();
                            frmloadMain.Controls["App1North"].Text = reader["App1North"].ToString();
                            frmloadMain.Controls["App1East"].Text = reader["App1East"].ToString();
                            frmloadMain.Controls["App2East"].Text = reader["App2East"].ToString();
                            frmloadMain.Controls["App2North"].Text = reader["App2North"].ToString();
                            frmloadMain.Controls["Safety"].Text = reader["Safety"].ToString();
                            frmloadMain.Controls["Diversion"].Text = reader["Diversion"].ToString();
                            frmloadMain.Controls["RotorDia"].Text = reader["RotorDia"].ToString();
                            frmloadMain.Controls["FlatFunnel"].Text = reader["FlatFunnel"].ToString();
                            frmloadMain.Controls["OEdge"].Text = reader["OEdge"].ToString();
                            frmloadMain.Controls["HRPElevation"].Text = reader["HRPElevation"].ToString();
                            frmloadMain.Controls["Fato"].Text = reader["Fato"].ToString();
                            frmloadMain.Controls["Tolf"].Text = reader["TOLF"].ToString();
                            frmloadMain.Controls["SelectedID"].Text = this.SelectedID.Text;
                            frmloadMain.Controls["Head"].Text = reader["Location"].ToString(); ;
                        }
                    }
                        frmloadMain.Show();

                    }

                }
                catch (Exception ex)
                {
                    var st = new StackTrace(ex, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    MessageBox.Show("Exception Message: " + ex.Message+" "+line.ToString());
                }

               
            }
        }
    }
}
