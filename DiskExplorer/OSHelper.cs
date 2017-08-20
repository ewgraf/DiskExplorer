using System.Windows.Forms;

namespace DiskExplorer
{
	public static class OSHelper
	{
		public static string SelectFolder()
		{
			using (var fbd = new FolderBrowserDialog())
			{
				DialogResult result = fbd.ShowDialog();
				if (result == DialogResult.OK) {
					return fbd.SelectedPath;
				}
			}
			return null;
		}
	}
}
