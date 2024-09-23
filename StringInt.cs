/****************************************************************************\
|                                StringInt.cs                                |
\****************************************************************************/
using System;

//using TStringIntDict = System.Collections.Generic.D;

using System.Collections.Generic;
using System.Collections;

//using TStringIntDict = System.Collections.Generic.Dictionary<int,string>;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Mysqlx.Notice;
using WorkHours;

namespace OmerEisCommon {
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
/*
	public class TStringIntDict : Dictionary<int,string> {
		public TStringIntDict() : base() { }
//------------------------------------------------------------------------------
		public void Add (TStringInt si) {
			if (si != null)
				Add (si.ID, si.Name);
		}
	}
*/
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
	public class TStringInt {
		private int m_id;
		private string m_strName;
//------------------------------------------------------------------------------
		public int ID {get{return (m_id);}set{m_id=value;}}
		public String Name {get{return (m_strName);}set{m_strName=value;}}
//------------------------------------------------------------------------------
		public TStringInt () {
			Clear ();
		}
//------------------------------------------------------------------------------
		public TStringInt (TStringInt other) {
			AssignAll (other);
		}
//------------------------------------------------------------------------------
		public void Clear ()  {
			ID   = 0;
			Name = "";
		}
//------------------------------------------------------------------------------
		public void AssignAll (TStringInt other) {
			ID   = other.ID;
			Name = other.Name;
		}
//------------------------------------------------------------------------------
		public override string ToString() {
			return (Name);
		}
//------------------------------------------------------------------------------
		public override bool Equals(object? obj) {
			bool fEq = false;

			if (obj == null) {
				TStringInt other = (TStringInt) obj;
				if (other.ID == ID)
					if (String.Compare (other.Name, Name, StringComparison.OrdinalIgnoreCase) == 0)
						fEq = true;
			}
			return (fEq);
		}
	}
//------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//------------------------------------------------------------------------------
	public class TStringIntListDB  : Object {
		//protected TStringIntDict m_dctItems = new TStringIntDict();
		protected TStringInt[] m_aItems;
		protected string m_strTable;
		protected string m_strFieldId;
		protected string m_strFieldName;
		protected string m_strTitle;
//------------------------------------------------------------------------------
		protected string Table {get{return (m_strTable);}}
		protected string FieldID{get{return (m_strFieldId);}}
		protected string FieldName{get{return (m_strFieldName);}}
		public String Title {get{return (m_strTitle);}}
//------------------------------------------------------------------------------
		public TStringInt[] Items {get{return (m_aItems);}set{m_aItems=value;}}
//------------------------------------------------------------------------------
		//public TStringIntDict Items {get{return(m_dctItems);}}
//------------------------------------------------------------------------------
		public TStringIntListDB (string strTable, string strFieldId, string strFieldName) : base() {
			m_strTable = strTable;
			m_strFieldId = strFieldId;
			m_strFieldName = strFieldName;
			m_aItems = null;
		}
//------------------------------------------------------------------------------
		public int GetItemsCount () {
			int nCount = 0;
			if (Items != null)
				nCount = Items.Length;
			return (nCount);
		}
//------------------------------------------------------------------------------
		public static string GetSqlInt (TStringIntListDB lst) {
			string str;

			if (lst != null)
				str = lst.Items[0].ID.ToString();
			else
				str = "null";
			str = String.Format ("'{0}'", str);
			return (str);
		}
//------------------------------------------------------------------------------
/*
		public TStringInt ElementAt (int index) {
			TStringInt si = null;
			if (m_aItems != null) {
				if ((index >= 0) && (index < m_aItems.Length)) {
					si = Items[index];
				}
			}
			return (si);
		}
*/
//------------------------------------------------------------------------------
		public bool LoadFromDB (MySqlCommand cmd, ref string strErr) {
			ArrayList al = new ArrayList();
			bool fLoad = false;
			MySqlDataReader reader = null;
			try {
				TStringInt si = new TStringInt();
				string strSql = "select * from " + m_strTable;
				cmd.CommandText = strSql;
				if ((reader = cmd.ExecuteReader()) != null) {
					fLoad = true;
					while ((reader.Read()) && (fLoad))
						if ((fLoad = LoadFromReader (si, reader, ref strErr)) == true)
							al.Add(new TStringInt (si));
				}
				if (al.Count > 0)
					Items = ToArray(al);
				fLoad = true;
			}
			catch (Exception e) {
				strErr = e.Message;
			}
			finally {
				if (reader != null)
					reader.Close ();
			}
			return (fLoad);
		}
//------------------------------------------------------------------------------
		public static TStringInt[] ToArray (ArrayList al) {
			TStringInt[] ar = null;
			if (al.Count > 0) {
				ar = new TStringInt[al.Count];
				for (int n=0 ; n < al.Count ; n++)
					ar[n] = (TStringInt) al[n];
			}
			return (ar);
		}
//------------------------------------------------------------------------------
		public bool LoadFromReader (TStringInt si, MySqlDataReader reader, ref string strErr) {
			bool fLoad=false;
			try {
				si.Clear();
				si.ID = TMisc.ReadIntField(reader, m_strFieldId);
				si.Name = reader.GetString(m_strFieldName);
				fLoad = true;
			}
			catch (Exception e) {
				strErr = e.Message;
			}
			return (fLoad);
		}
//------------------------------------------------------------------------------
		public bool UpdateInDB (TStringInt si, MySqlCommand cmd, ref string strErr) {
			bool fSave;

			try {
				cmd.CommandText = String.Format ("update {0} set {1}='{2}' where {3}={4};",
									Table, FieldName, TMisc.ModifySqlString(si.Name), FieldID, si.ID);
				cmd.ExecuteNonQuery();
				fSave = true;
			}
			catch (Exception e) {
				strErr = e.Message;
				fSave=false;
			}
			return (fSave);
		}
//------------------------------------------------------------------------------
		public bool InsertAsNew (TStringInt si, MySqlCommand cmd, ref int id, ref string strErr) {
			bool fSave;

			try {
				if (TMisc.GetFieldMax (cmd, Table, FieldID, ref id, ref strErr)) {
					cmd.CommandText = String.Format ("insert into {0} ({1},{2}) values ({3},'{4}');",
										Table, FieldID, FieldName, id + 1, TMisc.ModifySqlString(si.Name));
					if (cmd.ExecuteNonQuery() > 0)
						si.ID = id = id + 1;
				}
				fSave = true;
			}
			catch (Exception e) {
				strErr = e.Message;
				fSave=false;
			}
			return (fSave);
		}
//------------------------------------------------------------------------------
		public bool SaveToDB (MySqlCommand cmd, ref string strErr) {
			bool fSave=true;
			int id=0;
			string s="";

			try {
				if (Items != null) {
					for (int n=0 ; n < Items.Length ; n++) {
						if (Items[n].ID > 0)
							fSave &= UpdateInDB (Items[n], cmd, ref s);
						else
							if ((fSave &= InsertAsNew (Items[n], cmd, ref id, ref s)) == true)
								Items[n].ID = id;
						if (!fSave)
							strErr += s + Environment.NewLine;
					}
				}
				fSave = true;
			}
			catch (Exception e) {
				strErr = e.Message;
				fSave=false;
			}
			return (fSave);
		}
//------------------------------------------------------------------------------
		public bool DeleteFromDB (MySqlCommand cmd, int id, ref string strErr) {
			return (TStringIntListDB.DeleteFromDB (cmd, Table, FieldID, id, ref strErr));
		}
///------------------------------------------------------------------------------
		public static bool DeleteFromDB (MySqlCommand cmd, string strTable, string strFieldID, int id, ref string strErr) {
			bool fDel;

			try {
				cmd.CommandText = String.Format ("delete from {0} where {1}={2};",
									strTable, strFieldID, id);
				cmd.ExecuteNonQuery ();
				fDel = true;
			}
			catch (Exception e) {
				strErr=e.Message;
				fDel = false;
			}
			return (fDel);
		}
///------------------------------------------------------------------------------
		public bool SetItems (ArrayList al) {
			bool fSet;

			try {
				Items = null;
				if (al != null) {
					if (al.Count > 0) {
						Items = new TStringInt[al.Count];
						for (int n=0 ; n < al.Count ; n++)
							Items[n] = (TStringInt) al[n];
					}
				}
				fSet = true;
			}
			catch (Exception e) {
				fSet = false;
			}
			return (fSet);
		}
//------------------------------------------------------------------------------
		public static int GetMinID (TStringInt[] aItems) {
			int nMinID = 0;
			if (aItems != null) {
				foreach (TStringInt si in aItems)
					nMinID = Math.Min(nMinID, si.ID);
			}
			return (nMinID);
		}
//------------------------------------------------------------------------------
		public static string GetNewName(TStringInt si) {
			int n = Math.Abs (si.ID);
			string str = "Item " + n.ToString();
			return (str);
		}
//------------------------------------------------------------------------------
		public TStringInt CreateNewItem () {
			TStringInt si = new TStringInt();
			//TClient client = new TClient ();
			si.ID = GetMinID (Items);
			if (si.ID == 0)
				si.ID = -1;
			si.Name = GetNewName(si);
			return (si);
		}
//------------------------------------------------------------------------------
		public int FindItem (TStringInt si) {
			int n, nFound=-1;

			for (n=0 ; (n < Items.Length) && (nFound < 0) ; n++)
				if (si.Equals (Items[n]))
					nFound = n;
			return (nFound);
		}
//------------------------------------------------------------------------------
		public void AddItem (object obj) {
			try {
				AddItem ((TStringInt) obj);
			}
			catch { }
		}
//------------------------------------------------------------------------------
		public void AddItem (TStringInt si) {
			ArrayList al = new ArrayList();
			int n;
			if (Items != null)
				for (n=0 ; n < Items.Length ; n++)
					al.Add (Items[n]);
			al.Add(si);
			Items = new TStringInt[al.Count];
			for (n=0 ; n < al.Count ; n++)
				Items[n] = (TStringInt) al[n];
		}
//------------------------------------------------------------------------------
	}
}
