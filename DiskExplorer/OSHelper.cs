using Microsoft.WindowsAPICodePack.Dialogs;

namespace DiskExplorer
{
	public static class OSHelper
	{
		public static string SelectFolder()
		{
            using (var ofd = new CommonOpenFileDialog()
            {
                Multiselect = false,
                RestoreDirectory = true,
                IsFolderPicker = true,
                ShowHiddenItems = true
            }) {
                if (ofd.ShowDialog() == CommonFileDialogResult.Ok) {
                    return ofd.FileName;
                }
            }
			return null;
		}
	}
}
