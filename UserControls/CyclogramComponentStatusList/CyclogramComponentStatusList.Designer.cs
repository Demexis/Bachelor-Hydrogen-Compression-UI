
namespace Bachelor_Project.UserControls.CyclogramComponentStatusList
{
    partial class CyclogramComponentStatusList
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
            this.splitContainer13 = new System.Windows.Forms.SplitContainer();
            this.label_SelectedStep = new System.Windows.Forms.Label();
            this.tableLayoutPanel_ComponentsAndStatuses = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).BeginInit();
            this.splitContainer13.Panel1.SuspendLayout();
            this.splitContainer13.Panel2.SuspendLayout();
            this.splitContainer13.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer13
            // 
            this.splitContainer13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer13.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer13.Location = new System.Drawing.Point(0, 0);
            this.splitContainer13.Name = "splitContainer13";
            this.splitContainer13.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer13.Panel1
            // 
            this.splitContainer13.Panel1.Controls.Add(this.label_SelectedStep);
            // 
            // splitContainer13.Panel2
            // 
            this.splitContainer13.Panel2.AutoScroll = true;
            this.splitContainer13.Panel2.Controls.Add(this.tableLayoutPanel_ComponentsAndStatuses);
            this.splitContainer13.Size = new System.Drawing.Size(441, 295);
            this.splitContainer13.SplitterDistance = 42;
            this.splitContainer13.TabIndex = 1;
            // 
            // label_SelectedStep
            // 
            this.label_SelectedStep.AutoSize = true;
            this.label_SelectedStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_SelectedStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_SelectedStep.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_SelectedStep.Location = new System.Drawing.Point(0, 0);
            this.label_SelectedStep.Name = "label_SelectedStep";
            this.label_SelectedStep.Size = new System.Drawing.Size(132, 24);
            this.label_SelectedStep.TabIndex = 4;
            this.label_SelectedStep.Text = "Selected Step:";
            // 
            // tableLayoutPanel_ComponentsAndStatuses
            // 
            this.tableLayoutPanel_ComponentsAndStatuses.ColumnCount = 1;
            this.tableLayoutPanel_ComponentsAndStatuses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_ComponentsAndStatuses.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel_ComponentsAndStatuses.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_ComponentsAndStatuses.Name = "tableLayoutPanel_ComponentsAndStatuses";
            this.tableLayoutPanel_ComponentsAndStatuses.RowCount = 1;
            this.tableLayoutPanel_ComponentsAndStatuses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_ComponentsAndStatuses.Size = new System.Drawing.Size(441, 50);
            this.tableLayoutPanel_ComponentsAndStatuses.TabIndex = 0;
            // 
            // CyclogramComponentStatusList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.splitContainer13);
            this.Name = "CyclogramComponentStatusList";
            this.Size = new System.Drawing.Size(441, 295);
            this.splitContainer13.Panel1.ResumeLayout(false);
            this.splitContainer13.Panel1.PerformLayout();
            this.splitContainer13.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).EndInit();
            this.splitContainer13.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer13;
        private System.Windows.Forms.Label label_SelectedStep;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_ComponentsAndStatuses;
    }
}
