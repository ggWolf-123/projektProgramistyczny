using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektProgramistyczny
{
    public partial class nieKlikaj : Form
    {
        private Size originalSize;
        private Dictionary<Control, Rectangle> originalControlBounds = new Dictionary<Control, Rectangle>();
        public nieKlikaj()
        {
            InitializeComponent();
            this.Load += przerabianieLoad;
            this.Resize += przerabianieResize;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
            string videoPath = Path.Combine(Application.StartupPath, "why.mp4");
            axWindowsMediaPlayer1.URL = videoPath;
        }
        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8)
            {
                this.Close();
            }
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
    }
}
