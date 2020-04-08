using System;
using System.Windows;
using Troublemaker.Editor.Framework;

namespace Troublemaker.Editor.Settings
{
    public sealed class SignUserCommand : BaseCommand
    {
        public override Boolean CanExecute(Object parameter)
        {
            UserSettingsViewModel us = UserSettingsViewModel.Instance;
            return us.IsAnonymousMode || us.CanSignIn || us.CanSignUp;
        }

        public override void Execute(Object parameter)
        {
            UserSettingsViewModel us = UserSettingsViewModel.Instance;

            if (us.IsAnonymousMode)
            {
                us.Complete();
            }
            else if (us.CanSignUp)
            {
                UserCollection.Instance.Register(us.UserName, new UserPassword(us.Password));
                us.Complete();
            }
            else if (us.CanSignIn)
            {
                if (UserCollection.Instance.IsValidUser(us.UserName, us.Password))
                {
                    us.Complete();
                }
                else
                {
                    MessageBox.Show("Invalid password.", "Authentication error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            us.NotifyUsernameControlChanged();
        }
    }
}