using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public object Value { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
