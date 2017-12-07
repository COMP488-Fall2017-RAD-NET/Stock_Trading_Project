using System;
using System.Collections.Generic;

/// <summary>
/// User's Portfolio
/// </summary>
public class Portfolio
{
    public User user { get; private set; }
    public Dictionary<String, int> stocks { get; set; }
    public double money { get; set; }
    public double initialValue { get; private set; }
    public double currentValue { get; set; }
    public IList<Stock> stocksList { get; set; }

    public Portfolio(User user)
    {
        this.user = user;
        this.stocks = new Dictionary<string, int>();
        this.money = 10000.0;
        this.initialValue = money;
    }

    public int GetUserId()
    {
        return user.id;
    }

    public int GetStock(String ticker)
    {
        int value;
        if (stocks.TryGetValue(ticker, out value))
        {
            return value;
        }
        else
        {
            return -1;
        }
    }

    public override string ToString()
    {
        String result = String.Format("Money: {0}", money);
        foreach (String key in stocks.Keys)
        {
            int value;
            stocks.TryGetValue(key, out value);
            result = String.Concat(String.Format(", {0}{1}: {2}", result, key, value.ToString()));
        }

        return result;
    }

    public void UpdateCurrentValue()
    {
        stocksList.Clear();
        foreach (String key in stocks.Keys)
        {
            int v;
            if (stocks.TryGetValue(key, out v))
            {
                if (key != "MONEY")
                {
                    Stock s = new Stock(key);
                    stocksList.Add(s);
                    currentValue += v * s.currentPrice;
                }
                else
                {
                    currentValue += v;
                }
            }
        }
    }

    public Stock GetStockFromList(String ticker)
    {
        foreach (Stock s in stocksList)
        {
            if (s.ticker == ticker)
            {
                return s;
            }
        }

        return null;
    }

}