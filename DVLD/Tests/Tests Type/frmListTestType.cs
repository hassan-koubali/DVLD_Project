using DVLD.Applications;
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
using static DVLD_Buisness.clsApplication;

namespace DVLD.Tests.TestsType
{
    public partial class frmListTestsType : Form
    {
        DataTable dtAllTestType;
        public frmListTestsType()
        {
            InitializeComponent();
        }

        private void frmListTestType_Load(object sender, EventArgs e)
        {
            dtAllTestType = clsTestType.GetAllTestTypes();
            lblRecordsCount.Text = dtAllTestType.Rows.Count.ToString();
            dgvTestsType.DataSource = dtAllTestType;
            if (dtAllTestType.Rows.Count > 0)
            {
                dgvTestsType.Columns[0].HeaderText = "ID";
                dgvTestsType.Columns[0].Width = 75;

                dgvTestsType.Columns[1].HeaderText = "Title";
                dgvTestsType.Columns[1].Width = 150;

                dgvTestsType.Columns[2].HeaderText = "Description";
                dgvTestsType.Columns[2].Width = 300;

                dgvTestsType.Columns[3].HeaderText = "Fees";
                dgvTestsType.Columns[3].Width = 100;

            }
            else
                MessageBox.Show("No Test Types found in the system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)dgvTestsType.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            frmListTestType_Load(null, null);

        }
    }
}
