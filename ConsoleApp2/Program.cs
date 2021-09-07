using System;
using System.Linq;
using System.Runtime.InteropServices;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete;
using DataAccess.Concrete.Dapper;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Dapper

            //OrderManager orderManager = new OrderManager(new DapperOrderDal());
            //GetOrderDetailsConsole(orderManager);

            //var order = new Order()
            //{ CustomerID = "ALFKI", OrderDate = DateTime.Now.AddDays(1), ShippedDate = DateTime.Now.AddMonths(1) };
            //AddOrderConsole(orderManager, order);

            //var order = new Order()
            //{ CustomerID = "ALFKI", OrderDate = DateTime.Now.AddDays(1), ShippedDate = DateTime.Now.AddMonths(1), ShipName = "helehele", OrderID = 11078 };
            //UpdateOrderConsole(orderManager, order);

            //var result = DeleteOrderConsole(orderManager, 11078); 
            #endregion

            #region EntityFramework
            OrderManager orderManager = new OrderManager(new EfOrderDal());
            //GetAllOrderConsole(orderManager);

            //var order = new Order(){ CustomerID = "ALFKI", OrderDate = DateTime.Now.AddDays(1), ShippedDate = DateTime.Now.AddMonths(1) ,ShipName = "denemef"};
            //AddOrderConsole(orderManager, order);

            //var order = new Order()
            //{ CustomerID = "ALFKI", OrderDate = DateTime.Now.AddDays(1), ShippedDate = DateTime.Now.AddMonths(1), ShipName = "helehele", OrderID = 11079 };
            //UpdateOrderConsole(orderManager, order);

            //var result = DeleteOrderConsole(orderManager, 11079); 

            var list = orderManager.GetAllByShippedName("Toms").Data;
            foreach (var item in list)
            {
                Console.WriteLine(item.ShipName);
            }

            #endregion
        }

        #region Show Data
        public static void GetAllOrderConsole(OrderManager manager)
        {
            var all = manager.GetAll().Data.Where(f => f.OrderID < 10260);

            foreach (var item in all)
            {
                var sonuc = "Order Id: " + item.OrderID + " - " + " Order Date: " + item.OrderDate;
                Console.WriteLine(sonuc);
            }
        }
        public static void GetOrderDetailsConsole(OrderManager manager)
        {
            var all = manager.GetOrderDetails("Select Top(20) * FROM [Northwind].[dbo].[Orders] as o left join [Northwind].[dbo].[Customers] as c on o.CustomerID=c.CustomerID;").Data;

            foreach (var item in all)
            {
                var sonuc = "Order Id: " + item.CompanyName + " - " + " Order Date: " + item.OrderDate;
                Console.WriteLine(sonuc);
            }
        }
        public static IDataResult<long> AddOrderConsole(OrderManager manager, Order newOrder)
        {
            return manager.Add(newOrder);
        }
        public static bool UpdateOrderConsole(OrderManager manager, Order updateOrder)
        {
            return manager.Update(updateOrder).Success;
        }
        public static bool DeleteOrderConsole(OrderManager manager, int id)
        {
            return manager.Delete(id).Success;
        }

        #endregion
    }
}
