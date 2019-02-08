using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace get_folder_files_by_size
{
    public partial class frmMain : Form
    {
        List<FileInfo> files = new List<FileInfo>();


        public frmMain()
        {
            InitializeComponent();
            gridFiles.AutoGenerateColumns = true;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSelectedFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnFetchFiles_Click(object sender, EventArgs e)
        {
            clear();

            var dirinfo = new DirectoryInfo(txtSelectedFolder.Text);
            GetFiles(dirinfo);

            gridFiles.DataSource = files.OrderByDescending(o => o.Length).Select(n => new { n.Name, n.Length, n.DirectoryName }).ToList();
            
        }

        private void GetFiles(DirectoryInfo directory)
        {
            foreach (var subdir in directory.GetDirectories())
            {
                GetFiles(subdir);
            }

            foreach (var file in directory.GetFiles())
            {
                files.Add(file);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            gridFiles.DataSource = null;
            //gridFiles.Rows.Clear();
            gridFiles.Refresh();
            files.Clear();
        }
    }
}
