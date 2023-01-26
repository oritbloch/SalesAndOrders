using System.Data.Common;
using System.Data;
using SalesDataServices;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace SalesDataServices
{
    public static class Constants
    {
        public const string SP_GetNumOfSalesAndOrdersData = "SP_GetNumOfSalesAndOrdersData";
        public const string SP_GetOrderTimesAndDates = "SP_GetOrderTimesAndDates";
    }
    public class ServiceHelper:IServiceHelper
    {
        private readonly IConfiguration _config;
        public ServiceHelper(IConfiguration config)
        {
            _config = config;
        }
        public  List<SaleDetails> GetNumOfSales(string orderBy, string orderDir)
        {
            string conString = _config.GetValue<string>("connectionString");
            List<SaleDetails> saleDetails = new List<SaleDetails>();
            DataSet resultsFromDB = DataBaseHelper.GetDataFromDB(conString,Constants.SP_GetNumOfSalesAndOrdersData, "@sortBy:" + orderBy, "@sortDirection:" + orderDir.ToLower());
            if (resultsFromDB != null)
            {
                foreach (DataRow row in resultsFromDB.Tables[0].Rows)
                {
                    saleDetails.Add(new SaleDetails
                    {
                        ProductID = (long)row["ProductID"],
                        ProductName = (!DBNull.Value.Equals(row["ProductName"])) ? row["ProductName"].ToString() : "",
                        City = (!DBNull.Value.Equals(row["City"])) ? row["City"].ToString() : "",
                        NumOfSales = (int)row["NumOfSales"]
                    });
                }
            }
            return saleDetails;
        }

        public  List<OrderTimes> GetOrderTimes(string beforeOrAfterTime)
        {
            string conString = _config.GetValue<string>("connectionString");
            List<OrderTimes> orderTimes = new List<OrderTimes>();
            DataSet resultsFromDB = DataBaseHelper.GetDataFromDB(conString,Constants.SP_GetOrderTimesAndDates, "@beforeOrAfterTime:" + beforeOrAfterTime.ToLower());
            if (resultsFromDB != null)
            {
                foreach (DataRow row in resultsFromDB.Tables[0].Rows)
                {
                    try
                    {
                        OrderTimes order = new OrderTimes();
                        order.OrderId = Convert.ToInt64(row["order_id"]);
                        if (row["order_date"] != DBNull.Value)
                        {
                            order.OrderDate = Convert.ToDateTime(row["order_date"]).ToString("dd/MM/yyyy");
                        }
                        if (row["required_date"] != DBNull.Value)
                        {
                            order.RequiredDate = Convert.ToDateTime(row["required_date"]).ToString("dd/MM/yyyy");
                        }
                        if (row["shipped_date"] != DBNull.Value)
                        {
                            order.ShippedDate = Convert.ToDateTime(row["shipped_date"]).ToString("dd/MM/yyyy");
                        }
                        order.NumOfDiffDays = (int)row["diff_days"];
                        orderTimes.Add(order);
                    }
                    catch(Exception ex)
                    {
                        //writeLog - error converting row with order order.OrderId to "OrderTimes" object:ex.ToString()

                    }
                }
            }
            return orderTimes;
        }
    }
}
