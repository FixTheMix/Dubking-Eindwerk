﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class ConfirmDeleteMessage
    {
        public bool CanDelete { get; set; }
        public string VTName { get; set; }
    }
}
