using System;
using System.Linq;
using System.Runtime.InteropServices;
using Business.Concrete;
using Core.DataAccess.Nhibernate;
using Core.Utilities.Results;
using DataAccess.Concrete;
using DataAccess.Concrete.Dapper;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.Nhibernate;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderManager orderManager;

            #region Dapper

            //OrderManager orderManager = new OrderManager(new DapperOrderDal());
            #endregion

            #region EntityFramework
            orderManager = new OrderManager(new EfOrderDal());
            #endregion

           
            GetAllOrderConsole(orderManager);

            //var order = new Order() { CustomerID = "ALFKI", OrderDate = DateTime.Now.AddDays(1), ShippedDate = DateTime.Now.AddMonths(1), ShipName = "nhibernategemisi" };
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
        public static IResult AddOrderConsole(OrderManager manager, Order newOrder)
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
