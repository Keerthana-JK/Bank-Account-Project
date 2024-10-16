﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWpfApp
{
	/// <summary>
	/// Represents a repository for managing accounts in memory.
	/// </summary>
	public class AccountMemoryRepo : IAccountRepo
	{
		/// <summary>
		/// Gets the instance of the AccountMemoryRepo class.
		/// </summary>
		private static AccountMemoryRepo _instance;
		private ObservableCollection<Account> accounts;

		/// <summary>
		/// Initializes a new instance of the AccountMemoryRepo class.
		/// </summary>
		private AccountMemoryRepo()
		{
			accounts = new ObservableCollection<Account>();
			InitializeAccounts();

		}
		/// <summary>
		/// Initializes the accounts collection with default accounts.
		/// </summary>
		private void InitializeAccounts()
		{
			accounts.Add(new Account
			{
				AccNumber = 1234,
				Name = "Lian",
				Balance = 0,
				Type = "savings",
				Email = "lian@gmail.com",
				Phone = "6526526567",
				Address = "Address",
				IsActive = true,
				InterestPercentage = "0",
				TransactionCount = 0,
				LastTransactionDate = DateTime.Now,
			});
			accounts.Add(new Account
			{
				AccNumber = 12345,
				Name = "Peter",
				Balance = 0,
				Type = "current",
				Email = "peter@gmail.com",
				Phone = "2652652365",
				Address = "address",
				IsActive = true,
				InterestPercentage = "0",
				TransactionCount = 0,
				LastTransactionDate = DateTime.Now,
			});
		}

		/// <summary>
		/// Creates an object for the AccountMemoryRepo class
		/// </summary>
		public static AccountMemoryRepo Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new AccountMemoryRepo();
				}
				return _instance;
			}
		}


		/// <summary>
		/// Creates a new account in the repository.
		/// </summary>
		/// <param name="account">The account to create.</param>
		/// <exception cref="AccountException">Thrown if an error occurs while creating the account.</exception>
		public void Create(Account account)
		{
			try
			{
				accounts.Add(account);
			}
			catch (AccountException ae)
			{
				throw new AccountException("Error in creating account");
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		/// <summary>
		/// Updates an existing account in the repository.
		/// </summary>
		/// <param name="account">The account to update.</param>
		/// <exception cref="AccountException">Thrown if the account does not exist.</exception>
		public void Update(Account account)
		{
			try
			{
				var existingAccount = accounts.FirstOrDefault(a => a.AccNumber == account.AccNumber);
				if (existingAccount != null)
				{
					existingAccount.Address = account.Address;
				}
				else
				{
					throw new AccountException("Account doesn't exists");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		/// <summary>
		/// Retrieves all accounts from the repository.
		/// </summary>
		/// <returns>A collection of all accounts in the repository.</returns>
		/// <exception cref="AccountException">Thrown if an error occurs while reading accounts.</exception>
		public ObservableCollection<Account> ReadAll()
		{
			try
			{
				return accounts;
			}
			catch (AccountException ae)
			{
				throw new AccountException("Error reading accounts");
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		/// <summary>
		/// Deletes an account from the repository.
		/// </summary>
		/// <param name="acNo">The account number of the account to delete.</param>
		/// <param name="account">The account to delete.</param>
		public void Delete(Account account)
		{
			try
			{
				var existingAccount = accounts.FirstOrDefault(a => a.AccNumber == account.AccNumber);
				if (existingAccount != null)
				{
					existingAccount.IsActive = false;
				}
				else
				{
					throw new AccountException("Account doesn't exists");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Deposits a specified amount into an account.
		/// </summary>
		/// <param name="acNo">The account number of the account to deposit into.</param>
		/// <param name="Amount">The amount to deposit.</param>
		/// <exception cref="AccountException">Thrown if the account does not exist</exception>
		public void Deposit(int acNo, int Amount)
		{
			try
			{
				var account = accounts.FirstOrDefault(a => a.AccNumber == acNo);
				if (account != null)
				{
					account.Balance = account.Balance + Amount;
					account.LastTransactionDate = DateTime.Now;
					account.TransactionCount = account.TransactionCount + 1;

				}
				else
				{
					throw new AccountException("Account Not Found , Please input valid account number");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Withdraws a specified amount from an account.
		/// </summary>
		/// <param name="acNo">The account number of the account to withdraw from.</param>
		/// <param name="Amount">The amount to withdraw.</param>
		/// <exception cref="AccountException">Thrown if the account does not exist or if the balance is insufficient.</exception>
		public void Withdraw(int acNo, int Amount)
		{
			try
			{
				var account = accounts.FirstOrDefault(a => a.AccNumber == acNo);
				if (account != null)
				{
					if (account.Balance < Amount)
					{
						throw new AccountException("Insufficient balance");

					}
					account.Balance = account.Balance - Amount;
					account.LastTransactionDate = DateTime.Now;
					account.TransactionCount = account.TransactionCount + 1;

				}
				else
				{
					throw new AccountException("Account Not Found , Please input valid account number");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		public void Transfer(int senderAccNo, int receiverAccNo, int amount)
		{
			try
			{
				Withdraw(senderAccNo, amount);
				Deposit(receiverAccNo, amount);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void CalculateInterestAndUpdateBalance()
		{
			throw new NotImplementedException();
		}

	}
}
