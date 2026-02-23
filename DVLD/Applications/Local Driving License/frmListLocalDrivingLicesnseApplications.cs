using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests;
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

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }
        public static DataTable dtLocalDrivingLicenses = new DataTable();

        public void _FillPeopleInDataGridView()
        {
            dtLocalDrivingLicenses = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = dtLocalDrivingLicenses;
            lblRecordsCount.Text = dtLocalDrivingLicenses.Rows.Count.ToString();
            if (dtLocalDrivingLicenses.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 80;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 200;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 100;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 250;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 70;

                dgvLocalDrivingLicenseApplications.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns[6].Width = 70;

            }
            else
                MessageBox.Show("No Application To Show Them.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


            cbFilter.SelectedIndex = 0;
            txtFilterValue.Enabled = false;



        }

        private void CheckIfComboBoxHasSpace(string Value)
        {
            DataView dataView = dtLocalDrivingLicenses.DefaultView;
            switch (cbFilter.Text)
            {
                case "None":
                    dataView.RowFilter = "";
                    txtFilterValue.Enabled = false;
                    break;
                case "L.D.L.AppID":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"CONVERT(LocalDrivingLicenseApplicationID, 'System.String') LIKE '{Value}%'";
                    dgvLocalDrivingLicenseApplications.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                    break;
                case "National No":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"NationalNo LIKE '{Value}%'";
                    dgvLocalDrivingLicenseApplications.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                    break;
                case "Full Name":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"FullName LIKE '{Value}%'";
                    dgvLocalDrivingLicenseApplications.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                    break;
                case "Status":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"Status LIKE '{Value}%'";
                    dgvLocalDrivingLicenseApplications.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                    break;
               
            }

        }

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppoitments frm = new frmListTestAppoitments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            //refresh
            frmListLocalDrivingApplication_Load(null, null);

        }

        private void frmListLocalDrivingApplication_Load(object sender, EventArgs e)
        {
            _FillPeopleInDataGridView();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            CheckIfComboBoxHasSpace(txtFilterValue.Text);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
            CheckIfComboBoxHasSpace(txtFilterValue.Text);
        }

        private void brnAddLocalDrivingLicense_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frmAddUpdateLocalDrivingLicense = new frmAddUpdateLocalDrivingLicenseApplication();
            frmAddUpdateLocalDrivingLicense.ShowDialog();
            frmListLocalDrivingApplication_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseInfo frmLocalDrivingLicenseInfo = new frmLocalDrivingLicenseInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmLocalDrivingLicenseInfo.ShowDialog();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frmAddUpdateLocalDrivingLicense = new frmAddUpdateLocalDrivingLicenseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmAddUpdateLocalDrivingLicense.ShowDialog();
        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            if(LocalDrivingLicenseApplication.Delete())
                {
                MessageBox.Show("Application Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListLocalDrivingApplication_Load(null, null);
            }
            else
                MessageBox.Show("Error Deleting Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            LocalDrivingLicenseApplication.Cancel();
            MessageBox.Show("Application Canceled Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmListLocalDrivingApplication_Load(null, null);
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int _LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int PersonID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID).ApplicantPersonID;
            frmShowPersonLicenseHistory frmShowPersonLicenses = new frmShowPersonLicenseHistory(PersonID);
            frmShowPersonLicenses.ShowDialog();
            //MessageBox.Show("This Feature Is Not Available Now.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void isseuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseFirstTime frmIssueDriverLicenseFirstTime = new frmIssueDriverLicenseFirstTime((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frmIssueDriverLicenseFirstTime.ShowDialog();
            frmListLocalDrivingApplication_Load(null, null);
            //MessageBox.Show("This Feature Is Not Available Now.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Feature Is Not Available Now.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(
               LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);

        }

        private void sechduleWrittingTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);

        }

        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
                                                    (LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            //Enabled only if person passed all tests and Does not have license. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            ScheduleTestsMenue.Enabled = !LicenseExists;

            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            CancelApplicaitonToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            DeleteApplicationToolStripMenuItem.Enabled =
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);



            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (ScheduleTestsMenue.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cbFilter.SelectedItem.ToString() != null && cbFilter.SelectedItem.ToString() == "L.D.L.AppID"))
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
           
        }
    }
}
