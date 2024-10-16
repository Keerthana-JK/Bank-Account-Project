﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWpfApp
{
	public interface IAccountRepo
	{
		/// <summary>
		/// Creates a new account.
		/// </summary>
		/// <param name="account">The account to create.</param>
		void Create(Account account);

		/// <summary>
		/// Updates an existing account.
		/// </summary>
		/// <param name="account">The account to update.</param>
		void Update(Account account);

		/// <summary>
		/// Deletes an account.
		/// </summary>
		/// <param name="acNo">The account number of the account to delete.</param>
		/// <param name="account">The account to delete.</param>
		void Delete(Account account);

		/// <summary>
		/// Deposits money into an account.
		/// </summary>
		/// <param name="acNo">The account number of the account to deposit into.</param>
		/// <param name="Amount">The amount to deposit.</param>
		void Deposit(int acNo, int Amount);

		/// <summary>
		/// Withdraws money from an account.
		/// </summary>
		/// <param name="acNo">The account number of the account to withdraw from.</param>
		/// <param name="Amount">The amount to withdraw.</param>
		void Withdraw(int acNo, int Amount);

		/// <summary>
		/// Calculates the interest and updates the balance for all accounts.
		/// </summary>
		void CalculateInterestAndUpdateBalance();

		/// <summary>
		/// Retrieves all accounts.
		/// </summary>
		/// <returns>A collection of all accounts.</returns>
		ObservableCollection<Account> ReadAll();
	}
}
