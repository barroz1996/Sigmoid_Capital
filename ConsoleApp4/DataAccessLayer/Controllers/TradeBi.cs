using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;

namespace ConsoleApp4.DataAccessLayer.Controllers
{



    class TradeBi : DalController
    {
        public TradeBi() : base("TradeBI")
        {
        }

        public bool Insert(DTOs.TradeBiDTO Trade) //Creates a new task in the database.
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand(connection);
                int res = -1;
                try
                {
                    connection.Open();
                   // command.CommandText = $"INSERT INTO {_tableName}  ({DTOs.TradeBiDTO.TasksIdColumnId} ,{DTOs.TradeBiDTO.TasksTitleColumnTitle}) " +
                     //   $"VALUES (@idVal,@titleVal);";
                     command.CommandText = $"INSERT INTO {_tableName}  ({DTOs.TradeBiDTO.TradeBIposTradeID} ,{DTOs.TradeBiDTO.TradeBItraderName},{DTOs.TradeBiDTO.TradeBIbrokerName}, {DTOs.TradeBiDTO.TradeBISymbol},{DTOs.TradeBiDTO.TradeBIaccountID},{DTOs.TradeBiDTO.TradeBIaccountSize},{DTOs.TradeBiDTO.TradeBIcurrStrategyName},{DTOs.TradeBiDTO.TradeBItradeProfile},{DTOs.TradeBiDTO.TradeBIentryType},{DTOs.TradeBiDTO.TradeBIexitType},{DTOs.TradeBiDTO.TradeBIstartDate},{DTOs.TradeBiDTO.TradeBIendDate},{DTOs.TradeBiDTO.TradeBIduration},{DTOs.TradeBiDTO.TradeBIcurrEntryPrice},{DTOs.TradeBiDTO.TradeBIcurrExitPrice},{DTOs.TradeBiDTO.TradeBItradeContracts},{DTOs.TradeBiDTO.TradeBIpositionSize},{DTOs.TradeBiDTO.TradeBItradeMargin},{DTOs.TradeBiDTO.TradeBItradeCommission},{DTOs.TradeBiDTO.TradeBIprofit},{DTOs.TradeBiDTO.TradeBIdrawDown},{DTOs.TradeBiDTO.TradeBIdrawDownPercent},{DTOs.TradeBiDTO.TradeBIrunUp},{DTOs.TradeBiDTO.TradeBIrunUpPrecent}) "+
                        $"VALUES (@tradeIDVal,@traderVal,@brokerVal,@symbolVal,@accountIDVal,@accountSizeVal,@StartegyNameVal,@profileVal,@entryTypeVal,@exitTypeVal,@startDateVal,@endDateVal,@durationVal ,@startPriceVal,@endPriceVal,@contractsVal,@positionSizeVal,@marginVal,@commissionVal,@profitVal,@drawDownVal,@drawDownPercentVal,@runUpVal,@runUpPercentVal);";
                    

                    var traderIDParam = new SQLiteParameter(@"tradeIDVal", Trade.TradeposTradeID);
                    var traderParam = new SQLiteParameter(@"traderVal", Trade.TradetraderName);
                    var brokerParam = new SQLiteParameter(@"brokerVal", Trade.TradebrokerName);
                    var symbolParam = new SQLiteParameter(@"symbolVal", Trade.TradeSymbol);
                    var accountIDParam = new SQLiteParameter(@"accountIDVal", Trade.TradeaccountID);
                    var accountSizeParam = new SQLiteParameter(@"accountSizeVal", Trade.TradeaccountSize);
                    var startegySizeParam = new SQLiteParameter(@"StartegyNameVal", Trade.TradecurrStrategyName);
                    var profileParam = new SQLiteParameter(@"profileVal", Trade.TradetradeProfile);
                    var entryTypeParam = new SQLiteParameter(@"entryTypeVal", Trade.TradeentryType);
                    var exitTypeParam = new SQLiteParameter(@"exitTypeVal", Trade.TradeexitType);
                    var start_dateParam = new SQLiteParameter(@"startDateVal", Trade.TradestartDate);
                    var end_dateParam = new SQLiteParameter(@"endDateVal", Trade.TradeendDate);
                    var durationParam = new SQLiteParameter(@"durationVal", Trade.Tradeduration);
                    var start_priceParam = new SQLiteParameter(@"startPriceVal", Trade.TradecurrEntryPrice);
                    var end_priceParam = new SQLiteParameter(@"endPriceVal", Trade.TradecurrExitPrice);
                    var contractsParam = new SQLiteParameter(@"contractsVal", Trade.TradetradeContracts);
                    var position_sizeParam = new SQLiteParameter(@"positionSizeVal", Trade.TradepositionSize);
                    var marginParam = new SQLiteParameter(@"marginVal", Trade.TradetradeMargin);
                    var commissionParam = new SQLiteParameter(@"commissionVal", Trade.TradetradeCommission);
                    var profitParam = new SQLiteParameter(@"profitVal", Trade.Tradeprofit);
                    var drawDownParam = new SQLiteParameter(@"drawDownVal", Trade.TradedrawDown);
                    var drawDownPercentParam = new SQLiteParameter(@"drawDownPercentVal", Trade.TradedrawDownPrecent);
                    var runUpParam = new SQLiteParameter(@"runUpVal", Trade.TraderunUp);
                    var runUpPercentParam = new SQLiteParameter(@"runUpPercentVal", Trade.TraderunUpPercent);
                 
                  

                    command.Parameters.Add(traderIDParam);
                    command.Parameters.Add(traderParam);
                    command.Parameters.Add(brokerParam);
                   
                    command.Parameters.Add(symbolParam);
                    command.Parameters.Add(accountIDParam);
                    command.Parameters.Add(accountSizeParam);
                    command.Parameters.Add(startegySizeParam);
                    command.Parameters.Add(profileParam);
                    command.Parameters.Add(entryTypeParam);
                    command.Parameters.Add(exitTypeParam);
                    command.Parameters.Add(start_dateParam);
                    command.Parameters.Add(end_dateParam);
                    command.Parameters.Add(durationParam);
                    command.Parameters.Add(start_priceParam);
                    command.Parameters.Add(end_priceParam);
                    command.Parameters.Add(contractsParam);
                    command.Parameters.Add(position_sizeParam);
                    command.Parameters.Add(marginParam);
                    command.Parameters.Add(commissionParam);
                    command.Parameters.Add(profitParam);
                    command.Parameters.Add(drawDownParam);
                    command.Parameters.Add(drawDownPercentParam);
                    command.Parameters.Add(runUpParam);
                    command.Parameters.Add(runUpPercentParam);
                 
                
      
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;

            }
        }
    }
}

