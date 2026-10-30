using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace key_luxery1
{
	/// <summary>Tiny key=value file reader/writer used for settings and themes.</summary>
	public static class IniStore
	{
		private const string NL = "<NL>";

		public static string Folder
		{
			get
			{
				string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeyLuxery");
				if (!Directory.Exists(p)) Directory.CreateDirectory(p);
				return p;
			}
		}

		public static Dictionary<string, string> Read(string path)
		{
			var d = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			if (!File.Exists(path)) return d;
			foreach (string raw in File.ReadAllLines(path))
			{
				string line = raw.Trim();
				if (line.Length == 0 || line.StartsWith(";")) continue;
				int i = line.IndexOf('=');
				if (i <= 0) continue;
				d[line.Substring(0, i).Trim()] = line.Substring(i + 1).Trim().Replace(NL, Environment.NewLine);
			}
			return d;
		}

		public static void Write(string path, Dictionary<string, string> d)
		{
			File.WriteAllLines(path, d.Select(kv => kv.Key + "=" + (kv.Value ?? "").Replace("\r\n", NL).Replace("\n", NL)).ToArray());
		}

		public static string Get(Dictionary<string, string> d, string key, string def)
		{
			string v;
			return d.TryGetValue(key, out v) ? v : def;
		}
	}

	/// <summary>General (non-visual) application settings, saved to %APPDATA%\KeyLuxery\settings.ini</summary>
	public class AppSettings
	{
		public string DbServer = "";
		public string Currency = "USD ($)";
		public bool AutoBackup = true;
		public string AutoBackupFolder = @"C:\Key_Luxury_Backups";
		public int KeepBackups = 7;
		public DateTime LastAutoBackup = DateTime.MinValue;

		// Receipt / invoice details (use these in your invoice print code)
		public string CompanyName = "";
		public string CompanyPhone = "";
		public string CompanyAddress = "";
		public string ReceiptFooter = "";

		private static AppSettings _current;
		public static AppSettings Current
		{
			get { if (_current == null) _current = Load(); return _current; }
		}

		private static string FilePath { get { return Path.Combine(IniStore.Folder, "settings.ini"); } }

		public static AppSettings Load()
		{
			var s = new AppSettings();
			try
			{
				var d = IniStore.Read(FilePath);
				s.DbServer = IniStore.Get(d, "DbServer", s.DbServer);
				s.Currency = IniStore.Get(d, "Currency", s.Currency);
				bool b; int n; DateTime dt;
				if (bool.TryParse(IniStore.Get(d, "AutoBackup", ""), out b)) s.AutoBackup = b;
				s.AutoBackupFolder = IniStore.Get(d, "AutoBackupFolder", s.AutoBackupFolder);
				if (int.TryParse(IniStore.Get(d, "KeepBackups", ""), out n) && n > 0) s.KeepBackups = n;
				if (DateTime.TryParse(IniStore.Get(d, "LastAutoBackup", ""), CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) s.LastAutoBackup = dt;
				s.CompanyName = IniStore.Get(d, "CompanyName", "");
				s.CompanyPhone = IniStore.Get(d, "CompanyPhone", "");
				s.CompanyAddress = IniStore.Get(d, "CompanyAddress", "");
				s.ReceiptFooter = IniStore.Get(d, "ReceiptFooter", "");
			}
			catch { }
			return s;
		}

		public void Save()
		{
			var d = new Dictionary<string, string>();
			d["DbServer"] = DbServer;
			d["Currency"] = Currency;
			d["AutoBackup"] = AutoBackup.ToString();
			d["AutoBackupFolder"] = AutoBackupFolder;
			d["KeepBackups"] = KeepBackups.ToString();
			d["LastAutoBackup"] = LastAutoBackup.ToString("o", CultureInfo.InvariantCulture);
			d["CompanyName"] = CompanyName;
			d["CompanyPhone"] = CompanyPhone;
			d["CompanyAddress"] = CompanyAddress;
			d["ReceiptFooter"] = ReceiptFooter;
			IniStore.Write(FilePath, d);
		}
	}

	/// <summary>
	/// Automatic daily backup. Call AutoBackup.RunIfDue() once when the main form loads.
	/// (The manual Backup/Restore buttons in uSettings are untouched.)
	/// </summary>
	public static class AutoBackup
	{
		private static readonly object _lock = new object();
		private static bool _running;

		public static void RunIfDue()
		{
			var s = AppSettings.Current;
			if (!s.AutoBackup || s.LastAutoBackup.Date == DateTime.Today) return;
			Task.Run(() => { string msg; RunNow(out msg); });
		}

		public static bool RunNow(out string message)
		{
			lock (_lock)
			{
				if (_running) { message = "A backup is already running."; return false; }
				_running = true;
			}

			try
			{
				var s = AppSettings.Current;
				string folder = string.IsNullOrWhiteSpace(s.AutoBackupFolder) ? @"C:\Key_Luxury_Backups" : s.AutoBackupFolder;
				if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

				string file = Path.Combine(folder, "Key_Luxury_Auto_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak");
				string query = "BACKUP DATABASE [Key_luxery] TO DISK = '" + file.Replace("'", "''") + "' WITH FORMAT, INIT, NAME = 'Key_Luxury-Auto Backup'";

				using (SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlConnectionString))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.CommandTimeout = 300;
						cmd.ExecuteNonQuery();
					}
				}

				s.LastAutoBackup = DateTime.Now;
				s.Save();
				Prune(folder, s.KeepBackups);

				message = "Backup created successfully!\nPath: " + file;
				return true;
			}
			catch (Exception ex)
			{
				message = "Automatic backup failed: " + ex.Message + "\n\nNote: Ensure the SQL Server service account has write permission to the backup folder.";
				return false;
			}
			finally
			{
				lock (_lock) { _running = false; }
			}
		}

		private static void Prune(string folder, int keep)
		{
			try
			{
				if (keep < 1) keep = 1;
				var old = Directory.GetFiles(folder, "Key_Luxury_Auto_*.bak")
					.OrderByDescending(f => File.GetCreationTime(f))
					.Skip(keep);
				foreach (string f in old)
				{
					try { File.Delete(f); } catch { }
				}
			}
			catch { }
		}
	}
}