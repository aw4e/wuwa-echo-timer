namespace EchoM
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            substatLabel = new Label();
            substatDropdown = new ComboBox();
            timeDropdown = new ComboBox();
            timeLabel = new Label();
            startButton = new Button();
            stopButton = new Button();
            groupBox1 = new GroupBox();
            infoBox = new RichTextBox();
            alwaysOnTopLabel = new Button();
            secondsDropdown = new ComboBox();
            secondsLabel = new Label();
            curTimeLabel = new Label();
            curTime = new Label();
            curTimeTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // substatLabel
            // 
            substatLabel.AutoSize = true;
            substatLabel.Location = new Point(12, 9);
            substatLabel.Name = "substatLabel";
            substatLabel.Size = new Size(119, 16);
            substatLabel.TabIndex = 0;
            substatLabel.Text = "Select Substat :";
            // 
            // substatDropdown
            // 
            substatDropdown.FormattingEnabled = true;
            substatDropdown.Location = new Point(133, 6);
            substatDropdown.Name = "substatDropdown";
            substatDropdown.Size = new Size(98, 24);
            substatDropdown.TabIndex = 1;
            substatDropdown.Text = "Select One";
            substatDropdown.SelectedValueChanged += substatDropdown_SelectedValueChanged;
            // 
            // timeDropdown
            // 
            timeDropdown.FormattingEnabled = true;
            timeDropdown.Location = new Point(133, 36);
            timeDropdown.Name = "timeDropdown";
            timeDropdown.Size = new Size(98, 24);
            timeDropdown.TabIndex = 3;
            timeDropdown.Text = "Select One";
            timeDropdown.SelectedValueChanged += timeDropdown_SelectedValueChanged;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(12, 39);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(119, 16);
            timeLabel.TabIndex = 2;
            timeLabel.Text = "Select Time    :";
            // 
            // startButton
            // 
            startButton.Location = new Point(239, 9);
            startButton.Name = "startButton";
            startButton.Size = new Size(70, 37);
            startButton.TabIndex = 4;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(315, 9);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(74, 37);
            stopButton.TabIndex = 5;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.Location = new Point(12, 101);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(377, 174);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Information";
            // 
            // infoBox
            // 
            infoBox.BackColor = SystemColors.Control;
            infoBox.BorderStyle = BorderStyle.None;
            infoBox.Location = new Point(18, 123);
            infoBox.Name = "infoBox";
            infoBox.ReadOnly = true;
            infoBox.Size = new Size(365, 146);
            infoBox.TabIndex = 0;
            infoBox.Text = "";
            // 
            // alwaysOnTopLabel
            // 
            alwaysOnTopLabel.Location = new Point(239, 52);
            alwaysOnTopLabel.Name = "alwaysOnTopLabel";
            alwaysOnTopLabel.Size = new Size(150, 37);
            alwaysOnTopLabel.TabIndex = 7;
            alwaysOnTopLabel.Text = "Always On Top";
            alwaysOnTopLabel.UseVisualStyleBackColor = true;
            alwaysOnTopLabel.Click += alwaysOnTopLabel_Click;
            // 
            // secondsDropdown
            // 
            secondsDropdown.FormattingEnabled = true;
            secondsDropdown.Location = new Point(133, 66);
            secondsDropdown.Name = "secondsDropdown";
            secondsDropdown.Size = new Size(98, 24);
            secondsDropdown.TabIndex = 9;
            secondsDropdown.Text = "Select One";
            secondsDropdown.SelectedValueChanged += secondsDropdown_SelectedValueChanged;
            // 
            // secondsLabel
            // 
            secondsLabel.AutoSize = true;
            secondsLabel.Location = new Point(12, 69);
            secondsLabel.Name = "secondsLabel";
            secondsLabel.Size = new Size(119, 16);
            secondsLabel.TabIndex = 8;
            secondsLabel.Text = "Select Second  :";
            // 
            // curTimeLabel
            // 
            curTimeLabel.AutoSize = true;
            curTimeLabel.Location = new Point(12, 278);
            curTimeLabel.Name = "curTimeLabel";
            curTimeLabel.Size = new Size(98, 16);
            curTimeLabel.TabIndex = 10;
            curTimeLabel.Text = "Current Time:";
            // 
            // curTime
            // 
            curTime.AutoSize = true;
            curTime.Location = new Point(113, 278);
            curTime.Name = "curTime";
            curTime.Size = new Size(91, 16);
            curTime.TabIndex = 11;
            curTime.Text = "00:00:00:000";
            // 
            // curTimeTimer
            // 
            curTimeTimer.Interval = 1;
            curTimeTimer.Tick += curTimeTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 303);
            Controls.Add(curTime);
            Controls.Add(curTimeLabel);
            Controls.Add(secondsDropdown);
            Controls.Add(secondsLabel);
            Controls.Add(alwaysOnTopLabel);
            Controls.Add(infoBox);
            Controls.Add(groupBox1);
            Controls.Add(stopButton);
            Controls.Add(startButton);
            Controls.Add(timeDropdown);
            Controls.Add(timeLabel);
            Controls.Add(substatDropdown);
            Controls.Add(substatLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "EchoM";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label substatLabel;
        private ComboBox substatDropdown;
        private ComboBox timeDropdown;
        private Label timeLabel;
        private Button startButton;
        private Button stopButton;
        private GroupBox groupBox1;
        private RichTextBox infoBox;
        private Button alwaysOnTopLabel;
        private ComboBox secondsDropdown;
        private Label secondsLabel;
        private Label curTimeLabel;
        private Label curTime;
        private System.Windows.Forms.Timer curTimeTimer;
    }
}
