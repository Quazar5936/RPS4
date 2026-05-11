using System.ComponentModel;
namespace KR33
{
    partial class MainForm
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
        ///  the contents of this method with the code editor
        /// </summary>
        private void InitializeComponent()
        {
            BuildChartButton = new Button();
            LeftBoundLabel = new Label();
            ALabel = new Label();
            RightBoundLabel = new Label();
            LeftBoundTextBox = new TextBox();
            RightBoundTextBox = new TextBox();
            ATextBox = new TextBox();
            StepTextBox = new TextBox();
            StepLabel = new Label();
            formsPlot = new ScottPlot.WinForms.FormsPlot();
            dataGridView = new DataGridView();
            pictureBox = new PictureBox();
            ExcelSaveButton = new Button();
            ExcelLoadButton = new Button();
            AuthorButton = new Button();
            ((ISupportInitialize)dataGridView).BeginInit();
            ((ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // BuildChartButton
            // 
            BuildChartButton.Location = new Point(12, 369);
            BuildChartButton.Name = "BuildChartButton";
            BuildChartButton.Size = new Size(244, 29);
            BuildChartButton.TabIndex = 0;
            BuildChartButton.Text = "Построить график";
            BuildChartButton.UseVisualStyleBackColor = true;
            BuildChartButton.Click += BuildChartButton_Click;
            // 
            // LeftBoundLabel
            // 
            LeftBoundLabel.AutoSize = true;
            LeftBoundLabel.Location = new Point(12, 228);
            LeftBoundLabel.Name = "LeftBoundLabel";
            LeftBoundLabel.Size = new Size(113, 20);
            LeftBoundLabel.TabIndex = 1;
            LeftBoundLabel.Text = "Левая граница";
            // 
            // ALabel
            // 
            ALabel.AutoSize = true;
            ALabel.Location = new Point(12, 339);
            ALabel.Name = "ALabel";
            ALabel.Size = new Size(116, 20);
            ALabel.TabIndex = 2;
            ALabel.Text = "Коэффициент a";
            // 
            // RightBoundLabel
            // 
            RightBoundLabel.AutoSize = true;
            RightBoundLabel.Location = new Point(12, 263);
            RightBoundLabel.Name = "RightBoundLabel";
            RightBoundLabel.Size = new Size(123, 20);
            RightBoundLabel.TabIndex = 3;
            RightBoundLabel.Text = "Правая граница";
            // 
            // LeftBoundTextBox
            // 
            LeftBoundTextBox.Location = new Point(155, 228);
            LeftBoundTextBox.Name = "LeftBoundTextBox";
            LeftBoundTextBox.Size = new Size(101, 27);
            LeftBoundTextBox.TabIndex = 4;
            // 
            // RightBoundTextBox
            // 
            RightBoundTextBox.Location = new Point(155, 263);
            RightBoundTextBox.Name = "RightBoundTextBox";
            RightBoundTextBox.Size = new Size(101, 27);
            RightBoundTextBox.TabIndex = 5;
            // 
            // ATextBox
            // 
            ATextBox.Location = new Point(155, 336);
            ATextBox.Name = "ATextBox";
            ATextBox.Size = new Size(101, 27);
            ATextBox.TabIndex = 6;
            // 
            // StepTextBox
            // 
            StepTextBox.Location = new Point(155, 299);
            StepTextBox.Name = "StepTextBox";
            StepTextBox.Size = new Size(101, 27);
            StepTextBox.TabIndex = 7;
            // 
            // StepLabel
            // 
            StepLabel.AutoSize = true;
            StepLabel.Location = new Point(12, 302);
            StepLabel.Name = "StepLabel";
            StepLabel.Size = new Size(37, 20);
            StepLabel.TabIndex = 8;
            StepLabel.Text = "Шаг";
            // 
            // formsPlot
            // 
            formsPlot.Location = new Point(262, 12);
            formsPlot.Name = "formsPlot";
            formsPlot.Size = new Size(526, 484);
            formsPlot.TabIndex = 9;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 89);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(244, 123);
            dataGridView.TabIndex = 10;
            // 
            // pictureBox
            // 
            pictureBox.Image = Properties.Resources.Безымянный;
            pictureBox.InitialImage = null;
            pictureBox.Location = new Point(12, 21);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(244, 50);
            pictureBox.TabIndex = 11;
            pictureBox.TabStop = false;
            // 
            // ExcelSaveButton
            // 
            ExcelSaveButton.BackColor = Color.ForestGreen;
            ExcelSaveButton.Location = new Point(12, 404);
            ExcelSaveButton.Name = "ExcelSaveButton";
            ExcelSaveButton.Size = new Size(244, 29);
            ExcelSaveButton.TabIndex = 12;
            ExcelSaveButton.Text = "Сохранить в EXCEL";
            ExcelSaveButton.UseVisualStyleBackColor = false;
            ExcelSaveButton.Click += ExcelSaveButton_Click;
            // 
            // ExcelLoadButton
            // 
            ExcelLoadButton.BackColor = Color.Green;
            ExcelLoadButton.Location = new Point(12, 439);
            ExcelLoadButton.Name = "ExcelLoadButton";
            ExcelLoadButton.Size = new Size(244, 29);
            ExcelLoadButton.TabIndex = 13;
            ExcelLoadButton.Text = "Загрузить из Excel";
            ExcelLoadButton.UseVisualStyleBackColor = false;
            ExcelLoadButton.Click += ExcelLoadButton_Click;
            // 
            // AuthorButton
            // 
            AuthorButton.Location = new Point(12, 474);
            AuthorButton.Name = "AuthorButton";
            AuthorButton.Size = new Size(244, 29);
            AuthorButton.TabIndex = 14;
            AuthorButton.Text = "Об авторе";
            AuthorButton.UseVisualStyleBackColor = true;
            AuthorButton.Click += AuthorButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 508);
            Controls.Add(AuthorButton);
            Controls.Add(ExcelLoadButton);
            Controls.Add(ExcelSaveButton);
            Controls.Add(pictureBox);
            Controls.Add(dataGridView);
            Controls.Add(formsPlot);
            Controls.Add(StepLabel);
            Controls.Add(StepTextBox);
            Controls.Add(ATextBox);
            Controls.Add(RightBoundTextBox);
            Controls.Add(LeftBoundTextBox);
            Controls.Add(RightBoundLabel);
            Controls.Add(ALabel);
            Controls.Add(LeftBoundLabel);
            Controls.Add(BuildChartButton);
            Name = "MainForm";
            Text = "Кубика Чирнгауза";
            ((ISupportInitialize)dataGridView).EndInit();
            ((ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BuildChartButton;
        private Label LeftBoundLabel;
        private Label ALabel;
        private Label RightBoundLabel;
        private TextBox LeftBoundTextBox;
        private TextBox RightBoundTextBox;
        private TextBox ATextBox;
        private TextBox StepTextBox;
        private Label StepLabel;
        private ScottPlot.WinForms.FormsPlot formsPlot;
        private DataGridView dataGridView;
        private BindingList<VisualData> visualData;
        private decimal A;
        private decimal Step;
        private const int ColX = 1, ColY = 2, ColminusY = 3, ColA = 4, ColStep = 5;
        private const decimal Epsilon = 1e-10m;
        private PictureBox pictureBox;
        private Button ExcelSaveButton;
        private Button ExcelLoadButton;
        private Button AuthorButton;
    }
}
