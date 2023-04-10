using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net.Mail;
using System.Net.Http;
using System.Text;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp4.DataAccessLayer.Controllers
{
    class TradeBi : DalController
    {
        public TradeBi() : base("TradeBI")
        {
        }

        public async Task<bool> InsertAsync(DTOs.TradeBiDTO Trade,bool crash) //Creates a new task in the database.
        {
            using (var connection = new SqlConnection(@"Data Source=DESKTOP-0G3N8AU;Initial Catalog=TradeBIDataBase;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Insert into TableBIT values(@tradeID,@trader,@broker,@symbol,@accountID,@accountSize,@strategyName,@profile,@entryType,@exitType,@startDate,@endDate,@tradeDurationMinute,@startPrice,@endPrice,@contracts,@positionSize,@margin,@commission,@profit,@drawDown,@drawDownPercent,@runUp,@runUpPercent)", connection);
                //var command = new SQLiteCommand(connection);
                int res = -1;
                try
                {
                    cmd.Parameters.AddWithValue("@tradeID", Trade.TradeposTradeID);
                    cmd.Parameters.AddWithValue("@trader", Trade.TradetraderName);
                    cmd.Parameters.AddWithValue("@broker", Trade.TradebrokerName);
                    cmd.Parameters.AddWithValue("@symbol", Trade.TradeSymbol);
                    cmd.Parameters.AddWithValue("@accountID", Trade.TradeaccountID);

                    if (!string.Equals(Trade.TradeaccountSize, "Null"))
                        cmd.Parameters.AddWithValue("@accountSize", float.Parse(Trade.TradeaccountSize));
                    else
                        cmd.Parameters.AddWithValue("@accountSize", DBNull.Value);
                    cmd.Parameters.AddWithValue("@strategyName", Trade.TradecurrStrategyName);
                    cmd.Parameters.AddWithValue("@profile", Trade.TradetradeProfile);
                    cmd.Parameters.AddWithValue("@entryType", Trade.TradeentryType);
                    cmd.Parameters.AddWithValue("@exitType", Trade.TradeexitType);

                    if (!string.Equals(Trade.TradestartDate, "Null"))
                        cmd.Parameters.AddWithValue("@startDate", DateTime.Parse(Trade.TradestartDate));
                    else
                        cmd.Parameters.AddWithValue("@startDate", DBNull.Value);

                    if (!string.Equals(Trade.TradeendDate, "Null"))
                        cmd.Parameters.AddWithValue("@endDate", DateTime.Parse(Trade.TradeendDate));
                    else
                        cmd.Parameters.AddWithValue("@endDate", DBNull.Value);

                    if (!string.Equals(Trade.Tradeduration, "Null"))
                        cmd.Parameters.AddWithValue("@tradeDurationMinute", float.Parse(Trade.Tradeduration));
                    else
                        cmd.Parameters.AddWithValue("@tradeDurationMinute", DBNull.Value);

                    if (!string.Equals(Trade.TradecurrEntryPrice, "Null"))
                        cmd.Parameters.AddWithValue("@startPrice", float.Parse(Trade.TradecurrEntryPrice));
                    else
                        cmd.Parameters.AddWithValue("@startPrice", DBNull.Value);

                    if (!string.Equals(Trade.TradecurrExitPrice, "Null"))
                        cmd.Parameters.AddWithValue("@endPrice", float.Parse(Trade.TradecurrExitPrice));
                    else
                        cmd.Parameters.AddWithValue("@endPrice", DBNull.Value);

                    if (!string.Equals(Trade.TradetradeContracts, "Null"))
                        cmd.Parameters.AddWithValue("@contracts", float.Parse(Trade.TradetradeContracts));
                    else
                        cmd.Parameters.AddWithValue("@contracts", DBNull.Value);

                    if (!string.Equals(Trade.TradepositionSize, "Null"))
                        cmd.Parameters.AddWithValue("@positionSize", float.Parse(Trade.TradepositionSize));
                    else
                        cmd.Parameters.AddWithValue("@positionSize", DBNull.Value);

                    if (!string.Equals(Trade.TradetradeMargin, "Null"))
                        cmd.Parameters.AddWithValue("@margin", float.Parse(Trade.TradetradeMargin));
                    else
                        cmd.Parameters.AddWithValue("@margin", DBNull.Value);

                    if (!string.Equals(Trade.TradetradeCommission, "Null"))
                        cmd.Parameters.AddWithValue("@commission", float.Parse(Trade.TradetradeCommission));
                    else
                        cmd.Parameters.AddWithValue("@commission", DBNull.Value);

                    if (!string.Equals(Trade.Tradeprofit, "Null"))
                        cmd.Parameters.AddWithValue("@profit", float.Parse(Trade.Tradeprofit));
                    else
                        cmd.Parameters.AddWithValue("@profit", DBNull.Value);

                    if (!string.Equals(Trade.TradedrawDown, "Null"))
                        cmd.Parameters.AddWithValue("@drawDown", float.Parse(Trade.TradedrawDown));
                    else
                        cmd.Parameters.AddWithValue("@drawDown", DBNull.Value);

                    if (!string.Equals(Trade.TradedrawDownPercent, "Null"))
                        cmd.Parameters.AddWithValue("@drawDownPercent", float.Parse(Trade.TradedrawDownPercent));
                    else
                        cmd.Parameters.AddWithValue("@drawDownPercent", DBNull.Value);

                    if (!string.Equals(Trade.TraderunUp, "Null"))
                        cmd.Parameters.AddWithValue("@runUp", float.Parse(Trade.TraderunUp));
                    else
                        cmd.Parameters.AddWithValue("@runUp", DBNull.Value);

                    if (!string.Equals(Trade.TraderunUpPercent, "Null"))
                        cmd.Parameters.AddWithValue("@runUpPercent", float.Parse(Trade.TraderunUpPercent));
                    else
                        cmd.Parameters.AddWithValue("@runUpPercent", DBNull.Value); 
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    if (!crash)
                    {
                        StreamWriter sw = new StreamWriter("log.txt", true);
                        sw.WriteLine();
                        sw.WriteLine(e.Message);
                        sw.WriteLine("The record details: " + Trade.TradeposTradeID + "," + Trade.TradetraderName + "," + Trade.TradebrokerName + "," + Trade.TradeSymbol + "," + Trade.TradeaccountID + "," + Trade.TradeaccountSize + "," + Trade.TradecurrStrategyName + "," + Trade.TradetradeProfile + "," + Trade.TradeentryType + "," + Trade.TradeexitType + "," + Trade.TradestartDate + "," + Trade.TradeendDate + "," + Trade.Tradeduration + "," + Trade.TradecurrEntryPrice + "," + Trade.TradecurrExitPrice + "," + Trade.TradetradeContracts + "," + Trade.TradepositionSize + "," + Trade.TradetradeMargin + "," + Trade.TradetradeCommission + "," + Trade.Tradeprofit + "," + Trade.TradedrawDown + "," + Trade.TradedrawDownPercent + "," + Trade.TradedrawDownPercent + "," + Trade.TraderunUp + "," + Trade.TraderunUpPercent);
                        sw.Close();
                        // Console.WriteLine(e);
                        // var WebHookUrl = "https://hooks.slack.com/services/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";

                        /*
                        var errorObj = new
                        {
                            Message = e.Message,

                        };
                        var errorJson = JsonConvert.SerializeObject(errorObj);
                        var mainObj = new
                        {
                            Error = errorJson
                        };
                        var mainJson = JsonConvert.SerializeObject(mainObj);
                       */
                        // var httpClient = new HttpClient();
                        // var webhookUrl = "https://hooks.slack.com/services/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";
                        // var payload = "{\"text\": \"Hello, Slack!\"}";
                        //var content = new StringContent(mainJson, Encoding.UTF8, "application/json");
                        // var response = await httpClient.PostAsync(webhookUrl, content);
                     }
                }
                finally
                {
                    connection.Close();
                }
                return res > 0;
            }
        }
    }
}

