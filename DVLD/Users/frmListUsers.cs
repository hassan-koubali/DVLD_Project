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

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {
        public static DataTable dtUsers;


        public frmListUsers()
        {
            InitializeComponent();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser addUserForm = new frmAddUpdateUser();
            addUserForm.ShowDialog();
            _FillUsersInDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillUsersInDataGridView()
        {
            dtUsers = clsUser.GetAllUsers();
            lblRecordsCount.Text = dtUsers.Rows.Count.ToString();
            cbFilter.SelectedIndex = 0;
            dgvUsers.DataSource = dtUsers;
            if (dtUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 120;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;

            }
            else
                MessageBox.Show("No people found in the system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        




        private void TrimTxtFilter(string Value)
        {
            DataView dataView = dtUsers.DefaultView;
            switch (cbFilter.Text)
            {
                case "None":
                    txtFilterValue.Visible = true;
                    dataView.RowFilter = "";
                    txtFilterValue.Enabled = false;
                    cbIsActive.Visible = false;
                    return;
                  
                case "User ID":

                    txtFilterValue.Visible = true;
                    cbIsActive.Visible = false;
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"CONVERT(UserID, 'System.String') LIKE '{Value}%'";
                    dgvUsers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                    break;
                case "Person ID":

                    txtFilterValue.Visible = true;
                    cbIsActive.Visible = false;
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"CONVERT(PersonID, 'System.String') LIKE '{Value}%'";
                    dgvUsers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                    break;
                case "Full Name":

                    txtFilterValue.Visible = true;
                    cbIsActive.Visible = false;
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"FullName LIKE '{Value}%'";
                    dgvUsers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                    break;
                case "User Name":

                    txtFilterValue.Visible = true;
                    cbIsActive.Visible = false;
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"UserName LIKE '{Value}%'";
                    dgvUsers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                    break;
                case "Is Active":
                    txtFilterValue.Visible = false;
                    cbIsActive.Visible = true;
                    break;
            }
        }
        private void _CheckIfUserIsActive(string cbValue)
        {

            DataView dataView = dtUsers.DefaultView;
            switch (cbIsActive.Text)
            {
                case "All":
                    dataView.RowFilter = "";
                    dgvUsers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                    break;
                case "Yes":
                    dataView.RowFilter = $"IsActive = True";
                    dgvUsers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                    break;
                case "No":
                    dataView.RowFilter = $"IsActive = False";
                    dgvUsers.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                    break;
            }


        }



        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _FillUsersInDataGridView();
            

        }

        

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Enabled = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else 
            {
                txtFilterValue.Visible = (cbFilter.Text != "None");
                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            










        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string Value = txtFilterValue.Text;
            TrimTxtFilter(Value);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CheckIfUserIsActive(cbIsActive.SelectedItem.ToString());
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmUserInfo.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser addUserForm = new frmAddUpdateUser();
            addUserForm.ShowDialog();
            _FillUsersInDataGridView();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser addUserForm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            addUserForm.ShowDialog();
            _FillUsersInDataGridView();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                if (clsUser.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete User has Been Successfully", "Delete", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("User is Not Deleted Due to data Connected To it.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            _FillUsersInDataGridView();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[1].Value);
            frmChangePassword.ShowDialog();
            _FillUsersInDataGridView();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Function is Not Currently available.");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Function is Not Currently available.");
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cbFilter.SelectedItem.ToString() != null && cbFilter.SelectedItem.ToString() == "Person ID") || cbFilter.SelectedItem.ToString() != null && cbFilter.SelectedItem.ToString() == "User ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
