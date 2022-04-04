
namespace Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.ControlPanel
{
    partial class ControlPanel
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_LeftEnd = new System.Windows.Forms.Button();
            this.button_RightEnd = new System.Windows.Forms.Button();
            this.button_StepBackward = new System.Windows.Forms.Button();
            this.button_PlayPause = new System.Windows.Forms.Button();
            this.button_StepForward = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.button_LeftEnd, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_StepBackward, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_PlayPause, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_StepForward, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_RightEnd, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 90);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_LeftEnd
            // 
            this.button_LeftEnd.BackgroundImage = global::Bachelor_Project_Hydrogen_Compression_WinForms.Properties.Resources.end_left_button;
            this.button_LeftEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_LeftEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_LeftEnd.FlatAppearance.BorderSize = 0;
            this.button_LeftEnd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_LeftEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_LeftEnd.Location = new System.Drawing.Point(3, 3);
            this.button_LeftEnd.Name = "button_LeftEnd";
            this.button_LeftEnd.Size = new System.Drawing.Size(80, 84);
            this.button_LeftEnd.TabIndex = 0;
            this.button_LeftEnd.UseVisualStyleBackColor = true;
            this.button_LeftEnd.Click += new System.EventHandler(this.button_LeftEnd_Click);
            // 
            // button_RightEnd
            // 
            this.button_RightEnd.BackgroundImage = global::Bachelor_Project_Hydrogen_Compression_WinForms.Properties.Resources.end_right_button;
            this.button_RightEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_RightEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RightEnd.FlatAppearance.BorderSize = 0;
            this.button_RightEnd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_RightEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_RightEnd.Location = new System.Drawing.Point(347, 3);
            this.button_RightEnd.Name = "button_RightEnd";
            this.button_RightEnd.Size = new System.Drawing.Size(82, 84);
            this.button_RightEnd.TabIndex = 4;
            this.button_RightEnd.UseVisualStyleBackColor = true;
            this.button_RightEnd.Click += new System.EventHandler(this.button_RightEnd_Click);
            // 
            // button_StepBackward
            // 
            this.button_StepBackward.BackgroundImage = global::Bachelor_Project_Hydrogen_Compression_WinForms.Properties.Resources.step_backward_button;
            this.button_StepBackward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_StepBackward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_StepBackward.FlatAppearance.BorderSize = 0;
            this.button_StepBackward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_StepBackward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_StepBackward.Location = new System.Drawing.Point(89, 3);
            this.button_StepBackward.Name = "button_StepBackward";
            this.button_StepBackward.Size = new System.Drawing.Size(80, 84);
            this.button_StepBackward.TabIndex = 1;
            this.button_StepBackward.UseVisualStyleBackColor = true;
            this.button_StepBackward.Click += new System.EventHandler(this.button_StepBackward_Click);
            // 
            // button_PlayPause
            // 
            this.button_PlayPause.BackgroundImage = global::Bachelor_Project_Hydrogen_Compression_WinForms.Properties.Resources.play_button;
            this.button_PlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_PlayPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PlayPause.FlatAppearance.BorderSize = 0;
            this.button_PlayPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_PlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_PlayPause.Location = new System.Drawing.Point(175, 3);
            this.button_PlayPause.Name = "button_PlayPause";
            this.button_PlayPause.Size = new System.Drawing.Size(80, 84);
            this.button_PlayPause.TabIndex = 2;
            this.button_PlayPause.UseVisualStyleBackColor = true;
            this.button_PlayPause.Click += new System.EventHandler(this.button_PlayPause_Click);
            // 
            // button_StepForward
            // 
            this.button_StepForward.BackgroundImage = global::Bachelor_Project_Hydrogen_Compression_WinForms.Properties.Resources.step_forward_button;
            this.button_StepForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_StepForward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_StepForward.FlatAppearance.BorderSize = 0;
            this.button_StepForward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_StepForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_StepForward.Location = new System.Drawing.Point(261, 3);
            this.button_StepForward.Name = "button_StepForward";
            this.button_StepForward.Size = new System.Drawing.Size(80, 84);
            this.button_StepForward.TabIndex = 3;
            this.button_StepForward.UseVisualStyleBackColor = true;
            this.button_StepForward.Click += new System.EventHandler(this.button_StepForward_Click);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(432, 90);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_LeftEnd;
        private System.Windows.Forms.Button button_StepBackward;
        private System.Windows.Forms.Button button_PlayPause;
        private System.Windows.Forms.Button button_StepForward;
        private System.Windows.Forms.Button button_RightEnd;
    }
}
