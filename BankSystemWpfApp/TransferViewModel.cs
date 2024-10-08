using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BankSystemWpfApp
{
	public class TransferViewModel
	{
		public string senderAccNo{get;set;}
		public string receiverAccNo {get;set;}
		public string amount {get;set;}
		private Dictionary<string, decimal> accounts = new Dictionary<string, decimal>();

		/// <summary>
		/// Gets the command for withdrawing money from an account.
		/// </summary>
		public ICommand TransferCommand { get; }


		/// <summary>
		/// Initializes a new instance of the <see cref="WithdrawViewModel"/> class.
		/// </summary>
		public TransferViewModel()
		{
			//initialize the TransferCommand property in the constructor
			TransferCommand = new RelayCommand(Transfer);
		}

		//Input Validation
		public void Transfer()
		{
			var result = MessageBox.Show(messageBoxText: "Are you sure to Withdraw?",
					caption: "Confirm",
					button: MessageBoxButton.YesNo,
					icon: MessageBoxImage.Question);
			if (result != MessageBoxResult.Yes)
			{
				return;
			}

			
			// Perform transfer
			try
			{
				// Check if sender and receiver accounts are the same
				if (senderAccNo == receiverAccNo)
				{
					MessageBox.Show(messageBoxText: "Sender and receiver accounts cannot be the same",
						caption: "Alert",
						button: MessageBoxButton.OK,
						icon: MessageBoxImage.Information);
					return;
				}

				// Check if transfer amount is zero
				if (decimal.TryParse(amount, out decimal transferAmount) && transferAmount == 0)
				{
					MessageBox.Show(messageBoxText: "Transfer amount cannot be zero",
						caption: "Alert",
						button: MessageBoxButton.OK,
						icon: MessageBoxImage.Information);
					return;
				}

				// Validate sender and recipient accounts
				if (!accounts.ContainsKey(senderAccNo))
				{
					MessageBox.Show(messageBoxText: "Sender account not found",
						caption: "Alert",
						button: MessageBoxButton.OK,
						icon: MessageBoxImage.Information);
					return;
				}
				if (!accounts.ContainsKey(receiverAccNo))
				{
					MessageBox.Show(messageBoxText: "Receiver account not found",
						caption: "Alert",
						button: MessageBoxButton.OK,
						icon: MessageBoxImage.Information);
					return;
				}

				// Ensure sender has enough balance
				if (accounts[senderAccNo] < transferAmount)
				{
					MessageBox.Show(messageBoxText: "Insufficient fund in Sender's account",
						caption: "Alert",
						button: MessageBoxButton.OK,
						icon: MessageBoxImage.Information);
					return;
				}

				//transfer logic
				accounts[senderAccNo] -= transferAmount; // deduct from sender
				accounts[receiverAccNo] += transferAmount; // add to receiver

				// Log the transfer event
				Logger.log.Info($"Transferred {amount} rupees Successfully from account {senderAccNo} to account {receiverAccNo}");

				// Update the UI
				MessageBox.Show(messageBoxText: $"Transferred {amount} rupees Successfully from account {senderAccNo} to account {receiverAccNo}",
					caption: "Alert",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Information);

				// Reset fields
				this.senderAccNo = "0";
				this.receiverAccNo = "0";
				this.amount = "0";
			}
			catch (AccountException ae)
			{
				MessageBox.Show(messageBoxText: $"{ae.Message}",
					caption: "Warning",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Warning);

				Logger.log.Error(ae.Message);
			}
			catch (Exception ex)
			{
				Logger.log.Error(ex.Message);
			}
		}

	}
}
