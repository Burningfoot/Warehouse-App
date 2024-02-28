using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDB
{
    public class Branch
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public Area Area { get; set; }
        public Item Item { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
