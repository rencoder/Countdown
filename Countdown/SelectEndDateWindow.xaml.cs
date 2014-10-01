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

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsNumber(e.Text.ElementAtOrDefault(0));
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int intResult;
            var textBox = sender as TextBox;
            if (!Int32.TryParse(textBox.Text, out intResult))
                textBox.Clear();
        }
    }
}