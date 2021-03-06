﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLAPI.Data
{
    internal static class UnsignedIntegerArrayExtensions
    {
        public static bool BitAt(this uint[] data, long index) => (data[index / 32] & (1 << (int)(index % 32))) != 0;
    }
}