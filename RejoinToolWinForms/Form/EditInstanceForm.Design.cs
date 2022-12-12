using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using RejoinToolLib.Model;

namespace RejoinToolWinForms.Form;

partial class EditInstanceForm : System.Windows.Forms.Form {
	void initializeComponent() {
		const int
			textBoxW = 320,
			margin	= 12,
			padding	= 6;

		int curW = 0, curH = 0;
		Assembly execAsm = Assembly.GetExecutingAssembly();

		/*\
		|*| Contextmenu Initialization
		\*/
		components				= new Container();
		instanceIdContextMenu	= new ContextMenuStrip(components);
		copyLaunchInstanceLink	= new ToolStripMenuItem();
		copyInstanceLink		= new ToolStripMenuItem();
		saveLaunchInstanceLink	= new ToolStripMenuItem();
		saveInstanceLink		= new ToolStripMenuItem();

		copyInstanceLink.Text			= "Copy Instance (https://) Link (&C)";
		copyInstanceLink.Click			+= new EventHandler(copyInstanceLinkClick);
		copyInstanceLink.ShortcutKeys	= Keys.Control | Keys.C;

		copyLaunchInstanceLink.Text 		= "Copy Instance (vrchat://) Link (&V)";
		copyLaunchInstanceLink.Click		+= new EventHandler(copyLaunchInstanceLinkClick);
		copyLaunchInstanceLink.ShortcutKeys	= Keys.Control | Keys.Shift | Keys.C;

		saveLaunchInstanceLink.Text			= "Save Instance (vrchat://) Shortcut (&S)";
		saveLaunchInstanceLink.Click		+= new EventHandler(saveLaunchInstanceLinkClick);
		saveLaunchInstanceLink.ShortcutKeys	= Keys.Control | Keys.S;

		saveInstanceLink.Text			= "Save Instance (https://) Shortcut (&S)";
		saveInstanceLink.Click			+= new EventHandler(saveInstanceLinkClick);
		saveInstanceLink.ShortcutKeys	= Keys.Control | Keys.Shift | Keys.S;

		instanceIdContextMenu.Opened	+= new EventHandler(openContextMenu);
		instanceIdContextMenu.Items.Add(copyLaunchInstanceLink);
		instanceIdContextMenu.Items.Add(copyInstanceLink);
		instanceIdContextMenu.Items.Add(saveLaunchInstanceLink);
		instanceIdContextMenu.Items.Add(saveInstanceLink);

		/*\
		|*| UI Initialization
		\*/
		worldIdLabel		= new Label();
		worldId				= new TextBox();
		permissionLabel		= new Label();
		permission			= new ComboBox();
		regionLabel			= new Label();
		region				= new ComboBox();
		customRegion		= new TextBox();
		instanceName		= new TextBox();
		instanceNameLabel	= new Label();
		nonce				= new TextBox();
		nonceLabel			= new Label();
		ownerId				= new TextBox();
		ownerIdLabel		= new Label();
		instanceIdLabel		= new Label();
		instanceId			= new Label();
		launchVrc			= new Button();
		detail				= new Button();
		userDetail			= new Button();

		SuspendLayout();
		curH = padding;
		curW = margin;

		/*\
		|*| World ID
		\*/
		worldIdLabel.Text		= "World ID";
		worldIdLabel.AutoSize	= false;
		worldIdLabel.Location	= new Point(curW, curH);
		worldIdLabel.Size		= new Size(textBoxW, 18);
		worldIdLabel.Font		= new Font("Consolas", 12F);

		curH += worldIdLabel.Size.Height;
		curH += padding;

		// worldId.Text			= instance.WorldId;
		worldId.Size			= new Size(textBoxW, 20);
		worldId.Font			= new Font("Consolas", 9F);
		worldId.Location		= new Point(curW, curH);
		worldId.TextChanged	+= new EventHandler(textBoxChanged);

		curH += worldId.Size.Height;
		curH += padding;

		/*\
		|*| Permission
		\*/
		permissionLabel.Text		= "Permission";
		permissionLabel.AutoSize	= false;
		permissionLabel.Location	= new Point(curW, curH);
		permissionLabel.Size		= new Size(textBoxW, 18);
		permissionLabel.Font		= new Font("Consolas", 12F);

		curH += permissionLabel.Size.Height;
		curH += padding;

		permission.DataSource				= Enum.GetValues(typeof(Permission));
		permission.DropDownStyle			= ComboBoxStyle.DropDownList;
		permission.Size					= new Size(textBoxW, 20);
		permission.Font					= new Font("Consolas", 9F);
		permission.Location				= new Point(curW, curH);
		permission.SelectedIndexChanged	+= new EventHandler(permissionChanged);

		curH += permission.Size.Height;
		curH += padding;

		/*\
		|*| Region
		\*/
		regionLabel.Text		= "Region";
		regionLabel.AutoSize	= false;
		regionLabel.Location	= new Point(curW, curH);
		regionLabel.Size		= new Size(textBoxW, 18);
		regionLabel.Font		= new Font("Consolas", 12F);

		curH += regionLabel.Size.Height;
		curH += padding;

		region.DataSource				= Enum.GetValues(typeof(VRChatServer));
		region.DropDownStyle			= ComboBoxStyle.DropDownList;
		region.Size					= new Size(textBoxW, 20);
		region.Font					= new Font("Consolas", 9F);
		region.Location				= new Point(curW, curH);
		region.SelectedIndexChanged	+= new EventHandler(regionChanged);

		curH += region.Size.Height;
		curH += padding;

		// customRegion.Text			= instance.CustomRegion;
		customRegion.Size			= new Size(textBoxW, 20);
		customRegion.Font			= new Font("Consolas", 9F);
		customRegion.Location		= new Point(curW, curH);
		customRegion.TextChanged	+= new EventHandler(textBoxChanged);

		curH += customRegion.Size.Height;
		curH += padding;

		/*\
		|*| InstanceName
		\*/
		instanceNameLabel.Text		= "Instance Name (invalid)";
		instanceNameLabel.AutoSize	= false;
		instanceNameLabel.Location	= new Point(curW, curH);
		instanceNameLabel.Size		= new Size(textBoxW, 18);
		instanceNameLabel.Font		= new Font("Consolas", 12F);

		curH += instanceNameLabel.Size.Height;
		curH += padding;

		// instanceName.Text			= instance.InstanceName;
		instanceName.Size			= new Size(textBoxW, 20);
		instanceName.Font			= new Font("Consolas", 9F);
		instanceName.Location		= new Point(curW, curH);
		instanceName.TextChanged	+= new EventHandler(textBoxChanged);

		curH += instanceName.Size.Height;
		curH += padding;

		/*\
		|*| Owner ID
		\*/
		ownerIdLabel.Text		= "Owner ID (invalid)";
		ownerIdLabel.AutoSize	= false;
		ownerIdLabel.Location	= new Point(curW, curH);
		ownerIdLabel.Size		= new Size(textBoxW, 18);
		ownerIdLabel.Font		= new Font("Consolas", 12F);

		curH += ownerIdLabel.Size.Height;
		curH += padding;

		// ownerId.Text			= instance.OwnerId;
		ownerId.Size			= new Size(textBoxW, 20);
		ownerId.Font			= new Font("Consolas", 9F);
		ownerId.Location		= new Point(curW, curH);
		ownerId.TextChanged	+= new EventHandler(textBoxChanged);

		curH += ownerId.Size.Height;
		curH += padding;

		/*\
		|*| Nonce
		\*/
		nonceLabel.Text		= "Nonce (invalid)";
		nonceLabel.AutoSize	= false;
		nonceLabel.Location	= new Point(curW, curH);
		nonceLabel.Size		= new Size(textBoxW, 18);
		nonceLabel.Font		= new Font("Consolas", 12F);
		nonceLabel.DoubleClick	+= new EventHandler(nonceLabelDoubleClick);

		curH += nonceLabel.Size.Height;
		curH += padding;

		// nonce.Text			= instance.Nonce;
		nonce.Size			= new Size(textBoxW, 20);
		nonce.Font			= new Font("Consolas", 9F);
		nonce.Location		= new Point(curW, curH);
		nonce.TextChanged	+= new EventHandler(textBoxChanged);

		curH += nonce.Size.Height;
		curH += padding;

		/*\
		|*| Instance Id
		\*/
		instanceIdLabel.Text		= "Instance ID";
		instanceIdLabel.AutoSize	= false;
		instanceIdLabel.Location	= new Point(curW, curH);
		instanceIdLabel.Size		= new Size(textBoxW, 18);
		instanceIdLabel.Font		= new Font("Consolas", 12F);
		
		curH += instanceIdLabel.Size.Height;
		curH += padding;

		instanceId.Text		= "wrld_xxx:12345~public";
		instanceId.AutoSize	= false;
		instanceId.Location	= new Point(curW, curH);
		instanceId.Size		= new Size(textBoxW, 75);
		instanceId.Font		= new Font("Consolas", 9F);

		curH += instanceId.Size.Height;
		curH += padding;

		/*\
		|*| Buttons
		\*/
		launchVrc.Text			= "Launch (&L)";
		launchVrc.Location		= new Point(curW, curH);
		launchVrc.Size			= new Size(75, 23);
		launchVrc.Click		+= new EventHandler(launchVrcButtonClick);
		launchVrc.UseMnemonic	= true;

		curW += launchVrc.Size.Width;
		curW += padding;

		detail.Text		= "Detail (&D)";
		detail.Location	= new Point(curW, curH);
		detail.Size		= new Size(75, 23);
		detail.Click		+= new EventHandler(detailButtonClick);
		detail.UseMnemonic	= true;

		curW += detail.Size.Width;
		curW += padding;

		userDetail.Text		= "User (&U)";
		userDetail.Location	= new Point(curW, curH);
		userDetail.Size		= new Size(75, 23);
		userDetail.Click		+= new EventHandler(userDetailButtonClick);
		userDetail.UseMnemonic	= true;

		curW = margin;
		curH += launchVrc.Size.Height;
		curH += padding;

		/*\
		|*| Form
		\*/
		Text				= "Edit Instance - VRChat RejoinTool";

		ClientSize			= new Size(textBoxW + (margin * 2), curH);

		MinimumSize		= Size;
		MaximumSize		= Size;
		FormBorderStyle	= FormBorderStyle.FixedSingle;
		Icon				= Icon.ExtractAssociatedIcon(execAsm.Location);
		ContextMenuStrip	= instanceIdContextMenu;
		KeyDown			+= new KeyEventHandler(windowKeyDown);
		KeyPreview			= true;

		Controls.Add(worldIdLabel);
		Controls.Add(worldId);
		Controls.Add(permissionLabel);
		Controls.Add(permission);
		Controls.Add(regionLabel);
		Controls.Add(region);
		Controls.Add(customRegion);
		Controls.Add(instanceNameLabel);
		Controls.Add(instanceName);
		Controls.Add(ownerIdLabel);
		Controls.Add(ownerId);
		Controls.Add(nonceLabel);
		Controls.Add(nonce);
		Controls.Add(instanceId);
		Controls.Add(instanceIdLabel);
		Controls.Add(instanceId);
		Controls.Add(launchVrc);
		Controls.Add(detail);
		Controls.Add(userDetail);

		ResumeLayout(false);
	}
}

