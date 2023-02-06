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
            SessionTime = new Label();
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
            StatusBox.Location = new Point(12, 381);
            StatusBox.Name = "StatusBox";
            StatusBox.Size = new Size(501, 156);
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
            // SessionTime
            // 
            SessionTime.AutoSize = true;
            SessionTime.Location = new Point(12, 35);
            SessionTime.Name = "SessionTime";
            SessionTime.Size = new Size(77, 15);
            SessionTime.TabIndex = 4;
            SessionTime.Text = "UDP Format: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 596);
            Controls.Add(SessionTime);
            Controls.Add(VerLabel);
            Controls.Add(StatusBox);
            Controls.Add(ListenerButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ListenerButton;
        private RichTextBox StatusBox;
        private Label VerLabel;
        private Label SessionTime;
    }
}