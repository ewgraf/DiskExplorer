﻿using System.Collections;
using System.Windows.Forms;

namespace DiskExplorer
{
	// https://social.msdn.microsoft.com/Forums/windows/en-US/0424a834-1a7a-4285-8d2b-7c71b814032a/natural-numeric-sort-in-listview-controls?forum=winforms
	/// <summary>
	/// This class is an implementation of the 'IComparer' interface.
	/// </summary>
	public class ListViewColumnSorter : IComparer {
		/// <summary>
		/// Specifies the column to be sorted
		/// </summary>
		private int ColumnToSort;
		/// <summary>
		/// Specifies the order in which to sort (i.e. 'Ascending').
		/// </summary>
		private SortOrder OrderOfSort;
		/// <summary>
		/// Case insensitive comparer object
		/// </summary>
		private CaseInsensitiveComparer ObjectCompare;

		/// <summary>
		/// Class constructor.  Initializes various elements
		/// </summary>
		public ListViewColumnSorter() {
			// Initialize the column to '0'
			ColumnToSort = 0;

			// Initialize the sort order to 'descending'
			OrderOfSort = SortOrder.Descending;

			// Initialize the CaseInsensitiveComparer object
			ObjectCompare = new CaseInsensitiveComparer();
		}

		/// <summary>
		/// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
		/// </summary>
		/// <param name="x">First object to be compared</param>
		/// <param name="y">Second object to be compared</param>
		/// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
		public int Compare(object x, object y) {
			int compareResult;
			ListViewItem listviewX, listviewY;

			// Cast the objects to be compared to ListViewItem objects
			listviewX = (ListViewItem)x;
			listviewY = (ListViewItem)y;

			long xx = 0, yy = 0;
			bool parsed;
			parsed = Form1.TryParseFileSize(listviewX.SubItems[ColumnToSort].Text, out xx);
			parsed = parsed && Form1.TryParseFileSize(listviewY.SubItems[ColumnToSort].Text, out yy);
			if (parsed) {
				compareResult = xx.CompareTo(yy);
			} else {
				// Compare the two items
				compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort]?.Text, listviewY.SubItems[ColumnToSort]?.Text);
			}
			// Calculate correct return value based on object comparison
			if (OrderOfSort == SortOrder.Ascending) {
				// Ascending sort is selected, return normal result of compare operation
				return compareResult;
			} else if (OrderOfSort == SortOrder.Descending) {
				// Descending sort is selected, return negative result of compare operation
				return (-compareResult);
			} else {
				// Return '0' to indicate they are equal
				return 0;
			}
		}

		/// <summary>
		/// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
		/// </summary>
		public int SortColumn {
			set {
				ColumnToSort = value;
			}
            get {
				return ColumnToSort;
			}
		}

		/// <summary>
		/// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
		/// </summary>
		public SortOrder Order {
			set {
				OrderOfSort = value;
			}
			get {
				return OrderOfSort;
			}
		}
	}
}
