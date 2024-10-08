using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankSystemWpfApp
{
	/// <summary>
	/// Interaction logic for AddAccountWindow.xaml
	/// </summary>
	public partial class NewAccountWindow : Window
	{
		public NewAccountWindow()
		{
			InitializeComponent();
			DataContext = AppConfig.ViewModel;
		}
		public void WindowClose()
		{
			this.Hide();
		}
		private void Window_Closed(object sender, EventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			//Email Validation
			string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
			bool isEmailValid=Regex.IsMatch(txtEmail.Text, emailPattern);

			//Phone Validation (only numbers, 10 digits)
			string phonePattern = @"^\d{10}$";
			bool isPhoneValid = Regex.IsMatch(txtPhone.Text, phonePattern);

			//Display Errors
			if (!isEmailValid && !isPhoneValid)
			{
				MessageBox.Show(messageBoxText: $"Invalid Email and Phone",
				   caption: "Warning",
				   button: MessageBoxButton.OK,
				   icon: MessageBoxImage.Warning);
				return;
			}
			if (!isEmailValid)
			{
				MessageBox.Show(messageBoxText: $"Invalid Email format",
				   caption: "Warning",
				   button: MessageBoxButton.OK,
				   icon: MessageBoxImage.Warning);
				return;
			}
			else if (!isPhoneValid)
			{
				MessageBox.Show(messageBoxText: $"Invalid Phone number. Must be 10 digits",
				   caption: "Warning",
				   button: MessageBoxButton.OK,
				   icon: MessageBoxImage.Warning);
				return;
			}

		}
    }
}
