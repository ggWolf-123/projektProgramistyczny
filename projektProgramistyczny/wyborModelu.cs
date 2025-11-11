using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektProgramistyczny
{
    public partial class wyborModelu : Form
    {
        public int wybranyModel { get; private set; } = 0;
        private Size originalSize;
        private Dictionary<Control, Rectangle> originalControlBounds = new Dictionary<Control, Rectangle>();
        public wyborModelu()
        {
            InitializeComponent();
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
                if (!originalControlBounds.ContainsKey(ctrl))
                    originalControlBounds[ctrl] = ctrl.Bounds;

                if (ctrl.Controls.Count > 0)
                    StoreOriginalBoundsRecursive(ctrl);
            }
        }
        private void ResizeControlsRecursive(Control parent)
        {
            if (originalSize.Width == 0 || originalSize.Height == 0) return;

            float xRatio = (float)this.Width / originalSize.Width;
            float yRatio = (float)this.Height / originalSize.Height;

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

        private void button1_Click(object sender, EventArgs e)
        {
            wybranyModel = 1;
            this.Close();
        }

        private void model2_Click(object sender, EventArgs e)
        {
            wybranyModel = 2;
            this.Close();
        }

        private void model3_Click(object sender, EventArgs e)
        {
            wybranyModel = 3;
            this.Close();
        }

        private void noClick_Click(object sender, EventArgs e)
        {
            this.Close();
            przerabianie.Instance.Hide();
            nieKlikaj film=new nieKlikaj();
            film.FormClosed += (s, args) =>
            {
                przerabianie.Instance.Show();
            };
            film.Show();
        }
    }
}
