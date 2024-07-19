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
            substatLabel = new Label();
            substatDropdown = new ComboBox();
            timeDropdown = new ComboBox();
            timeLabel = new Label();
            startButton = new Button();
            stopButton = new Button();
            groupBox1 = new GroupBox();
            infoBox = new RichTextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // substatLabel
            // 
            substatLabel.AutoSize = true;
            substatLabel.Location = new Point(12, 9);
            substatLabel.Name = "substatLabel";
            substatLabel.Size = new Size(112, 16);
            substatLabel.TabIndex = 0;
            substatLabel.Text = "Select Substat:";
            // 
            // substatDropdown
            // 
            substatDropdown.FormattingEnabled = true;
            substatDropdown.Location = new Point(135, 6);
            substatDropdown.Name = "substatDropdown";
            substatDropdown.Size = new Size(121, 24);
            substatDropdown.TabIndex = 1;
            substatDropdown.Text = "Select One";
            substatDropdown.SelectedValueChanged += substatDropdown_SelectedValueChanged;
            // 
            // timeDropdown
            // 
            timeDropdown.FormattingEnabled = true;
            timeDropdown.Location = new Point(135, 36);
            timeDropdown.Name = "timeDropdown";
            timeDropdown.Size = new Size(121, 24);
            timeDropdown.TabIndex = 3;
            timeDropdown.Text = "Select One";
            timeDropdown.SelectedValueChanged += timeDropdown_SelectedValueChanged;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(12, 39);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(91, 16);
            timeLabel.TabIndex = 2;
            timeLabel.Text = "Select Time:";
            // 
            // startButton
            // 
            startButton.Location = new Point(12, 84);
            startButton.Name = "startButton";
            startButton.Size = new Size(117, 23);
            startButton.TabIndex = 4;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(135, 84);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(121, 23);
            stopButton.TabIndex = 5;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.Controls.Add(infoBox);
            groupBox1.Location = new Point(12, 113);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(244, 196);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Information";
            // 
            // infoBox
            // 
            infoBox.BackColor = SystemColors.Control;
            infoBox.BorderStyle = BorderStyle.None;
            infoBox.Location = new Point(6, 22);
            infoBox.Name = "infoBox";
            infoBox.ReadOnly = true;
            infoBox.Size = new Size(232, 168);
            infoBox.TabIndex = 0;
            infoBox.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(268, 321);
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
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
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
    }
}
