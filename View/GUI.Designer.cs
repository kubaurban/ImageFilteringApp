namespace View
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.BrushShapesBox = new System.Windows.Forms.GroupBox();
            this.RemovePolygonButton = new System.Windows.Forms.Button();
            this.AddPolygonButton = new System.Windows.Forms.Button();
            this.BrushButton = new System.Windows.Forms.Button();
            this.FiltersBox = new System.Windows.Forms.GroupBox();
            this.BrezierButton = new System.Windows.Forms.RadioButton();
            this.ContrastButton = new System.Windows.Forms.RadioButton();
            this.GammaButton = new System.Windows.Forms.RadioButton();
            this.BrightnessButton = new System.Windows.Forms.RadioButton();
            this.NegationButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.BrushShapesBox.SuspendLayout();
            this.FiltersBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.Canvas, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BrushShapesBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FiltersBox, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1111, 542);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Canvas.Location = new System.Drawing.Point(336, 3);
            this.Canvas.MaximumSize = new System.Drawing.Size(493, 536);
            this.Canvas.MinimumSize = new System.Drawing.Size(493, 536);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(493, 536);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            // 
            // BrushShapesBox
            // 
            this.BrushShapesBox.Controls.Add(this.RemovePolygonButton);
            this.BrushShapesBox.Controls.Add(this.AddPolygonButton);
            this.BrushShapesBox.Controls.Add(this.BrushButton);
            this.BrushShapesBox.Location = new System.Drawing.Point(3, 3);
            this.BrushShapesBox.Name = "BrushShapesBox";
            this.BrushShapesBox.Size = new System.Drawing.Size(327, 102);
            this.BrushShapesBox.TabIndex = 1;
            this.BrushShapesBox.TabStop = false;
            this.BrushShapesBox.Text = "Brush shapes";
            // 
            // RemovePolygonButton
            // 
            this.RemovePolygonButton.Image = ((System.Drawing.Image)(resources.GetObject("RemovePolygonButton.Image")));
            this.RemovePolygonButton.Location = new System.Drawing.Point(226, 22);
            this.RemovePolygonButton.Name = "RemovePolygonButton";
            this.RemovePolygonButton.Size = new System.Drawing.Size(70, 70);
            this.RemovePolygonButton.TabIndex = 2;
            this.RemovePolygonButton.UseVisualStyleBackColor = true;
            // 
            // AddPolygonButton
            // 
            this.AddPolygonButton.Image = ((System.Drawing.Image)(resources.GetObject("AddPolygonButton.Image")));
            this.AddPolygonButton.Location = new System.Drawing.Point(146, 22);
            this.AddPolygonButton.Name = "AddPolygonButton";
            this.AddPolygonButton.Size = new System.Drawing.Size(70, 70);
            this.AddPolygonButton.TabIndex = 1;
            this.AddPolygonButton.UseVisualStyleBackColor = true;
            // 
            // BrushButton
            // 
            this.BrushButton.Image = ((System.Drawing.Image)(resources.GetObject("BrushButton.Image")));
            this.BrushButton.Location = new System.Drawing.Point(30, 22);
            this.BrushButton.Name = "BrushButton";
            this.BrushButton.Size = new System.Drawing.Size(70, 70);
            this.BrushButton.TabIndex = 0;
            this.BrushButton.UseVisualStyleBackColor = true;
            // 
            // FiltersBox
            // 
            this.FiltersBox.Controls.Add(this.BrezierButton);
            this.FiltersBox.Controls.Add(this.ContrastButton);
            this.FiltersBox.Controls.Add(this.GammaButton);
            this.FiltersBox.Controls.Add(this.BrightnessButton);
            this.FiltersBox.Controls.Add(this.NegationButton);
            this.FiltersBox.Location = new System.Drawing.Point(3, 111);
            this.FiltersBox.Name = "FiltersBox";
            this.tableLayoutPanel1.SetRowSpan(this.FiltersBox, 2);
            this.FiltersBox.Size = new System.Drawing.Size(327, 156);
            this.FiltersBox.TabIndex = 2;
            this.FiltersBox.TabStop = false;
            this.FiltersBox.Text = "Filters";
            // 
            // BrezierButton
            // 
            this.BrezierButton.AutoSize = true;
            this.BrezierButton.Location = new System.Drawing.Point(6, 125);
            this.BrezierButton.Name = "BrezierButton";
            this.BrezierButton.Size = new System.Drawing.Size(108, 19);
            this.BrezierButton.TabIndex = 4;
            this.BrezierButton.TabStop = true;
            this.BrezierButton.Text = "Brezier function";
            this.BrezierButton.UseVisualStyleBackColor = true;
            // 
            // ContrastButton
            // 
            this.ContrastButton.AutoSize = true;
            this.ContrastButton.Location = new System.Drawing.Point(6, 100);
            this.ContrastButton.Name = "ContrastButton";
            this.ContrastButton.Size = new System.Drawing.Size(70, 19);
            this.ContrastButton.TabIndex = 3;
            this.ContrastButton.TabStop = true;
            this.ContrastButton.Text = "Contrast";
            this.ContrastButton.UseVisualStyleBackColor = true;
            // 
            // GammaButton
            // 
            this.GammaButton.AutoSize = true;
            this.GammaButton.Location = new System.Drawing.Point(6, 75);
            this.GammaButton.Name = "GammaButton";
            this.GammaButton.Size = new System.Drawing.Size(124, 19);
            this.GammaButton.TabIndex = 2;
            this.GammaButton.TabStop = true;
            this.GammaButton.Text = "Gamma correction";
            this.GammaButton.UseVisualStyleBackColor = true;
            // 
            // BrightnessButton
            // 
            this.BrightnessButton.AutoSize = true;
            this.BrightnessButton.Location = new System.Drawing.Point(6, 50);
            this.BrightnessButton.Name = "BrightnessButton";
            this.BrightnessButton.Size = new System.Drawing.Size(80, 19);
            this.BrightnessButton.TabIndex = 1;
            this.BrightnessButton.TabStop = true;
            this.BrightnessButton.Text = "Brightness";
            this.BrightnessButton.UseVisualStyleBackColor = true;
            // 
            // NegationButton
            // 
            this.NegationButton.AutoSize = true;
            this.NegationButton.Location = new System.Drawing.Point(6, 25);
            this.NegationButton.Name = "NegationButton";
            this.NegationButton.Size = new System.Drawing.Size(74, 19);
            this.NegationButton.TabIndex = 0;
            this.NegationButton.TabStop = true;
            this.NegationButton.Text = "Negation";
            this.NegationButton.UseVisualStyleBackColor = true;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1151, 605);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1151, 605);
            this.Name = "GUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageFiltrator";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.BrushShapesBox.ResumeLayout(false);
            this.FiltersBox.ResumeLayout(false);
            this.FiltersBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox Canvas;
        private GroupBox BrushShapesBox;
        private GroupBox FiltersBox;
        private RadioButton BrezierButton;
        private RadioButton ContrastButton;
        private RadioButton GammaButton;
        private RadioButton BrightnessButton;
        private RadioButton NegationButton;
        private Button RemovePolygonButton;
        private Button AddPolygonButton;
        private Button BrushButton;
    }
}