namespace projektProgramistyczny
{
    partial class wyborModelu
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
            this.model1 = new System.Windows.Forms.Button();
            this.model2 = new System.Windows.Forms.Button();
            this.model3 = new System.Windows.Forms.Button();
            this.noClick = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // model1
            // 
            this.model1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.model1.Location = new System.Drawing.Point(12, 12);
            this.model1.Name = "model1";
            this.model1.Size = new System.Drawing.Size(241, 71);
            this.model1.TabIndex = 0;
            this.model1.Text = "modelV1";
            this.model1.UseVisualStyleBackColor = true;
            this.model1.Click += new System.EventHandler(this.button1_Click);
            // 
            // model2
            // 
            this.model2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.model2.Location = new System.Drawing.Point(12, 89);
            this.model2.Name = "model2";
            this.model2.Size = new System.Drawing.Size(241, 71);
            this.model2.TabIndex = 1;
            this.model2.Text = "modelV2";
            this.model2.UseVisualStyleBackColor = true;
            this.model2.Click += new System.EventHandler(this.model2_Click);
            // 
            // model3
            // 
            this.model3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.model3.Location = new System.Drawing.Point(12, 166);
            this.model3.Name = "model3";
            this.model3.Size = new System.Drawing.Size(241, 71);
            this.model3.TabIndex = 2;
            this.model3.Text = "modelV3";
            this.model3.UseVisualStyleBackColor = true;
            this.model3.Click += new System.EventHandler(this.model3_Click);
            // 
            // noClick
            // 
            this.noClick.AutoSize = true;
            this.noClick.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.noClick.Location = new System.Drawing.Point(399, 127);
            this.noClick.Name = "noClick";
            this.noClick.Size = new System.Drawing.Size(131, 24);
            this.noClick.TabIndex = 3;
            this.noClick.Text = "Nie klikaj mnie";
            this.noClick.Click += new System.EventHandler(this.noClick_Click);
            // 
            // wyborModelu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 280);
            this.Controls.Add(this.noClick);
            this.Controls.Add(this.model3);
            this.Controls.Add(this.model2);
            this.Controls.Add(this.model1);
            this.Name = "wyborModelu";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button model1;
        private System.Windows.Forms.Button model2;
        private System.Windows.Forms.Button model3;
        private System.Windows.Forms.Label noClick;
    }
}