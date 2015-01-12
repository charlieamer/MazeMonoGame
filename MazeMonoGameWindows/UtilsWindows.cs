using System;
using System.Windows.Forms;
using System.Windows;

namespace SpaceMaze
{
	public partial class Utils
	{
		public static String OpenFile(String [] types = null) {
			OpenFileDialog fd = new OpenFileDialog ();
			string[] winTypes = new string[types.Length];
			for (int i = 0; i < types.Length; i++) {
				winTypes [i] = types[i].ToUpper() + " files (*." + types[i] + ")|*." + types [i];
			}
			fd.Title = "Open file";
			if (types != null)
				fd.Filter = String.Join ("|", winTypes);
			if (fd.ShowDialog () == DialogResult.OK) {
				return fd.FileName;
			}
			return null;
		}
	}
}

