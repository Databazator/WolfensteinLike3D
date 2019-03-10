namespace wolfensteinTest
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox_3D = new System.Windows.Forms.PictureBox();
            this.pictureBox_2D = new System.Windows.Forms.PictureBox();
            this.timerWulf = new System.Windows.Forms.Timer(this.components);
            this.label_3dx = new System.Windows.Forms.Label();
            this.label_WierdAngle = new System.Windows.Forms.Label();
            this.label_1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_3D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_2D)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_3D
            // 
            this.pictureBox_3D.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_3D.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_3D.Name = "pictureBox_3D";
            this.pictureBox_3D.Size = new System.Drawing.Size(785, 703);
            this.pictureBox_3D.TabIndex = 0;
            this.pictureBox_3D.TabStop = false;
            this.pictureBox_3D.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // pictureBox_2D
            // 
            this.pictureBox_2D.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_2D.Location = new System.Drawing.Point(791, 0);
            this.pictureBox_2D.Name = "pictureBox_2D";
            this.pictureBox_2D.Size = new System.Drawing.Size(548, 703);
            this.pictureBox_2D.TabIndex = 1;
            this.pictureBox_2D.TabStop = false;
            this.pictureBox_2D.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_2D_Paint);
            // 
            // timerWulf
            // 
            this.timerWulf.Interval = 10;
            this.timerWulf.Tick += new System.EventHandler(this.timerWulf_Tick);
            // 
            // label_3dx
            // 
            this.label_3dx.AutoSize = true;
            this.label_3dx.Location = new System.Drawing.Point(1295, 706);
            this.label_3dx.Name = "label_3dx";
            this.label_3dx.Size = new System.Drawing.Size(32, 13);
            this.label_3dx.TabIndex = 2;
            this.label_3dx.Text = "3dX: ";
            // 
            // label_WierdAngle
            // 
            this.label_WierdAngle.AutoSize = true;
            this.label_WierdAngle.Location = new System.Drawing.Point(1094, 706);
            this.label_WierdAngle.Name = "label_WierdAngle";
            this.label_WierdAngle.Size = new System.Drawing.Size(25, 13);
            this.label_WierdAngle.TabIndex = 3;
            this.label_WierdAngle.Text = "kek";
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Location = new System.Drawing.Point(908, 706);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(31, 13);
            this.label_1.TabIndex = 4;
            this.label_1.Text = "kek2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 728);
            this.Controls.Add(this.label_1);
            this.Controls.Add(this.label_WierdAngle);
            this.Controls.Add(this.label_3dx);
            this.Controls.Add(this.pictureBox_2D);
            this.Controls.Add(this.pictureBox_3D);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_3D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_2D)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_3D;
        private System.Windows.Forms.PictureBox pictureBox_2D;
        public System.Windows.Forms.Timer timerWulf;
        public System.Windows.Forms.Label label_3dx;
        public System.Windows.Forms.Label label_WierdAngle;
        public System.Windows.Forms.Label label_1;
    }
}

