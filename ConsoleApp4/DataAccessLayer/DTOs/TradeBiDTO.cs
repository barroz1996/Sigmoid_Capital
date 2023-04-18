using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsoleApp4.DataAccessLayer.DTOs
{
    // Type of csv table we get and parsing to sql
    class TradeBiDTO   
    {
        public const string TradeBIposTradeID = "TradeID";
        public const string TradeBItraderName = "Trader";
        public const string TradeBIbrokerName = "Broker";
        public const string TradeBISymbol = "Symbol";
        public const string TradeBIaccountID = "AccountID";
        public const string TradeBIaccountSize = "AccountSize";
        public const string TradeBIcurrStrategyName = "StrategyName";
        public const string TradeBItradeProfile = "Profile";
        public const string TradeBIentryType = "EntryType";
        public const string TradeBIexitType = "ExitType";
        public const string TradeBIstartDate = "StartDate";
        public const string TradeBIendDate = "EndDate";
        public const string TradeBIduration = "TradeDuartionMinute";
        public const string TradeBIcurrEntryPrice = "StartPrice";
        public const string TradeBIcurrExitPrice = "EndPrice";
        public const string TradeBItradeContracts = "Contracts";
        public const string TradeBIpositionSize = "PositionSize";
        public const string TradeBItradeMargin = "Margin";
        public const string TradeBItradeCommission = "Commission";
        public const string TradeBIprofit = "Profit";
        public const string TradeBIdrawDown = "DrawDown";
        public const string TradeBIdrawDownPercent = "DrawDownPercent";
        public const string TradeBIrunUp = "RunUp";
        public const string TradeBIrunUpPrecent = "RunUpPercent";
    
        private readonly Controllers.TradeBi _controller;

        public TradeBiDTO(string posTradeID, string traderName, string brokerName,string Symbol, string accountID, string accountSize, 
            string currStrategyName,string Profile, string Entry_Type, string Exit_Type, string Start_Date, string End_Date, string duration,
            string Start_Price,string End_Price, string Contracts, string Position_size, string Margin, string Commission, string Profit,
            string Drawdown,string Drawdown_percent, string Run_up, string Run_up_percernt)
        {
           TradeposTradeID = posTradeID;
           TradetraderName = traderName;
           TradebrokerName = brokerName;
           TradeSymbol = Symbol;
           TradeaccountID = accountID;
           TradeaccountSize = accountSize;
           TradecurrStrategyName = currStrategyName;
           TradetradeProfile = Profile;
           TradeentryType =Entry_Type;
           TradeexitType = Exit_Type;
           TradestartDate = Start_Date;
           TradeendDate = End_Date;
           Tradeduration = duration;
           TradecurrEntryPrice = Start_Price;
           TradecurrExitPrice = End_Price;
           TradetradeContracts = Contracts;
           TradepositionSize = Position_size;
           TradetradeMargin = Margin;
           TradetradeCommission = Commission;
           Tradeprofit = Profit;
           TradedrawDown = Drawdown;
           TradedrawDownPercent = Drawdown_percent;
           TraderunUp = Run_up;
           TraderunUpPercent = Run_up_percernt;

            _controller = new Controllers.TradeBi();
        }

        public string TradeposTradeID { get; set; }
        public string TradetraderName { get; set; }
        public string TradebrokerName { get; set; }
        public string TradeSymbol { get; set; }
        public string TradeaccountID { get; set; }
        public string TradeaccountSize { get; set; }
        public string TradecurrStrategyName { get; set; }
        public string TradetradeProfile { get; set; }
        public string TradeentryType { get; set; }
        public string TradeexitType { get; set; }
        public string TradestartDate { get; set; }
        public string TradeendDate { get; set; }
        public string Tradeduration { get; set; }
        public string TradecurrEntryPrice { get; set; }
        public string TradecurrExitPrice { get; set; }
        public string TradetradeContracts { get; set; }
        public string TradepositionSize { get; set; }
        public string TradetradeMargin { get; set; }
        public string TradetradeCommission { get; set; }
        public string Tradeprofit { get; set; }
        public string TradedrawDown { get; set; }
        public string TradedrawDownPercent { get; set; }
        public string TraderunUp { get; set; }
        public string TraderunUpPercent { get; set; }
        
    }
}
