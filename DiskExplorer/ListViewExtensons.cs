using System.Collections.Generic;
using System.Windows.Forms;

public static class ListViewExtensons {
    public static ListViewItem[] ToArray(this ListView.CheckedListViewItemCollection collection) {
        var items = new List<ListViewItem>();
        foreach (ListViewItem item in collection) {
            items.Add(item);
        }
        return items.ToArray();
    }
}
