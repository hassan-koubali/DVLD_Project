using DVLD.Classes;
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
    public partial class frmEditeApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationType _ApplicationType;

        public frmEditeApplicationType(int ApplicationID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationID;
        }

        private void EditeApplicationType_Load(object sender, EventArgs e)
        {
            lblID.Text = _ApplicationType.ID.ToString();
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);
            if (_ApplicationType != null)
            {
                txtApplicationTypeTitle.Text = _ApplicationType.Title;
                txtApplicationFees.Text = _ApplicationType.Fees.ToString();

            }
        }




        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _ApplicationType.Title = txtApplicationTypeTitle.Text.Trim();
            _ApplicationType.Fees = Convert.ToSingle(txtApplicationFees.Text.Trim());
            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Is Not Saved Daccessfuly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtApplicationTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtApplicationTypeTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationTypeTitle, "Title Cannot be Empty!");
            }
            else
            {
                errorProvider1.SetError(txtApplicationTypeTitle, null);
            }
        }

        private void txtApplicationFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtApplicationFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "Fees Cannot be Empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtApplicationFees, null);
            }
            if (!clsValidatoin.IsNumber(txtApplicationFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtApplicationFees, null);
            }
        }
    }
}
