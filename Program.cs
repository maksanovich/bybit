namespace bybit;

using System;
using bybit.net.api;
using bybit.net.api.ApiServiceImp;
using bybit.net.api.Models;
using bybit.net.api.Models.Account;
using bybit.net.api.Models.Market;
using bybit.net.api.Models.Trade;
using MySql.Data.MySqlClient;
using bybit.net.api.WebSocketStream;
public struct OrderData {
    public string CoinName;
    public string EntryPrice;
    public string TypeOrder;
    public string VolumnNumber;
    public string SliceSize;
    public string StopLess;
    public string TPL1;
    public string TPL2;
    public string TPL3;
    public string TPL4;
    public string TPL5;
}
class Program
{
    static async Task Main(string[] args)
    {
        
        MySqlConnection conn;
        string strConnection = "server=localhost;uid=root;pwd=secret;database=db_trade;";

        try
        {
            conn = new MySqlConnection();
            conn.ConnectionString = strConnection;
            conn.Open();
 
            string strQuery = "SELECT *FROM bybit_datas";
            MySqlCommand cmd = new MySqlCommand(strQuery, conn);
            cmd.CommandText = strQuery;
            cmd.ExecuteNonQuery();
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<OrderData> lstOrder = new List<OrderData>();

            while(rdr.Read()) {
                OrderData orderData;
                orderData.CoinName = rdr.GetString(1);
                orderData.EntryPrice = rdr.GetString(1);
                orderData.TypeOrder = rdr.GetString(1);
                orderData.VolumnNumber = rdr.GetString(1);
                orderData.SliceSize = rdr.GetString(1);
                orderData.StopLess = rdr.GetString(1);
                orderData.TPL1 = rdr.GetString(1);
                orderData.TPL2 = rdr.GetString(1);
                orderData.TPL3 = rdr.GetString(1);
                orderData.TPL4 = rdr.GetString(1);
                orderData.TPL5 = rdr.GetString(1);
                lstOrder.Add(orderData);
            }
            rdr.Close();

            strQuery = "SELECT *FROM bybit_users";
            cmd.CommandText = strQuery;
            cmd.ExecuteNonQuery();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string apiKey = rdr.GetString(2);
                string apiSecret = rdr.GetString(3);


                BybitAccountService accountService = new(apiKey: apiKey, apiSecret: apiSecret, BybitConstants.HTTP_TESTNET_URL);
                var accountInfo = await accountService.GetAccountBalance(accountType: AccountType.Unified);
                Console.WriteLine(accountInfo);
                // await Task.Run(() => StartUser(apiKey, apiSecret, lstOrder));
            }
        } catch (Exception ex) {
            Console.WriteLine("Error Detected: " + ex.Message);
        }
    }

    static async Task StartUser(string apiKey, string apiSecret, List<OrderData> lstOrder) {
        Console.WriteLine(apiKey + " " + apiSecret + " " + lstOrder[0].CoinName);
        // Console.WriteLine(lstOrder);
        BybitTradeService tradeService = new(apiKey: apiKey, apiSecret: apiSecret, BybitConstants.HTTP_TESTNET_URL);
        var orderInfo = await tradeService.PlaceOrder(category: Category.LINEAR, symbol: "ETHUSDT", side: Side.BUY, orderType: OrderType.LIMIT, qty: "1", timeInForce: TimeInForce.GTC, price: "1000");
        Console.WriteLine(orderInfo);
    }
}
