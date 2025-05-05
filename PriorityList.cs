using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmerEisCommon {
//-----------------------------------------------------------------------------
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//-----------------------------------------------------------------------------
	public class TPriorityItem {
		private int m_nOrder;
		private string m_strValue;
//-----------------------------------------------------------------------------
		public int Order {get{return(m_nOrder);}set{m_nOrder=value;}}
		public string Value {get{return(m_strValue);}set{m_strValue=value;}}
//-----------------------------------------------------------------------------
		public TPriorityItem () {
			Clear ();
		}
//-----------------------------------------------------------------------------
		public TPriorityItem (TPriorityItem other) {
			AssignAll (other);
		}
//-----------------------------------------------------------------------------
		public TPriorityItem (int nOrder, string strValue) {
			Clear ();
			Order = nOrder;
			Value = strValue;
		}
//-----------------------------------------------------------------------------
		public void Clear () {
			Order = 0;
			Value = "";
		}
//-----------------------------------------------------------------------------
		public void AssignAll (TPriorityItem other) {
			Order = other.Order;
			Value = other.Value;
		}

	}
//-----------------------------------------------------------------------------
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//-----------------------------------------------------------------------------
	public class TPriorityList {
		private TPriorityItem[] m_aItems;
//-----------------------------------------------------------------------------
		public TPriorityItem[] Items {get{return(m_aItems);}set{m_aItems=value;}}
//-----------------------------------------------------------------------------
		public TPriorityList () {
			Items = null;
		}
//-----------------------------------------------------------------------------
		public TPriorityList (TPriorityList other) {
			if (other.Items == null)
				Items = null;
			else
				Items = (TPriorityItem[]) other.Items.Clone ();
		}
//-----------------------------------------------------------------------------
		public void Sort () {
			if (Items != null) {
				TPriorityItem[] aItems = Items.OrderBy (x => x.Order).ToArray();
			}
		}
//-----------------------------------------------------------------------------
		public int GetMaxOrder () {
			int nOrder = -1;

			if (Items != null)
				nOrder = Items[Items.Length - 1].Order;
			return (nOrder);
		}
//-----------------------------------------------------------------------------
		public int Add (String strValue) {
			TPriorityItem[] aItems;
			TPriorityItem item = new TPriorityItem (GetMaxOrder () + 1, strValue);

			if (Items == null)
				aItems = new TPriorityItem[1];
			else
				aItems = new TPriorityItem[Items.Length + 1];
			aItems[aItems.Length - 1] = item;
			Items = aItems;
			return (item.Order);
		}
//-----------------------------------------------------------------------------
		public int Add (int nOrder, String strValue) {
			TPriorityItem[] aItems;

			if (Items == null)
				Add (strValue);
			else {
				TPriorityItem item = new TPriorityItem (GetMaxOrder () + 1, strValue);
			}
		}
//-----------------------------------------------------------------------------
	}
}
