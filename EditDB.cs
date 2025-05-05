/******************************************************************************\
|                                  EditDB.cs                                   |
\******************************************************************************/
//using WorkHours;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using OmerEisGlobal;


#if MYSQL
using MySql.Data.MySqlClient;
#endif


namespace OmerEisCommon  {
	public partial class EditDB : Form {
		public EditDB() {
			InitializeComponent();
		}
//-----------------------------------------------------------------------------
		private void btnOK_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
		}
//-----------------------------------------------------------------------------
		public bool Execute(TDBParams db_params) {
			Download(db_params);
			bool f = (ShowDialog() == DialogResult.OK);
			if(f)
				Upload(db_params);
			return (f);
		}
//-----------------------------------------------------------------------------
		private void Download(TDBParams db_params) {
			txtbxServer.Text = db_params.Server;
			txtbxDatabase.Text = db_params.Database;
			txtbxUsername.Text = db_params.Username;
			txtbxPassword.Text = db_params.Password;
		}
//-----------------------------------------------------------------------------
		private void Upload(TDBParams db_params) {
			db_params.Server = txtbxServer.Text;
			db_params.Database = txtbxDatabase.Text;
			db_params.Username = txtbxUsername.Text;
			db_params.Password = txtbxPassword.Text;
		}
//-----------------------------------------------------------------------------
		private void btnTest_Click(object sender, EventArgs e) {
			TDBParams db_params = new TDBParams();
			Upload(db_params);
#if MYSQL
			TestMySql ();
#else
			TestSqlServer(db_params);
#endif
		}
//-----------------------------------------------------------------------------
		private void TestSqlServer(TDBParams db_params) {
			SqlConnection conn = null;
			bool fConnect;
			string strMessage;

			try {
				conn = new SqlConnection(db_params.GetConnectionString());
				conn.Open();
				fConnect = true;
				strMessage = "Connection OK";
				conn.Close();
			} catch(Exception ex) {
				fConnect = false;
				strMessage = ex.Message;
			}
			MessageBox.Show(strMessage);
		}
//-----------------------------------------------------------------------------
#if MYSQL
		private void TestMySql () {
			MySqlConnection database = null;
			Cursor c = Cursor.Current;
			try {
				Cursor.Current = Cursors.WaitCursor;
				database = new MySqlConnection(db_params.GetConnectionString());
				database.Open();
				MessageBox.Show("Database open");
				database.Close();
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
			finally {
				Cursor.Current = c;
			}
		}
#endif
//-----------------------------------------------------------------------------
		private void button2_Click(object sender, EventArgs e) {
			Download(new TDBParams());
		}
//-----------------------------------------------------------------------------
		private void button1_Click(object sender, EventArgs e) {
			TDBParams db_params = new TDBParams();
			if(db_params.FromJson(txtJson.Text))
				Download(db_params);
		}
//-----------------------------------------------------------------------------
		private void button3_Click(object sender, EventArgs e) {
			TDBParams db_params = new TDBParams();
			Upload(db_params);
			txtJson.Text = db_params.ToJson();
		}
//-----------------------------------------------------------------------------
		private void EditDB_Load(object sender, EventArgs e) {
			btnSqlServerDefaults.Visible = true;
#if MYSQL
			btnSqlServerDefaults.Visible = false;
#endif
		}
//-----------------------------------------------------------------------------
		private void btnSqlServerDefaults_Click(object sender, EventArgs e) {
			string str = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=master;Data Source=OMER\\SQLEXPRESS;TrustServerCertificate=True;";
			txtConnection.Text = str; 
		}
//-----------------------------------------------------------------------------
	}
}
