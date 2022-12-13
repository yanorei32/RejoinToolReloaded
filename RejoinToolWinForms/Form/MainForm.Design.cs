using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace RejoinToolWinForms.Form;

partial class MainForm : System.Windows.Forms.Form {
	void initializeComponent() {
		const int
			imgW	= 320,
			imgH	= 84,
			margin	= 12,
			padding	= 6;

		int curW = 0, curH = 0;
		var execAsm = Assembly.GetExecutingAssembly();

		/*\
		|*| Contextmenu Initialization
		\*/
		components				= new Container();
		instanceIdContextMenu	= new ContextMenuStrip(components);
		copyLaunchInstanceLink	= new ToolStripMenuItem();
		copyInstanceLink		= new ToolStripMenuItem();
		saveLaunchInstanceLink	= new ToolStripMenuItem();
		saveInstanceLink		= new ToolStripMenuItem();
		editInstance			= new ToolStripMenuItem();

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

		editInstance.Text			= "Edit Instance (&E)";
		editInstance.Click			+= new EventHandler(editInstanceClick);
		editInstance.ShortcutKeys	= Keys.Control | Keys.E;
		editInstance.Enabled		= false;

		instanceIdContextMenu.Items.Add(copyLaunchInstanceLink);
		instanceIdContextMenu.Items.Add(copyInstanceLink);
		instanceIdContextMenu.Items.Add(saveLaunchInstanceLink);
		instanceIdContextMenu.Items.Add(saveInstanceLink);
		instanceIdContextMenu.Items.Add(editInstance);

		instanceIdContextMenu.ResumeLayout(false);

		/*\
		|*| UI Initialization
		\*/
		logo		= new PictureBox();
		prev		= new Button();
		next		= new Button();
		launchVrc	= new Button();
		detail		= new Button();
		ownerDetail	= new Button();
		datetime	= new Label();
		instance	= new Label();
		permission	= new Label();
		worldname	= new Label();

		SuspendLayout();
		curH = padding;
		curW = margin;

		/*\
		|*| Logo column
		\*/
		logo.Location			= new Point(curH, curW);
		logo.Size				= new Size(imgW, imgH);
		logo.BackgroundImage	= new Bitmap(
			execAsm.GetManifestResourceStream("logo")
		);

		curH += logo.Size.Height;
		curH += padding;

		/*\
		|*| Prev/Next button column
		\*/
		prev.Text			= "< Newer (&N)";
		prev.Size			= new Size(75, 23);
		prev.Location		= new Point(curW, curH);
		prev.Click			+= new EventHandler(prevButtonClick);
		prev.UseMnemonic	= true;


		curW += prev.Size.Width;
		curW += padding;

		next.Text			= "(&O) Older >";
		next.Size			= new Size(75, 23);
		next.Location		= new Point(curW, curH);
		next.Click			+= new EventHandler(nextButtonClick);
		next.UseMnemonic	= true;

		curW = margin;
		curH += next.Size.Height;
		curH += padding;

		/*\
		|*| World column
		\*/
		worldname.Text		= "Abdefg";
		worldname.AutoSize	= false;
		worldname.Location	= new Point(curW, curH);
		worldname.Size		= new Size(imgW, 25);
		worldname.Font		= new Font("Consolas", 16F);

		curH += worldname.Size.Height;
		curH += padding;

		/*\
		|*| Permission column
		\*/
		permission.Text		= "Invite";
		permission.AutoSize	= false;
		permission.Location	= new Point(curW, curH);
		permission.Size		= new Size(imgW, 20);
		permission.Font		= new Font("Consolas", 14F);

		curH += permission.Size.Height;
		curH += padding;

		/*\
		|*| Joined date time column
		\*/
		datetime.Text		= "  0000/00/00 00:00:00";
		datetime.AutoSize	= false;
		datetime.Location	= new Point(curW, curH);
		datetime.Size		= new Size(imgW, 20);
		datetime.Font		= new Font("Consolas", 14F);

		curH += datetime.Size.Height;
		curH += padding * 2;

		/*\
		|*| Instance column
		\*/
		instance.Text		= "wrld_xxx";
		instance.AutoSize	= false;
		instance.Location	= new Point(curW, curH);
		instance.Size		= new Size(imgW, 75);
		instance.Font		= new Font("Consolas", 9F);

		curH += instance.Size.Height;
		curH += padding;

		/*\
		|*| Launch button column
		\*/
		launchVrc.Text			= "Launch (&L)";
		launchVrc.Location		= new Point(curW, curH);
		launchVrc.Size			= new Size(75, 23);
		launchVrc.Click			+= new EventHandler(launchVrcButtonClick);
		launchVrc.UseMnemonic	= true;

		curW += launchVrc.Size.Width;
		curW += padding;

		detail.Text			= "Detail (&D)";
		detail.Location		= new Point(curW, curH);
		detail.Size			= new Size(75, 23);
		detail.Click		+= new EventHandler(detailButtonClick);
		detail.UseMnemonic	= true;

		curW += detail.Size.Width;
		curW += padding;

		ownerDetail.Text		= "Owner (&O)";
		ownerDetail.Location	= new Point(curW, curH);
		ownerDetail.Size		= new Size(75, 23);
		ownerDetail.Click		+= new EventHandler(ownerDetailButtonClick);
		ownerDetail.UseMnemonic	= true;

		curW = margin;
		curH += launchVrc.Size.Height;
		curH += padding;

		/*\
		|*| Form
		\*/
		var version = FileVersionInfo.GetVersionInfo(execAsm.Location);
		Text				= $"VRChat RejoinTool {version.ProductVersion}";
		ClientSize			= new Size(imgW + (margin * 2), curH);
		MinimumSize			= Size;
		MaximumSize			= Size;
		FormBorderStyle		= FormBorderStyle.FixedSingle;
		Icon				= Icon.ExtractAssociatedIcon(execAsm.Location);
		ContextMenuStrip	= instanceIdContextMenu;
		Controls.Add(logo);
		Controls.Add(launchVrc);
		Controls.Add(detail);
		Controls.Add(ownerDetail);
		Controls.Add(prev);
		Controls.Add(next);
		Controls.Add(datetime);
		Controls.Add(instance);
		Controls.Add(permission);
		Controls.Add(worldname);
		ResumeLayout(false);
	}
}
