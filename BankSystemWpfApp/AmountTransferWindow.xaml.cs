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
	/// Interaction logic for AmountTransferWindow.xaml
	/// </summary>
	public partial class AmountTransferWindow : Window
	{
		public AmountTransferWindow()
		{
			InitializeComponent();
			this.DataContext = AppConfig.transferViewModel;
		}
		//Simulated DB of accounts

		//{
		//	{"1001",5000.00m }, //sender's account
		//	{"1001",5000.00m } //receiver's account
		//};
		//private void btnTransfer_Click(object sender, RoutedEventArgs e)
		//{
		//	string senderAccNo = txtSenderAccNo.Text;
		//	string receiverAccNo = txtReceiverAccNo.Text;
		//	string amount = txtAmount.Text;

		//}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}
