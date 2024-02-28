using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDB
{
    public class Package
    {
        public Guid? Id { get; set; }
        public int Amount { get; set; }
        public Area? Area { get; set; }
        public Branch Branch { get; set; }
    }
}
