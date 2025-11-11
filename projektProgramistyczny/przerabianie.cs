using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektProgramistyczny
{
    public partial class przerabianie : Form
    {
        public static przerabianie Instance { get; private set; }
        private int version;
        private Size originalSize;
        private Dictionary<Control, Rectangle> originalControlBounds =  new Dictionary<Control, Rectangle>();
        public przerabianie()
        {
            InitializeComponent();
            Instance = this;
            this.Load += przerabianieLoad;
            this.Resize += przerabianieResize;
        }
        private void przerabianieLoad(object sender, EventArgs e)
        {
            originalSize = this.Size;
            StoreOriginalBoundsRecursive(this);
        }
        private void przerabianieResize(object sender, EventArgs E)
        {
            ResizeControlsRecursive(this);
        }
        private void StoreOriginalBoundsRecursive(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if(!originalControlBounds.ContainsKey(ctrl))
                    originalControlBounds[ctrl] = ctrl.Bounds;

                if (ctrl.Controls.Count > 0)
                    StoreOriginalBoundsRecursive(ctrl);
            }
        }
        private void ResizeControlsRecursive(Control parent)
        {
            if (originalSize.Width == 0 || originalSize.Height == 0) return;

            float xRatio=(float)this.Width / originalSize.Width;
            float yRatio=(float)this.Height / originalSize.Height;

            foreach (Control ctrl in parent.Controls)
            {
                if (originalControlBounds.ContainsKey(ctrl))
                {
                    Rectangle orig = originalControlBounds[ctrl];
                    int newX = (int)(orig.X * xRatio);
                    int newY = (int)(orig.Y * yRatio);
                    int newWidth = (int)(orig.Width * xRatio);
                    int newHeight = (int)(orig.Height * yRatio);
                    ctrl.Bounds = new Rectangle(newX, newY, newWidth, newHeight);
                }
                if (ctrl.Controls.Count > 0)
                {
                    ResizeControlsRecursive(ctrl);
                }
            }
        }
        private void getPhoto_Click(object sender, EventArgs e)
        {
            photoPath.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            photoPath.Title = "Wybierz zdjęcie";
            if (photoPath.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image img = Image.FromFile(photoPath.FileName);
                    photoIn.Image = img;
                    photoIn.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas ładowania zdjęcia: " + ex.Message);
                }
            }
        }

        private void changePhoto_Click(object sender, EventArgs e)
        {
            if (photoIn.Image == null)
            {
                MessageBox.Show("Proszę najpierw wybrać zdjęcie.");
                return;
            }
            var model = new wyborModelu();
            model.ShowDialog();
            if (model.wybranyModel== 0)
            {
                return;
            }
            switch (model.wybranyModel)
            {
                case 1:
                    version = 1;
                    break;
                case 2:
                    version = 2;
                    break;
                case 3:
                    version = 3;
                    break;
            }
            string inputPath = photoPath.FileName;

            string outputPath = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(inputPath),
                "output.png"
            );
            string scriptPath = System.IO.Path.Combine(Application.StartupPath, "Python", "modelCode.py");
            string pythonExe = "python";

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = pythonExe;
            psi.Arguments = $"\"{scriptPath}\" \"{inputPath}\" \"{outputPath}\" {version}";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            try
            {
                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(psi))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show("Bład Pythona: " + error);
                        return;
                    }
                    if (System.IO.File.Exists(outputPath))
                    {
                        Image resultImg = Image.FromFile(outputPath);
                        photoOut.Image = resultImg;
                        photoOut.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        MessageBox.Show("Nie znaleziono pliku wyjściowego.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wywoływania Pythona: " + ex.Message);
            }
        }
    }
}
