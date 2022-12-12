using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RejoinToolWinForms.Form;

static class Utility {
	public static void CreateShortcut(string filename, string uri) {
		var d = new SaveFileDialog();

		d.FileName = $"{filename}.lnk";
		d.Title = "Save Instance";
		d.Filter = "Link (*.lnk)|*.lnk|All files (*.*)|*.*";

		if (d.ShowDialog() != DialogResult.OK) return;

		dynamic shell = Activator.CreateInstance(
			Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"))
		);
		var f = shell.CreateShortcut(d.FileName);
		f.IconLocation = $"{Application.ExecutablePath},0";
		f.TargetPath = uri;
		f.Save();
		Marshal.FinalReleaseComObject(f);
		Marshal.FinalReleaseComObject(shell);
	}

	public static void OpenURI(string uri) {
		Process.Start(new ProcessStartInfo {
			FileName = uri,
			UseShellExecute = true,
		});
	}
}
