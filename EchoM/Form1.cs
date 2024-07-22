using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EchoM
{
    public partial class Form1 : Form
    {
        private ComboBoxItem selectedSubstatItem;
        private ComboBoxItem selectedTimeItem;
        private ComboBoxItem selectedSecondItem;
        private System.Threading.Timer autoClickTimer;
        private DateTime lastClickTime = DateTime.MinValue;
        private int intValue;
        private int intValue2;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        public Form1()
        {
            InitializeComponent();
            InitializeComboBoxes();
            curTimeTimer.Start();
        }

        private void InitializeComboBoxes()
        {
            // For you guys if wanna add calculation of accuracy
            InitializeDropdown(substatDropdown, new[]
            {
                "ATK", "HP", "DEF", "ATK%", "HP%", "DEF%",
                "Energy Regen", "Crit Rate", "Crit DMG",
                "Basic Atk DMG", "Heavy Atk DMG",
                "Res Skill DMG", "Res Lib DMG"
            }, Enumerable.Range(0, 13).ToArray());

            InitializeTimeDropdown(timeDropdown);
            InitializeTimeDropdown(secondsDropdown);

            SetDefaultSelection();
        }

        private void InitializeDropdown(ComboBox comboBox, string[] names, int[] values)
        {
            comboBox.Items.AddRange(names.Zip(values, (name, value) => new ComboBoxItem(name, value)).ToArray());
        }

        private void InitializeTimeDropdown(ComboBox comboBox)
        {
            comboBox.Items.AddRange(
                Enumerable.Range(0, 10)
                          .Select(i => new ComboBoxItem($"X{i}", i))
                          .Concat(new[] { new ComboBoxItem("0X", 10) })
                          .ToArray());
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (selectedSubstatItem == null || selectedTimeItem == null || selectedSecondItem == null)
            {
                UpdateRichBox("Please select Substat, Time, and Seconds first.\n");
                return;
            }

            if (selectedTimeItem.Value is int value && selectedSecondItem.Value is int value2)
            {
                intValue = value;
                intValue2 = value2;
                UpdateRichBox(GetStartMessage(intValue, intValue2));
                StartAutoClickTimer();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            autoClickTimer?.Dispose();
            autoClickTimer = null;
            UpdateRichBox("Stopping Auto-Clicking\n");
        }

        private void alwaysOnTopLabel_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        private void curTimeTimer_Tick(object sender, EventArgs e)
        {
            curTime.Text = DateTime.Now.ToString("HH:mm:ss:ff");
        }

        private void SetDefaultSelection()
        {
            secondsDropdown.SelectedItem = secondsDropdown.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Name == "0X");
        }

        private string GetStartMessage(int value, int value2)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Starting Auto-Clicking with Minutes {(value == 10 ? "0" : value.ToString())}");

            if (value == 10 && value2 == 10)
            {
                sb.AppendLine("Available times: '0X', X = 1-9");
                sb.AppendLine("With Seconds: '0X', X = 1-9");
            }
            else if (value == 10)
            {
                sb.AppendLine("Available times: '0X', X = 1-9");
                sb.AppendLine($"With Seconds: '1-9X, X = {value2}'");
            }
            else if (value2 == 10)
            {
                sb.AppendLine($"Available times: '1-9X, X = {value}'");
                sb.AppendLine("With Seconds: '0X', X = 1-9");
            }
            else
            {
                sb.AppendLine($"Available times: '1-9X, X = {value}'");
                sb.AppendLine($"With Seconds: '1-9X, X = {value2}'");
            }

            sb.AppendLine($"Time Zone: {TimeZoneInfo.Local.DisplayName}");
            return sb.ToString();
        }

        private void substatDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            if (substatDropdown.SelectedItem is ComboBoxItem selectedItem)
            {
                selectedSubstatItem = selectedItem;
                CheckAndUpdateRichBox();
            }
        }

        private void timeDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            if (timeDropdown.SelectedItem is ComboBoxItem selectedItem)
            {
                selectedTimeItem = selectedItem;
                CheckAndUpdateRichBox();
            }
        }

        private void secondsDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            if (secondsDropdown.SelectedItem is ComboBoxItem selectedItem)
            {
                selectedSecondItem = selectedItem;
            }
            else
            {
                selectedSecondItem = secondsDropdown.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Name == "0X");
            }
            CheckAndUpdateRichBox();
        }

        private void CheckAndUpdateRichBox()
        {
            if (selectedSubstatItem != null && selectedTimeItem != null && selectedSecondItem != null)
            {
                string text = $"Substat: {selectedSubstatItem.Name}\n" +
                              $"Time: {selectedTimeItem.Name}\n" +
                              $"Seconds: {selectedSecondItem.Name}\n";

                UpdateRichBox(text);
            }
        }

        private void StartAutoClickTimer()
        {
            autoClickTimer?.Dispose();
            autoClickTimer = new System.Threading.Timer(TimerCallback, null, 0, 10); // Check every 10 milliseconds
        }

        private void TimerCallback(object state)
        {
            DateTime now = DateTime.Now;
            int milisecond = now.Millisecond / 10;

            int checkMinute = intValue == 10 ? now.Minute / 10 : now.Minute % 10;
            int checkSecond = intValue2 == 10 ? now.Second / 10 : now.Second % 10;
            int checkMilisecond = intValue == 10 ? milisecond / 10 : milisecond % 10;

            bool shouldClick = checkMinute == (intValue == 10 ? 0 : intValue) &&
                               checkSecond == (intValue2 == 10 ? 0 : intValue2) &&
                               checkMilisecond == (intValue == 10 ? 0 : intValue);

            if (shouldClick && now.Second != lastClickTime.Second)
            {
                PerformClick();
                lastClickTime = now;
            }
        }

        private void PerformClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            string timeOfClick = DateTime.Now.ToString("HH:mm:ss:ff");
            AppendTextOnce($"Clicked at {timeOfClick}\n");
        }

        private void AppendTextOnce(string text)
        {
            if (infoBox.InvokeRequired)
            {
                infoBox.Invoke(new Action(() =>
                {
                    infoBox.AppendText(text);
                    infoBox.ScrollToCaret();
                }));
            }
            else
            {
                infoBox.AppendText(text);
                infoBox.ScrollToCaret();
            }
        }

        private void UpdateRichBox(string text)
        {
            if (infoBox.InvokeRequired)
            {
                infoBox.Invoke(new Action(() =>
                {
                    infoBox.Text = text;
                    infoBox.ScrollToCaret();
                }));
            }
            else
            {
                infoBox.Text = text;
                infoBox.ScrollToCaret();
            }
        }

        private class ComboBoxItem
        {
            public string Name { get; }
            public object Value { get; }

            public ComboBoxItem(string name, object value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString() => Name;
        }
    }
}
