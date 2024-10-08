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
using System.Windows.Shapes;

namespace BankSystemWpfApp
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
			DataContext = AppConfig.ViewModel;
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			if (txtUsername.Text == "quest" && txtPassword.Password == "1234")
			{
				AppConfig.dashboardWindow.Show();
				this.Hide();
			}
			else
			{
				MessageBox.Show(messageBoxText: $"Invalid username or password",
				   caption: "Warning",
				   button: MessageBoxButton.OK,
				   icon: MessageBoxImage.Warning);
				return;
			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
