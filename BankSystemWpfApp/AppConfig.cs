using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankSystemWpfApp
{
	public static class AppConfig
	{
		public static Window createAccountWindow {get; set;}
		public static Window loginWindow { get; set; }
		public static Window accListWindow { get; set; }
		public static Window depositWindow { get; set; }
		public static Window withdrawWindow { get; set; }
		public static Window viewWindow { get; set; }
		public static Window editAccountWindow { get; set; }
		public static DashboardWindow dashboardWindow { get; set; }
		public static BankAccountViewModel ViewModel { get; set; }
		public static DepositViewModel depositViewModel { get; set; }
		public static WithdrawViewModel withdrawViewModel { get; set; }
		public static AccountViewWindow accountViewWindow { get; set; }
		public static TransferViewModel transferViewModel { get; set; }
		public static Window amountTransferWindow { get; set; }


		static AppConfig()
		{
			ViewModel = new BankAccountViewModel();
			depositViewModel = new DepositViewModel();
			withdrawViewModel = new WithdrawViewModel();
			transferViewModel = new TransferViewModel();

			amountTransferWindow = new AmountTransferWindow();
			createAccountWindow = new NewAccountWindow();
			editAccountWindow = new EditAccountWindow();
			dashboardWindow = new DashboardWindow();
			loginWindow = new LoginWindow();
			accListWindow = new AccountListWindow();
			depositWindow = new DepositWindow();
			withdrawWindow = new WithdrawWindow();
			viewWindow = new AccountViewWindow();
			accountViewWindow = new AccountViewWindow();
		}
	}
}
