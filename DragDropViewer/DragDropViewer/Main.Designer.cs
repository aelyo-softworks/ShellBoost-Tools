
namespace DragDropViewer
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.textBoxDragZone = new System.Windows.Forms.TextBox();
            this.labelHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxDragZone
            // 
            this.textBoxDragZone.BackColor = System.Drawing.Color.White;
            this.textBoxDragZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDragZone.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDragZone.Location = new System.Drawing.Point(0, 0);
            this.textBoxDragZone.Multiline = true;
            this.textBoxDragZone.Name = "textBoxDragZone";
            this.textBoxDragZone.ReadOnly = true;
            this.textBoxDragZone.Size = new System.Drawing.Size(984, 761);
            this.textBoxDragZone.TabIndex = 0;
            // 
            // labelHint
            // 
            this.labelHint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelHint.AutoSize = true;
            this.labelHint.BackColor = System.Drawing.Color.White;
            this.labelHint.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelHint.Location = new System.Drawing.Point(8, 8);
            this.labelHint.Name = "labelHint";
            this.labelHint.Size = new System.Drawing.Size(214, 30);
            this.labelHint.TabIndex = 1;
            this.labelHint.Text = "Drag Something Here";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.labelHint);
            this.Controls.Add(this.textBoxDragZone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drag & Drop View";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDragZone;
        private System.Windows.Forms.Label labelHint;
    }
}

