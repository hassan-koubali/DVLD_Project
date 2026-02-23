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
    public partial class frmLocalDrivingLicenseInfo : Form
    {
        private int _ApplicationID = -1;
        public frmLocalDrivingLicenseInfo(int ApplicationID)
        {
            InitializeComponent();
            _ApplicationID = ApplicationID;

        }

        private void frmLocalDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLincensInfo1.LoadApplicationInfoByLocalDrivingAppID(_ApplicationID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
