using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfClientApplication
{
    /// <summary>
    /// Web browser utility to provide attached properties to a WebBrowser Control
    /// </summary>
    public static class WebBrowserUtility
    {
        /// <summary>
        /// The bindable source dependency property
        /// </summary>
        public static readonly DependencyProperty BindableSourceProperty = DependencyProperty.RegisterAttached("BindableSource", typeof(Uri), typeof(WebBrowserUtility), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        /// <summary>
        /// Gets the bindable source.
        /// </summary>
        /// <param name="obj">The dependency obj.</param>
        /// <returns>A Uri source</returns>
        public static Uri GetBindableSource(DependencyObject obj)
        {
            return (Uri)obj.GetValue(BindableSourceProperty);
        }

        /// <summary>
        /// Sets the bindable source.
        /// </summary>
        /// <param name="obj">The dependency obj.</param>
        /// <param name="value">The Uri to set.</param>
        public static void SetBindableSource(DependencyObject obj, Uri value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        /// <summary>
        /// Occurs when the bindable source changes
        /// </summary>
        /// <param name="o">The depedency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                Uri uri = e.NewValue as Uri;
                browser.Source = uri;
            }
        }
    }
}
