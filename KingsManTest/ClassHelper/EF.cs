using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingsManTest.ClassHelper
{
    internal class EF
    {
        public static DB.KingsManEntities Context { get; set; } = new DB.KingsManEntities();
    }
}
