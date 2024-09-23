/*****************************************************************************#\
|                               DBParams.cs                                    |
\******************************************************************************/


using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OmerEisCommon;
using System.Text.Json;

namespace OmerEisGlobal {
	public class TDBParams {
		private string m_strServer;
		private string m_strDatabase;
		private string m_strUsername;
		private string m_strPassword;
		TIniFile m_ini;

		public string Server {get{return (m_strServer);}set{m_strServer=value;}}
		public string Database {get{return (m_strDatabase);}set{m_strDatabase=value;}}
		public string Username {get{return (m_strUsername);}set{m_strUsername=value;}}
		public string Password {get{return (m_strPassword);}set{m_strPassword=value;}}
//------------------------------------------------------------------------------
		public TDBParams() {
			Clear();
		}
//------------------------------------------------------------------------------
		public TDBParams(TDBParams other) {
			AssignAll (other);
		}
//------------------------------------------------------------------------------
		public void Clear() {
			Server   = "";
			Database = "";
			Username = "";
			Password = "";
		}
//------------------------------------------------------------------------------
		public void AssignAll (TDBParams other) {
			Server   = other.Server;
			Database = other.Database;
			Username = other.Username;
			Password = other.Password;
		}
//------------------------------------------------------------------------------
		public string ToJson () {
			string strJson = JsonSerializer.Serialize (this);
			return (strJson);
		}
//------------------------------------------------------------------------------
		public bool FromJson (string strJson) {
			bool fConvert;
			try {
				TDBParams? db_params = JsonSerializer.Deserialize<TDBParams> (strJson);
				if (db_params != null) {
					AssignAll (db_params);
					fConvert = true;
				}
				else 
					fConvert = false;
			}
			catch (Exception e) {
				Console.WriteLine (e.Message);
				fConvert = false;
			}
			return (fConvert);
		}
//------------------------------------------------------------------------------
		public string GetConnectionString () {
			string strConn = "";
			try {
				strConn = string.Format("Server='{0}'; database='{1}'; UID='{2}'; password='{3}';", Server, Database, Username, Password);
			}
			catch (Exception e) {
				Console.WriteLine ();
			}
			return (strConn);
		}
	}
}
