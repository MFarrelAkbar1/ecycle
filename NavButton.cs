using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ecycle
{
    public class NavButton : ListBoxItem
    {
        static NavButton()
        {
            // Set default style key to allow for styling through XAML
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
        }

        // DependencyProperty for the Navlink Uri
        public Uri Navlink
        {
            get { return (Uri)GetValue(NavlinkProperty); }
            set { SetValue(NavlinkProperty, value); }
        }

        public static readonly DependencyProperty NavlinkProperty =
            DependencyProperty.Register("Navlink", typeof(Uri), typeof(NavButton), new PropertyMetadata(null));

        // DependencyProperty for the Icon Geometry
        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(NavButton), new PropertyMetadata(null));

        // Optionally, you can add a method to handle navigation
        public void Navigate()
        {
            // Perform navigation logic here, example:
            if (Navlink != null)
            {
                // Assuming `MainWindow` has a Frame or NavigationService defined
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(Navlink); // Navigate to the URI set in Navlink
                }
            }
        }
    }
}
