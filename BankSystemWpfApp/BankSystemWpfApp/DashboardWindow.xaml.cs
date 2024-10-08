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
	/// Interaction logic for DashboardWindow.xaml
	/// </summary>
	public partial class DashboardWindow : Window
	{
		public DashboardWindow()
		{
			InitializeComponent();
			DataContext = AppConfig.dashboardWindow;
		}

		private void btnDeposit_Click(object sender, RoutedEventArgs e)
		{
			AppConfig.depositWindow.Show();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void btnManager_Click(object sender, RoutedEventArgs e)
		{
			AppConfig.accListWindow.Show();
		}

		private void btnWithdraw_Click(object sender, RoutedEventArgs e)
		{
			AppConfig.withdrawWindow.Show();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}
