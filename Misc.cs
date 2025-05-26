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
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using Microsoft.Data.SqlClient;

#if MYSQL
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Google.Protobuf.WellKnownTypes;
#endif

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
#if MYSQL
//-----------------------------------------------------------------------------
		public static double ReadDoubleField (MySqlDataReader reader, string strField, double dDef=0) {
			double dValue = 0;
			try {
				int nCol = reader.GetOrdinal(strField);
				if (!reader.IsDBNull(nCol))
					dValue = reader.GetDouble(nCol);
			}
			catch (Exception ex) {
				dValue = dDef;
			}
			return (dValue);
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
		public static double ReadRealField  (MySqlDataReader reader, string strField, ref string strErr, double dDef=0) {
			double dValue;
			try {
				int nCol = reader.GetOrdinal(strField);
				if (!reader.IsDBNull(nCol))
					dValue = reader.GetDouble (nCol);
				else
					dValue = dDef;
			}
			catch (Exception ex) {
				dValue = dDef;
				strErr = ex.Message;
			}
			return (dValue);
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
		public static bool CountItems (MySqlCommand cmd, string strTable, string strField, string strValue, ref int nCount, ref string strErr) {
			bool fID;
			MySqlDataReader reader = null;

			try {
				cmd.CommandText = String.Format ("select count(*) from {0} where {1}=\"{2}\";", strTable, strField, strValue);
				reader = cmd.ExecuteReader ();
				if (reader.Read()) {
					nCount = reader.GetInt32 (0);
				}
				fID = true;
			}
			catch (Exception ex) {
				fID = false;
				strErr = ex.Message;
				nCount = 0;
			}
			finally {
				if (reader != null)
					reader.Close();
			}
			return (fID);
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
		public static int ReadIntField (MySqlDataReader reader, string strField, ref string strErr) {
			int nValue=0;

			try {
				nValue = reader.GetInt32 (strField);
			}
			catch (Exception ex) {
				strErr = ex.Message;
				nValue = 0;
			}
			return (nValue);
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
#else
//------------------------------------------------------------------------------
		public static int ReadIntField (SqlDataReader reader, string strField, ref string strErr) {
			int nValue=0;

			try {
				string strValue = ReadTextField (reader, strField, ref strErr);
				nValue = TMisc.ToIntDef (strValue);
			}
			catch (Exception ex) {
				strErr = ex.Message;
				nValue = 0;
			}
			return (nValue);
		}
//------------------------------------------------------------------------------
		public static string ReadTextField (SqlDataReader reader, string strField, ref string strErr) {
			string strValue;

			try {
				strValue = "";
				if (strField != null) {
					if (strField.Length > 0)
						strValue = reader[strField].ToString ();
				}
			}
			catch (Exception ex) {
				strErr = ex.Message;
				strValue = "";
			}
			return (strValue);
		}
//------------------------------------------------------------------------------
		public static float ReadFloatField (SqlDataReader reader, string strField, ref string strErr) {
			float rValue=0;

			try {
				string strValue = ReadTextField (reader, strField, ref strErr);
				if (strValue.Length > 0)
					rValue = (float) TMisc.ToDoubleDef (strValue);
			}
			catch (Exception ex) {
				strErr=ex.Message;
				rValue = 0;
			}
			return (rValue);
		}
//------------------------------------------------------------------------------
		public static DateTime? ReadDateTimeField (SqlDataReader reader, string strField) {
			DateTime? dt = null;

			try {
				int nField = reader.GetOrdinal (strField);
				dt = reader.GetDateTime (nField);
			}
			catch {
				dt = null;
			}
			return (dt);
		}
#endif
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
		public static bool IsValidDouble (string strValue) {
			bool fValid;

			try {
				double dValue = Convert.ToDouble (strValue);
				fValid = true;
			}
			catch (Exception) {
				fValid = false;
			}
			return (fValid);
		}
//-----------------------------------------------------------------------------
		public static double ToDoubleDef (string strValue, double dDef=0) {
			double dValue=0;
			try {
				if (strValue != null)
					dValue = Convert.ToDouble(strValue);
			}
			catch (Exception e) {
				//m_strErr = e.Message;
				dValue = dDef;
			}
			return (dValue);
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
		public static string GetSqlString (DateTime? dt) {
			string str;

			if (dt == null)
				str = "null";
			else
				str = "'" + dt.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
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
		public static int CompareDates (DateTime? dt1, DateTime? dt2) {
			int ret;

			if ((dt1 == null) || (dt2 == null))
				ret = 0;
			else
				ret = DateTime.Compare ((DateTime) dt1, (DateTime) dt2);
			return (ret);
		}
		public static bool is_string (object obj) {
			return (obj is string);
		}
//-----------------------------------------------------------------------------
		public static string GetSqlUpdate (string strTable, string strFldKey, int idKey, params object[] list) {
			string strSql, strValues, strParam;
			int n;

			strValues = "";
			for (n=0 ; n < list.Length ; n++) {
				if ((n % 2) == 0)
					strValues += list[n].ToString();
				else {
					if (is_string (list[n]))
						strParam = String.Format ("'{0}'", ModifySqlString (list[n].ToString ()));
					else
						strParam = list[n].ToString ();
					strValues += String.Format ("={0}", strParam);
					if (n < list.Length - 1)
						strValues += ",";
				}
			}
			strSql = String.Format ("update {0} set {1} where {2}={3};", strTable, strValues, strFldKey, idKey);
			return (strSql);
		}
//----------------------------------------------------------------------------
		public static string GetIniName() {
			string str = Environment.GetCommandLineArgs()[0];//Application.StartupPath;
			string strIni = Path.ChangeExtension(str, ".ini");
			return (strIni);
		}
//------------------------------------------------------------------------------
		public static int GetCurrentYear () {
			return (DateTime.Now.Year);
		}
//-----------------------------------------------------------------------------
		public static char GetRandomChar() {
			string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			Random rand = new Random();
			int num = rand.Next(0, chars.Length - 1);
			return chars[num];
		}
//-----------------------------------------------------------------------------
		public static void AddUpdateField (ArrayList al, string strField, double dValue) {
			AddUpdateField (al, strField, dValue.ToString());
		}
//-----------------------------------------------------------------------------
		public static void AddUpdateField (ArrayList al, string strField, string strValue) {
			string str;
			if (strValue.Trim().Length > 0)
				//al.Add ();
				str = String.Format("{0}={1}", strField, TMisc.GetDBUpdateValue (strValue));
			else
				//al.Add (String.Format("{0}=null", strField));
				str = String.Format("{0}=null", strField);
			al.Add (str);
		}
//-----------------------------------------------------------------------------
		public static string GetSqlUpdateSet (ArrayList al) {
			string strSet = "";

			for (int n=0 ; n < al.Count ; n++) {
				strSet += (string) al[n];
				if (n < al.Count - 1)
					strSet += ",";
			}
			return (strSet);
		}
//-----------------------------------------------------------------------------
		public static string AppDateTime (DateTime? dt) {
			string str = AppDate (dt) + ", " + AppTime(dt);
			return (str);
		}
//-----------------------------------------------------------------------------
		public static string AppDate (DateTime? dt) {
			string str = "";
			if (dt != null)
				str = String.Format ("{0}/{1}/{2}", dt.Value.Day, dt.Value.Month, dt.Value.Year);
			return (str);
		}
//-----------------------------------------------------------------------------
		public static string AppTime (DateTime? dt) {
			string str = "";
			if (dt != null)
				str = String.Format ("{0}:{1}:{2}", dt.Value.Hour, dt.Value.Minute, dt.Value.Second);
			return (str);
		}
//-----------------------------------------------------------------------------
		public static string IntFormat (int nValue) {
			string str = String.Format ("{0:#,0}", nValue);
			return (str);
		}
//----------------------------------------------------------------------------
		public static bool SaveToCsv (string strFileName, ArrayList al) {
			bool fWrite;
			StreamWriter writer = null;
			string str="";

			try {
				writer = new StreamWriter(strFileName);
				for (int nLines=0 ; nLines < al.Count ; nLines++) {
					str = "";
					string[] astr = (string[]) al[nLines];
					for (int nCols=0 ; nCols < astr.Length ; nCols++) {
						str += astr[nCols];
						if (nCols < astr.Length - 1)
							str += ",";
					}
					writer.WriteLine(str);
				}
				fWrite = true;
			}
			catch (Exception ex) {
				Console.WriteLine (ex.Message);
				fWrite = false;
			}
			finally {
				if (writer != null)
					writer.Close();
			}
			return (fWrite);
		}
	}
}
