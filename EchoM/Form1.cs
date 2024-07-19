using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EchoM
{
    public partial class Form1 : Form
    {
        private ComboBoxItem selectedSubstatItem;
        private ComboBoxItem selectedTimeItem;
        private System.Threading.Timer autoClickTimer;
        private int intValue;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        public Form1()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // I don't know what this code is for but I put it in case it comes in handy.
            substatDropdown.Items.AddRange(new[]
            {
                new ComboBoxItem("ATK", 0),
                new ComboBoxItem("HP", 1),
                new ComboBoxItem("DEF", 2),
                new ComboBoxItem("ATK%", 3),
                new ComboBoxItem("HP%", 4),
                new ComboBoxItem("DEF%", 5),
                new ComboBoxItem("Energy Regen", 6),
                new ComboBoxItem("Crit Rate", 7),
                new ComboBoxItem("Crit DMG", 8),
                new ComboBoxItem("Basic Atk DMG", 9),
                new ComboBoxItem("Heavy Atk DMG", 10),
                new ComboBoxItem("Res Skill DMG", 11),
                new ComboBoxItem("Res Lib DMG", 12)
            });

            timeDropdown.Items.AddRange(Enumerable.Range(0, 10)
                .Select(i => new ComboBoxItem($"X{i}", i))
                .ToArray());
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (selectedSubstatItem == null || selectedTimeItem == null)
            {
                UpdateRichBox("Please select Substat and Time first.\n");
                return;
            }

            if (selectedTimeItem.Value is int value)
            {
                intValue = value;
                string text = GetStartMessage(intValue);
                UpdateRichBox(text);
                StartAutoClickTimer();
            }
        }

        private string GetStartMessage(int value)
        {
            var messages = new Dictionary<int, string>
            {
                { 0, "00, '10', '20', '30', '40', '50'" },
                { 1, "01, '11', '21', '31', '41', '51'" },
                { 2, "02, '12', '22', '32', '42', '52'" },
                { 3, "03, '13', '23', '33', '43', '53'" },
                { 4, "04, '14', '24', '34', '44', '54'" },
                { 5, "05, '15', '25', '35', '45', '55'" },
                { 6, "06, '16', '26', '36', '46', '56'" },
                { 7, "07, '17', '27', '37', '47', '57'" },
                { 8, "08, '18', '28', '38', '48', '58'" },
                { 9, "09, '19', '29', '39', '49', '59'" }
            };

            return messages.TryGetValue(value, out var availableTimes)
                ? $"Starting Auto-Clicking with Minutes {value}\nAvailable times: '{availableTimes}'\nTime Zone: {TimeZoneInfo.Local.DisplayName}\n"
                : "Value Tidak Tersedia.\n";
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            autoClickTimer?.Dispose();
            autoClickTimer = null;
            UpdateRichBox("Stopping Auto-Clicking\n");
        }

        private void substatDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            if (substatDropdown.SelectedItem != null)
            {
                selectedSubstatItem = (ComboBoxItem)substatDropdown.SelectedItem;
                CheckAndUpdateRichBox();
            }
        }

        private void timeDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            if (timeDropdown.SelectedItem != null)
            {
                selectedTimeItem = (ComboBoxItem)timeDropdown.SelectedItem;
                CheckAndUpdateRichBox();
            }
        }

        private void CheckAndUpdateRichBox()
        {
            if (selectedSubstatItem != null && selectedTimeItem != null)
            {
                string text = $"Substat: {selectedSubstatItem.Name}\nTime: {selectedTimeItem.Name}\n";
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
            int Milisecond = now.Millisecond / 10;

            if (now.Minute % 10 == intValue &&
                now.Second % 10 == intValue &&
                Milisecond % 10 == intValue)
            {
                PerformClick();
            }
        }

        private void PerformClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void UpdateRichBox(string text)
        {
            if (infoBox.InvokeRequired)
            {
                infoBox.Invoke(new Action(() => { 
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