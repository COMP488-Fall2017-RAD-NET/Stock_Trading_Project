using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// A Transaction
/// </summary>
public class Transaction
{
    public int id { get; private set; }
    public int userId { get; private set; }
    public String ticker { get; private set; }
    public String type { get; private set; }
    public int amount { get; private set; }
    public double price { get; private set; }


    public Transaction(int id, int userId, String ticker, String type, int amount, double price)
    {
        this.id = id;
        this.userId = userId;
        this.ticker = ticker;
        this.type = type;
        this.amount = amount;
        this.price = price;
    }

    // needed for sql insert
    public override string ToString()
    {
        return String.Format("'{0}','{1}','{2}','{3}','{4}'", userId, ticker, type, amount, price);
    }
}