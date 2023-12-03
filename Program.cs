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
using Newtonsoft.Json;

public struct OrderData {
    public string CoinName;
    public string EntryPrice;
    public string TypeOrder;
    public string VolumnNumber;
    public string SliceSize;
    public string StopLoss;
    public string TPL1;
    public string TPL2;
    public string TPL3;
    public string TPL4;
    public string TPL5;
    public string TPL6;
    public string Qty;
    public string Type;
}
class Program
{
    static async Task Main(string[] args)
    {
        
        Console.WriteLine("Starting Bybit Bot...");
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
                orderData.EntryPrice = rdr.GetString(2);
                orderData.TypeOrder = rdr.GetString(3);
                orderData.VolumnNumber = rdr.GetString(4);
                orderData.SliceSize = rdr.GetString(5);
                orderData.StopLoss = rdr.GetString(6);
                orderData.TPL1 = rdr.GetString(7);
                orderData.TPL2 = rdr.GetString(8);
                orderData.TPL3 = rdr.GetString(9);
                orderData.TPL4 = rdr.GetString(10);
                orderData.TPL5 = rdr.GetString(11);
                orderData.TPL6 = rdr.GetString(12);
                orderData.Qty = rdr.GetString(13);
                orderData.Type = rdr.GetString(14);
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


                // BybitAccountService accountService = new(apiKey: apiKey, apiSecret: apiSecret, BybitConstants.HTTP_TESTNET_URL);
                // var accountInfo = await accountService.GetAccountBalance(accountType: AccountType.Unified);
                // Console.WriteLine(accountInfo);
                await Task.Run(() => StartUser(apiKey, apiSecret, lstOrder));
            }
        } catch (Exception ex) {
            Console.WriteLine("Error Detected: " + ex.Message);
        }
    }

    static async Task StartUser(string apiKey, string apiSecret, List<OrderData> lstOrder) {
        for(int i = 0; i < lstOrder.Count; i++) {
            BybitTradeService tradeService = new(apiKey: apiKey, apiSecret: apiSecret, BybitConstants.HTTP_TESTNET_URL);
            var orderInfo = "";
            if(lstOrder[i].Type == "Buy") {
                orderInfo = await tradeService.PlaceOrder(category: Category.LINEAR, symbol: lstOrder[i].CoinName, side: Side.BUY, orderType: OrderType.LIMIT, qty: lstOrder[i].Qty, timeInForce: TimeInForce.GTC, price: lstOrder[i].EntryPrice, takeProfit: lstOrder[i].TPL1, stopLoss: lstOrder[i].StopLoss);
            } else {
                orderInfo = await tradeService.PlaceOrder(category: Category.LINEAR, symbol: lstOrder[i].CoinName, side: Side.SELL, orderType: OrderType.LIMIT, qty: lstOrder[i].Qty, timeInForce: TimeInForce.GTC, price: lstOrder[i].EntryPrice, takeProfit: lstOrder[i].TPL1, stopLoss: lstOrder[i].StopLoss);
            }
            dynamic jsonObj = JsonConvert.DeserializeObject(orderInfo);
            string orderId = jsonObj.result.orderId;
            Console.WriteLine("PlaceOrder:" + orderInfo);
            await Task.Run(() => AutoTrade(apiKey, apiSecret, orderId, lstOrder[i]));
        }
    }

    static async Task AutoTrade(string apiKey, string apiSecret, string orderId, OrderData orderData) {
        int nCount = 0;
        BybitTradeService tradeService = new(apiKey: apiKey, apiSecret: apiSecret, BybitConstants.HTTP_TESTNET_URL);
        while(true) {
            if(nCount % 5 == 0) {
                var orderInfo = await tradeService.AmendOrder(orderId: orderId, category: Category.LINEAR, symbol: orderData.CoinName, qty: orderData.Qty, takeProfit: orderData.TPL2, stopLoss: orderData.StopLoss/*, price: orderData.TPL2*/);
                Console.WriteLine("AmendOrder:" + orderInfo);
            }
            if(nCount % 5 == 1) {
                var orderInfo = await tradeService.AmendOrder(orderId: orderId, category: Category.LINEAR, symbol: orderData.CoinName, qty: orderData.Qty, takeProfit: orderData.TPL3, stopLoss: orderData.StopLoss/*, price: orderData.TPL2*/);
                Console.WriteLine("AmendOrder:" + orderInfo);
            }
            if(nCount % 5 == 2) {
                var orderInfo = await tradeService.AmendOrder(orderId: orderId, category: Category.LINEAR, symbol: orderData.CoinName, qty: orderData.Qty, takeProfit: orderData.TPL4, stopLoss: orderData.StopLoss/*, price: orderData.TPL2*/);
                Console.WriteLine("AmendOrder:" + orderInfo);
            }
            if(nCount % 5 == 3) {
                var orderInfo = await tradeService.AmendOrder(orderId: orderId, category: Category.LINEAR, symbol: orderData.CoinName, qty: orderData.Qty, takeProfit: orderData.TPL5, stopLoss: orderData.StopLoss/*, price: orderData.TPL2*/);
                Console.WriteLine("AmendOrder:" + orderInfo);
            }
            if(nCount % 5 == 4) {
                var orderInfo = await tradeService.AmendOrder(orderId: orderId, category: Category.LINEAR, symbol: orderData.CoinName, qty: orderData.Qty, takeProfit: orderData.TPL6, stopLoss: orderData.StopLoss/*, price: orderData.TPL2*/);
                Console.WriteLine("AmendOrder:" + orderInfo);
            }
            nCount++;
            await Task.Delay(3000);
        }
    }
}
