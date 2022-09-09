using ATMSoftware.Model;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ATMSoftware.DAL
{
    public class CountryDAL
    {
        private string _connectionString;
        public CountryDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<UserModel> GetBalance(long Accno, int Pin)
        {
            var UserModellist = new List<UserModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("CheckBalance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param1 = new SqlParameter
                    {
                        ParameterName = "@AccountNumber",
                        SqlDbType = SqlDbType.BigInt,
                        Value = Accno,
                        Direction = ParameterDirection.Input,

                    };
                    SqlParameter param2 = new SqlParameter
                    {
                        ParameterName = "@CardPin",
                        SqlDbType = SqlDbType.Int,
                        Value = Pin,
                        Direction = ParameterDirection.Input,

                    };
                    cmd.Parameters.Add(param1);
                    cmd.Parameters.Add(param2);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UserModellist.Add(new UserModel
                        {
                            TotalBalance = Convert.ToInt32(rdr[0]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UserModellist;
        }
        public void SetDeposit (long Accno, float amt)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DepositMoney", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param1 = new SqlParameter
                    {
                        ParameterName = "@AccountNumber",
                        SqlDbType = SqlDbType.BigInt,
                        Value = Accno,
                        Direction = ParameterDirection.Input,

                    };
                    SqlParameter param2 = new SqlParameter
                    {
                        ParameterName = "@Amount",
                        SqlDbType = SqlDbType.Float,
                        Value = amt,
                        Direction = ParameterDirection.Input,

                    };
                    cmd.Parameters.Add(param1);
                    cmd.Parameters.Add(param2);
                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetWithdraw(long Accno, float amt)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("WithdrawMoney", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param1 = new SqlParameter
                    {
                        ParameterName = "@AccountNumber",
                        SqlDbType = SqlDbType.BigInt,
                        Value = Accno,
                        Direction = ParameterDirection.Input,

                    };
                    SqlParameter param2 = new SqlParameter
                    {
                        ParameterName = "@Amount",
                        SqlDbType = SqlDbType.Float,
                        Value = amt,
                        Direction = ParameterDirection.Input,

                    };
                    cmd.Parameters.Add(param1);
                    cmd.Parameters.Add(param2);
                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetTransaction(long Accno, long ToAccno, string branch, float amt)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("NewTransactions", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param1 = new SqlParameter
                    {
                        ParameterName = "@AccountNumber",
                        SqlDbType = SqlDbType.BigInt,
                        Value = Accno,
                        Direction = ParameterDirection.Input,

                    };
                    SqlParameter param2 = new SqlParameter
                    {
                        ParameterName = "@TransferAccount",
                        SqlDbType = SqlDbType.BigInt,
                        Value = ToAccno,
                        Direction = ParameterDirection.Input,

                    };
                    SqlParameter param3 = new SqlParameter
                    {
                        ParameterName = "@BranchName",
                        SqlDbType = SqlDbType.VarChar,
                        Value = branch,
                        Direction = ParameterDirection.Input,

                    };
                    SqlParameter param4 = new SqlParameter
                    {
                        ParameterName = "@AmountToTransfer",
                        SqlDbType = SqlDbType.Float,
                        Value = amt,
                        Direction = ParameterDirection.Input,

                    };
                    cmd.Parameters.Add(param1);
                    cmd.Parameters.Add(param2);
                    cmd.Parameters.Add(param3);
                    cmd.Parameters.Add(param4);
                    con.Open();
                    cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}