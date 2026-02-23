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
    public partial class frmShowPersonDetails : Form
    {
        public frmShowPersonDetails(int PresonID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PresonID);

        }
        public frmShowPersonDetails(string NatonalNo)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(NatonalNo);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
