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
            panel1 = new Panel();
            LiveTelemetryLabel = new Label();
            TyreCompundLabel = new Label();
            ERSModeLabel = new Label();
            ERSStorageLabel = new Label();
            SuspendLayout();
            // 
            // ListenerButton
            // 
            ListenerButton.Location = new Point(226, 543);
            ListenerButton.Name = "ListenerButton";
            ListenerButton.Size = new Size(75, 41);
            ListenerButton.TabIndex = 0;
            ListenerButton.Text = "Start Session";
            ListenerButton.UseVisualStyleBackColor = true;
            ListenerButton.Click += ListenerButton_Click;
            // 
            // StatusBox
            // 
            StatusBox.Location = new Point(12, 429);
            StatusBox.Name = "StatusBox";
            StatusBox.Size = new Size(501, 108);
            StatusBox.TabIndex = 1;
            StatusBox.Text = "";
            // 
            // VerLabel
            // 
            VerLabel.AutoSize = true;
            VerLabel.Location = new Point(12, 9);
            VerLabel.Name = "VerLabel";
            VerLabel.Size = new Size(77, 15);
            VerLabel.TabIndex = 2;
            VerLabel.Text = "UDP Format: ";
            // 
            // SessionTimeLabel
            // 
            SessionTimeLabel.AutoSize = true;
            SessionTimeLabel.Location = new Point(12, 35);
            SessionTimeLabel.Name = "SessionTimeLabel";
            SessionTimeLabel.Size = new Size(118, 15);
            SessionTimeLabel.TabIndex = 4;
            SessionTimeLabel.Text = "Session Time Elapsed";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(525, 66);
            panel1.TabIndex = 5;
            // 
            // LiveTelemetryLabel
            // 
            LiveTelemetryLabel.AutoSize = true;
            LiveTelemetryLabel.Location = new Point(207, 81);
            LiveTelemetryLabel.Name = "LiveTelemetryLabel";
            LiveTelemetryLabel.Size = new Size(109, 15);
            LiveTelemetryLabel.TabIndex = 6;
            LiveTelemetryLabel.Text = "Live Telemetry Data";
            // 
            // TyreCompundLabel
            // 
            TyreCompundLabel.AutoSize = true;
            TyreCompundLabel.Location = new Point(21, 132);
            TyreCompundLabel.Name = "TyreCompundLabel";
            TyreCompundLabel.Size = new Size(98, 15);
            TyreCompundLabel.TabIndex = 7;
            TyreCompundLabel.Text = "Tyre Compound: ";
            // 
            // ERSModeLabel
            // 
            ERSModeLabel.AutoSize = true;
            ERSModeLabel.Location = new Point(21, 158);
            ERSModeLabel.Name = "ERSModeLabel";
            ERSModeLabel.Size = new Size(103, 15);
            ERSModeLabel.TabIndex = 8;
            ERSModeLabel.Text = "ERS Deploy Mode:";
            // 
            // ERSStorageLabel
            // 
            ERSStorageLabel.AutoSize = true;
            ERSStorageLabel.Location = new Point(21, 183);
            ERSStorageLabel.Name = "ERSStorageLabel";
            ERSStorageLabel.Size = new Size(72, 15);
            ERSStorageLabel.TabIndex = 9;
            ERSStorageLabel.Text = "ERS Storage:";
            // 
            // mainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 596);
            Controls.Add(ERSStorageLabel);
            Controls.Add(ERSModeLabel);
            Controls.Add(TyreCompundLabel);
            Controls.Add(LiveTelemetryLabel);
            Controls.Add(SessionTimeLabel);
            Controls.Add(VerLabel);
            Controls.Add(StatusBox);
            Controls.Add(ListenerButton);
            Controls.Add(panel1);
            Name = "mainWindow";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ListenerButton;
        private RichTextBox StatusBox;
        private Label VerLabel;
        private Label SessionTimeLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panel1;
        private Label LiveTelemetryLabel;
        private Label TyreCompundLabel;
        private Label ERSModeLabel;
        private Label ERSStorageLabel;
    }
}