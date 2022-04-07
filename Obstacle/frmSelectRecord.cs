using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;

namespace Obstacle
{
    public partial class frmSelectRecord : Form
    {
        public frmSelectRecord()
        {
            InitializeComponent();

        }
        private void frmSelectRecord_Load(object sender, EventArgs e)
        {

            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ObstaclesData.accdb";
            string getRecords = "SELECT * from Master";

             
            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
            {
                OleDbCommand cmd = new OleDbCommand(getRecords, connection);
                try
                {
                    DataTable dt = new DataTable();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(getRecords, connection))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];
                        dataGridView1.Refresh();

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ObstaclesData.accdb";
            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
            {

                try
                {
                    string getMaster = "Select * from Master where ID=@MasterID";
    

                    string DeleteData = "Delete from SurveyData";
                    string AppendSurveyData = "Insert into SurveyData Select * from SurveyDataBank " +
                        "where MasterID=@MasterID";
                    Form1 frmloadMain = new Form1();

                    OleDbCommand cmd = new OleDbCommand(getMaster, connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@MasterId", int.Parse(this.SelectedID.Text));

                    using (OleDbDataReader reader = cmd.ExecuteReader())
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
                            frmloadMain.Controls["LatD"].Text = reader["LatD"].ToString();
                            frmloadMain.Controls["LatM"].Text = reader["LatM"].ToString();
                            frmloadMain.Controls["LatS"].Text = reader["LatS"].ToString();
                            frmloadMain.Controls["LngD"].Text = reader["LngD"].ToString();
                            frmloadMain.Controls["LngM"].Text = reader["LngM"].ToString();
                            frmloadMain.Controls["LngS"].Text = reader["LngS"].ToString();
                            frmloadMain.Controls["Zone"].Text = reader["Zones"].ToString();

                        }
                        cmd = new OleDbCommand(DeleteData, connection);
                        cmd.ExecuteNonQuery();
                        cmd = new OleDbCommand(AppendSurveyData, connection);
                        cmd.Parameters.AddWithValue("@MasterId", int.Parse(this.SelectedID.Text));
                        cmd.ExecuteNonQuery();
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
                    MessageBox.Show("Exception Message: " + ex.Message + " " + line);
                }

            }
           this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                this.SelectedID.Text = (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }
    }
}
