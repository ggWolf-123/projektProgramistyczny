using System.Windows.Forms;

namespace projektProgramistyczny
{
    partial class przerabianie
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.changePhoto = new System.Windows.Forms.Button();
            this.getPhoto = new System.Windows.Forms.Button();
            this.photoIn = new System.Windows.Forms.PictureBox();
            this.photoOut = new System.Windows.Forms.PictureBox();
            this.photoPath = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.photoIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoOut)).BeginInit();
            this.SuspendLayout();
            // 
            // changePhoto
            // 
            this.changePhoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.changePhoto.Location = new System.Drawing.Point(404, 381);
            this.changePhoto.Name = "changePhoto";
            this.changePhoto.Size = new System.Drawing.Size(384, 57);
            this.changePhoto.TabIndex = 0;
            this.changePhoto.Text = "Podaj obróbce";
            this.changePhoto.UseVisualStyleBackColor = true;
            this.changePhoto.Click += new System.EventHandler(this.changePhoto_Click);
            // 
            // getPhoto
            // 
            this.getPhoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.getPhoto.Location = new System.Drawing.Point(12, 381);
            this.getPhoto.Name = "getPhoto";
            this.getPhoto.Size = new System.Drawing.Size(386, 57);
            this.getPhoto.TabIndex = 1;
            this.getPhoto.Text = "Podaj zdjęcie";
            this.getPhoto.UseVisualStyleBackColor = true;
            this.getPhoto.Click += new System.EventHandler(this.getPhoto_Click);
            // 
            // photoIn
            // 
            this.photoIn.Location = new System.Drawing.Point(12, 12);
            this.photoIn.Name = "photoIn";
            this.photoIn.Size = new System.Drawing.Size(386, 363);
            this.photoIn.TabIndex = 2;
            this.photoIn.TabStop = false;
            // 
            // photoOut
            // 
            this.photoOut.Location = new System.Drawing.Point(404, 12);
            this.photoOut.Name = "photoOut";
            this.photoOut.Size = new System.Drawing.Size(384, 363);
            this.photoOut.TabIndex = 3;
            this.photoOut.TabStop = false;
            // 
            // photoPath
            // 
            this.photoPath.FileName = "photoPath";
            // 
            // przerabianie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.photoOut);
            this.Controls.Add(this.photoIn);
            this.Controls.Add(this.getPhoto);
            this.Controls.Add(this.changePhoto);
            this.Name = "przerabianie";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.photoIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button changePhoto;
        private System.Windows.Forms.Button getPhoto;
        private System.Windows.Forms.PictureBox photoIn;
        private System.Windows.Forms.PictureBox photoOut;
        private System.Windows.Forms.OpenFileDialog photoPath;
    }
}

