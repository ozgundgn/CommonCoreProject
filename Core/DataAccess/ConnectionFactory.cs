using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Core.DataAccess
{
    public static class ConnectionFactory
    {
        public static string GetConnectionString()
        {
            //return db connectionstring
            return @"Server=OZGUN;Database=Northwind;Trusted_Connection=true";
        }

    }
}

