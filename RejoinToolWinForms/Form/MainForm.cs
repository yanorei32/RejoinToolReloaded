using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using RejoinToolLib.Model;

namespace RejoinToolWinForms.Form;

partial class MainForm : System.Windows.Forms.Form {
	PictureBox?	logo;

	Button?	launchVrc,
			detail,
			next,
			prev,
			userDetail;

	Label?	datetime,
			instance,
			worldname,
			permission;

	IContainer?			components;
	ContextMenuStrip?	instanceIdContextMenu;
	ToolStripMenuItem?	copyLaunchInstanceLink,
						copyInstanceLink,
						saveInstanceLink,
						saveLaunchInstanceLink,
						editInstance;

	List<JoinEvent>	events;
	int index = 0;

	void editInstanceClick(object sender, EventArgs e) {
		(new EditInstanceForm(events[index].InstanceInformation)).Show();
	}

	void saveInstanceLinkClick(object sender, EventArgs e) {
		Utility.CreateShortcut(
			$"web_{events[index].WorldName}_{events[index].InstanceInformation.Permission}",
			events[index].Token.ToWebSiteURI()
		);
	}

	void copyInstanceLinkClick(object sender, EventArgs e) {
		Clipboard.SetText(events[index].Token.ToWebSiteURI());
	}

	void saveLaunchInstanceLinkClick(object sender, EventArgs e) {
		Utility.CreateShortcut(
			$"launch_{events[index].WorldName}_{events[index].InstanceInformation.Permission}",
			events[index].Token.ToLaunchURI()
		);
	}

	void copyLaunchInstanceLinkClick(object sender, EventArgs e) {
		Clipboard.SetText(events[index].Token.ToLaunchURI());
	}

	void detailButtonClick(object sender, EventArgs e) {
		Utility.OpenURI(
			events[index].Token.ToWebSiteURI()
		);
	}

	void userDetailButtonClick(object sender, EventArgs e) {
		Utility.OpenURI(
			events[index].InstanceInformation.OwnerId?.ToWebSiteURI()!
		);
	}

	void launchVrcButtonClick(object sender, EventArgs e) {
		Utility.OpenURI(
			events[index].Token.ToLaunchURI()
		);

		Application.Exit();
	}

	void prevButtonClick(object sender, EventArgs e) {
		seek(-1);
	}

	void nextButtonClick(object sender, EventArgs e) {
		seek(1);
	}

	void seek(int inc) {
		if (prev == null) return;
		if (next == null) return;
		if (worldname == null) return;
		if (permission == null) return;
		if (instance == null) return;
		if (datetime == null) return;
		if (userDetail == null) return;

		SuspendLayout();

		index += inc;
		prev.Enabled = 0 < index;
		next.Enabled = index < events.Count - 1;

		JoinEvent e = events[index];
		var i = e.InstanceInformation;

		worldname.Text = e.WorldName == null ? "(!) Failed to Joining" : e.WorldName;
		permission.Text = $"  {i.Permission}:{i.ActualServer}:{i.Name}";
		instance.Text = e.Token.RawToken;
		datetime.Text = e.TimeStamp.ToString("  yyyy/MM/dd HH:mm:ss");
		userDetail.Enabled = i.OwnerId != null;

		ResumeLayout(false);
	}

	public MainForm(List<JoinEvent> events) {
		this.events = events;
		initializeComponent();
		seek(0);
	}
}
