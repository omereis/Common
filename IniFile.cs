/******************************************************************************\
|                                  IniFile.cs                                  |
\******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
//----------------------------------------------------------------------------
using System.Collections;
using System.Globalization;
//----------------------------------------------------------------------------
namespace OmerEisGlobal {
	public class TIniFile {
		private string m_strFile;
//----------------------------------------------------------------------------
		public TIniFile () {
			m_strFile = "";
		}
//----------------------------------------------------------------------------
		public TIniFile  (string strIniName) {
			m_strFile = strIniName;
		}
//----------------------------------------------------------------------------
		public string[] Sections() {
			string[] astrLines = TMisc.ReadFile(m_strFile);
			string[] astrSections=null;
			string str;
			if (astrLines != null) {
				ArrayList al = new ArrayList();
				for (int n=0 ; n < astrLines.Length ; n++) {
					str = astrLines[n].Trim();
					if (str.Length > 0) {
						if (str[0] == '[') {
							if (str[str.Length-1] == ']') {
								str = str.Substring(1, str.Length-2);
								al.Add(str);
							}
						}
					}
				}
				astrSections = TMisc.ToArray(al);
			}
			return (astrSections);
		}
//----------------------------------------------------------------------------
		public string[] Keys(string strSection) {
			string[] astrLines = TMisc.ReadFile(m_strFile);
			return (Keys(astrLines, strSection));
		}
/*
 //----------------------------------------------------------------------------
		private bool IsSectionLine (string strLine) {
			bool fSection = false;
			string str = strLine.Trim();
			if (str[0] == '[')
				if (str[str.Length - 1] == ']')
					fSection = true;
			return (fSection);
		}
*/
//----------------------------------------------------------------------------
		public string[] Keys(string[] astrLines, int iSection) {
			string[] astrKeys = null;
			ArrayList al = new ArrayList();
			if ((astrLines != null) && (iSection  >= 0)) {
				int n = iSection;
				if (IsSectionLine (astrLines[n]))
					n++;
				for ( ; ((n < astrLines.Length) && (!IsSectionLine(astrLines[n]))) ; n++)
					if (astrLines[n].Trim().Length > 0)
						al.Add(astrLines[n]);
			}
			if (al.Count > 0)
				astrKeys = TMisc.ToArray(al);
			return (astrKeys);
		}
//----------------------------------------------------------------------------
		public string[] KeysValues(string strSection) {
			string[] astrLines = TMisc.ReadFile(m_strFile);
			return (KeysValues(astrLines, strSection));

		}
//----------------------------------------------------------------------------
		public string[] KeysValues(string[] astrLines, string strSection) {
			string[] astrKeys = null;
			ArrayList al = new ArrayList();
			int iSection = FindSection (astrLines, strSection), n, nFound=-1;
			if ((astrLines != null) && (iSection  >= 0)) {
				n = iSection;
				if (IsSectionLine (astrLines[n]))
					n++;
				for ( ; (n < astrLines.Length) && (nFound < 0) ; n++) {
					if (IsSectionLine(astrLines[n]))
						nFound = n;
					else
						if (astrLines[n].Trim().Length > 0)
							al.Add (astrLines[n]);
				}
				while ((n < astrLines.Length) && (!IsSectionLine(astrLines[n])))
					al.Add(astrLines[n]);
			}
			if (al.Count > 0)
				astrKeys = TMisc.ToArray(al);
			return (astrKeys);
		}
//----------------------------------------------------------------------------
		public string[] Keys(string[] astrLines, string strSection) {
			string[] astrKeys = null;
			ArrayList al = new ArrayList();
			if (astrLines != null) {
				int iSection = FindSection (astrLines, strSection);
				if (iSection >= 0) {
					for (int n=iSection ; n < astrLines.Length ; n++) {
						if (!IsSectionLine(astrLines[n]))
							if (!IsCommentLine (astrLines[n]))
								if (IsKeyLine(astrLines[n]))
									al.Add (GetKeyFromLine (astrLines[n]));
					}
				}
				if (al.Count > 0)
					astrKeys = TMisc.ToArray(al);
			}
			return (astrKeys);
		}
//----------------------------------------------------------------------------
		private bool IsCommentLine (string str) {
			bool fComment = false;
			string strLine = str?.Trim();
			if (strLine != null)
				if (strLine.Length > 0)
					if (strLine[0] == '#')
						fComment = true;
			return (fComment);
		}
