using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace BankSystemWpfApp
{
	public delegate void DWindowClose();
	public class BankAccountViewModel : ViewModelBase, IDataErrorInfo
	{
		private Account _newAccount = null;

		public DWindowClose NewWindowClose;
		public DWindowClose EditWindowClose;

		/// <summary>
		/// Gets or sets the new account.
		/// </summary>
		public Account NewAccount
		{
			get { return _newAccount; }
			set
			{
				_newAccount = value;
				onPropertyChanged(nameof(NewAccount));
			}
		}

		private Account _selectedAccount = null;
		/// <summary>
		/// Gets or sets the selected account.
		/// </summary>
		public Account SelectedAccount
		{
			get => _selectedAccount;
			set
			{
				_selectedAccount = value;
				onPropertyChanged(nameof(SelectedAccount));
			}
		}



		/// <summary>
		/// Gets the accounts repository.
		/// </summary>
		private IAccountRepo _repo = AccountMemoryRepo.Instance;

		// <summary>
		/// Gets the collection of accounts.
		/// </summary>
		public ObservableCollection<Account> Accounts
		{
			get
			{
				try
				{
					return _repo.ReadAll();
				}
				catch (AccountException ae)
				{
					Logger.log.Error(ae.Message);
					throw;
				}

			}
		}

		/// <summary>
		/// Gets the command for creating a new account.
		/// </summary>
		public ICommand CreateCommand { get; }

		/// <summary>
		/// Gets the command for updating an existing account.
		/// </summary>
		public ICommand UpdateCommand { get; }

		/// <summary>
		/// Gets the command for deleting an existing account.
		/// </summary>
		public ICommand DeleteCommand { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountViewModel"/> class.
		/// </summary>
		public BankAccountViewModel()
		{
			this.NewAccount = new Account
			{
				AccNumber = 00000,
				Name = "",
				Balance = 0,
				Type = "",
				Email = "",
				Phone = "",
				Address = "",
				IsActive = true,
				InterestPercentage = "0",
				TransactionCount = 0,
				LastTransactionDate = DateTime.Now,


			};
			CreateCommand = new RelayCommand(Create,() => CanCreate()); //CanCreate() "name/Lambda captured", but wont run now
																		// runs when Create is running			
			UpdateCommand = new RelayCommand(Update);
			DeleteCommand = new RelayCommand(Delete);

		}
		public bool CanCreate()
		{
			return (Balance > 0) && (Email.Length != 0);
		}

		/// <summary>
		/// Creates a new account.
		/// </summary>

		public void Create()
		{
			Account newAccount = new Account
			{
				AccNumber = NewAccount.AccNumber,
				Name = NewAccount.Name,
				Balance = NewAccount.Balance,
				Type = NewAccount.Type,
				Email = NewAccount.Email,
				Phone = NewAccount.Phone,
				Address = NewAccount.Address,
				IsActive = NewAccount.IsActive,
				InterestPercentage = NewAccount.InterestPercentage,
				TransactionCount = NewAccount.TransactionCount,
				LastTransactionDate = NewAccount.LastTransactionDate,
			};
			var result = MessageBox.Show(messageBoxText: "Are you sure to create?",
					caption: "Confirm",
					button: MessageBoxButton.YesNo,
					icon: MessageBoxImage.Question);
			if (result != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				_repo.Create(newAccount);
				result = MessageBox.Show(messageBoxText: "Created Successfully",
				   caption: "Alert",
				   button: MessageBoxButton.OK,
				   icon: MessageBoxImage.Information);
				Logger.log.Info($"An account with acoount number {newAccount.AccNumber} has been created successfully");
				this.NewAccount = new Account { AccNumber = 0, Name = "", Balance = 0, Type = "", Email = "", Phone = "", Address = "", IsActive = false, InterestPercentage = "0", TransactionCount = 0, LastTransactionDate = DateTime.Now };
			}
			catch (AccountException ae)
			{
				Logger.log.Error(ae.Message);
			}

			if (NewWindowClose != null)
			{
				NewWindowClose();
			}
		}
		/// <summary>
		/// Updates an existing account.
		/// </summary>
		public void Update()
		{
			if (this.SelectedAccount == null)
			{
				return;
			}

			var res = MessageBox.Show(messageBoxText: "Are you sure to Update?",
					caption: "Confirm",
					button: MessageBoxButton.YesNo,
					icon: MessageBoxImage.Question);

			if (res != MessageBoxResult.Yes)
			{
				return;
			}

			try
			{
				_repo.Update(this.SelectedAccount);
				this.SelectedAccount = this.SelectedAccount;
				var result = MessageBox.Show(messageBoxText: $"Account {SelectedAccount.AccNumber} is updated successfully",
						caption: "Alert",
						button: MessageBoxButton.OK,
						icon: MessageBoxImage.Information);
				Logger.log.Info($"Account {SelectedAccount.AccNumber} is updated successfully");
			}
			catch (AccountException ae)
			{
				Logger.log.Error(ae.Message);
			}


			if (EditWindowClose != null)
			{
				EditWindowClose();
			}
		}

		/// <summary>
		/// Deletes an existing account.
		/// </summary>
		public void Delete()
		{
			if (this.SelectedAccount == null)
			{
				var result = MessageBox.Show(messageBoxText: "Please select an account",
					caption: "Alert",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Information);
				return;
			}

			var res = MessageBox.Show(messageBoxText: "Are you sure to Delete?",
					caption: "Confirm",
					button: MessageBoxButton.YesNo,
					icon: MessageBoxImage.Question);

			if (res != MessageBoxResult.Yes)
			{
				return;
			}

			try
			{

				_repo.Delete(this.SelectedAccount);
				this.SelectedAccount = this.SelectedAccount;
				var result = MessageBox.Show(messageBoxText: $"Account {SelectedAccount.AccNumber} is marked as deleted successfully",
						caption: "Alert",
						button: MessageBoxButton.OK,
						icon: MessageBoxImage.Information);
				Logger.log.Info($"Account {SelectedAccount.AccNumber} is marked as deleted successfully");
			}
			catch (AccountException ae)
			{
				Logger.log.Error(ae.Message);
			}
		}


		public int CRUD { get; set; } = 1;//1-Create
		//IDataError Info implementation
		public string Error => null;

		public string this[string columnName]
		{
			get 
			{
				Account opAccount = CRUD ==1? NewAccount : SelectedAccount; //selected acc in edit window
				string validationMessage = null;
				switch (columnName)
				{
					case nameof(opAccount.Balance):
						if (NewAccount.Balance < 0)
						{
							validationMessage = "Balance cannot be negative";
						}
						break;
					case nameof(Email):
						if (opAccount.Email.Length == 0)
						{
							validationMessage = "Please enter email";
						}
						break;
				} 
				return validationMessage;
			}
		}

		//Wrapper
		public decimal Balance
		{
			get 
			{
				Account opAccount = CRUD == 1 ? NewAccount : SelectedAccount;
				return opAccount.Balance;
			}
			set
			{
				Account opAccount = CRUD == 1 ? NewAccount : SelectedAccount;
				opAccount.Balance = value;
				onPropertyChanged(nameof(Balance));
			}
		}

		public string Email
		{
			get
			{
				Account opAccount = CRUD == 1 ? NewAccount : SelectedAccount;
				return opAccount.Email;
			}
			set
			{
				Account opAccount = CRUD == 1 ? NewAccount : SelectedAccount;
				opAccount.Email = value;
				onPropertyChanged(nameof(Email));
			}
		}
	}
}
