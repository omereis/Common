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


namespace OmerEisGlobal {
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
//------------------------------------------------------------------------------
	}
}
