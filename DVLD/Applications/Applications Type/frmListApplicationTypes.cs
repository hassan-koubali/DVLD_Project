using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable dtAllApplicationType;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _FillApplicationTypesInDataGridView()
        {
            dtAllApplicationType = clsApplicationType.GetAllApplicationTypes();

            dgvApplicationTypes.DataSource = dtAllApplicationType;

            if (dtAllApplicationType.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 100;

                lblRecordsCount.Text = dtAllApplicationType.Rows.Count.ToString();

            }
            else
                MessageBox.Show("No Application Types found in the system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblRecordsCount.Text = dtAllApplicationType.Rows.Count.ToString();
            dgvApplicationTypes.Columns["ApplicationTypeID"].HeaderText = "ID";
            dgvApplicationTypes.Columns["ApplicationTypeTitle"].HeaderText = "Title";
            dgvApplicationTypes.Columns["ApplicationFees"].HeaderText = "Fees";
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _FillApplicationTypesInDataGridView();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditeApplicationType EditeApplicationType = new frmEditeApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            EditeApplicationType.ShowDialog();
            frmListApplicationTypes_Load(null, null);

        }
    }
}
