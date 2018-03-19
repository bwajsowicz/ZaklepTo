﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    class Table
    {
        public Guid Id { get; private set; }
        public int NumberOfSeats { get; private set; }
        public (int x, int y) Coordinates { get; private set; }
    }
}