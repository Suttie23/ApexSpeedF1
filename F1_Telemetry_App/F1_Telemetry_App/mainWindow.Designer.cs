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
            DebugBox = new RichTextBox();
            VerLabel = new Label();
            SessionTimeLabel = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            TyreCompundLabel = new Label();
            ERSModeLabel = new Label();
            ERSStorageLabel = new Label();
            ERSGauge = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            DebugPanel = new Panel();
            DebugPanelTop = new Panel();
            ThrottleGauge = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            SpeedGauge = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            BrakeGauge = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            ThrottleLabel = new Label();
            SpeedLabel = new Label();
            BrakeLabel = new Label();
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
            // DebugBox
            // 
            DebugBox.Location = new Point(7, 922);
            DebugBox.Name = "DebugBox";
            DebugBox.Size = new Size(205, 108);
            DebugBox.TabIndex = 1;
            DebugBox.Text = "";
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
            TyreCompundLabel.Location = new Point(1773, 9);
            TyreCompundLabel.Name = "TyreCompundLabel";
            TyreCompundLabel.Size = new Size(98, 15);
            TyreCompundLabel.TabIndex = 7;
            TyreCompundLabel.Text = "Tyre Compound: ";
            // 
            // ERSModeLabel
            // 
            ERSModeLabel.AutoSize = true;
            ERSModeLabel.Location = new Point(1043, 822);
            ERSModeLabel.Name = "ERSModeLabel";
            ERSModeLabel.Size = new Size(103, 15);
            ERSModeLabel.TabIndex = 8;
            ERSModeLabel.Text = "ERS Deploy Mode:";
            // 
            // ERSStorageLabel
            // 
            ERSStorageLabel.AutoSize = true;
            ERSStorageLabel.Location = new Point(1056, 793);
            ERSStorageLabel.Name = "ERSStorageLabel";
            ERSStorageLabel.Size = new Size(69, 15);
            ERSStorageLabel.TabIndex = 9;
            ERSStorageLabel.Text = "ERS Storage";
            // 
            // ERSGauge
            // 
            ERSGauge.BorderStyle = BorderStyle.FixedSingle;
            ERSGauge.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            ERSGauge.InitialRotation = 0D;
            ERSGauge.IsClockwise = true;
            ERSGauge.Location = new Point(942, 477);
            ERSGauge.Margin = new Padding(2);
            ERSGauge.MaxAngle = 360D;
            ERSGauge.Name = "ERSGauge";
            ERSGauge.Size = new Size(300, 314);
            ERSGauge.TabIndex = 10;
            ERSGauge.Total = null;
            // 
            // DebugPanel
            // 
            DebugPanel.BackColor = Color.FromArgb(255, 24, 1);
            DebugPanel.Controls.Add(DebugPanelTop);
            DebugPanel.Controls.Add(ListenerButton);
            DebugPanel.Controls.Add(DebugBox);
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
            // ThrottleGauge
            // 
            ThrottleGauge.BorderStyle = BorderStyle.FixedSingle;
            ThrottleGauge.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            ThrottleGauge.InitialRotation = 0D;
            ThrottleGauge.IsClockwise = true;
            ThrottleGauge.Location = new Point(282, 76);
            ThrottleGauge.Margin = new Padding(2);
            ThrottleGauge.MaxAngle = 360D;
            ThrottleGauge.Name = "ThrottleGauge";
            ThrottleGauge.Size = new Size(300, 314);
            ThrottleGauge.TabIndex = 12;
            ThrottleGauge.Total = null;
            // 
            // SpeedGauge
            // 
            SpeedGauge.BorderStyle = BorderStyle.FixedSingle;
            SpeedGauge.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            SpeedGauge.InitialRotation = 0D;
            SpeedGauge.IsClockwise = true;
            SpeedGauge.Location = new Point(942, 76);
            SpeedGauge.Margin = new Padding(2);
            SpeedGauge.MaxAngle = 360D;
            SpeedGauge.Name = "SpeedGauge";
            SpeedGauge.Size = new Size(300, 314);
            SpeedGauge.TabIndex = 13;
            SpeedGauge.Total = null;
            // 
            // BrakeGauge
            // 
            BrakeGauge.BorderStyle = BorderStyle.FixedSingle;
            BrakeGauge.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            BrakeGauge.InitialRotation = 0D;
            BrakeGauge.IsClockwise = true;
            BrakeGauge.Location = new Point(1571, 76);
            BrakeGauge.Margin = new Padding(2);
            BrakeGauge.MaxAngle = 360D;
            BrakeGauge.Name = "BrakeGauge";
            BrakeGauge.Size = new Size(300, 314);
            BrakeGauge.TabIndex = 14;
            BrakeGauge.Total = null;
            // 
            // ThrottleLabel
            // 
            ThrottleLabel.AutoSize = true;
            ThrottleLabel.Location = new Point(409, 392);
            ThrottleLabel.Name = "ThrottleLabel";
            ThrottleLabel.Size = new Size(48, 15);
            ThrottleLabel.TabIndex = 15;
            ThrottleLabel.Text = "Throttle";
            // 
            // SpeedLabel
            // 
            SpeedLabel.AutoSize = true;
            SpeedLabel.Location = new Point(1056, 392);
            SpeedLabel.Name = "SpeedLabel";
            SpeedLabel.Size = new Size(77, 15);
            SpeedLabel.TabIndex = 16;
            SpeedLabel.Text = "Speed (MPH)";
            // 
            // BrakeLabel
            // 
            BrakeLabel.AutoSize = true;
            BrakeLabel.Location = new Point(1695, 392);
            BrakeLabel.Name = "BrakeLabel";
            BrakeLabel.Size = new Size(83, 15);
            BrakeLabel.TabIndex = 17;
            BrakeLabel.Text = "Brake Pressure";
            // 
            // mainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(BrakeLabel);
            Controls.Add(SpeedLabel);
            Controls.Add(ThrottleLabel);
            Controls.Add(BrakeGauge);
            Controls.Add(SpeedGauge);
            Controls.Add(ThrottleGauge);
            Controls.Add(DebugPanel);
            Controls.Add(ERSGauge);
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
        private RichTextBox DebugBox;
        private Label VerLabel;
        private Label SessionTimeLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label TyreCompundLabel;
        private Label ERSModeLabel;
        private Label ERSStorageLabel;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart ERSGauge;
        private Panel DebugPanel;
        private Panel DebugPanelTop;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart ThrottleGauge;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart SpeedGauge;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart BrakeGauge;
        private Label ThrottleLabel;
        private Label SpeedLabel;
        private Label BrakeLabel;
    }
}