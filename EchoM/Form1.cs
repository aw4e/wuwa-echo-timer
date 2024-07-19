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

        // Data for substat values at different times
        private readonly Dictionary<int, Dictionary<int, double>> statData = new Dictionary<int, Dictionary<int, double>>
        {
            { 0, new Dictionary<int, double> { { 0, 3 }, { 1, 6 }, { 2, 4 }, { 3, 4 }, { 4, 4 }, { 5, 5 }, { 6, 7 }, { 7, 4 }, { 8, 5 }, { 9, 4 }, { 10, 5 }, { 11, 4 }, { 12, 4 } } },
            { 1, new Dictionary<int, double> { { 0, 1 }, { 1, 3 }, { 2, 3 }, { 3, 1 }, { 4, 2 }, { 5, 2 }, { 6, 2 }, { 7, 1 }, { 8, 2 }, { 9, 1 }, { 10, 2 }, { 11, 3 }, { 12, 3 } } },
            { 2, new Dictionary<int, double> { { 0, 7 }, { 1, 6 }, { 2, 3 }, { 3, 5 }, { 4, 6 }, { 5, 6 }, { 6, 6 }, { 7, 8 }, { 8, 11 }, { 9, 3 }, { 10, 5 }, { 11, 2 }, { 12, 3 } } },
            { 3, new Dictionary<int, double> { { 0, 9 }, { 1, 17 }, { 2, 12 }, { 3, 11 }, { 4, 7 }, { 5, 14 }, { 6, 15 }, { 7, 22 }, { 8, 13 }, { 9, 9 }, { 10, 16 }, { 11, 14 }, { 12, 14 } } },
            { 4, new Dictionary<int, double> { { 0, 4 }, { 1, 2 }, { 2, 1 }, { 3, 5 }, { 4, 3 }, { 5, 6 }, { 6, 7 }, { 7, 4 }, { 8, 4 }, { 9, 4 }, { 10, 5 }, { 11, 7 }, { 12, 8 } } },
            { 5, new Dictionary<int, double> { { 0, 21 }, { 1, 21 }, { 2, 17 }, { 3, 22 }, { 4, 20 }, { 5, 30 }, { 6, 40 }, { 7, 34 }, { 8, 31 }, { 9, 30 }, { 10, 34 }, { 11, 21 }, { 12, 21 } } },
            { 6, new Dictionary<int, double> { { 0, 9 }, { 1, 7 }, { 2, 8 }, { 3, 10 }, { 4, 13 }, { 5, 7 }, { 6, 4 }, { 7, 13 }, { 8, 12 }, { 9, 9 }, { 10, 8 }, { 11, 12 }, { 12, 7 } } },
            { 7, new Dictionary<int, double> { { 0, 64 }, { 1, 66 }, { 2, 74 }, { 3, 73 }, { 4, 76 }, { 5, 68 }, { 6, 100 }, { 7, 153 }, { 8, 149 }, { 9, 66 }, { 10, 92 }, { 11, 84 }, { 12, 57 } } },
            { 8, new Dictionary<int, double> { { 0, 6 }, { 1, 5 }, { 2, 7 }, { 3, 5 }, { 4, 6 }, { 5, 8 }, { 6, 2 }, { 7, 9 }, { 8, 6 }, { 9, 4 }, { 10, 6 }, { 11, 6 }, { 12, 6 } } },
            { 9, new Dictionary<int, double> { { 0, 32 }, { 1, 13 }, { 2, 22 }, { 3, 27 }, { 4, 15 }, { 5, 14 }, { 6, 22 }, { 7, 32 }, { 8, 37 }, { 9, 25 }, { 10, 24 }, { 11, 26 }, { 12, 23 } } }
        };

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

            intValue = (int)selectedTimeItem.Value;
            UpdateRichBox(GetStartMessage(intValue));
            StartAutoClickTimer();
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

        private string CalculateAccuracy(int substatIndex, int timeIndex)
        {
            if (statData.TryGetValue(timeIndex, out var substatValues) &&
                substatValues.TryGetValue(substatIndex, out var value))
            {
                double total = 0;
                foreach (var val in substatValues.Values)
                {
                    total += val;
                }

                double percentage = (value / total) * 100;
                return $"{percentage:F2}%\n";
            }
            return "No Data Available\n";
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            autoClickTimer?.Dispose();
            autoClickTimer = null;
            UpdateRichBox("Stopping Auto-Clicking\n");
        }

        private void substatDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedSubstatItem = (ComboBoxItem)substatDropdown.SelectedItem;
            CheckAndUpdateRichBox();
        }

        private void timeDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedTimeItem = (ComboBoxItem)timeDropdown.SelectedItem;
            CheckAndUpdateRichBox();
        }

        private void CheckAndUpdateRichBox()
        {
            if (selectedSubstatItem != null && selectedTimeItem != null)
            {
                string accuracy = CalculateAccuracy((int)selectedSubstatItem.Value, (int)selectedTimeItem.Value);
                string text = $"Substat: {selectedSubstatItem.Name}\n" +
                              $"Time: {selectedTimeItem.Name}\n" +
                              $"Accuracy: {accuracy}";
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
            int millisecond = now.Millisecond / 10;

            if (now.Minute % 10 == intValue &&
                now.Second % 10 == intValue &&
                millisecond % 10 == intValue)
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
