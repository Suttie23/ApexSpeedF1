namespace F1_Telemetry_App
{
    partial class mainWindow
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
            ListenerButton = new Button();
            StatusBox = new RichTextBox();
            VerLabel = new Label();
            SessionTimeLabel = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            TyreCompundLabel = new Label();
            ERSModeLabel = new Label();
            ERSStorageLabel = new Label();
            pieChart1 = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            DebugPanel = new Panel();
            DebugPanelTop = new Panel();
            DebugPanel.SuspendLayout();
            SuspendLayout();
            // 
            // ListenerButton
            // 
            ListenerButton.FlatAppearance.BorderSize = 0;
            ListenerButton.FlatStyle = FlatStyle.Flat;
            ListenerButton.ForeColor = SystemColors.ButtonHighlight;
            ListenerButton.Location = new Point(0, 76);
            ListenerButton.Name = "ListenerButton";
            ListenerButton.Size = new Size(220, 60);
            ListenerButton.TabIndex = 0;
            ListenerButton.Text = "Start Session";
            ListenerButton.UseVisualStyleBackColor = true;
            ListenerButton.Click += ListenerButton_Click;
            // 
            // StatusBox
            // 
            StatusBox.Location = new Point(7, 922);
            StatusBox.Name = "StatusBox";
            StatusBox.Size = new Size(205, 108);
            StatusBox.TabIndex = 1;
            StatusBox.Text = "";
            // 
            // VerLabel
            // 
            VerLabel.AutoSize = true;
            VerLabel.Location = new Point(242, 9);
            VerLabel.Name = "VerLabel";
            VerLabel.Size = new Size(77, 15);
            VerLabel.TabIndex = 2;
            VerLabel.Text = "UDP Format: ";
            // 
            // SessionTimeLabel
            // 
            SessionTimeLabel.AutoSize = true;
            SessionTimeLabel.Location = new Point(242, 35);
            SessionTimeLabel.Name = "SessionTimeLabel";
            SessionTimeLabel.Size = new Size(118, 15);
            SessionTimeLabel.TabIndex = 4;
            SessionTimeLabel.Text = "Session Time Elapsed";
            // 
            // TyreCompundLabel
            // 
            TyreCompundLabel.AutoSize = true;
            TyreCompundLabel.Location = new Point(363, 521);
            TyreCompundLabel.Name = "TyreCompundLabel";
            TyreCompundLabel.Size = new Size(98, 15);
            TyreCompundLabel.TabIndex = 7;
            TyreCompundLabel.Text = "Tyre Compound: ";
            // 
            // ERSModeLabel
            // 
            ERSModeLabel.AutoSize = true;
            ERSModeLabel.Location = new Point(358, 456);
            ERSModeLabel.Name = "ERSModeLabel";
            ERSModeLabel.Size = new Size(103, 15);
            ERSModeLabel.TabIndex = 8;
            ERSModeLabel.Text = "ERS Deploy Mode:";
            // 
            // ERSStorageLabel
            // 
            ERSStorageLabel.AutoSize = true;
            ERSStorageLabel.Location = new Point(370, 401);
            ERSStorageLabel.Name = "ERSStorageLabel";
            ERSStorageLabel.Size = new Size(69, 15);
            ERSStorageLabel.TabIndex = 9;
            ERSStorageLabel.Text = "ERS Storage";
            // 
            // pieChart1
            // 
            pieChart1.BorderStyle = BorderStyle.FixedSingle;
            pieChart1.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            pieChart1.InitialRotation = 0D;
            pieChart1.IsClockwise = true;
            pieChart1.Location = new Point(255, 85);
            pieChart1.Margin = new Padding(2);
            pieChart1.MaxAngle = 360D;
            pieChart1.Name = "pieChart1";
            pieChart1.Size = new Size(300, 314);
            pieChart1.TabIndex = 10;
            pieChart1.Total = null;
            // 
            // DebugPanel
            // 
            DebugPanel.BackColor = Color.FromArgb(255, 24, 1);
            DebugPanel.Controls.Add(DebugPanelTop);
            DebugPanel.Controls.Add(ListenerButton);
            DebugPanel.Controls.Add(StatusBox);
            DebugPanel.Dock = DockStyle.Left;
            DebugPanel.Location = new Point(0, 0);
            DebugPanel.Name = "DebugPanel";
            DebugPanel.Size = new Size(220, 1041);
            DebugPanel.TabIndex = 11;
            // 
            // DebugPanelTop
            // 
            DebugPanelTop.BackColor = Color.FromArgb(210, 24, 1);
            DebugPanelTop.Dock = DockStyle.Top;
            DebugPanelTop.Location = new Point(0, 0);
            DebugPanelTop.Name = "DebugPanelTop";
            DebugPanelTop.Size = new Size(220, 80);
            DebugPanelTop.TabIndex = 0;
            // 
            // mainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(DebugPanel);
            Controls.Add(pieChart1);
            Controls.Add(ERSStorageLabel);
            Controls.Add(ERSModeLabel);
            Controls.Add(TyreCompundLabel);
            Controls.Add(SessionTimeLabel);
            Controls.Add(VerLabel);
            Name = "mainWindow";
            Text = "Form1";
            DebugPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ListenerButton;
        private RichTextBox StatusBox;
        private Label VerLabel;
        private Label SessionTimeLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label TyreCompundLabel;
        private Label ERSModeLabel;
        private Label ERSStorageLabel;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieChart1;
        private Panel DebugPanel;
        private Panel DebugPanelTop;
    }
}