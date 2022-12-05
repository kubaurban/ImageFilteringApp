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
            this.ApplyButton = new System.Windows.Forms.Button();
            this.RemovePolygonButton = new System.Windows.Forms.Button();
            this.AddPolygonButton = new System.Windows.Forms.Button();
            this.BrushButton = new System.Windows.Forms.Button();
            this.PaintbrushTrackBar = new System.Windows.Forms.TrackBar();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.BrushShapeLabel = new System.Windows.Forms.Label();
            this.FiltersBox = new System.Windows.Forms.GroupBox();
            this.GammaNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.BrightnessNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ContrastNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.NoneButton = new System.Windows.Forms.RadioButton();
            this.BezierButton = new System.Windows.Forms.RadioButton();
            this.ContrastButton = new System.Windows.Forms.RadioButton();
            this.GammaButton = new System.Windows.Forms.RadioButton();
            this.BrightnessButton = new System.Windows.Forms.RadioButton();
            this.NegativeButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.BrushShapesBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaintbrushTrackBar)).BeginInit();
            this.FiltersBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GammaNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.Canvas, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.BrushShapesBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LoadImageButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.BrushShapeLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.FiltersBox, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1270, 815);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.Canvas, 2);
            this.Canvas.Location = new System.Drawing.Point(384, 44);
            this.Canvas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Canvas.MaximumSize = new System.Drawing.Size(563, 767);
            this.Canvas.MinimumSize = new System.Drawing.Size(563, 767);
            this.Canvas.Name = "Canvas";
            this.tableLayoutPanel1.SetRowSpan(this.Canvas, 5);
            this.Canvas.Size = new System.Drawing.Size(563, 767);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnCanvasClick);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnCanvasClickedMouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnCanvasClickedMouseUp);
            // 
            // BrushShapesBox
            // 
            this.BrushShapesBox.Controls.Add(this.ApplyButton);
            this.BrushShapesBox.Controls.Add(this.RemovePolygonButton);
            this.BrushShapesBox.Controls.Add(this.AddPolygonButton);
            this.BrushShapesBox.Controls.Add(this.BrushButton);
            this.BrushShapesBox.Controls.Add(this.PaintbrushTrackBar);
            this.BrushShapesBox.Location = new System.Drawing.Point(3, 4);
            this.BrushShapesBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BrushShapesBox.Name = "BrushShapesBox";
            this.BrushShapesBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.SetRowSpan(this.BrushShapesBox, 2);
            this.BrushShapesBox.Size = new System.Drawing.Size(374, 129);
            this.BrushShapesBox.TabIndex = 1;
            this.BrushShapesBox.TabStop = false;
            this.BrushShapesBox.Text = "Brush shapes";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Enabled = false;
            this.ApplyButton.Location = new System.Drawing.Point(288, 25);
            this.ApplyButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(80, 93);
            this.ApplyButton.TabIndex = 3;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.OnApplyButtonClick);
            // 
            // RemovePolygonButton
            // 
            this.RemovePolygonButton.Image = ((System.Drawing.Image)(resources.GetObject("RemovePolygonButton.Image")));
            this.RemovePolygonButton.Location = new System.Drawing.Point(202, 25);
            this.RemovePolygonButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RemovePolygonButton.Name = "RemovePolygonButton";
            this.RemovePolygonButton.Size = new System.Drawing.Size(80, 93);
            this.RemovePolygonButton.TabIndex = 2;
            this.RemovePolygonButton.UseVisualStyleBackColor = true;
            this.RemovePolygonButton.Click += new System.EventHandler(this.OnRemovePolygonButtonClick);
            // 
            // AddPolygonButton
            // 
            this.AddPolygonButton.Image = ((System.Drawing.Image)(resources.GetObject("AddPolygonButton.Image")));
            this.AddPolygonButton.Location = new System.Drawing.Point(116, 25);
            this.AddPolygonButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AddPolygonButton.Name = "AddPolygonButton";
            this.AddPolygonButton.Size = new System.Drawing.Size(80, 93);
            this.AddPolygonButton.TabIndex = 1;
            this.AddPolygonButton.UseVisualStyleBackColor = true;
            this.AddPolygonButton.Click += new System.EventHandler(this.OnAddPolygonButtonClick);
            // 
            // BrushButton
            // 
            this.BrushButton.Image = ((System.Drawing.Image)(resources.GetObject("BrushButton.Image")));
            this.BrushButton.Location = new System.Drawing.Point(11, 25);
            this.BrushButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BrushButton.Name = "BrushButton";
            this.BrushButton.Size = new System.Drawing.Size(80, 60);
            this.BrushButton.TabIndex = 0;
            this.BrushButton.UseVisualStyleBackColor = true;
            this.BrushButton.Click += new System.EventHandler(this.OnPaintBrushButtonClick);
            // 
            // PaintbrushTrackBar
            // 
            this.PaintbrushTrackBar.Location = new System.Drawing.Point(4, 87);
            this.PaintbrushTrackBar.Maximum = 100;
            this.PaintbrushTrackBar.Minimum = 10;
            this.PaintbrushTrackBar.Name = "PaintbrushTrackBar";
            this.PaintbrushTrackBar.Size = new System.Drawing.Size(95, 56);
            this.PaintbrushTrackBar.SmallChange = 10;
            this.PaintbrushTrackBar.TabIndex = 4;
            this.PaintbrushTrackBar.TickFrequency = 5;
            this.PaintbrushTrackBar.Value = 55;
            this.PaintbrushTrackBar.ValueChanged += new System.EventHandler(this.OnPaintbrushTrackBarValueChanged);
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.LoadImageButton.Location = new System.Drawing.Point(828, 3);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(120, 34);
            this.LoadImageButton.TabIndex = 3;
            this.LoadImageButton.Text = "Load image";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.OnLoadImageButtonClick);
            // 
            // BrushShapeLabel
            // 
            this.BrushShapeLabel.AutoSize = true;
            this.BrushShapeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.BrushShapeLabel.Location = new System.Drawing.Point(384, 0);
            this.BrushShapeLabel.Name = "BrushShapeLabel";
            this.BrushShapeLabel.Size = new System.Drawing.Size(0, 40);
            this.BrushShapeLabel.TabIndex = 4;
            this.BrushShapeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FiltersBox
            // 
            this.FiltersBox.Controls.Add(this.GammaNumericUpDown);
            this.FiltersBox.Controls.Add(this.BrightnessNumericUpDown);
            this.FiltersBox.Controls.Add(this.ContrastNumericUpDown);
            this.FiltersBox.Controls.Add(this.NoneButton);
            this.FiltersBox.Controls.Add(this.BezierButton);
            this.FiltersBox.Controls.Add(this.ContrastButton);
            this.FiltersBox.Controls.Add(this.GammaButton);
            this.FiltersBox.Controls.Add(this.BrightnessButton);
            this.FiltersBox.Controls.Add(this.NegativeButton);
            this.FiltersBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FiltersBox.Location = new System.Drawing.Point(3, 141);
            this.FiltersBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FiltersBox.Name = "FiltersBox";
            this.FiltersBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FiltersBox.Size = new System.Drawing.Size(375, 125);
            this.FiltersBox.TabIndex = 2;
            this.FiltersBox.TabStop = false;
            this.FiltersBox.Text = "Filters";
            // 
            // GammaNumericUpDown
            // 
            this.GammaNumericUpDown.DecimalPlaces = 2;
            this.GammaNumericUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.GammaNumericUpDown.Location = new System.Drawing.Point(311, 90);
            this.GammaNumericUpDown.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            65536});
            this.GammaNumericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.GammaNumericUpDown.Name = "GammaNumericUpDown";
            this.GammaNumericUpDown.Size = new System.Drawing.Size(57, 27);
            this.GammaNumericUpDown.TabIndex = 8;
            this.GammaNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.GammaNumericUpDown.ValueChanged += new System.EventHandler(this.OnGammaNumericUpDownValueChanged);
            // 
            // BrightnessNumericUpDown
            // 
            this.BrightnessNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.BrightnessNumericUpDown.Location = new System.Drawing.Point(311, 59);
            this.BrightnessNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BrightnessNumericUpDown.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.BrightnessNumericUpDown.Name = "BrightnessNumericUpDown";
            this.BrightnessNumericUpDown.Size = new System.Drawing.Size(57, 27);
            this.BrightnessNumericUpDown.TabIndex = 7;
            this.BrightnessNumericUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.BrightnessNumericUpDown.ValueChanged += new System.EventHandler(this.OnBrightnessNumericUpDownValueChanged);
            // 
            // ContrastNumericUpDown
            // 
            this.ContrastNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ContrastNumericUpDown.Location = new System.Drawing.Point(311, 27);
            this.ContrastNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ContrastNumericUpDown.Name = "ContrastNumericUpDown";
            this.ContrastNumericUpDown.Size = new System.Drawing.Size(57, 27);
            this.ContrastNumericUpDown.TabIndex = 6;
            this.ContrastNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ContrastNumericUpDown.ValueChanged += new System.EventHandler(this.OnContrastNumericUpDownValueChanged);
            // 
            // NoneButton
            // 
            this.NoneButton.AutoSize = true;
            this.NoneButton.Location = new System.Drawing.Point(11, 27);
            this.NoneButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NoneButton.Name = "NoneButton";
            this.NoneButton.Size = new System.Drawing.Size(66, 24);
            this.NoneButton.TabIndex = 5;
            this.NoneButton.TabStop = true;
            this.NoneButton.Text = "None";
            this.NoneButton.UseVisualStyleBackColor = true;
            this.NoneButton.CheckedChanged += new System.EventHandler(this.OnNoneFilterCheckedChanged);
            // 
            // BezierButton
            // 
            this.BezierButton.AutoSize = true;
            this.BezierButton.Location = new System.Drawing.Point(11, 90);
            this.BezierButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BezierButton.Name = "BezierButton";
            this.BezierButton.Size = new System.Drawing.Size(110, 24);
            this.BezierButton.TabIndex = 4;
            this.BezierButton.TabStop = true;
            this.BezierButton.Text = "Bezier curve";
            this.BezierButton.UseVisualStyleBackColor = true;
            this.BezierButton.CheckedChanged += new System.EventHandler(this.OnBezierFilterCheckedChanged);
            // 
            // ContrastButton
            // 
            this.ContrastButton.AutoSize = true;
            this.ContrastButton.Location = new System.Drawing.Point(144, 27);
            this.ContrastButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ContrastButton.Name = "ContrastButton";
            this.ContrastButton.Size = new System.Drawing.Size(85, 24);
            this.ContrastButton.TabIndex = 3;
            this.ContrastButton.TabStop = true;
            this.ContrastButton.Text = "Contrast";
            this.ContrastButton.UseVisualStyleBackColor = true;
            this.ContrastButton.CheckedChanged += new System.EventHandler(this.OnContrastFilterCheckedChanged);
            // 
            // GammaButton
            // 
            this.GammaButton.AutoSize = true;
            this.GammaButton.Location = new System.Drawing.Point(144, 90);
            this.GammaButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GammaButton.Name = "GammaButton";
            this.GammaButton.Size = new System.Drawing.Size(153, 24);
            this.GammaButton.TabIndex = 2;
            this.GammaButton.TabStop = true;
            this.GammaButton.Text = "Gamma correction";
            this.GammaButton.UseVisualStyleBackColor = true;
            this.GammaButton.CheckedChanged += new System.EventHandler(this.OnGammaCorrectionFilterCheckedChanged);
            // 
            // BrightnessButton
            // 
            this.BrightnessButton.AutoSize = true;
            this.BrightnessButton.Location = new System.Drawing.Point(144, 59);
            this.BrightnessButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BrightnessButton.Name = "BrightnessButton";
            this.BrightnessButton.Size = new System.Drawing.Size(98, 24);
            this.BrightnessButton.TabIndex = 1;
            this.BrightnessButton.TabStop = true;
            this.BrightnessButton.Text = "Brightness";
            this.BrightnessButton.UseVisualStyleBackColor = true;
            this.BrightnessButton.CheckedChanged += new System.EventHandler(this.OnBrightnessFilterCheckedChanged);
            // 
            // NegativeButton
            // 
            this.NegativeButton.AutoSize = true;
            this.NegativeButton.Location = new System.Drawing.Point(11, 59);
            this.NegativeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NegativeButton.Name = "NegativeButton";
            this.NegativeButton.Size = new System.Drawing.Size(90, 24);
            this.NegativeButton.TabIndex = 0;
            this.NegativeButton.TabStop = true;
            this.NegativeButton.Text = "Negative";
            this.NegativeButton.UseVisualStyleBackColor = true;
            this.NegativeButton.CheckedChanged += new System.EventHandler(this.OnNegativeFilterCheckedChanged);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 838);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1313, 885);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1313, 885);
            this.Name = "GUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageFiltrator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.BrushShapesBox.ResumeLayout(false);
            this.BrushShapesBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaintbrushTrackBar)).EndInit();
            this.FiltersBox.ResumeLayout(false);
            this.FiltersBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GammaNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox Canvas;
        private GroupBox BrushShapesBox;
        private GroupBox FiltersBox;
        private RadioButton BezierButton;
        private RadioButton ContrastButton;
        private RadioButton GammaButton;
        private RadioButton BrightnessButton;
        private RadioButton NegativeButton;
        private Button RemovePolygonButton;
        private Button AddPolygonButton;
        private Button BrushButton;
        private Button LoadImageButton;
        private Label BrushShapeLabel;
        private RadioButton NoneButton;
        private Button ApplyButton;
        private NumericUpDown GammaNumericUpDown;
        private NumericUpDown BrightnessNumericUpDown;
        private NumericUpDown ContrastNumericUpDown;
        private TrackBar PaintbrushTrackBar;
    }
}