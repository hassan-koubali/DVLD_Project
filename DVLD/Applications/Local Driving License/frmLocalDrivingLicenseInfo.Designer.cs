namespace DVLD.Applications.Local_Driving_License
{
    partial class frmLocalDrivingLicenseInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlLocalDrivingLincensInfo1 = new DVLD.Applications.Local_Driving_License.ctrlDrivingLicenseApplicationInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(626, 373);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlLocalDrivingLincensInfo1
            // 
            this.ctrlLocalDrivingLincensInfo1.Location = new System.Drawing.Point(4, 4);
            this.ctrlLocalDrivingLincensInfo1.Name = "ctrlLocalDrivingLincensInfo1";
            this.ctrlLocalDrivingLincensInfo1.Size = new System.Drawing.Size(756, 385);
            this.ctrlLocalDrivingLincensInfo1.TabIndex = 0;
            // 
            // frmLocalDrivingLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 416);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlLocalDrivingLincensInfo1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLocalDrivingLicenseInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmLocalDrivingLicenseInfo";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlDrivingLicenseApplicationInfo ctrlLocalDrivingLincensInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}