using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    public class DatabaseLogger:LoggingBaseService
    {
        public DatabaseLogger() : base("DatabaseLogger")
        {
        }
    }
}
