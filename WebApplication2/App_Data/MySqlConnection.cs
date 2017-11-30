using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// A class to manage connection with a SQL server
/// </summary>
public class MySqlConnection
{
    // properties
    private SqlConnection connection;
    private SqlCommand command;
    private SqlDataReader reader;
    private const String connString =
        "Data Source = sql.cs.luc.edu; Initial Catalog = stocktrading; Persist Security Info = True; User ID = afedorov; Password = p52521";

    public MySqlConnection()
    {
        connection = new SqlConnection();
    }

    // establish database connection
    public bool Connect()
    {
        connection.ConnectionString = connString;
        try
        {
            connection.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // housekeeping
    public void Disconnect()
    {
        reader.Close();
        command.Dispose();
        connection.Close();
    }

    public int InsertQuery(String sql)
    {
        //String sql = "insert into transactions values (1,'AAPL','BUY',100,98.54)";
        command = new SqlCommand(sql, connection);
        return command.ExecuteNonQuery();
    }

    public List<Transaction> SelectTransactions()
    {
        List<Transaction> transactions = new List<Transaction>();
        String sql = "select * from transactions";
        command = new SqlCommand(sql, connection);
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            transactions.Add(new Transaction(
                (int)reader.GetValue(0),
                (int)reader.GetValue(1),
                reader.GetValue(2).ToString().TrimEnd(),
                reader.GetValue(3).ToString().TrimEnd(),
                (int)reader.GetValue(4),
                (double)reader.GetValue(5)));
        }
        reader.Close();
        return transactions;
    }

    public int InsertTransaction(Transaction t)
    {
        String sql = String.Format("insert into transactions values ({0})", t.ToString());
        command = new SqlCommand(sql, connection);
        return command.ExecuteNonQuery();
    }

    public User SelectUser(int userId)
    {
        User user = new User(userId);
        String sql = String.Format("select * from Users u where u.Id = '{0}'", userId);
        command = new SqlCommand(sql, connection);
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            user.username = reader.GetValue(1).ToString().TrimEnd();
            user.firstName = reader.GetValue(2).ToString().TrimEnd();
            user.lastName = reader.GetValue(3).ToString().TrimEnd();
            user.email = reader.GetValue(4).ToString().TrimEnd();
            user.password = reader.GetValue(5).ToString().TrimEnd();
        }
        reader.Close();
        return user;
    }

    public Portfolio SelectUserPortfolio(User user)
    {
        Portfolio portfolio = new Portfolio(user);
        String sql = String.Format("select Ticker, Amount from Portfolio p where p.UserId = '{0}'", portfolio.GetUserId());
        command = new SqlCommand(sql, connection);
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            String ticker = reader.GetValue(0).ToString().TrimEnd();
            if (ticker == "MONEY")
            {
                portfolio.money = (double)reader.GetValue(1);
            }
            else
            {
                portfolio.stocks.Add(ticker, Convert.ToInt32(reader.GetValue(1)));
            }
        }
        reader.Close();
        return portfolio;
    }

    public int UpdateUserPortfolio(Transaction t, Portfolio p)
    {
        int result = 0, pAmount = 0;

        String sql;
        double pMoney = p.money;
        bool isNewStock = !p.stocks.TryGetValue(t.ticker, out pAmount);

        switch (t.type)
        {
            case "BUY":
                // update money
                pMoney -= t.amount * t.price;
                sql = String.Format("update Portfolio set Amount = '{0}' where UserId = '{1}' and Ticker = 'MONEY'", pMoney, p.GetUserId());
                command = new SqlCommand(sql, connection);
                result += command.ExecuteNonQuery();

                // update stock
                pAmount += t.amount;
                if (isNewStock)
                {
                    sql = String.Format("insert into Portfolio values('{0}','{1}','{2}')",
                        p.GetUserId(), t.ticker, pAmount);
                }
                else
                {
                    sql = String.Format("update Portfolio set Amount = '{0}' where UserId = '{1}' and Ticker = '{2}'",
                        pAmount, p.GetUserId(), t.ticker);
                }
                command = new SqlCommand(sql, connection);
                result += command.ExecuteNonQuery();

                break;

            case "SELL":


                break;
        }

        return result;
    }


}