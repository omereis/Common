/******************************************************************************\
|                                HtmlMisc.cs                                   |
\******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
//-----------------------------------------------------------------------------
using System.Collections;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using MySqlX.XDevAPI.Relational;
using System.IO;
using Google.Protobuf.WellKnownTypes;
using System.Security.Policy;
//-----------------------------------------------------------------------------
namespace OmerEisCommon {
	public enum EHtmlHeading {
		H1, H2, H3, H4, H5, H6, H7, H8, Normal
	}
//-----------------------------------------------------------------------------
	public enum EHtmlAlign {
		Left, Right, Center, Justify, None
	}
//-----------------------------------------------------------------------------
	public class THtmlMisc {
//-----------------------------------------------------------------------------
		public static string FormatPage (string strHtml) {
			string strPage = String.Format ("<html>{0}</html>", strHtml);
			return (strPage);
		}
//-----------------------------------------------------------------------------
		public static string FormatHeading (EHtmlHeading eHeading, string strText) {
			string strHead = GetHtmlHeading (eHeading);
			string str = string.Format ("<{0}>{1}</{0}>", strHead, strText);
			return (str);
		}
//-----------------------------------------------------------------------------
		public static string GetHtmlHeading (EHtmlHeading eHeading) {
			string strHeading = "";
			
			if (eHeading == EHtmlHeading.H1)
				strHeading = "H1";
			else if (eHeading == EHtmlHeading.H2)
				strHeading = "H2";
			else if (eHeading == EHtmlHeading.H3)
				strHeading = "H3";
			else if (eHeading == EHtmlHeading.H4)
				strHeading = "H4";
			else if (eHeading == EHtmlHeading.H5)
				strHeading = "H5";
			else if (eHeading == EHtmlHeading.H6)
				strHeading = "H6";
			else if (eHeading == EHtmlHeading.H7)
				strHeading = "H7";
			else if (eHeading == EHtmlHeading.H8)
				strHeading = "H8";
			else
				strHeading = "";
			return (strHeading);

		}
//-----------------------------------------------------------------------------
		public static string FormatDiv (string strText, EHtmlAlign eAlign=EHtmlAlign.None) {
			string strAlign = GetAlign (eAlign);
			string strDiv = String.Format ("<div {0}>{1}</div>", strAlign, strText);
			return (strDiv);
		}
//-----------------------------------------------------------------------------
		private static string GetAlign (EHtmlAlign eAlign) {
			string strAlign;

			if (eAlign == EHtmlAlign.Left)
				strAlign = "left";
			else if (eAlign == EHtmlAlign.Right)
				strAlign= "right";
			else if (eAlign == EHtmlAlign.Center)
				strAlign = "center";
			else if (eAlign == EHtmlAlign.Justify)
				strAlign = "justify";
			else
				strAlign = "";
			if (strAlign.Length > 0)
				strAlign = String.Format ("align={0}", strAlign);
			return (strAlign);
		}
	}
//-----------------------------------------------------------------------------
}