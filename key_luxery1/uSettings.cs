using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace key_luxery1
{
	public partial class uSettings : UserControl
	{
		private sharedClass db = new sharedClass();

		// ---- theme editing state ----
		private AppTheme _edit;
		private bool _loading;
		private bool _uiBuilt;

		private class ColorSlot
		{
			public string Caption;
			public Func<AppTheme, Color> Get;
			public Action<AppTheme, Color> Set;
			public Button Swatch;
		}
		private readonly List<ColorSlot> _slots = new List<ColorSlot>();

		// ---- controls created in code ----
		private TextBox txtBgImage;
		private ComboBox cmbBgLayout, cmbFont;
		private NumericUpDown numFontScale, numKeepBackups;
		private CheckBox chkTransparent, chkRecolorButtons, chkFlatButtons, chkStripedGrid, chkKeepColored;
		private Panel previewHost;
		private TextBox txtAutoFolder, txtCompanyName, txtCompanyPhone, txtCompanyAddress, txtReceiptFooter;
		private Button btnBrowseAutoFolder;
		private Label lblAutoStatus;

		public uSettings()
		{
			InitializeComponent();
		}

		// =====================================================================
		//  LOAD
		// =====================================================================
		private void uSettings_Load(object sender, EventArgs e)
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

			try
			{
				_loading = true;
				ThemeManager.Initialize();
				_edit = ThemeManager.Current.Clone();

				if (!_uiBuilt)
				{
					BuildGeneralExtras();
					BuildAppearanceTab();
					BuildBackupExtras();
					_uiBuilt = true;
				}

				LoadGeneralSettings();
				SyncAppearanceUi();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				_loading = false;
			}
			RefreshPreview();
		}

		private void LoadGeneralSettings()
		{
			var s = AppSettings.Current;

			try
			{
				var b = new SqlConnectionStringBuilder(Properties.Settings.Default.SqlConnectionString);
				txtDbServer.Text = b.DataSource;
			}
			catch
			{
				if (!string.IsNullOrEmpty(s.DbServer)) txtDbServer.Text = s.DbServer;
			}

			if (!string.IsNullOrEmpty(s.Currency)) cmbCurrency.Text = s.Currency;
			chkAutoBackup.Checked = s.AutoBackup;
			txtAutoFolder.Text = s.AutoBackupFolder;
			numKeepBackups.Value = Math.Max(numKeepBackups.Minimum, Math.Min(numKeepBackups.Maximum, s.KeepBackups));
			txtCompanyName.Text = s.CompanyName;
			txtCompanyPhone.Text = s.CompanyPhone;
			txtCompanyAddress.Text = s.CompanyAddress;
			txtReceiptFooter.Text = s.ReceiptFooter;
			UpdateAutoControls();
		}

		// =====================================================================
		//  SAVE
		// =====================================================================
		private void btnSaveSettings_Click(object sender, EventArgs e)
		{
			try
			{
				if (chkAutoBackup.Checked && string.IsNullOrWhiteSpace(txtAutoFolder.Text))
				{
					MessageBox.Show("Please choose a folder for the automatic backups.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				string note;
				if (!ApplyDbServer(out note)) return;      // user cancelled

				var s = AppSettings.Current;
				s.DbServer = txtDbServer.Text.Trim();
				s.Currency = cmbCurrency.Text.Trim();
				s.AutoBackup = chkAutoBackup.Checked;
				s.AutoBackupFolder = txtAutoFolder.Text.Trim();
				s.KeepBackups = (int)numKeepBackups.Value;
				s.CompanyName = txtCompanyName.Text.Trim();
				s.CompanyPhone = txtCompanyPhone.Text.Trim();
				s.CompanyAddress = txtCompanyAddress.Text.Trim();
				s.ReceiptFooter = txtReceiptFooter.Text.Trim();
				s.Save();

				ThemeManager.SetTheme(_edit, true);       // saves + restyles the whole application
				RefreshPreview();

				string msg = "System preferences and database configuration updated successfully!";
				if (!string.IsNullOrEmpty(note)) msg += "\n\n" + note;
				MessageBox.Show(msg, "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error saving settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>Returns false if the user cancelled. 'note' carries an optional extra message.</summary>
		private bool ApplyDbServer(out string note)
		{
			note = null;
			string server = txtDbServer.Text.Trim();
			if (server.Length == 0) return true;

			var b = new SqlConnectionStringBuilder(Properties.Settings.Default.SqlConnectionString);
			if (string.Equals(b.DataSource, server, StringComparison.OrdinalIgnoreCase)) return true;

			string err;
			if (!TestServer(server, out err))
			{
				if (MessageBox.Show("Cannot connect to '" + server + "'.\n\n" + err + "\n\nSave this server anyway?",
					"Connection Test Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
					return false;
			}

			b.DataSource = server;
			try
			{
				Properties.Settings.Default["SqlConnectionString"] = b.ConnectionString;
				Properties.Settings.Default.Save();
				note = "The new SQL Server source will be used the next time the application starts.";
			}
			catch
			{
				note = "The server name was saved, but the connection string setting is read-only (Application scope). " +
					   "Change the SqlConnectionString setting's Scope to 'User' in Project > Properties > Settings to apply it.";
			}
			return true;
		}

		private bool TestServer(string server, out string error)
		{
			error = null;
			try
			{
				var b = new SqlConnectionStringBuilder(Properties.Settings.Default.SqlConnectionString);
				b.DataSource = server;
				b.ConnectTimeout = 5;
				using (SqlConnection con = new SqlConnection(b.ConnectionString))
				{
					con.Open();
				}
				return true;
			}
			catch (Exception ex)
			{
				error = ex.Message;
				return false;
			}
		}

		private void btnTestConnection_Click(object sender, EventArgs e)
		{
			string server = txtDbServer.Text.Trim();
			if (server.Length == 0) { MessageBox.Show("Enter a SQL Server name first.", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

			Cursor = Cursors.WaitCursor;
			string err;
			bool ok = TestServer(server, out err);
			Cursor = Cursors.Default;

			if (ok) MessageBox.Show("Connection to '" + server + "' succeeded.", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else MessageBox.Show("Connection failed:\n" + err, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		// =====================================================================
		//  BACKUP / RESTORE  (unchanged)
		// =====================================================================
		private void btnBackupDb_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "Backup Files (*.bak)|*.bak";
				sfd.FileName = "Key_Luxury_Backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					string filePath = sfd.FileName;

					// SQL Server backup query (must be run against master or target DB)
					string query = $"BACKUP DATABASE [Key_luxery] TO DISK = '{filePath}' WITH FORMAT, INIT, NAME = 'Key_Luxury-Full Database Backup'";

					using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlConnectionString))
					{
						con.Open();
						using (SqlCommand cmd = new SqlCommand(query, con))
						{
							cmd.ExecuteNonQuery();
						}
					}

					MessageBox.Show("Database backup created successfully!\nPath: " + filePath, "Backup Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Backup failed: " + ex.Message + "\n\nNote: Ensure SQL Server service account has write permission to the selected folder.", "Database Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnRestoreDb_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Restoring a database will overwrite current active data with the backup snapshot. Do you wish to continue?", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					OpenFileDialog ofd = new OpenFileDialog();
					ofd.Filter = "Backup Files (*.bak)|*.bak";
					ofd.Title = "Select Database Backup File (.bak)";

					if (ofd.ShowDialog() == DialogResult.OK)
					{
						string filePath = ofd.FileName;

						// Switch to master database to drop/restore connections on Key_luxery
						SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Properties.Settings.Default.SqlConnectionString);
						builder.InitialCatalog = "master";

						using (SqlConnection con = new SqlConnection(builder.ConnectionString))
						{
							con.Open();

							// Set database to single user mode to kill active connections
							string prepQuery = "ALTER DATABASE [Key_luxery] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
							using (SqlCommand cmdPrep = new SqlCommand(prepQuery, con))
							{
								cmdPrep.ExecuteNonQuery();
							}

							// Restore database command
							string restoreQuery = $"RESTORE DATABASE [Key_luxery] FROM DISK = '{filePath}' WITH REPLACE";
							using (SqlCommand cmdRestore = new SqlCommand(restoreQuery, con))
							{
								cmdRestore.ExecuteNonQuery();
							}

							// Revert database back to multi user mode
							string postQuery = "ALTER DATABASE [Key_luxery] SET MULTI_USER";
							using (SqlCommand cmdPost = new SqlCommand(postQuery, con))
							{
								cmdPost.ExecuteNonQuery();
							}
						}

						MessageBox.Show("Database restored successfully from backup!", "Restore Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Restore failed: " + ex.Message, "Database Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// =====================================================================
		//  UI BUILDERS (controls are created in code to keep the designer small)
		// =====================================================================
		private Label AddLabel(Control parent, string text, int x, int y, bool heading = false)
		{
			var l = new Label();
			l.AutoSize = true;
			l.Text = text;
			l.Location = new System.Drawing.Point(x, y);
			l.Font = heading ? new Font("Segoe UI", 12F, FontStyle.Bold) : new Font("Segoe UI", 9.5F);
			parent.Controls.Add(l);
			return l;
		}

		private TextBox AddText(Control parent, int x, int y, int w)
		{
			var t = new TextBox();
			t.Font = new Font("Segoe UI", 9.5F);
			t.Location = new System.Drawing.Point(x, y);
			t.Size = new Size(w, 24);
			parent.Controls.Add(t);
			return t;
		}

		private Button AddButton(Control parent, string text, int x, int y, int w, int h, EventHandler click)
		{
			var b = new Button();
			b.Text = text;
			b.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			b.FlatStyle = FlatStyle.Flat;
			b.FlatAppearance.BorderSize = 0;
			b.Location = new System.Drawing.Point(x, y);
			b.Size = new Size(w, h);
			b.Click += click;
			parent.Controls.Add(b);
			return b;
		}

		private CheckBox AddCheck(Control parent, string text, int x, int y, EventHandler changed)
		{
			var c = new CheckBox();
			c.AutoSize = true;
			c.Text = text;
			c.Font = new Font("Segoe UI", 9.5F);
			c.Location = new System.Drawing.Point(x, y);
			c.CheckedChanged += changed;
			parent.Controls.Add(c);
			return c;
		}

		// ---------- General tab extras ----------
		private void BuildGeneralExtras()
		{
			Control p = tabGeneral;

			chkAutoBackup.CheckedChanged += (s, e) => UpdateAutoControls();

			AddLabel(p, "Backup Folder :", 20, 248);
			txtAutoFolder = AddText(p, 180, 245, 300);
			btnBrowseAutoFolder = AddButton(p, "Browse...", 490, 243, 90, 28, btnBrowseAutoFolder_Click);

			AddLabel(p, "Keep Last :", 20, 292);
			numKeepBackups = new NumericUpDown();
			numKeepBackups.Font = new Font("Segoe UI", 9.5F);
			numKeepBackups.Minimum = 1;
			numKeepBackups.Maximum = 365;
			numKeepBackups.Value = 7;
			numKeepBackups.Location = new System.Drawing.Point(180, 289);
			numKeepBackups.Size = new Size(70, 24);
			p.Controls.Add(numKeepBackups);
			AddLabel(p, "automatic backups", 258, 292);

			lblAutoStatus = AddLabel(p, "", 20, 335);

			// right column : company / receipt details
			AddLabel(p, "Company / Receipt Details", 660, 20, true);
			AddLabel(p, "Company Name :", 660, 68);
			txtCompanyName = AddText(p, 790, 65, 250);
			AddLabel(p, "Phone :", 660, 113);
			txtCompanyPhone = AddText(p, 790, 110, 250);
			AddLabel(p, "Address :", 660, 158);
			txtCompanyAddress = AddText(p, 790, 155, 250);
			AddLabel(p, "Receipt Footer :", 660, 203);
			txtReceiptFooter = AddText(p, 790, 200, 250);
			txtReceiptFooter.Multiline = true;
			txtReceiptFooter.Height = 70;
		}

		private void UpdateAutoControls()
		{
			bool on = chkAutoBackup.Checked;
			if (txtAutoFolder != null) txtAutoFolder.Enabled = on;
			if (btnBrowseAutoFolder != null) btnBrowseAutoFolder.Enabled = on;
			if (numKeepBackups != null) numKeepBackups.Enabled = on;

			if (lblAutoStatus != null)
			{
				DateTime last = AppSettings.Current.LastAutoBackup;
				lblAutoStatus.Text = last == DateTime.MinValue
					? "Last automatic backup: never"
					: "Last automatic backup: " + last.ToString("yyyy-MM-dd HH:mm");
			}
		}

		private void btnBrowseAutoFolder_Click(object sender, EventArgs e)
		{
			using (var fbd = new FolderBrowserDialog())
			{
				fbd.Description = "Select the folder for automatic database backups";
				if (Directory.Exists(txtAutoFolder.Text)) fbd.SelectedPath = txtAutoFolder.Text;
				if (fbd.ShowDialog() == DialogResult.OK) txtAutoFolder.Text = fbd.SelectedPath;
			}
		}

		// ---------- Backup tab extras ----------
		private void BuildBackupExtras()
		{
			Control p = tabBackup;
			AddLabel(p, "Automatic backups are configured on the General tab. You can also run one right now:", 20, 130);

			var run = AddButton(p, "Run Auto-Backup Now", 20, 165, 200, 36, btnRunAutoBackup_Click);
			var open = AddButton(p, "Open Backup Folder", 235, 165, 200, 36, btnOpenBackupFolder_Click);
		}

		private void btnRunAutoBackup_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtAutoFolder.Text))
			{
				MessageBox.Show("Choose a backup folder on the General tab first.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// use what is currently typed, without forcing the user to press Save first
			AppSettings.Current.AutoBackupFolder = txtAutoFolder.Text.Trim();
			AppSettings.Current.KeepBackups = (int)numKeepBackups.Value;

			Cursor = Cursors.WaitCursor;
			string msg;
			bool ok = AutoBackup.RunNow(out msg);
			Cursor = Cursors.Default;

			UpdateAutoControls();
			MessageBox.Show(msg, ok ? "Backup Success" : "Database Backup Error", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
		}

		private void btnOpenBackupFolder_Click(object sender, EventArgs e)
		{
			string f = txtAutoFolder.Text.Trim();
			if (Directory.Exists(f)) Process.Start("explorer.exe", f);
			else MessageBox.Show("That folder does not exist yet. It is created when the first backup runs.", "Backup Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		// ---------- Appearance tab ----------
		private void BuildAppearanceTab()
		{
			Control p = tabAppearance;

			// presets
			cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbTheme.Items.Clear();
			foreach (var pr in AppTheme.Presets) cmbTheme.Items.Add(pr.Name);
			cmbTheme.Items.Add("Custom");
			cmbTheme.SelectedIndexChanged += cmbTheme_SelectedIndexChanged;

			// colors
			AddLabel(p, "Theme Colors", 20, 62, true);
			_slots.Add(new ColorSlot { Caption = "Window Background", Get = t => t.Background, Set = (t, c) => t.Background = c });
			_slots.Add(new ColorSlot { Caption = "Cards / Panels", Get = t => t.Surface, Set = (t, c) => t.Surface = c });
			_slots.Add(new ColorSlot { Caption = "Sidebar / Header / Menu", Get = t => t.Sidebar, Set = (t, c) => t.Sidebar = c });
			_slots.Add(new ColorSlot { Caption = "Sidebar Text", Get = t => t.SidebarText, Set = (t, c) => t.SidebarText = c });
			_slots.Add(new ColorSlot { Caption = "Main Text", Get = t => t.Text, Set = (t, c) => t.Text = c });
			_slots.Add(new ColorSlot { Caption = "Buttons / Highlights", Get = t => t.Accent, Set = (t, c) => t.Accent = c });
			_slots.Add(new ColorSlot { Caption = "Button Text", Get = t => t.AccentText, Set = (t, c) => t.AccentText = c });
			_slots.Add(new ColorSlot { Caption = "Input Fields", Get = t => t.InputBack, Set = (t, c) => t.InputBack = c });

			int y = 95;
			foreach (var slot in _slots)
			{
				AddLabel(p, slot.Caption + " :", 20, y + 5);
				var sw = new Button();
				sw.Tag = "keep";                       // the theme engine must not recolor the swatches
				sw.FlatStyle = FlatStyle.Flat;
				sw.FlatAppearance.BorderColor = Color.Gray;
				sw.UseVisualStyleBackColor = false;
				sw.Font = new Font("Consolas", 9.5F, FontStyle.Bold);
				sw.Location = new System.Drawing.Point(220, y);
				sw.Size = new Size(120, 30);
				var captured = slot;
				sw.Click += (s, e) => PickColor(captured);
				slot.Swatch = sw;
				p.Controls.Add(sw);
				y += 40;
			}

			// background image
			int bx = 380;
			AddLabel(p, "Background Image", bx, 62, true);
			txtBgImage = AddText(p, bx, 100, 230);
			txtBgImage.ReadOnly = true;
			AddButton(p, "Browse...", bx + 240, 98, 80, 28, btnBrowseBg_Click);
			AddButton(p, "Clear", bx + 325, 98, 60, 28, btnClearBg_Click);

			AddLabel(p, "Layout :", bx, 142);
			cmbBgLayout = new ComboBox();
			cmbBgLayout.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbBgLayout.Font = new Font("Segoe UI", 9.5F);
			cmbBgLayout.Items.AddRange(new object[] { "Stretch", "Zoom", "Tile", "Center" });
			cmbBgLayout.Location = new System.Drawing.Point(bx + 70, 139);
			cmbBgLayout.Size = new Size(150, 25);
			cmbBgLayout.SelectedIndexChanged += (s, e) =>
			{
				if (_loading) return;
				ImageLayout l;
				if (Enum.TryParse<ImageLayout>(cmbBgLayout.Text, out l)) _edit.BackgroundLayout = l;
				RefreshPreview();
			};
			p.Controls.Add(cmbBgLayout);

			chkTransparent = AddCheck(p, "Let the image show through panels", bx, 177, (s, e) => { if (_loading) return; _edit.TransparentPanels = chkTransparent.Checked; RefreshPreview(); });

			// typography
			AddLabel(p, "Typography", bx, 222, true);
			AddLabel(p, "Font :", bx, 262);
			cmbFont = new ComboBox();
			cmbFont.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbFont.Font = new Font("Segoe UI", 9.5F);
			cmbFont.Items.Add("(Original fonts)");
			foreach (string fn in FontFamily.Families.Select(f => f.Name).OrderBy(n => n)) cmbFont.Items.Add(fn);
			cmbFont.Location = new System.Drawing.Point(bx + 70, 259);
			cmbFont.Size = new Size(250, 25);
			cmbFont.SelectedIndexChanged += (s, e) =>
			{
				if (_loading) return;
				_edit.FontName = cmbFont.SelectedIndex <= 0 ? "" : cmbFont.Text;
				RefreshPreview();
			};
			p.Controls.Add(cmbFont);

			AddLabel(p, "Text Size :", bx, 302);
			numFontScale = new NumericUpDown();
			numFontScale.Font = new Font("Segoe UI", 9.5F);
			numFontScale.Minimum = 80;
			numFontScale.Maximum = 130;
			numFontScale.Increment = 5;
			numFontScale.Value = 100;
			numFontScale.Location = new System.Drawing.Point(bx + 70, 299);
			numFontScale.Size = new Size(70, 24);
			numFontScale.ValueChanged += (s, e) => { if (_loading) return; _edit.FontScale = (float)numFontScale.Value / 100f; RefreshPreview(); };
			p.Controls.Add(numFontScale);
			AddLabel(p, "%", bx + 145, 302);

			// style options
			AddLabel(p, "Style Options", bx, 345, true);
			chkRecolorButtons = AddCheck(p, "Recolor buttons with the accent color", bx, 380, (s, e) => { if (_loading) return; _edit.RecolorButtons = chkRecolorButtons.Checked; RefreshPreview(); });
			chkFlatButtons = AddCheck(p, "Flat modern button style", bx, 405, (s, e) => { if (_loading) return; _edit.FlatButtons = chkFlatButtons.Checked; RefreshPreview(); });
			chkStripedGrid = AddCheck(p, "Striped rows in tables", bx, 430, (s, e) => { if (_loading) return; _edit.StripedGrid = chkStripedGrid.Checked; RefreshPreview(); });
			chkKeepColored = AddCheck(p, "Keep custom-colored panels (dashboard tiles)", bx, 455, (s, e) => { if (_loading) return; _edit.PreserveColoredPanels = chkKeepColored.Checked; RefreshPreview(); });

			// preview + actions
			int px = 760;
			AddLabel(p, "Live Preview", px, 62, true);
			BuildPreview(p, px, 98);

			var apply = AddButton(p, "Apply to Entire App", px, 325, 270, 36, btnApplyTheme_Click);
			apply.Tag = "keep";
			apply.BackColor = Color.FromArgb(46, 204, 113);
			apply.ForeColor = Color.White;
			apply.UseVisualStyleBackColor = false;

			var imp = AddButton(p, "Import Theme", px, 372, 130, 30, btnImportTheme_Click);
			var exp = AddButton(p, "Export Theme", px + 140, 372, 130, 30, btnExportTheme_Click);
			var rst = AddButton(p, "Reset to Default", px, 410, 270, 30, btnResetTheme_Click);
		}

		private void BuildPreview(Control parent, int x, int y)
		{
			previewHost = new Panel();
			previewHost.Tag = "notheme";            // real theme engine skips it; we theme it manually
			previewHost.Location = new System.Drawing.Point(x, y);
			previewHost.Size = new Size(270, 215);
			previewHost.BorderStyle = BorderStyle.FixedSingle;

			var header = new Panel { Tag = "sidebar", Location = new System.Drawing.Point(0, 0), Size = new Size(268, 34) };
			header.Controls.Add(new Label { Text = "Key Luxery", AutoSize = true, Location = new System.Drawing.Point(10, 8), Font = new Font("Segoe UI", 10F, FontStyle.Bold) });

			var side = new Panel { Tag = "sidebar", Location = new System.Drawing.Point(0, 34), Size = new Size(70, 179) };
			side.Controls.Add(new Label { Text = "Menu", AutoSize = true, Location = new System.Drawing.Point(10, 10), Font = new Font("Segoe UI", 9F) });
			side.Controls.Add(new Button { Text = "Sales", Location = new System.Drawing.Point(6, 38), Size = new Size(58, 26), Font = new Font("Segoe UI", 8.5F) });

			var card = new Panel { Tag = "card", Location = new System.Drawing.Point(82, 48), Size = new Size(176, 152) };
			card.Controls.Add(new Label { Text = "Invoice Total", AutoSize = true, Location = new System.Drawing.Point(10, 10), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
			card.Controls.Add(new TextBox { Text = "1,250.00", Location = new System.Drawing.Point(10, 36), Size = new Size(150, 24), Font = new Font("Segoe UI", 9.5F) });
			card.Controls.Add(new Button { Text = "Save", Location = new System.Drawing.Point(10, 72), Size = new Size(72, 28), Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
			card.Controls.Add(new CheckBox { Text = "Paid", AutoSize = true, Location = new System.Drawing.Point(10, 112), Font = new Font("Segoe UI", 9.5F) });

			previewHost.Controls.Add(header);
			previewHost.Controls.Add(side);
			previewHost.Controls.Add(card);
			parent.Controls.Add(previewHost);
		}

		private void RefreshPreview()
		{
			if (previewHost == null || _edit == null) return;
			ThemeManager.ApplyPreview(previewHost, _edit);
		}

		// push the _edit values into the appearance controls
		private void SyncAppearanceUi()
		{
			bool prev = _loading;
			_loading = true;
			try
			{
				foreach (var s in _slots)
				{
					Color c = s.Get(_edit);
					s.Swatch.BackColor = c;
					s.Swatch.Text = AppTheme.Hex(c);
					s.Swatch.ForeColor = ThemeManager.Contrast(c);
				}

				int idx = cmbTheme.Items.IndexOf(_edit.Name);
				cmbTheme.SelectedIndex = idx >= 0 ? idx : cmbTheme.Items.Count - 1;

				txtBgImage.Text = _edit.BackgroundImage;
				int li = cmbBgLayout.Items.IndexOf(_edit.BackgroundLayout.ToString());
				cmbBgLayout.SelectedIndex = li >= 0 ? li : 0;
				chkTransparent.Checked = _edit.TransparentPanels;

				int fi = string.IsNullOrEmpty(_edit.FontName) ? 0 : cmbFont.Items.IndexOf(_edit.FontName);
				cmbFont.SelectedIndex = fi >= 0 ? fi : 0;
				decimal pct = (decimal)Math.Round(_edit.FontScale * 100f);
				numFontScale.Value = Math.Max(numFontScale.Minimum, Math.Min(numFontScale.Maximum, pct));

				chkRecolorButtons.Checked = _edit.RecolorButtons;
				chkFlatButtons.Checked = _edit.FlatButtons;
				chkStripedGrid.Checked = _edit.StripedGrid;
				chkKeepColored.Checked = _edit.PreserveColoredPanels;
			}
			finally { _loading = prev; }
		}

		// ---------- Appearance handlers ----------
		private void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_loading || _edit == null) return;
			string name = cmbTheme.Text;
			var preset = AppTheme.Presets.FirstOrDefault(x => x.Name == name);
			if (preset != null)
			{
				_edit.CopyColorsFrom(preset);
				_edit.Name = preset.Name;
			}
			else
			{
				_edit.Name = "Custom";
			}
			SyncAppearanceUi();
			RefreshPreview();
		}

		private void PickColor(ColorSlot slot)
		{
			using (var dlg = new ColorDialog())
			{
				dlg.AnyColor = true;
				dlg.FullOpen = true;
				dlg.Color = slot.Get(_edit);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					slot.Set(_edit, dlg.Color);
					_edit.Name = "Custom";
					SyncAppearanceUi();
					RefreshPreview();
				}
			}
		}

		private void btnBrowseBg_Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog())
			{
				ofd.Title = "Select a background image";
				ofd.Filter = "Images (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					_edit.BackgroundImage = ofd.FileName;
					_edit.TransparentPanels = true;     // otherwise the image would be hidden behind the panels
					SyncAppearanceUi();
					RefreshPreview();
				}
			}
		}

		private void btnClearBg_Click(object sender, EventArgs e)
		{
			_edit.BackgroundImage = "";
			_edit.TransparentPanels = false;
			SyncAppearanceUi();
			RefreshPreview();
		}

		private void btnApplyTheme_Click(object sender, EventArgs e)
		{
			ThemeManager.SetTheme(_edit, true);
			RefreshPreview();
		}

		private void btnResetTheme_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Reset all appearance settings to the default theme?", "Reset Theme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
			_edit = AppTheme.Presets[0].Clone();
			SyncAppearanceUi();
			RefreshPreview();
			ThemeManager.SetTheme(_edit, true);
		}

		private void btnExportTheme_Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
			{
				sfd.Filter = "Theme files (*.theme)|*.theme";
				sfd.FileName = "KeyLuxery_" + (_edit.Name ?? "Custom").Replace(" ", "_") + ".theme";
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					try { IniStore.Write(sfd.FileName, _edit.ToDictionary()); }
					catch (Exception ex) { MessageBox.Show("Could not export theme: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				}
			}
		}

		private void btnImportTheme_Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog())
			{
				ofd.Filter = "Theme files (*.theme)|*.theme";
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					try
					{
						_edit = AppTheme.FromDictionary(IniStore.Read(ofd.FileName));
						SyncAppearanceUi();
						RefreshPreview();
					}
					catch (Exception ex) { MessageBox.Show("Could not import theme: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				}
			}
		}
	}
}