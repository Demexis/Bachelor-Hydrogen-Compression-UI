
namespace Bachelor_Project
{
    partial class Cyclogram
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer_main = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer_main
            // 
            this.timer_main.Enabled = true;
            this.timer_main.Interval = 10;
            this.timer_main.Tick += new System.EventHandler(this.timer_main_Tick);
            // 
            // Cyclogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "Cyclogram";
            this.Size = new System.Drawing.Size(322, 262);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Cyclogram_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cyclogram_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Cyclogram_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Cyclogram_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Cyclogram_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Cyclogram_MouseUp);
            this.Resize += new System.EventHandler(this.Cyclogram_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_main;
    }
}
