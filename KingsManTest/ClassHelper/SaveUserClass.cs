﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingsManTest.ClassHelper
{
    internal class SaveUserClass
    {
        public static DB.Employee SavedEmployee { get; set; } = null;
        public static DB.Client SavedClient { get; set; } = null;
    }
}
