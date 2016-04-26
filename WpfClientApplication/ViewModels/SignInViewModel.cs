using System.ComponentModel;
using System.Windows.Input;
using WpfClientApplication.AuthServiceReference;

namespace WpfClientApplication.ViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SignInViewModel()
        {
            SignInCommand = new ActionCommand(() => new AuthServiceClient().Authorize(), () => true);
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SignInCommand { get; private set; }
    }
}
