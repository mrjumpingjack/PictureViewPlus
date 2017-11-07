using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewPlus
{
    public partial class MetadataView : Form
    {
        public MetadataView()
        {
            InitializeComponent();
        }
        private IEnumerable<MetadataExtractor.Directory> dirs;


        public IEnumerable<MetadataExtractor.Directory> Dirs
        {
            set { dirs = value; }
            get { return dirs; }
        }



        private void MetadataView_Load(object sender, EventArgs e)
        {
            foreach (var directory in dirs)
            {
                foreach (var tag in directory.Tags)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv1.Rows[0].Clone();
                    row.Cells[0].Value = tag;
                    row.Cells[1].Value = tag.Description;
                    dgv1.Rows.Add(row);
                }
            }

            dgv1.Columns[0].Width = dgv1.Width / 2;
            dgv1.Columns[1].Width = dgv1.Width / 2;
        }

        private void MetadataView_Resize(object sender, EventArgs e)
        {
            dgv1.Columns[0].Width = dgv1.Width / 2;
            dgv1.Columns[1].Width = dgv1.Width / 2;
        }
    }
}
