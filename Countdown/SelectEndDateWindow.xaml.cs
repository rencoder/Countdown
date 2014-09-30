using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Countdown
{
    /// <summary>
    /// Interaction logic for SelectEndDateWindow.xaml
    /// </summary>
    public partial class SelectEndDateWindow : Window
    {
        public SelectEndDateWindow()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, EventArgs e)
        {
            var date = calendar.SelectedDate.Value;
            try
            {
                Properties.Settings.Default.EndDate = new DateTime(date.Year, date.Month, date.Day,
                    Convert.ToInt32(txtHours.Text), Convert.ToInt32(txtMinutes.Text), Convert.ToInt32(txtSeconds.Text));
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Please enter a valid date/time.", String.Empty, MessageBoxButton.OK);
            }
            if (Properties.Settings.Default.EndDate < DateTime.Now)
                MessageBox.Show("You cannot choose a past date/time.", String.Empty, MessageBoxButton.OK);
            else
            {
                Properties.Settings.Default.Save();
                DialogResult = true;
                Close();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !(e.Key >= Key.D0 && e.Key <= Key.D9)
                && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                && e.Key != Key.LeftShift && e.Key != Key.RightShift && e.Key != Key.Tab;
        }
    }
}