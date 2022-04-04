
namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    partial class DeviceGovernForm
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
            this.button_ActivateChannels = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_ActivateChannels
            // 
            this.button_ActivateChannels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.button_ActivateChannels.FlatAppearance.BorderSize = 0;
            this.button_ActivateChannels.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(61)))), ((int)(((byte)(82)))));
            this.button_ActivateChannels.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(111)))), ((int)(((byte)(162)))));
            this.button_ActivateChannels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ActivateChannels.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ActivateChannels.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_ActivateChannels.Location = new System.Drawing.Point(46, 55);
            this.button_ActivateChannels.Margin = new System.Windows.Forms.Padding(4);
            this.button_ActivateChannels.Name = "button_ActivateChannels";
            this.button_ActivateChannels.Size = new System.Drawing.Size(182, 47);
            this.button_ActivateChannels.TabIndex = 5;
            this.button_ActivateChannels.Text = "Activate Channels";
            this.button_ActivateChannels.UseVisualStyleBackColor = false;
            this.button_ActivateChannels.Click += new System.EventHandler(this.button_ActivateChannels_Click);
            // 
            // DeviceGovernForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_ActivateChannels);
            this.Name = "DeviceGovernForm";
            this.Text = "DeviceGovernForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_ActivateChannels;
    }
}