using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Forms;

namespace DiskExplorer
{
	public static class OSHelper {
		public static string SelectFolder() {
            using (var dialog = new CommonOpenFileDialog() {
                Multiselect = false,
                RestoreDirectory = true,
                IsFolderPicker = true,
                ShowHiddenItems = true
            }) {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                    return dialog.FileName;
                }
            }
			return null;
		}

        public static string SaveFile(string defaultFileName, string filter) {
            using (var dialog = new SaveFileDialog() {
                RestoreDirectory = true,
                Filter = filter
            }) {
                dialog.FileName = defaultFileName;
                if (dialog.ShowDialog() == DialogResult.OK) {
                    return dialog.FileName;
                }
            }
			return null;
		}

        public static string OpenFile(string defaultFileName, string filter) {
            using (var dialog = new OpenFileDialog() {
                RestoreDirectory = true,
                Filter = filter
            }) {
                dialog.FileName = defaultFileName;
                if (dialog.ShowDialog() == DialogResult.OK) {
                    return dialog.FileName;
                }
            }
            return null;
        }
    }
}
