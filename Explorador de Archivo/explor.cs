using Explorador_de_Archivo.Forms;
using Explorador_de_Archivo.Models;
using Explorador_de_Archivo.Services;
using FileExplorer.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace Explorador_de_Archivo
{
    /// <summary>
    /// Punto de entrada principal del explorador.
    /// Coordina los builders de UI y los controladores de dominio.
    /// La implementación está distribuida en archivos parciales:
    ///   ExplorerUI.cs      — construcción de controles
    ///   ExplorerNavigation.cs — navegación y filtros
    ///   ExplorerFileOps.cs — operaciones CRUD sobre archivos
    ///   ExplorerRender.cs  — renderizado de grid/lista
    /// </summary>
    public partial class Form1 : Form
    {
        // ── Servicios ─────────────────────────────────────────────
        internal readonly FileService     Files;
        internal readonly DatabaseService Db;

        // ── Estado compartido ─────────────────────────────────────
        internal string      CurrentPath;
        internal FileItem?   SelectedItem;
        internal bool        IsGridView   = true;
        internal FileKind[]? ActiveFilter;

        internal readonly NavigationHistory History   = new();
        internal readonly FileClipboard     Clipboard = new();

        public Form1()
        {
            Files       = new FileService();
            Db          = new DatabaseService(ResolveDbPath());
            CurrentPath = DefaultHomePath;

            ConfigureWindow();
            BuildUI();
            Navigate(CurrentPath);
        }

        private static string DefaultHomePath =>
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        private static string ResolveDbPath()
        {
            var path = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "FileExplorer", "db.sqlite");
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);
            return path;
        }

        private void ConfigureWindow()
        {
            Theme.ApplyForm(this);
            Text          = "Explorador de Archivos";
            Size          = new System.Drawing.Size(1500, 900);
            MinimumSize   = new System.Drawing.Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState   = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e) { }
    }

    // ── Clases de soporte ─────────────────────────────────────────

    internal class NavigationHistory
    {
        private readonly System.Collections.Generic.List<string> _entries = new();
        private int _index = -1;

        public bool CanGoBack    => _index > 0;
        public bool CanGoForward => _index < _entries.Count - 1;

        public void Push(string path)
        {
            while (_entries.Count > _index + 1)
                _entries.RemoveAt(_entries.Count - 1);
            _entries.Add(path);
            _index = _entries.Count - 1;
        }

        public string GoBack()    { _index--; return _entries[_index]; }
        public string GoForward() { _index++; return _entries[_index]; }
    }

    internal class FileClipboard
    {
        private readonly System.Collections.Generic.List<string> _paths = new();

        public System.Collections.Generic.IReadOnlyList<string> Paths => _paths;
        public bool HasContent => _paths.Count > 0;
        public bool IsCut      { get; private set; }

        public void Copy(string path) { _paths.Clear(); _paths.Add(path); IsCut = false; }
        public void Cut(string path)  { _paths.Clear(); _paths.Add(path); IsCut = true;  }
        public void ClearIfCut()      { if (IsCut) _paths.Clear(); }
    }
}
