using System.Windows;
using System.Windows.Input;

namespace Countdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MouseDown += (sender, obj) =>
            {
                if (obj.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            };
        }
    }
}
