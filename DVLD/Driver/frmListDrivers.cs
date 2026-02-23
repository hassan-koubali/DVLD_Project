using DVLD.Licenses;
using DVLD.People;
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

namespace DVLD.Driver
{
    public partial class frmListDrivers : Form
    {
        private DataTable _dtAllDrivers;

  

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllDrivers = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtAllDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 120;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 320;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 170;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 150;
            }
            else
            MessageBox.Show("No people found in the system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");


            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }


        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            DataView dataView = _dtAllDrivers.DefaultView;
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    dataView.RowFilter = $"CONVERT(DriverID, 'System.String') LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvDrivers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                    break;

                case "Person ID":
                    dataView.RowFilter = $"CONVERT(PersonID, 'System.String') LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvDrivers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

                    break;
                case "National No":
                    dataView.RowFilter = $"NationalNo LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvDrivers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

                    break;


                case "Full Name":
                    dataView.RowFilter = $"FullName LIKE '{txtFilterValue.Text.Trim()}%'";
                    dgvDrivers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

                    break;

                default:
                    FilterColumn = "None";

                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();
            //refresh
            frmListDrivers_Load(null, null);
        }


        private void ShowPersonLicenseHistoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;


            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
