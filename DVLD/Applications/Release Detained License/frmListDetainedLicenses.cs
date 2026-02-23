using DVLD.Licenses;
using DVLD.Licenses.Detain_License;
using DVLD.Licenses.Local_Licenses;
using DVLD.People;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DVLD.Applications.Release_Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        public static DataTable _dtDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }


        private void frmListReleaseDetainedLicense_Load(object sender, EventArgs e)
        {



            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;
                
            }
            else
            {
                MessageBox.Show("No Detained Licenses found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                    //_dtDetainedLicenses.DefaultView.RowFilter = "";
                    //lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }


        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {


            string FilterColumn = "";
            DataView dataView = _dtDetainedLicenses.DefaultView;
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    dataView.RowFilter = $"CONVERT(DetainID, 'System.String') LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvDetainedLicenses.DataSource = dataView.ToTable();
                    lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                    break;

                case "Release Application ID":
                    dataView.RowFilter = $"CONVERT(ReleaseApplicationID, 'System.String') LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvDetainedLicenses.DataSource = dataView.ToTable();
                    lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                    break;
                case "National No":
                    dataView.RowFilter = $"NationalNo LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvDetainedLicenses.DataSource = dataView.ToTable();
                    lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                    break;


                case "Full Name":
                    dataView.RowFilter = $"FullName LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvDetainedLicenses.DataSource = dataView.ToTable();
                    lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                    break;

                default:
                    FilterColumn = "None";

                    break;


                    //Reset the filters in case nothing selected or filter value conains nothing.
                    if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
                    {
                        _dtDetainedLicenses.DefaultView.RowFilter = "";
                        lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                        return;
                    }

            }

        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dataView = _dtDetainedLicenses.DefaultView;
            switch (cbIsReleased.Text)
            {
                case "All":
                    dataView.RowFilter = "";
                    dgvDetainedLicenses.DataSource = dataView.ToTable();
                    lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                    break;
                case "Yes":
                    dataView.RowFilter = $"IsReleased = True";
                    dgvDetainedLicenses.DataSource = dataView.ToTable();
                    lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                    break;
                case "No":
                    dataView.RowFilter = $"IsReleased = False";
                    dgvDetainedLicenses.DataSource = dataView.ToTable();
                    lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                    break;
            }
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;

            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            //refresh
            frmListReleaseDetainedLicense_Load(null, null);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }



        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;

        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            //refresh
            frmListReleaseDetainedLicense_Load(null, null);
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            //refresh
            frmListReleaseDetainedLicense_Load(null, null);
        }
    }
}
