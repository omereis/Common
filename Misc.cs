/******************************************************************************\
|                                  IniFile.cs                                  |
\******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using System.Collections;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Globalization;


namespace OmerEisCommon {
	public class TMisc {
//------------------------------------------------------------------------------
		public static string[] ReadFile (string strFile) {
			string[] astr = null;
			StreamReader reader = null;

			try {
				reader = new StreamReader (strFile);
				string str = reader.ReadToEnd ();
				astr = str.Split ('\n');
			}
			catch (Exception exp) {
				Console.WriteLine(exp.Message);
				astr = null;
			}
			finally {
				if (reader != null)
					reader.Close ();
			}
			return (astr);
		}
//------------------------------------------------------------------------------
		public static bool WriteFile (string strFile, string[] astrLines) {
			bool fWrite = false;
			StreamWriter writer = null; 

			try {
				writer = new StreamWriter (strFile);
				foreach (string line in astrLines)
					writer.WriteLine (line);
				fWrite = true;
			}
			catch (Exception exp) {
				Console.WriteLine(exp.Message);
				fWrite = true;
			}
			finally {
				if (writer != null)
					writer.Close ();
			}
			return (fWrite);
		}
//------------------------------------------------------------------------------
		public static string[] ToArray (ArrayList al) {
			string[] astr=null;
			if (al != null) {
				if (al.Count > 0) {
					astr = new string[al.Count];
					for (int n=0 ; n < al.Count ; n++)
						astr[n] = (string) al[n];
				}
			}
			return (astr);
		}
//-----------------------------------------------------------------------------
		public static int ReadIntField (MySqlDataReader reader, string strField, int nDef=0) {
			int nValue = 0;
			try {
				int nCol = reader.GetOrdinal(strField);
				if (!reader.IsDBNull(nCol))
					nValue = reader.GetInt32 (strField);
			}
			catch (Exception ex) {
				nValue = nDef;
			}
			return (nValue);
		}
//-----------------------------------------------------------------------------
		public static int ToIntDef (object obj, int nDef=0) {
			int nValue;
			try {
				nValue = Convert.ToInt32(obj);
			}
			catch {
				nValue = nDef;
			}
			return (nValue);
		}
//-----------------------------------------------------------------------------
		public static int ToIntDef (string strValue, int nDef=0) {
			int nValue=0;
			try {
				if (strValue != null)
					nValue = Convert.ToInt32(strValue);
			}
			catch (Exception e) {
				//m_strErr = e.Message;
				nValue = nDef;
			}
			return (nValue);
		}
//-----------------------------------------------------------------------------
		public static bool GetFieldMax (MySqlCommand cmd, string table, string field, ref  int nValue, ref string strErr) {
			bool fID;
			MySqlDataReader reader = null;
			try {
				string strSql = String.Format ("select max({0}) from {1}", field, table);
				cmd.CommandText = strSql;
				reader = cmd.ExecuteReader ();
				if (reader.Read()) {
					nValue = reader.GetInt32 (0);
				}
				fID = true;
			}
			catch (Exception ex) {
				fID = false;
				strErr = ex.Message;
				nValue = 0;
			}
			finally {
				if (reader != null)
					reader.Close();
			}
			return (fID);
		}
//-----------------------------------------------------------------------------
		public static string GetDBUpdateValue (string strValue) {
			string strDB;

			if (strValue.Trim().Length > 0)
				strDB = String.Format("'{0}'", strValue);
			else
				strDB = "null";
			return (strDB);
		}
//-----------------------------------------------------------------------------
		public static string GetDBUpdateValue (int nValue) {
			string strDB;

			if (nValue > 0)
				strDB = nValue.ToString();
			else
				strDB = "null";
			return (strDB);
		}
//-----------------------------------------------------------------------------
		public static string GetDBUpdateValue (bool fValue) {
			int nValue = fValue ? 1 : 0;
			string strDB = nValue.ToString();
			return (strDB);
		}
//-----------------------------------------------------------------------------
		public static string ModifySqlString (string strSource)  {
			string strSql;

			strSql = strSource.Replace ("'", "''");
			return (strSql);
		}
//------------------------------------------------------------------------------
		public static string ReadDateTimeField (DateTime? dtSrc) {
			string str;

			if (dtSrc  != null) {
				DateTime dt = dtSrc.Value;
				str = String.Format ("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second);
			}
			else
				str = "";
			return (str);
		}
//------------------------------------------------------------------------------
		public static string GetSqlText (string strSrc)
		{
			return (Regex.Replace(strSrc,"'","''"));
		}
//------------------------------------------------------------------------------
		public static string GetDateString (DateTime? dt) {
			string str;

			if (dt != null) {
				string strFmt = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
				str = String.Format("{0:0000}-{1:00}-{2:00}", dt.Value.Year,dt.Value.Month,dt.Value.Day);
				str = dt.Value.ToShortDateString();
			}
			else
				str = "";
			return (str);
		}
//------------------------------------------------------------------------------
		public static string GetTimeString (DateTime? dt) {
			string str;

			if (dt != null) {
				string strFmt = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
				str = String.Format("{0:0000}-{1:00}-{2:00}", dt.Value.Year,dt.Value.Month,dt.Value.Day);
				str = dt.Value.ToShortTimeString();
			}
			else
				str = "";
			return (str);
		}
//------------------------------------------------------------------------------
		public static DateTime? ReadDateTimeField (MySqlDataReader reader, string strField) {
			DateTime? dt = null;

			try {
				dt = reader.GetDateTime (strField);
			}
			catch {
				dt = null;
			}
			return (dt);
		}
//------------------------------------------------------------------------------
		public static string ReadTextField (MySqlDataReader reader, string strField, ref string strErr) {
			string strValue;

			try {
				strValue = reader.GetString (strField);
			}
			catch (Exception ex) {
				strErr = ex.Message;
				strValue = "";
			}
			return (strValue);
		}
//------------------------------------------------------------------------------
		public static int CompareDates (DateTime? dt1, DateTime? dt2) {
			int ret;

			if ((dt1 == null) || (dt2 == null))
				ret = 0;
			else
				ret = DateTime.Compare ((DateTime) dt1, (DateTime) dt2);
			return (ret);
		}
//------------------------------------------------------------------------------
	}
}
