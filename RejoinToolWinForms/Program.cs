using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using RejoinToolLib.Model;
using RejoinToolLib.Parser;
using RejoinToolWinForms.Form;

namespace RejoinToolWinForms;

class Program {
	static byte[] ReadAllBytes(string path) {
		using (
			FileStream fs = new FileStream(
				path,
				FileMode.Open,
				FileAccess.Read,
				FileShare.ReadWrite
			)
		) {
			var buffer = new byte[fs.Length];

			fs.Read(buffer, 0, (int) fs.Length);

			return buffer;
		}
	}

	[STAThread]
	public static void Main(string[] args) {
		var events = new List<JoinEvent>();

		var logFileDir = Path.Combine(
			Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData
			),
			"../LocalLow/VRChat/VRChat"
		);

		if (!Directory.Exists(logFileDir)) {
			MessageBox.Show("Log file directory is not exists");
			return;
		}

		foreach (var p in Directory.GetFiles(logFileDir, "output_log_*.txt"))
			LogParser.GetJoinEvents(events, ReadAllBytes(p));

		if (!events.Any()) {
			MessageBox.Show("Failed to lookup some join logs.");
			return;
		}

		events = events.OrderByDescending(e => e.TimeStamp).ToList();

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(
			new MainForm(events)
		);
	}
}
