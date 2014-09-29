using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Data;
using System.Windows;

namespace CountingDown
{
    public class RemainingTimeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Timer timer;
        private readonly DateTime _endDate = Properties.Settings.Default.EndDate;
        private TimeSpan _remainingTime;

        public RemainingTimeViewModel()
        {
            if (_endDate.Equals(default(DateTime)))
            {
                var window = new SelectEndDateWindow();
                window.ShowDialog();
                return;
            }
            timer = new Timer(1);
            timer.Elapsed += delegate
            {
                RemainingTime = _endDate.Subtract(DateTime.Now);
            };
            timer.Enabled = true;
            timer.Start();
        }

        public TimeSpan RemainingTime
        {
            get { return _remainingTime; }
            set
            { 
                _remainingTime = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("RemainingTime"));
            }
        }
    }

    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is TimeSpan) || Properties.Settings.Default.EndDate.Equals(default(DateTime)))
                return String.Empty;
            var timeSpan = (TimeSpan)value;
            return String.Join(":",
                timeSpan.Days.ToString("00"),
                timeSpan.Hours.ToString("00"),
                timeSpan.Minutes.ToString("00"),
                timeSpan.Seconds.ToString("00"),
                timeSpan.Milliseconds.ToString("000"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
