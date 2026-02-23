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

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _LocaleDrivingLicenceApplicationID = -1;
        clsTestType.enTestType _testType = clsTestType.enTestType.VisionTest;
        private int _AppoitmentID = -1;

        public frmScheduleTest(int LocaleDrivingLicenceApplication, clsTestType.enTestType TestType, int AppoitmentID = -1)
        {
            InitializeComponent();
            _LocaleDrivingLicenceApplicationID = LocaleDrivingLicenceApplication;
            _testType = TestType;
            _AppoitmentID = AppoitmentID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlSceduleTest1.TestTypeID = _testType;
            ctrlSceduleTest1.LoadInfo(_LocaleDrivingLicenceApplicationID, _AppoitmentID);
        }


    }
}
