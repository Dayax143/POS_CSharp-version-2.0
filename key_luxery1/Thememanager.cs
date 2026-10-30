using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace key_luxery1
{
	// =====================================================================
	//  AppTheme : all the visual settings of the application
	// =====================================================================
	public class AppTheme
	{
		public string Name = "Deep Slate Blue (Default)";

		public Color Background = Color.FromArgb(245, 246, 250);   // window background
		public Color Surface = Color.White;                         // cards / panels
		public Color Sidebar = Color.FromArgb(44, 62, 80);          // sidebar, header, menu, table header
		public Color SidebarText = Color.White;
		public Color Text = Color.FromArgb(44, 62, 80);
		public Color Accent = Color.FromArgb(52, 152, 219);         // buttons, selections, links
		public Color AccentText = Color.White;
		public Color InputBack = Color.White;                       // text boxes, combo boxes, lists

		public string FontName = "";          // "" = keep every control's own font family
		public float FontScale = 1.0f;        // 0.8 - 1.3
		public string BackgroundImage = "";
		public ImageLayout BackgroundLayout = ImageLayout.Stretch;
		public bool TransparentPanels = false;
		public bool RecolorButtons = true;
		public bool FlatButtons = true;
		public bool StripedGrid = true;
		public bool PreserveColoredPanels = true;

		public AppTheme Clone() { return (AppTheme)MemberwiseClone(); }

		public void CopyColorsFrom(AppTheme o)
		{
			Background = o.Background; Surface = o.Surface; Sidebar = o.Sidebar; SidebarText = o.SidebarText;
			Text = o.Text; Accent = o.Accent; AccentText = o.AccentText; InputBack = o.InputBack;
		}

		// ---------- built-in presets ----------
		public static readonly List<AppTheme> Presets = BuildPresets();

		private static AppTheme Make(string name, Color bg, Color surface, Color side, Color sideText, Color text, Color accent, Color accentText, Color input)
		{
			return new AppTheme
			{
				Name = name,
				Background = bg,
				Surface = surface,
				Sidebar = side,
				SidebarText = sideText,
				Text = text,
				Accent = accent,
				AccentText = accentText,
				InputBack = input
			};
		}

		private static List<AppTheme> BuildPresets()
		{
			Func<int, int, int, Color> c = (r, g, b) => Color.FromArgb(r, g, b);
			return new List<AppTheme>
			{
				Make("Deep Slate Blue (Default)", c(245,246,250), Color.White, c(44,62,80), Color.White, c(44,62,80), c(52,152,219), Color.White, Color.White),
				Make("Charcoal Dark", c(24,26,30), c(36,39,45), c(16,17,20), c(230,230,230), c(232,234,237), c(86,156,214), Color.White, c(48,52,60)),
				Make("Modern Minimalist Light", c(250,250,250), Color.White, c(235,237,240), c(40,40,40), c(33,33,33), c(60,60,60), Color.White, c(245,245,245)),
				Make("Black & Gold (Key Luxury)", c(18,18,18), c(30,30,30), c(0,0,0), c(212,175,55), c(240,230,200), c(212,175,55), c(20,20,20), c(45,45,45)),
				Make("Ocean Blue", c(232,244,250), Color.White, c(13,71,111), Color.White, c(20,50,70), c(0,150,199), Color.White, Color.White),
				Make("Emerald Green", c(238,248,243), Color.White, c(22,84,62), Color.White, c(25,60,45), c(39,174,96), Color.White, Color.White),
				Make("Royal Purple", c(246,242,251), Color.White, c(74,35,115), Color.White, c(50,30,75), c(142,68,173), Color.White, Color.White),
				Make("Sunset Orange", c(253,246,240), Color.White, c(120,53,15), Color.White, c(70,35,15), c(230,126,34), Color.White, Color.White)
			};
		}

		// ---------- (de)serialisation ----------
		public static string Hex(Color c) { return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2"); }

		private static Color ParseColor(Dictionary<string, string> d, string key, Color def)
		{
			string v;
			if (!d.TryGetValue(key, out v)) return def;
			try { return ColorTranslator.FromHtml(v); } catch { return def; }
		}

		private static bool ParseBool(Dictionary<string, string> d, string key, bool def)
		{
			string v; bool b;
			return (d.TryGetValue(key, out v) && bool.TryParse(v, out b)) ? b : def;
		}

		public Dictionary<string, string> ToDictionary()
		{
			var d = new Dictionary<string, string>();
			d["Name"] = Name;
			d["Background"] = Hex(Background); d["Surface"] = Hex(Surface); d["Sidebar"] = Hex(Sidebar);
			d["SidebarText"] = Hex(SidebarText); d["Text"] = Hex(Text); d["Accent"] = Hex(Accent);
			d["AccentText"] = Hex(AccentText); d["InputBack"] = Hex(InputBack);
			d["FontName"] = FontName ?? "";
			d["FontScale"] = FontScale.ToString(CultureInfo.InvariantCulture);
			d["BackgroundImage"] = BackgroundImage ?? "";
			d["BackgroundLayout"] = BackgroundLayout.ToString();
			d["TransparentPanels"] = TransparentPanels.ToString();
			d["RecolorButtons"] = RecolorButtons.ToString();
			d["FlatButtons"] = FlatButtons.ToString();
			d["StripedGrid"] = StripedGrid.ToString();
			d["PreserveColoredPanels"] = PreserveColoredPanels.ToString();
			return d;
		}

		public static AppTheme FromDictionary(Dictionary<string, string> d)
		{
			var t = Presets[0].Clone();
			t.Name = IniStore.Get(d, "Name", "Custom");
			t.Background = ParseColor(d, "Background", t.Background);
			t.Surface = ParseColor(d, "Surface", t.Surface);
			t.Sidebar = ParseColor(d, "Sidebar", t.Sidebar);
			t.SidebarText = ParseColor(d, "SidebarText", t.SidebarText);
			t.Text = ParseColor(d, "Text", t.Text);
			t.Accent = ParseColor(d, "Accent", t.Accent);
			t.AccentText = ParseColor(d, "AccentText", t.AccentText);
			t.InputBack = ParseColor(d, "InputBack", t.InputBack);
			t.FontName = IniStore.Get(d, "FontName", "");
			float f;
			if (float.TryParse(IniStore.Get(d, "FontScale", "1"), NumberStyles.Float, CultureInfo.InvariantCulture, out f))
				t.FontScale = Math.Max(0.8f, Math.Min(1.3f, f));
			t.BackgroundImage = IniStore.Get(d, "BackgroundImage", "");
			ImageLayout l;
			if (Enum.TryParse<ImageLayout>(IniStore.Get(d, "BackgroundLayout", "Stretch"), out l)) t.BackgroundLayout = l;
			t.TransparentPanels = ParseBool(d, "TransparentPanels", false);
			t.RecolorButtons = ParseBool(d, "RecolorButtons", true);
			t.FlatButtons = ParseBool(d, "FlatButtons", true);
			t.StripedGrid = ParseBool(d, "StripedGrid", true);
			t.PreserveColoredPanels = ParseBool(d, "PreserveColoredPanels", true);
			return t;
		}
	}

	// =====================================================================
	//  ThemeManager : applies an AppTheme to every form / control
	//
	//  Control.Tag hints (optional):
	//    "sidebar" or "header"  -> panel is painted with the Sidebar colors
	//    "card"                 -> panel is painted with the Surface color
	//    "background"           -> panel is painted with the Background color
	//    "keep"                 -> button/label keeps its own colors
	//    "notheme"              -> control and its children are never touched
	// =====================================================================
	public static class ThemeManager
	{
		private enum Role { Background, Surface, Sidebar, Colored }
		private enum Ctx { Surface, Sidebar, Kept }

		private class Orig
		{
			public Color Back, Fore;
			public Font Font;
			public FlatStyle Flat;
			public Image Img;
			public ImageLayout Layout;
			public Role Role;
			public bool Unset;
			public Ctx ChildCtx = Ctx.Surface;
		}

		private static readonly ConditionalWeakTable<Control, Orig> Origs = new ConditionalWeakTable<Control, Orig>();
		private static readonly ConditionalWeakTable<Form, object> ThemedForms = new ConditionalWeakTable<Form, object>();
		private static readonly Dictionary<string, Font> FontCache = new Dictionary<string, Font>();
		private static bool _initialized;

		public static AppTheme Current { get; private set; }
		public static event EventHandler ThemeChanged;

		private static string ThemeFile { get { return Path.Combine(IniStore.Folder, "theme.ini"); } }

		static ThemeManager()
		{
			Current = LoadFromDisk();
		}

		// ---------- public API ----------

		/// <summary>Call once at startup (e.g. in your main form's constructor/Load).</summary>
		public static void Initialize()
		{
			if (_initialized) return;
			_initialized = true;
			Application.Idle += OnIdle;       // themes every form that opens later, automatically
			ToolStripManager.Renderer = new ToolStripProfessionalRenderer(new ThemeColorTable(Current));
			ApplyToAll();
		}

		public static void SetTheme(AppTheme theme, bool save)
		{
			Current = theme.Clone();
			if (save) SaveToDisk();
			ToolStripManager.Renderer = new ToolStripProfessionalRenderer(new ThemeColorTable(Current));
			ApplyToAll();
			EventHandler h = ThemeChanged;
			if (h != null) h(null, EventArgs.Empty);
		}

		public static void ApplyToAll()
		{
			foreach (Form f in Application.OpenForms.Cast<Form>().ToArray())
			{
				object d;
				if (!ThemedForms.TryGetValue(f, out d)) ThemedForms.Add(f, new object());
				Apply(f);
			}
		}

		/// <summary>Apply the current theme to one form or control tree.</summary>
		public static void Apply(Control root)
		{
			if (root == null) return;
			root.SuspendLayout();
			try { ApplyTree(root, Current, Ctx.Surface, true); }
			finally { root.ResumeLayout(true); }
		}

		/// <summary>Used by the settings screen to preview a theme without touching the real app.</summary>
		public static void ApplyPreview(Control host, AppTheme t)
		{
			Orig o = GetOrig(host);
			host.BackColor = t.Background;
			ApplyBackdrop(host, t, o);
			host.SuspendLayout();
			try { foreach (Control ch in host.Controls) ApplyTree(ch, t, Ctx.Surface, false); }
			finally { host.ResumeLayout(true); }
		}

		public static void SaveToDisk() { IniStore.Write(ThemeFile, Current.ToDictionary()); }

		private static AppTheme LoadFromDisk()
		{
			try
			{
				var d = IniStore.Read(ThemeFile);
				if (d.Count > 0) return AppTheme.FromDictionary(d);
			}
			catch { }
			return AppTheme.Presets[0].Clone();
		}

		// ---------- automatic hooks ----------

		private static void OnIdle(object sender, EventArgs e)
		{
			if (Application.OpenForms.Count == 0) return;
			foreach (Form f in Application.OpenForms.Cast<Form>().ToArray())
			{
				object d;
				if (ThemedForms.TryGetValue(f, out d)) continue;
				ThemedForms.Add(f, new object());
				Apply(f);
			}
		}

		private static void OnControlAdded(object sender, ControlEventArgs e)
		{
			Ctx ctx = Ctx.Surface;
			Control parent = sender as Control;
			Orig po;
			if (parent != null && Origs.TryGetValue(parent, out po)) ctx = po.ChildCtx;
			ApplyTree(e.Control, Current, ctx, true);
		}

		// ---------- tree walking ----------

		private static void ApplyTree(Control c, AppTheme t, Ctx ctx, bool hook)
		{
			string tag = c.Tag as string;
			if (tag != null && tag.Trim().Equals("notheme", StringComparison.OrdinalIgnoreCase)) return;

			Orig o = GetOrig(c);
			if (hook)
			{
				c.ControlAdded -= OnControlAdded;
				c.ControlAdded += OnControlAdded;
			}

			Ctx child = ApplyOne(c, t, o, ctx);
			o.ChildCtx = child;

			foreach (Control ch in c.Controls) ApplyTree(ch, t, child, hook);
		}

		private static Ctx ApplyOne(Control c, AppTheme t, Orig o, Ctx ctx)
		{
			ApplyFont(c, t, o);
			if (c.ContextMenuStrip != null) ApplyToolStrip(c.ContextMenuStrip, t);
			if (ctx == Ctx.Kept) return Ctx.Kept;          // inside a deliberately colored panel

			string tag = (c.Tag as string ?? "").Trim().ToLowerInvariant();
			bool keep = tag == "keep";
			bool sb = ctx == Ctx.Sidebar;
			Color fore = sb ? t.SidebarText : t.Text;
			Color inFore = Contrast(t.InputBack);
			bool img = LoadImage(t) != null;

			// ----- Form -----
			var form = c as Form;
			if (form != null)
			{
				form.BackColor = t.Background;
				form.ForeColor = t.Text;
				ApplyBackdrop(form, t, o);
				return Ctx.Surface;
			}

			// ----- UserControl (pages) -----
			var uc = c as UserControl;
			if (uc != null)
			{
				uc.ForeColor = t.Text;
				if (img && t.TransparentPanels)
				{
					uc.BackColor = Color.Transparent;
					if (uc.BackgroundImage != o.Img) uc.BackgroundImage = o.Img;
				}
				else
				{
					uc.BackColor = t.Background;
					ApplyBackdrop(uc, t, o);
				}
				return Ctx.Surface;
			}

			// ----- Menus / toolbars / status bars -----
			var ts = c as ToolStrip;
			if (ts != null) { ApplyToolStrip(ts, t); return Ctx.Surface; }

			// ----- Grids -----
			var dgv = c as DataGridView;
			if (dgv != null) { ApplyGrid(dgv, t); return Ctx.Surface; }

			// ----- Split containers -----
			var split = c as SplitContainer;
			if (split != null) { split.BackColor = Blend(t.Surface, t.Text, 0.15f); return Ctx.Surface; }

			// ----- Panels, group boxes, tab pages -----
			if (c is Panel || c is GroupBox)
			{
				Role r = o.Role;
				if (r == Role.Surface && sb && o.Unset) r = Role.Sidebar;
				if (r == Role.Colored) r = t.PreserveColoredPanels ? Role.Colored : Role.Sidebar;
				if (r == Role.Colored) return Ctx.Kept;

				var tp = c as TabPage;
				if (tp != null) tp.UseVisualStyleBackColor = false;

				switch (r)
				{
					case Role.Background:
						c.BackColor = t.Background; c.ForeColor = t.Text;
						return Ctx.Surface;
					case Role.Sidebar:
						c.BackColor = t.Sidebar; c.ForeColor = t.SidebarText;
						return Ctx.Sidebar;
					default:
						c.BackColor = (img && t.TransparentPanels && tp == null) ? Color.Transparent : t.Surface;
						c.ForeColor = t.Text;
						return Ctx.Surface;
				}
			}

			// ----- Labels -----
			var lbl = c as Label;
			if (lbl != null)
			{
				if (keep) return ctx;
				if (!IsColorful(o.Back))
				{
					if (!o.Unset) lbl.BackColor = Color.Transparent;
					if (!IsColorful(o.Fore)) lbl.ForeColor = fore;
				}
				var link = c as LinkLabel;
				if (link != null)
				{
					link.LinkColor = t.Accent;
					link.ActiveLinkColor = Blend(t.Accent, Color.Black, 0.25f);
					link.VisitedLinkColor = t.Accent;
				}
				return ctx;
			}

			// ----- Buttons -----
			var btn = c as Button;
			if (btn != null)
			{
				if (keep) return ctx;
				if (sb)
				{
					StyleButton(btn, t.Sidebar, t.SidebarText, Blend(t.Sidebar, t.Accent, 0.45f), true, o);
				}
				else if (t.RecolorButtons)
				{
					StyleButton(btn, t.Accent, t.AccentText, Hover(t.Accent), t.FlatButtons, o);
				}
				return ctx;
			}

			// ----- Check boxes / radio buttons -----
			if (c is CheckBox || c is RadioButton)
			{
				if (!keep && !IsColorful(o.Fore)) c.ForeColor = fore;
				return ctx;
			}

			// ----- Inputs and lists -----
			if (c is TextBoxBase || c is UpDownBase || c is ListBox || c is ListView || c is TreeView || c is ComboBox)
			{
				if (keep) return ctx;
				c.BackColor = t.InputBack;
				c.ForeColor = inFore;
				var cb = c as ComboBox;
				if (cb != null) cb.FlatStyle = FlatStyle.Flat;
				return ctx;
			}

			var dtp = c as DateTimePicker;
			if (dtp != null)
			{
				dtp.CalendarMonthBackground = t.InputBack;
				dtp.CalendarForeColor = inFore;
				dtp.CalendarTitleBackColor = t.Accent;
				dtp.CalendarTitleForeColor = t.AccentText;
				return ctx;
			}

			return ctx;
		}

		private static void StyleButton(Button b, Color back, Color fore, Color hover, bool flat, Orig o)
		{
			b.UseVisualStyleBackColor = false;
			b.BackColor = back;
			b.ForeColor = fore;
			if (flat)
			{
				b.FlatStyle = FlatStyle.Flat;
				b.FlatAppearance.BorderSize = 0;
				b.FlatAppearance.MouseOverBackColor = hover;
				b.FlatAppearance.MouseDownBackColor = Blend(hover, Lum(hover) < 0.5 ? Color.White : Color.Black, 0.15f);
			}
			else
			{
				b.FlatStyle = o.Flat == FlatStyle.Flat ? FlatStyle.Standard : o.Flat;
			}
		}

		private static void ApplyGrid(DataGridView g, AppTheme t)
		{
			g.EnableHeadersVisualStyles = false;
			g.BackgroundColor = t.Background;
			g.GridColor = Blend(t.Surface, t.Text, 0.15f);

			g.DefaultCellStyle.BackColor = t.Surface;
			g.DefaultCellStyle.ForeColor = t.Text;
			g.DefaultCellStyle.SelectionBackColor = t.Accent;
			g.DefaultCellStyle.SelectionForeColor = t.AccentText;

			g.AlternatingRowsDefaultCellStyle.BackColor = t.StripedGrid ? Blend(t.Surface, t.Text, 0.05f) : t.Surface;
			g.AlternatingRowsDefaultCellStyle.ForeColor = t.Text;
			g.AlternatingRowsDefaultCellStyle.SelectionBackColor = t.Accent;
			g.AlternatingRowsDefaultCellStyle.SelectionForeColor = t.AccentText;

			g.ColumnHeadersDefaultCellStyle.BackColor = t.Sidebar;
			g.ColumnHeadersDefaultCellStyle.ForeColor = t.SidebarText;
			g.ColumnHeadersDefaultCellStyle.SelectionBackColor = t.Sidebar;
			g.ColumnHeadersDefaultCellStyle.SelectionForeColor = t.SidebarText;

			g.RowHeadersDefaultCellStyle.BackColor = Blend(t.Surface, t.Text, 0.08f);
			g.RowHeadersDefaultCellStyle.ForeColor = t.Text;
			g.RowHeadersDefaultCellStyle.SelectionBackColor = t.Accent;
			g.RowHeadersDefaultCellStyle.SelectionForeColor = t.AccentText;
		}

		private static void ApplyToolStrip(ToolStrip ts, AppTheme t)
		{
			var renderer = new ToolStripProfessionalRenderer(new ThemeColorTable(t));
			ts.Renderer = renderer;
			ts.BackColor = t.Sidebar;
			ts.ForeColor = t.SidebarText;
			ApplyItems(ts.Items, t, renderer);
		}

		private static void ApplyItems(ToolStripItemCollection items, AppTheme t, ToolStripRenderer renderer)
		{
			foreach (ToolStripItem it in items)
			{
				it.ForeColor = t.SidebarText;

				var tb = it as ToolStripTextBox;
				if (tb != null) { tb.BackColor = t.InputBack; tb.ForeColor = Contrast(t.InputBack); }
				var cmb = it as ToolStripComboBox;
				if (cmb != null) { cmb.BackColor = t.InputBack; cmb.ForeColor = Contrast(t.InputBack); }

				var dd = it as ToolStripDropDownItem;
				if (dd != null && dd.HasDropDownItems)
				{
					dd.DropDown.Renderer = renderer;
					dd.DropDown.BackColor = Blend(t.Sidebar, Lum(t.Sidebar) < 0.5 ? Color.White : Color.Black, 0.06f);
					ApplyItems(dd.DropDownItems, t, renderer);
				}
			}
		}

		// ---------- fonts ----------

		private static void ApplyFont(Control c, AppTheme t, Orig o)
		{
			// Forms / UserControls use AutoScaleMode.Font: changing their font would rescale the layout.
			if (c is ContainerControl) return;

			Font target;
			bool noScale = Math.Abs(t.FontScale - 1f) < 0.01f;
			if (string.IsNullOrEmpty(t.FontName) && noScale)
			{
				target = o.Font;
			}
			else
			{
				string fam = string.IsNullOrEmpty(t.FontName) ? o.Font.FontFamily.Name : t.FontName;
				target = GetFont(fam, o.Font.Size * t.FontScale, o.Font.Style, o.Font.Unit);
			}
			if (!ReferenceEquals(c.Font, target) && !c.Font.Equals(target)) c.Font = target;
		}

		private static Font GetFont(string name, float size, FontStyle style, GraphicsUnit unit)
		{
			string key = name + "|" + size.ToString(CultureInfo.InvariantCulture) + "|" + (int)style + "|" + (int)unit;
			Font f;
			if (!FontCache.TryGetValue(key, out f))
			{
				f = new Font(name, size, style, unit);
				FontCache[key] = f;
			}
			return f;
		}

		// ---------- background image ----------

		private static string _imgPath;
		private static DateTime _imgStamp;
		private static DateTime _imgChecked = DateTime.MinValue;
		private static Image _img;

		private static Image LoadImage(AppTheme t)
		{
			string p = t.BackgroundImage;
			if (string.IsNullOrEmpty(p)) return null;
			try
			{
				if (_img != null && _imgPath == p && (DateTime.UtcNow - _imgChecked).TotalSeconds < 2) return _img;
				_imgChecked = DateTime.UtcNow;
				if (!File.Exists(p)) return null;
				DateTime stamp = File.GetLastWriteTime(p);
				if (_img != null && _imgPath == p && _imgStamp == stamp) return _img;
				using (var fs = new FileStream(p, FileMode.Open, FileAccess.Read, FileShare.Read))
				using (var tmp = Image.FromStream(fs))
				{
					_img = new Bitmap(tmp);
				}
				_imgPath = p;
				_imgStamp = stamp;
				return _img;
			}
			catch { return null; }
		}

		private static void ApplyBackdrop(Control c, AppTheme t, Orig o)
		{
			Image im = LoadImage(t);
			if (im != null)
			{
				c.BackgroundImage = im;
				c.BackgroundImageLayout = t.BackgroundLayout == ImageLayout.None ? ImageLayout.Stretch : t.BackgroundLayout;
			}
			else if (c.BackgroundImage != o.Img)
			{
				c.BackgroundImage = o.Img;
				c.BackgroundImageLayout = o.Layout;
			}
		}

		// ---------- remembering original looks ----------

		private static bool IsExplicit(Control c, string prop)
		{
			try
			{
				PropertyDescriptor p = TypeDescriptor.GetProperties(c)[prop];
				return p != null && p.ShouldSerializeValue(c);
			}
			catch { return false; }
		}

		private static Orig GetOrig(Control c)
		{
			Orig o;
			if (Origs.TryGetValue(c, out o)) return o;

			o = new Orig();
			bool backSet = IsExplicit(c, "BackColor");
			o.Back = backSet ? c.BackColor : Color.Empty;
			o.Fore = IsExplicit(c, "ForeColor") ? c.ForeColor : SystemColors.ControlText;
			o.Unset = !backSet || o.Back.IsEmpty || o.Back.A == 0 || o.Back.IsSystemColor;
			o.Img = c.BackgroundImage;
			o.Layout = c.BackgroundImageLayout;

			Font baseFont = c.Font;
			if (!IsExplicit(c, "Font"))
			{
				for (Control p = c.Parent; p != null; p = p.Parent)
				{
					Orig po;
					if (Origs.TryGetValue(p, out po)) { baseFont = po.Font; break; }
				}
			}
			o.Font = baseFont;

			var bb = c as ButtonBase;
			if (bb != null) o.Flat = bb.FlatStyle;

			o.Role = ComputeRole(c, o);
			Origs.Add(c, o);
			return o;
		}

		private static Role ComputeRole(Control c, Orig o)
		{
			string tag = (c.Tag as string ?? "").Trim().ToLowerInvariant();
			if (tag == "sidebar" || tag == "header") return Role.Sidebar;
			if (tag == "background") return Role.Background;
			if (tag == "surface" || tag == "card") return Role.Surface;
			if (c is TabPage || o.Unset) return Role.Surface;

			double lum = Lum(o.Back);
			if (IsColorful(o.Back))
			{
				bool docked = c.Dock != DockStyle.None && c.Dock != DockStyle.Fill;
				if (lum < 0.35 || docked) return Role.Sidebar;   // bars / headers
				return Role.Colored;                              // colored cards (KPI tiles, alerts)
			}
			if (lum < 0.35) return Role.Sidebar;
			if (lum >= 0.90 && lum < 0.995) return Role.Background;
			return Role.Surface;
		}

		// ---------- color helpers ----------

		public static double Lum(Color c) { return (0.299 * c.R + 0.587 * c.G + 0.114 * c.B) / 255.0; }

		public static Color Contrast(Color bg)
		{
			return Lum(bg) > 0.55 ? Color.FromArgb(30, 30, 30) : Color.FromArgb(240, 240, 240);
		}

		public static Color Blend(Color a, Color b, float r)
		{
			r = Math.Max(0f, Math.Min(1f, r));
			return Color.FromArgb(255,
				(int)(a.R + (b.R - a.R) * r),
				(int)(a.G + (b.G - a.G) * r),
				(int)(a.B + (b.B - a.B) * r));
		}

		private static Color Hover(Color accent)
		{
			return Blend(accent, Lum(accent) < 0.5 ? Color.White : Color.Black, 0.18f);
		}

		private static bool IsColorful(Color c)
		{
			return !c.IsEmpty && c.A > 0 && !c.IsSystemColor && c.GetSaturation() > 0.4f;
		}
	}

	// =====================================================================
	//  Colors used by menus / toolbars / status bars
	// =====================================================================
	internal class ThemeColorTable : ProfessionalColorTable
	{
		private readonly Color bar, drop, hover, pressed, border;

		public ThemeColorTable(AppTheme t)
		{
			UseSystemColors = false;
			bar = t.Sidebar;
			drop = ThemeManager.Blend(t.Sidebar, ThemeManager.Lum(t.Sidebar) < 0.5 ? Color.White : Color.Black, 0.06f);
			hover = ThemeManager.Blend(t.Sidebar, t.Accent, 0.55f);
			pressed = t.Accent;
			border = ThemeManager.Blend(t.Sidebar, t.SidebarText, 0.25f);
		}

		public override Color ToolStripDropDownBackground { get { return drop; } }
		public override Color ImageMarginGradientBegin { get { return drop; } }
		public override Color ImageMarginGradientMiddle { get { return drop; } }
		public override Color ImageMarginGradientEnd { get { return drop; } }
		public override Color MenuBorder { get { return border; } }
		public override Color MenuItemBorder { get { return hover; } }
		public override Color MenuItemSelected { get { return hover; } }
		public override Color MenuItemSelectedGradientBegin { get { return hover; } }
		public override Color MenuItemSelectedGradientEnd { get { return hover; } }
		public override Color MenuItemPressedGradientBegin { get { return pressed; } }
		public override Color MenuItemPressedGradientMiddle { get { return pressed; } }
		public override Color MenuItemPressedGradientEnd { get { return pressed; } }
		public override Color MenuStripGradientBegin { get { return bar; } }
		public override Color MenuStripGradientEnd { get { return bar; } }
		public override Color ToolStripGradientBegin { get { return bar; } }
		public override Color ToolStripGradientMiddle { get { return bar; } }
		public override Color ToolStripGradientEnd { get { return bar; } }
		public override Color ToolStripBorder { get { return bar; } }
		public override Color ToolStripContentPanelGradientBegin { get { return bar; } }
		public override Color ToolStripContentPanelGradientEnd { get { return bar; } }
		public override Color ToolStripPanelGradientBegin { get { return bar; } }
		public override Color ToolStripPanelGradientEnd { get { return bar; } }
		public override Color StatusStripGradientBegin { get { return bar; } }
		public override Color StatusStripGradientEnd { get { return bar; } }
		public override Color SeparatorDark { get { return border; } }
		public override Color SeparatorLight { get { return border; } }
		public override Color GripDark { get { return border; } }
		public override Color GripLight { get { return border; } }
		public override Color ButtonSelectedGradientBegin { get { return hover; } }
		public override Color ButtonSelectedGradientMiddle { get { return hover; } }
		public override Color ButtonSelectedGradientEnd { get { return hover; } }
		public override Color ButtonSelectedHighlight { get { return hover; } }
		public override Color ButtonSelectedBorder { get { return hover; } }
		public override Color ButtonPressedGradientBegin { get { return pressed; } }
		public override Color ButtonPressedGradientMiddle { get { return pressed; } }
		public override Color ButtonPressedGradientEnd { get { return pressed; } }
		public override Color ButtonPressedBorder { get { return pressed; } }
		public override Color OverflowButtonGradientBegin { get { return bar; } }
		public override Color OverflowButtonGradientMiddle { get { return bar; } }
		public override Color OverflowButtonGradientEnd { get { return bar; } }
	}
}