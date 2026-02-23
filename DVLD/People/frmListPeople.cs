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

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {
        public static DataTable dtPeople = clsPerson.GetSubDetailsAllPeople();

        private void _RefreshPeopleList()
        {
            dtPeople = clsPerson.GetSubDetailsAllPeople();
            dgvPeopleList.DataSource = dtPeople;
        }

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() != null && cbFilter.SelectedItem.ToString() == "Person ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }

        }
        private void _FillPeopleInDataGridView()
        {
            lblRecordsCount.Text = dtPeople.Rows.Count.ToString();
            if (dtPeople.Rows.Count > 0)
            {
                dgvPeopleList.DataSource = dtPeople;
                dgvPeopleList.Columns["PersonID"].Width = 80;
                dgvPeopleList.Columns["NationalNo"].Width = 100;
                dgvPeopleList.Columns["FirstName"].Width = 100;
                dgvPeopleList.Columns["SecondName"].Width = 100;
                dgvPeopleList.Columns["ThirdName"].Width = 100;
                dgvPeopleList.Columns["LastName"].Width = 100;
                dgvPeopleList.Columns["Gendor"].Width = 80;
                dgvPeopleList.Columns["DateOfBirth"].Width = 150;
                dgvPeopleList.Columns["Nationality"].Width = 100;
                dgvPeopleList.Columns["Phone"].Width = 120;
                dgvPeopleList.Columns["Email"].Width = 150;
            }
            else
                MessageBox.Show("No people found in the system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void _SetValueInComboBox()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("Person ID");
            cbFilter.Items.Add("National No");
            cbFilter.Items.Add("First Name");
            cbFilter.Items.Add("Second Name");
            cbFilter.Items.Add("Third Name");
            cbFilter.Items.Add("Last Name");
            cbFilter.Items.Add("Gendor");
            cbFilter.Items.Add("Date Of Birth");
            cbFilter.Items.Add("Nationality");
            cbFilter.Items.Add("Phone");
            cbFilter.Items.Add("Email");
            cbFilter.Items.Add("First Name");
            cbFilter.SelectedIndex = 0;
            txtFilterValue.Enabled = false;
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            
            _FillPeopleInDataGridView();
            _SetValueInComboBox();
            


        }


        private void CheckIfComboBoxHasSpace(string Value)
        {
            DataView dataView = dtPeople.DefaultView;
            switch (cbFilter.Text)
            {
                case "None":
                    dataView.RowFilter = "";
                    txtFilterValue.Enabled = false;
                    break;
                case "Person ID":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"CONVERT(PersonID, 'System.String') LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "National No":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"NationalNo LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "First Name":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"FirstName LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "Second Name":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"SecondName LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "Third Name":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"ThirdName LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "Last Name":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"LastName LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "Date Of Birth":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"LastName LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "Gendor":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"Gendor LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "Nationality":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"Nationality LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "Email":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"Email LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
                case "Phone":
                    txtFilterValue.Enabled = true;
                    dataView.RowFilter = $"Phone LIKE '{Value}%'";
                    dgvPeopleList.DataSource = dataView.ToTable();
                    lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
                    break;
            }
            
        }









        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeopleList();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CheckIfComboBoxHasSpace(txtFilterValue.Text);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Value = txtFilterValue.Text;
            CheckIfComboBoxHasSpace(Value);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonDetails frm = new frmShowPersonDetails((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeopleList.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)dgvPeopleList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            _RefreshPeopleList();

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Function is Not Currently available.");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Function is Not Currently available.");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
