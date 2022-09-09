using ATMSoftware.DAL;
using Microsoft.Extensions.Configuration;

namespace ATMSoftware
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            char ch = 'n';
            GetAppSettingsFile();
            Console.WriteLine("WELCOME TO YES BANK ATM SERVICE\n");
            do
            {
                PrintOptions();
                int opt = Convert.ToInt32(Console.ReadLine());
                switch (opt)
                {
                    case 1: CheckBalance(); break;
                    case 2: WithdrawMoney(); break;
                    case 3: DepositMoney(); break;
                    case 4: AmountTransfer(); break;
                    case 5: ExitOperation(); break;
                    default : Console.WriteLine("\nNo Such Option Available.."); break;
                    
                }
                Console.WriteLine("\nDo You Want to Continue Banking..(y/n)");
                ch = Convert.ToChar(Console.ReadLine());
                Console.WriteLine("\n");
            }
            while (ch == 'y');
            Console.WriteLine("\n\nTHANKS FOR USING YES ATM SERVICE");

        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }

        static void PrintOptions()
        {
                Console.WriteLine("1. Check Balance \n");
                Console.WriteLine("2. Withdraw Money \n");
                Console.WriteLine("3. Deposit Money \n");
                Console.WriteLine("4. Transfer to another account \n");
                Console.WriteLine("5. Cancel\n");
                Console.WriteLine("***************\n\n");
                Console.WriteLine("ENTER YOUR CHOICE : \n");
        }

        static void CheckBalance()
        {
            Console.WriteLine("\nEnter Account Number \n");
            long accno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("\nEnter CardPin \n");
            int pin = Convert.ToInt32(Console.ReadLine());
            var BalanceDAL = new CountryDAL(_iconfiguration);
            var UserBalancelist = BalanceDAL.GetBalance(accno, pin);
            if(UserBalancelist.Count == 0)
            {
                Console.WriteLine("\nEnter Correct Credentials");
                return;
            }
            Console.WriteLine("\nYour Balance is: " + UserBalancelist[0].TotalBalance);
        }
        static void WithdrawMoney()
        {
            Console.WriteLine("\nEnter Account Number \n");
            long accno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("\nEnter CardPin \n");
            int pin = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter Amount to Withdraw \n");
            int amt = Convert.ToInt32(Console.ReadLine());
            var BalanceDAL = new CountryDAL(_iconfiguration);
            var UserBalancelist = BalanceDAL.GetBalance(accno, pin);
            if (UserBalancelist.Count == 0)
            {
                Console.WriteLine("Enter Correct Credentials");
                return;
            }
            else if (UserBalancelist[0].TotalBalance < amt)
            {
                Console.WriteLine($"Cannot Withdraw Rs {amt} your account have Rs {UserBalancelist[0].TotalBalance}");
                return;
            }

            BalanceDAL.SetWithdraw(accno, amt);
            Console.WriteLine("\nYour Balance is Updated: ");
            Console.WriteLine("\nYour Balance is:" + (UserBalancelist[0].TotalBalance - amt));
        }

        static void DepositMoney()
        {
            Console.WriteLine("\nEnter Account Number \n");
            long accno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("\nEnter CardPin \n");
            int pin = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter Amount to Deposit \n");
            int amt = Convert.ToInt32(Console.ReadLine());
            var BalanceDAL = new CountryDAL(_iconfiguration);
            var UserBalancelist = BalanceDAL.GetBalance(accno, pin);
            if (UserBalancelist.Count == 0)
            {
                Console.WriteLine("Enter Correct Credentials");
                return;
            }

            BalanceDAL.SetDeposit(accno, amt);
            Console.WriteLine("\nYour Balance is Updated:");
            Console.WriteLine("\nYour Balance is:" + (UserBalancelist[0].TotalBalance + amt));
        }

        static  void AmountTransfer()
        {
            Console.WriteLine("\nEnter Your Account Number \n");
            long accno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("\nEnter Receipent Account Number \n");
            long Toaccno = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("\nEnter Receipent Branch Name \n");
            string branch = Console.ReadLine();
            Console.WriteLine("\nEnter Amount to Transfer \n");
            int amt = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter Your Pin \n");
            int pin = Convert.ToInt32(Console.ReadLine());
            var BalanceDAL = new CountryDAL(_iconfiguration);
            var UserBalancelist = BalanceDAL.GetBalance(accno, pin);
            if (UserBalancelist.Count == 0)
            {
                Console.WriteLine("Enter Correct Credentials");
                return;
            }
            else if (UserBalancelist[0].TotalBalance < amt)
            {
                Console.WriteLine($"Cannot Transfer Rs {amt} your account have Rs {UserBalancelist[0].TotalBalance}");
                return;
            }

            BalanceDAL.SetTransaction(accno, Toaccno, branch, amt);
            Console.WriteLine($"\nRs {amt} is Successfully Transferred to {Toaccno} Receipent. ");
            BalanceDAL.SetWithdraw(accno, amt);
            BalanceDAL.SetDeposit(Toaccno, amt);
            Console.WriteLine("\nYour Balance is: " + (UserBalancelist[0].TotalBalance - amt));
        }

        static void ExitOperation()
        {
            return;
        }
    }
}