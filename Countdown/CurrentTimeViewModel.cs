using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Data;

namespace CountingDown
{
    public class CurrentTimeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Timer timer = new Timer(1);
        private readonly DateTime _endDate = new DateTime(2014, 09, 24);
        private TimeSpan _remainingTime;

        public CurrentTimeViewModel()
        {
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
            if (value == null)
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
