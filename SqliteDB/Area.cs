using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDB
{
    public class Area
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public string Code { get; set; }

    }
}
