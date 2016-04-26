using System.ComponentModel;

namespace WpfClientApplication.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SignInViewModel signInViewModel;

        public MainViewModel()
        {
            SignInViewModel = new SignInViewModel();
        }

        public SignInViewModel SignInViewModel {
            get { return signInViewModel; }
            private set {
                signInViewModel = value;
                RaisePropertyChanged("SignInViewModel");
            }
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
