using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemWpfApp
{
	public interface IBankRepo
	{
		void groupByInterestRate();
		void groupByTransactionCount();
	}
}
