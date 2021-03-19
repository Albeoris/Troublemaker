using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Troublemaker.Editor.Annotations;
using Troublemaker.Editor.Pages;
using Troublemaker.Framework;
using Troublemaker.Xml;

namespace Troublemaker.Editor.Settings
{
    public sealed class UserSettingsViewModel : DependencyObject, INotifyPropertyChanged
    {
        public static UserSettingsViewModel Instance { get; } = new UserSettingsViewModel();
        
        private String _userName = "Anonymous";
        private Mode _mode = Mode.Anonymous;
        private WeakReference<LoginWindow> _window;
        private LoginWindow? Window => _window != null && _window.TryGetTarget(out var window) ? window : null;

        public Boolean IsValidName => !String.IsNullOrEmpty(UserName);
        public Boolean CanSignIn => IsValidName && UserCollection.Instance.IsKnownUser(UserName) && IsValidPassword;
        public Boolean CanSignUp => IsValidName && !UserCollection.Instance.IsKnownUser(UserName) && IsValidPassword;

        public String UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
                NotifyUsernameControlChanged();
            }
        }

        public Boolean IsAuthenticated { get; private set; }

        public void Complete()
        {
            IsAuthenticated = true;
            Issuer.CurrentName = UserName;
            Window.DialogResult = true;
            Window?.Close();
        }
        
        public SignUserCommand ButtonCommand { get; } = new SignUserCommand();

        public Boolean IsAnonymousMode => UserName == "Anonymous";
        public Boolean IsSignInMode => UserCollection.Instance.IsKnownUser(UserName);

        #region Password

        public String Password => Window?.PasswordBox.Password ?? String.Empty;
        public String ConfirmPassword => Window?.ConfirmPasswordBox.Password ?? String.Empty;
        public Boolean IsValidPassword
        {
            get
            {
                if (String.IsNullOrEmpty(Password))
                    return false;

                if (_mode == Mode.Register && Password != ConfirmPassword)
                    return false;

                return true;
            }
        }

        public void Bind(LoginWindow control)
        {
            if (_window?.TryGetTarget(out _) == true)
                throw new NotSupportedException("Another instance of PasswordBox was bound to this view model.");

            _mode = default;
            _window = new WeakReference<LoginWindow>(control);
            control.PasswordBox.PasswordChanged += OnPasswordChanged;
            control.ConfirmPasswordBox.PasswordChanged += OnPasswordChanged;

            NotifyUsernameControlChanged();
        }

        public void Unbind(LoginWindow control)
        {
            if (_window != null && _window.TryGetTarget(out var prev) && prev == control)
                _window = null;
            
            control.PasswordBox.PasswordChanged -= OnPasswordChanged;
            control.ConfirmPasswordBox.PasswordChanged -= OnPasswordChanged;
        }
        
        private void OnPasswordChanged(Object sender, RoutedEventArgs e)
        {
            NotifyUsernameControlChanged();
        }

        #endregion

        #region Property changed

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void NotifyUsernameControlChanged()
        {
            var wnd = Window;
            if (wnd == null)
                return;
            
            if (IsAnonymousMode)
            {
                if ( _mode != Mode.Anonymous)
                {
                    _mode = Mode.Anonymous;

                    wnd.PasswordBox.Visibility = Visibility.Collapsed;
                    wnd.ConfirmPasswordBox.Visibility = Visibility.Collapsed;
                    wnd.Button.Content = "OK";
                }
            }
            else if (IsSignInMode)
            {
                if ( _mode != Mode.Login)
                {
                    _mode = Mode.Login;

                    wnd.PasswordBox.Visibility = Visibility.Visible;
                    wnd.ConfirmPasswordBox.Visibility = Visibility.Collapsed;
                    wnd.Button.Content = "Login";
                }
            }
            else
            {
                if ( _mode != Mode.Register)
                {
                    _mode = Mode.Register;

                    wnd.PasswordBox.Visibility = Visibility.Visible;
                    wnd.ConfirmPasswordBox.Visibility = Visibility.Visible;
                    wnd.Button.Content = "Register";
                }
            }

            ButtonCommand.RaiseCanExecuteChanged();
        }

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        
        private enum Mode
        {
            Anonymous = 1,
            Login,
            Register
        }
    }
}