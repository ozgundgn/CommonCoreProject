using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.Mime;
using System.Reflection;
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

        public static string GetMainConfigurationFilePath()
        {
            var basedir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            // �st dizinlere ��karak config dosyas� ara
            while (basedir != null)
            {
                string configFile = Path.Combine(basedir.FullName, "Configuration.cfg");
                if (File.Exists(configFile))
                {
                    return configFile;
                }

                basedir = basedir.Parent;
            }

            throw new FileNotFoundException("Kurulum dizinlerinde Configuration.cfg dosyası bulunamadı!");
        }

    }
}

