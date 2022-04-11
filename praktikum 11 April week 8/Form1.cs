using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace praktikum_11_April_week_8
{

    public partial class FormHasilPertandingan : Form
    {
        public static string sqlConnection = "server=localhost;uid=root;pwd=;database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlConnection); //sebagai data koneksi ke DBMSnya
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter; 
        public string sqlQuery;
        public FormHasilPertandingan()
        {
            InitializeComponent();
        }

        private void FormHasilPertandingan_Load(object sender, EventArgs e)
        {
            sqlConnect.Open();
            DataTable dtPlayerHome = new DataTable();
            DataTable dtPlayerAway = new DataTable();
            sqlQuery = "SELECT t.team_name as 'Nama Tim', m.manager_id as 'ID_Manager' FROM team t, manager m WHERE m.manager_id=t.manager_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtPlayerHome);
            cBoxHomeTeam.DataSource = dtPlayerHome;
            cBoxHomeTeam.DisplayMember = "Nama Tim";
            cBoxHomeTeam.ValueMember = "ID_Manager";
            sqlQuery = "SELECT team_name as 'Nama Tim', m.manager_id as 'ID_Manager' FROM team t, manager m WHERE m.manager_id=t.manager_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtPlayerAway);
            cBoxAwayTeam.DataSource = dtPlayerAway;
            cBoxAwayTeam.DisplayMember = "Nama Tim";
            cBoxAwayTeam.ValueMember = "ID_Manager";
        }

        private void cBoxHomeTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtHome = new DataTable();
                // 
                sqlQuery = "SELECT m.manager_name, p.player_name, Concat(t.home_stadium, ', ',t.city), t.capacity FROM manager m, team t, player p WHERE m.manager_id=t.manager_id and  p.player_id=t.captain_id and m.manager_id='" + cBoxHomeTeam.SelectedValue.ToString() + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtHome);
                lbl_ManagerHome.Text = dtHome.Rows[0][0].ToString();
                lbl_CaptainHome.Text = dtHome.Rows[0][1].ToString();
                lbl_StadiumOutput.Text = dtHome.Rows[0][2].ToString();
                lbl_CapacityOutput.Text = dtHome.Rows[0][3].ToString();
                // 

            }
            catch (Exception)
            {

                
            }
            
        }

        private void cBoxAwayTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtAway = new DataTable();
                // 
                sqlQuery = "SELECT m.manager_name, p.player_name FROM manager m, team t, player p WHERE m.manager_id=t.manager_id and  p.player_id=t.captain_id and m.manager_id='" + cBoxAwayTeam.SelectedValue.ToString() + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtAway);
                lbl_ManagerAway.Text = dtAway.Rows[0][0].ToString();
                lbl_CaptainAway.Text = dtAway.Rows[0][1].ToString();
                // 

            }
            catch (Exception)
            {


            }
        }
    }
}
