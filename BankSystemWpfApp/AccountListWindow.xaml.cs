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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankSystemWpfApp
{
	/// <summary>
	/// Interaction logic for AccountListWindow.xaml
	/// </summary>
	public partial class AccountListWindow : Window
	{
		public AccountListWindow()
		{
			InitializeComponent();
			this.DataContext = AppConfig.ViewModel;
		}

		//Create Account button click
		private void btnAddAccount_Click(object sender, RoutedEventArgs e)
		{
			AppConfig.createAccountWindow.Show();
			NewAccountWindow newAccountWindow = (NewAccountWindow)AppConfig.createAccountWindow;
			AppConfig.ViewModel.NewWindowClose = newAccountWindow.WindowClose;
		}

		//Edit Account button click 
		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			if (grdAccounts.SelectedIndex == -1)
			{
				var result = MessageBox.Show(messageBoxText: "Please select an account to edit",
					caption: "Alert",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Information);
				return;
			}
			AppConfig.editAccountWindow.Show();

			EditAccountWindow editAccountWindow = (EditAccountWindow)AppConfig.editAccountWindow;
			AppConfig.ViewModel.EditWindowClose = editAccountWindow.WindowClose;
		}

		//View Account button click
		private void btnView_Click(object sender, RoutedEventArgs e)
		{
			if (grdAccounts.SelectedIndex == -1)
			{
				var result = MessageBox.Show(messageBoxText: "Please select an account",
					caption: "Alert",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Information);
				return;
			}
			AppConfig.accountViewWindow.Show();

		}


    }
}
