using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.ControlPanel
{
    public partial class ControlPanel : UserControl
    {
        public bool PlayButtonStatus = false;

        public Action<bool> OnPlayPauseClick;
        public Action OnStepForwardClick;
        public Action OnStepBackwardClick;
        public Action OnRightEndClick;
        public Action OnLeftEndClick;


        public ControlPanel()
        {
            InitializeComponent();
        }

        private void button_PlayPause_Click(object sender, EventArgs e)
        {
            PlayButtonStatus = !PlayButtonStatus;

            button_PlayPause.BackgroundImage = PlayButtonStatus ? Properties.Resources.pause_button : Properties.Resources.play_button;

            OnPlayPauseClick?.Invoke(PlayButtonStatus);
        }

        private void button_StepBackward_Click(object sender, EventArgs e)
        {
            OnStepBackwardClick?.Invoke();
        }

        private void button_LeftEnd_Click(object sender, EventArgs e)
        {
            OnLeftEndClick?.Invoke();
        }

        private void button_StepForward_Click(object sender, EventArgs e)
        {
            OnStepForwardClick?.Invoke();
        }

        private void button_RightEnd_Click(object sender, EventArgs e)
        {
            OnRightEndClick?.Invoke();
        }
    }
}
