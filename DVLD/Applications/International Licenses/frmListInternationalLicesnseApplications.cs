using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
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

namespace DVLD.Applications.International_Licenses
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private DataTable _dtInternationalLicenseApplications;
        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;

            dgvInternationalLicenses.DataSource = _dtInternationalLicenseApplications;
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 150;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;

            }
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            //refresh
            frmListInternationalLicesnseApplications_Load(null, null);
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow numbers only becasue all fiters are numbers.
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            if (cbFilterBy.Text == "Is Active")
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

                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dataView = _dtInternationalLicenseApplications.DefaultView;
            switch (cbIsReleased.Text)
            {
                case "All":
                    dataView.RowFilter = "";
                    dgvInternationalLicenses.DataSource = dataView.ToTable();
                    lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                    break;
                case "Yes":
                    dataView.RowFilter = $"IsActive = True";
                    dgvInternationalLicenses.DataSource = dataView.ToTable();
                    lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                    break;
                case "No":
                    dataView.RowFilter = $"IsActive = False";
                    dgvInternationalLicenses.DataSource = dataView.ToTable();
                    lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                    break;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            
            string FilterColumn = "";
            DataView dataView = _dtInternationalLicenseApplications.DefaultView;
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    dataView.RowFilter = $"CONVERT(InternationalLicenseID, 'System.String') LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvInternationalLicenses.DataSource = dataView.ToTable();
                    lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                    break;

                case "Application ID":
                    dataView.RowFilter = $"CONVERT(ApplicationID, 'System.String') LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvInternationalLicenses.DataSource = dataView.ToTable();
                    lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                    break;
                case "Driver ID":
                    dataView.RowFilter = $"CONVERT(DriverID, 'System.String') LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvInternationalLicenses.DataSource = dataView.ToTable();
                    lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                    break;
                case "Local License ID":
                    dataView.RowFilter = $"CONVERT(IssuedUsingLocalLicenseID, 'System.String') LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvInternationalLicenses.DataSource = dataView.ToTable();
                    lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                    break;

                default:
                    FilterColumn = "None";
                    break;


                    //Reset the filters in case nothing selected or filter value conains nothing.
                    if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
                    {
                        _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                        lblInternationalLicensesRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
                        return;
                    }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
