using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            bool withOutLatLong;

            if (!string.IsNullOrEmpty(NewFile) && NewFile.Length > 0)
            {
              
                try
                {
                    {
                       

                        withOutLatLong = false;
                        string appendMaster;
                        //   appendMaster = "Insert into Master (Location,H_Northing,H_Easting,App1East,App1North,App2East,App2North,HRPElevation,Safety,Diversion,Bearing,ReverseBearing,RotorDia,OEdge,FlatFunnel,Fato,Tolf,Zone,LatD,LatM,LatS,LngD,LngM,LngS) " +
                        //             " Values(@Location, @H_Northing, @H_Easting, @App1East, @App1North, @App2East, @App2North, @HRPElevation, @Safety, @Diversion, @Bearing, @ReverseBearing, @RotorDia, @OEdge, @FlatFunnel, @Fato, @Tolf, @Zone, @LatD, @LatM, @LatS, @LngD, @LngM, @LngS)";

                      
                        string GetMaxID = "SELECT Max(Master.ID) AS MaxOfID FROM Master";
                        string updateId = "UPDATE SurveyData SET SurveyData.MasterID =@MaxId";
                            //+ "LatD=@LatD,LatM=@LatM,LatS=@LatS," + LngD=@LngD,LngM=@LngM,LngS=@LngS";

                        string appendData = " Insert into SurveyDataBank Select * from SurveyData";

                        string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";
                        using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
                        {
                            appendMaster = "Insert into Master (Location,H_Northing,H_Easting,App1East,App1North," +
                          "App2East,App2North,HRPElevation,Safety,Diversion,Bearing,ReverseBearing,RotorDia," +
                          "OEdge,FlatFunnel,Fato,Tolf,Zones, LatD, LatM, LatS, LngD, LngM, LngS ) " +
                           " Values(@Location, @H_Northing, @H_Easting, @App1East, @App1North, " +
                           "@App2East, @App2North, @HRPElevation, @Safety, @Diversion, @Bearing," +
                           " @ReverseBearing, @RotorDia, @OEdge, @FlatFunnel, @Fato, @Tolf,@Zones, " +
                           "@LatDD, @LatMM, @LatSS, @LngDD, @LngMM, @LngSS )";


                            //,@LatD, @LatM, @LatS, @LngD, @LngM, @LngS  )";
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
                            cmd.Parameters.AddWithValue("@Zones", this.Zone.Text);
                            cmd.Parameters.AddWithValue("@LatDD", returnzero(this.LatD.Text));
                            cmd.Parameters.AddWithValue("@LatMM", returnzero(this.LatM.Text));// double.Parse(this.LatM.Text));
                            cmd.Parameters.AddWithValue("@LatSS", returnzero(this.LatS.Text)); //double.Parse(this.LatS.Text));
                            cmd.Parameters.AddWithValue("@LngDD", returnzero(this.LngD.Text));//double.Parse(this.LngD.Text));
                            cmd.Parameters.AddWithValue("@LngMM", returnzero(this.LngM.Text));//double.Parse(this.LngM.Text));
                            cmd.Parameters.AddWithValue("@LngSS", returnzero(this.LngS.Text));//double.Parse(this.LngS.Text));
                            


                            cmd.ExecuteNonQuery();
                        
                            cmd = new OleDbCommand(GetMaxID, connection);

                            int Lastid = Convert.ToInt32(cmd.ExecuteScalar());
                            this.MaxId.Text = Lastid.ToString();

                            cmd = new OleDbCommand(updateId, connection);
                            cmd.Parameters.AddWithValue("@MaxId", Lastid);
                            /*
                           
                        
                            */
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
                    var st = new StackTrace(ex, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    MessageBox.Show("Exception Message: " + ex.Message + " " + line);
                }
            }
        }

        private string returnzero(string val)
        {
            string ret;
            if(val=="")
            {
                ret = "0";

            }
            else
            {
                ret =  val;
            }
            return ret;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (showCoordinates.Text == "")
            {
                string surf;
                foreach (DataGridViewRow Myrow in dataGridView1.Rows)
                {
                    if (Myrow.IsNewRow) continue;
                  
                    if (!string.IsNullOrEmpty(Myrow.Cells[18].Value.ToString()))
                    {
                         surf= Myrow.Cells[18].Value.ToString();

                        try
                        {

                            if (Convert.ToDouble(Myrow.Cells[13].Value) > 0
                                || Convert.ToDouble(Myrow.Cells[15].Value) > 0
                                || Convert.ToDouble(Myrow.Cells[17].Value) > 0)

                            {
                                if (!string.IsNullOrEmpty(surf) && surf.Substring(0, 2) == "AP")
                                { Myrow.DefaultCellStyle.BackColor = System.Drawing.Color.Pink; }
                            }

                        }
                        catch (Exception ex)
                        {
                            var st = new StackTrace(ex, true);
                            // Get the top stack frame
                            var frame = st.GetFrame(0);
                            // Get the line number from the stack frame
                            var line = frame.GetFileLineNumber();
                            MessageBox.Show("Exception Message: " + ex.Message + " " + line);
                        }

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

                    string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), NewFile + ".Xlsx");

                    SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);

                    // Add a WorkbookPart to the document.
                    WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                    workbookpart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart.
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet(new SheetData());

                    // Add Style Sheet
                    var WorkbookStylesPart = workbookpart.AddNewPart<WorkbookStylesPart>();
                    WorkbookStylesPart.Stylesheet = GetStylesheet();
                    WorkbookStylesPart.Stylesheet.Save();


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
                            string selectsql= "SELECT [SL NO], OBJECT, LATITUDE, LONGITUDE, NORTHING , EASTING,  " +
                                " HRPDistance,Elevation, HRPBearing, X  , Y  , YFunnel, PEA  , OBA  , PEB, OBB, PEC,OBC, Surface  " +
                                "FROM SurveyData";
                            OleDbCommand cmd = new OleDbCommand(selectsql, connection);
                            connection.Open();
                            DataTable dt = new DataTable();
                            DataSet ds = new DataSet();
                            Row r = new Row();
                            DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            DocumentFormat.OpenXml.Spreadsheet.Row newRow1 = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            DocumentFormat.OpenXml.Spreadsheet.Row newRow2 = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            Cell c = new Cell()
                            {
                                CellValue = new CellValue("LIST OF OBJECTS AROUND THE HELIPORT")
                            };
                            newRow.AppendChild(c);
                            sheetData.AppendChild(newRow);

                            c = new Cell()
                            {
                                CellValue = new CellValue("(All Dimensions & elevations are in Metres/ EGM2008, and are measured from Helipad Centre. Bearings are True))")
                            };
                            newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            newRow.AppendChild(c);
                            sheetData.AppendChild(newRow);

                            c = new Cell()
                            {
                                CellValue = new CellValue("• Dimensions elevations are in Metres and distances are measured from Helipad Centre. ")
                            };
                            newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            newRow.AppendChild(c);
                            sheetData.AppendChild(newRow);

                            c = new Cell()
                            {
                                CellValue = new CellValue("• Elevations in EGM2008 (Earth Geodetic Model2008).")
                            };
                            newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            newRow.AppendChild(c);
                            sheetData.AppendChild(newRow);

                            c = new Cell()
                            {
                                CellValue = new CellValue("• Calculations are made w.r.t. Cat-‘A’, ‘B’ & ‘C’ operations with 10% divergence")
                            };
                            newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            newRow.AppendChild(c);
                            sheetData.AppendChild(newRow);

                            c = new Cell()
                            {
                                CellValue = new CellValue("• TS = Transitional Surface 	• HRP= Heliport Reference Point to be fixed at the newly Proposed Helipad Centre  • PE or P Elev = Permissible Elevation")
                            };
                            newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            newRow.AppendChild(c);
                            sheetData.AppendChild(newRow);


                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectsql, connection))
                            {
                                adapter.Fill(ds);
                                dt = ds.Tables[0];
                          
                                
                                List<String> columns = new List<string>();
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Obj.No.")
                                };
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Object Name")
                                    
                                };
                                
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("WGS84 Latitude")
                                };
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("WGS84 Longitude")
                                };
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Grid Northing (M)")
                                };
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Grid Easting (M)")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Distance from HRP")
                                };
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Top Elevation")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("True Brg.HRP")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("X=Long Dist.")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Y=Lat Dist. frm CL")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("YY=Dist Proposed HRP")
                                };
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("PElev Cat-A")
                                };
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Obst (+) or (-) Clear Cat-A")
                                };
                                headerRow.AppendChild(c);
                                 
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("PElev Cat-B")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Obst (+) or (-) Clear Cat-B")
                                };
                                headerRow.AppendChild(c);
                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("PElev Cat-C")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Obst (+) or (-) Clear Cat-C")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("OLS Surface")
                                };
                                headerRow.AppendChild(c);

                                c = new Cell()
                                {
                                    DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                    CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Obstr or Clear")
                                };
                                headerRow.AppendChild(c);

                                /*
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
                                }*/
                                sheetData.AppendChild(headerRow);

                                foreach (DataRow item in dt.Rows)
                                {
                                    newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                                    for (int i = 0; i < item.ItemArray.Length; i++)
                                    {
                                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                                         c = new Cell()
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
                            var st = new StackTrace(ex, true);
                            // Get the top stack frame
                            var frame = st.GetFrame(0);
                            // Get the line number from the stack frame
                            var line = frame.GetFileLineNumber();
                            MessageBox.Show("Exception Message: " + ex.Message + " " + line);
                        }

                    }
                }

                MessageBox.Show("Finished Creating Excel");
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
                        if (reader.HasRows)
                        {
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
                    MessageBox.Show("Exception Message: " + ex.Message + " " + line.ToString());
                }


            }
        }
        private static Stylesheet GetStylesheet()
        {
            var StyleSheet = new Stylesheet();

            // Create "fonts" node.
            var Fonts = new Fonts();
            Fonts.Append(new Font()
            {
                FontName = new FontName() { Val = "Tahoma" },
                FontSize = new FontSize() { Val = 11 },
                FontFamilyNumbering = new FontFamilyNumbering() { Val = 2 },
            });

            Fonts.Count = (uint)Fonts.ChildElements.Count;

            // Create "fills" node.
            var Fills = new Fills();
            Fills.Append(new Fill()
            {
                PatternFill = new PatternFill() { PatternType = PatternValues.None }
            });
            Fills.Append(new Fill()
            {
                PatternFill = new PatternFill() { PatternType = PatternValues.Gray125 }
            });

            Fills.Count = (uint)Fills.ChildElements.Count;

            // Create "borders" node.
            var Borders = new Borders();
            Borders.Append(new Border()
            {
                LeftBorder = new LeftBorder() { Style = BorderStyleValues.Thick },
                RightBorder = new RightBorder() { Style = BorderStyleValues.Thick },
                TopBorder = new TopBorder() { Style = BorderStyleValues.Thick },
                BottomBorder = new BottomBorder() { Style = BorderStyleValues.Thick},
                DiagonalBorder = new DiagonalBorder() { Style = BorderStyleValues.Thick }
            });

            Borders.Count = (uint)Borders.ChildElements.Count;

            // Create "cellStyleXfs" node.
            var CellStyleFormats = new CellStyleFormats();
            CellStyleFormats.Append(new CellFormat()
            {
                NumberFormatId = 0,
                FontId = 0,
                FillId = 0,
                BorderId = 0
            });

            CellStyleFormats.Count = (uint)CellStyleFormats.ChildElements.Count;

            // Create "cellXfs" node.
            var CellFormats = new CellFormats();

            // StyleIndex = 0, A default style that works for most things (But not strings? )
            CellFormats.Append(new CellFormat()
            {
                BorderId = 0,
                FillId = 0,
                FontId = 0,
                NumberFormatId = 0,
                FormatId = 0,
                ApplyNumberFormat = true
            });

            // StyleIndex = 1, A style that works for DateTime (just the date)
            CellFormats.Append(new CellFormat()
            {
                BorderId = 0,
                FillId = 0,
                FontId = 0,
                NumberFormatId = 14, //Date
                FormatId = 0,
                ApplyNumberFormat = true
            });

            // StyleIndex = 2, A style that works for DateTime (Date and Time)
            CellFormats.Append(new CellFormat()
            {
                BorderId = 0,
                FillId = 0,
                FontId = 0,
                NumberFormatId = 22, //Date Time
                FormatId = 0,
                ApplyNumberFormat = true
            });

            CellFormats.Count = (uint)CellFormats.ChildElements.Count;

            // Create "cellStyles" node.
            var CellStyles = new CellStyles();
            CellStyles.Append(new CellStyle()
            {
                Name = "Normal",
                FormatId = 0,
                BuiltinId = 0
            });
            CellStyles.Count = (uint)CellStyles.ChildElements.Count;

            // Append all nodes in order.
            StyleSheet.Append(Fonts);
            StyleSheet.Append(Fills);
            StyleSheet.Append(Borders);
            StyleSheet.Append(CellStyleFormats);
            StyleSheet.Append(CellFormats);
            StyleSheet.Append(CellStyles);

            return StyleSheet;
        }

    }
}