//----------------------------------------------------------------------------
		private bool IsSectionLine(string strLine) {
			bool fIsSection = false;
			string strSource = strLine.Trim();
			if (strSource.Length > 3)
				if (strSource[0] == '[')
					if (strSource[strSource.Length-1] == ']')
						fIsSection = true;
			return (fIsSection);
		}
//----------------------------------------------------------------------------
		private bool IsKeyLine(string strLine) {
			bool fIsKey = false;
			
			if (strLine != null) {
				if (!IsCommentLine (strLine)) {
					if (strLine.Length > 0) {
						//string[] astr = strLine.Split('=');
				        //if (astr.Length == 2)
						fIsKey = true;
					}
				}
			}
			return (fIsKey);
		}
//----------------------------------------------------------------------------
		private string GetKeyFromLine (string strLine) {
			string strKey="";

			if (IsKeyLine (strLine)) {
				string[] astr = strLine.Split('=');
				if (astr.Length >= 2)
					strKey = astr[0].Trim();
				else
					strKey = strLine.Trim();
			}
			return (strKey);
		}
//----------------------------------------------------------------------------
		public int FindSection (string[] astrLines, string strSectionText) {
			string strSection = "[" + strSectionText + "]";
			int n, nFound = -1;

			if (astrLines != null) {
				for (n=0 ; (n < astrLines.Length) && (nFound < 0) ; n++) {
					string s = astrLines[n].Trim();
					if (String.Compare (strSection, s, StringComparison.OrdinalIgnoreCase) == 0)
						nFound = n;
				}
			}
			return (nFound);
		}
//----------------------------------------------------------------------------
		public string ReadString (string strSection, string strKey) {
			string strValue="";
			bool fKeyFound = false;
			string[] astrKeys = KeysValues(strSection), astr;
			if (astrKeys != null) {
				for (int n=0 ; (n < astrKeys.Length) && (!fKeyFound) ; n++) {
					astr = astrKeys[n].Split('=');
					if (astr.Length >= 2) {
						if (String.Equals(strKey, astr[0],StringComparison.OrdinalIgnoreCase)) {
							fKeyFound = true;
							strValue = GetValue (astrKeys[n]);
						}
					}
				}
			}
			return (strValue);
		}
//----------------------------------------------------------------------------
		public string GetValue (string str) {
			string strValue = "";

			int nEq = str.IndexOf('=');
            if (nEq >= 0)
                strValue = str.Substring (nEq + 1, str.Length - nEq - 1);
			return (strValue.Trim());
		}
//----------------------------------------------------------------------------
		public static string FormatIniKeyValue (string strKey, string strValue) {
			string strKeyValue = String.Format("{0}={1}", strKey, strValue);
			return (strKeyValue);
		}
//----------------------------------------------------------------------------
		public static string FormatSection (string strSectionName) {
			string strSection = String.Format("[{0}]", strSectionName);
			return (strSection);
		}
//----------------------------------------------------------------------------
		public bool WriteString (string strSection, string strKey, string strValue) {
			bool fWrite;

			try {
				int iSection, iKey=-1;
				string[] astrLines = TMisc.ReadFile(m_strFile);
				string strKeyValue = FormatIniKeyValue (strKey, strValue);
				iSection = FindKey (astrLines, strSection, strKey, ref iKey);
				if (iSection >= 0) {
					if (iKey >= 0) {
						astrLines[iKey] = strKeyValue;
					}
					else
						astrLines.Append (strKeyValue);
				}
				else {
					if (astrLines == null)
						astrLines = new string[2];
					astrLines[0] = FormatSection (strSection);
					astrLines[1] = strKeyValue;
				}
				TMisc.WriteFile (m_strFile, astrLines);
				fWrite = true;
			}
			catch (Exception ex) {
				Console.WriteLine (ex.Message);
				fWrite = false;
			}
			return (fWrite);
			
		}
//----------------------------------------------------------------------------
		private int FindKey (string[] astrLines, string strSection, string strKey, ref int iKey) {
			iKey = -1;
			int iSection = FindSection (astrLines, strSection);
			if (iSection >= 0) {
				//string[] astrKeys = Keys(astrLines, iSection);
				for (int n=iSection + 1 ; (n < astrLines.Length) && (iKey < 0) ; n++) {
					if (IsSectionLine (astrLines[n]))
						n = astrLines.Length;
					else {
						string[] astr = astrLines[n].Split('=');
						if (astr.Length > 0)
							if (String.Compare (astr[0], strKey, StringComparison.OrdinalIgnoreCase) == 0)
								iKey = n;
					}
				}
			}
			return (iSection);
		}
//----------------------------------------------------------------------------
	}
}
