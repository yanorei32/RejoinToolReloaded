using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RejoinToolLib.Model;
using RejoinToolLib.Validator;

namespace RejoinToolWinForms.Form;

partial class EditInstanceForm : System.Windows.Forms.Form {
	// UI Elements
	ComboBox?	permission,
				region;

	TextBox?	worldId,
				customRegion,
				instanceName,
				ownerId,
				nonce;

	Label?		worldIdLabel,
				regionLabel,
				permissionLabel,
				instanceNameLabel,
				ownerIdLabel,
				nonceLabel,
				instanceId,
				instanceIdLabel;

	Button?		launchVrc,
				detail,
				userDetail;

	// ContextMenu
	IContainer?			components;
	ContextMenuStrip?	instanceIdContextMenu;
	ToolStripMenuItem?	copyLaunchInstanceLink,
						copyInstanceLink,
						saveLaunchInstanceLink,
						saveInstanceLink;
	
	InstanceInformation i;

	protected override void OnLoad(EventArgs e) {
		base.OnLoad(e);
		// this.permission.SelectedItem = this.instance.Permission;
		// this.region.SelectedItem = this.instance.Region;
	}

	void updateInstanceId() {
		// this.instanceId.Text = instance.Id;
	}

	void updateTextBox() {
		if (worldIdLabel == null) return;
		if (worldId == null) return;
		if (permissionLabel == null) return;
		if (permission == null) return;
		if (regionLabel == null) return;
		if (ownerId == null) return;
		if (instanceNameLabel == null) return;
		if (instanceName == null) return;
		if (ownerIdLabel == null) return;
		if (nonceLabel == null) return;

		var v = Validator.ValidateInstanceInformation(i);
		MessageBox.Show(i.Permission.ToString());
		worldIdLabel.Text = "World ID";
		worldIdLabel.ForeColor = Color.Black;
		worldId.Text = i.WorldId;
		permissionLabel.Text = "Permission";
		permissionLabel.ForeColor = Color.Black;
		permission.SelectedItem = i.Permission;
		instanceNameLabel.Text = "InstanceName";
		instanceNameLabel.ForeColor = Color.Black;
		instanceName.Text = i.Name;
		ownerIdLabel.Text = "Owner ID";
		ownerIdLabel.ForeColor = Color.Black;
		ownerId.Text = i.OwnerId?.Id;
	}

	void updateRegion() {
		// customRegion.Enabled = region.Enabled && (instance.Region == ServerRegion.Custom);
	}

	void updatePermission() {
		// nonce.Enabled
		// 	= instanceName.Enabled
		// 	= ownerId.Enabled
		// 	= region.Enabled
		// 	= instance.Permission != Permission.Unknown;
        //
		// ownerId.Enabled &= instance.Permission != Permission.Public;
		// nonce.Enabled &= instance.Permission != Permission.Public;
	}

	void permissionChanged(object sender, EventArgs e) {
		// instance.Permission = (Permission) permission.SelectedItem;
	}

	void regionChanged(object sender, EventArgs e) {
	}

	void textBoxChanged(object sender, EventArgs e) {
		// i.WorldId = worldId.Text;
		// instance.InstanceName = instanceName.Text;
		// instance.CustomRegion = customRegion.Text;
		// instance.OwnerId = ownerId.Text == "" ? null : ownerId.Text;
		// instance.Nonce = nonce.Text == "" ? null : nonce.Text;

	}

	void nonceLabelDoubleClick(object sender, EventArgs e) {
		if (nonce == null) return;

		if (!nonce.Enabled)
			return;

		nonce.Text = Guid.NewGuid().ToString();
	}

	void launchVrcButtonClick(object sender, EventArgs e) {
		// VRChat.Launch(instance, this.killVRC);
	}

	void detailButtonClick(object sender, EventArgs e) {
		// showDetail(instance);
	}

	void userDetailButtonClick(object sender, EventArgs e) {
		// showUserDetail(instance);
	}

	void copyLaunchInstanceLinkClick(object sender, EventArgs e) {
		// copyLaunchInstanceLinkToClipboard(instance);
		updateTextBox();
	}

	void windowKeyDown(object sender, KeyEventArgs e) {
		if ((e.KeyData & e.KeyCode) == Keys.Escape)
			this.Close();
	}

	void openContextMenu(object sender, EventArgs e) {
		// dirty Ctrl+C override avoidance (1/2)
		if (ActiveControl.GetType().Name == "TextBox") {
			TextBox t = (TextBox) ActiveControl;
			t.DeselectAll();
		}
	}

	void copyInstanceLinkClick(object sender, EventArgs e) {
		// dirty Ctrl+C override avoidance (2/2)
		if (ActiveControl.GetType().Name == "TextBox") {
			TextBox t = (TextBox) ActiveControl;

			if (t.SelectionLength != 0) {
				Clipboard.SetText(t.SelectedText);
				return;
			}
		}

		// copyInstanceLinkToClipboard(instance);
	}

	void saveLaunchInstanceLinkClick(object sender, EventArgs e) {
		// saveInstanceToShortcutGUI(instance);
	}

	void saveInstanceLinkClick(object sender, EventArgs e) {
		// saveInstanceToShortcutGUI(instance, true);
	}

	public EditInstanceForm(InstanceInformation i) {
		this.i = i;

		initializeComponent();
		updateTextBox();
	}
}
