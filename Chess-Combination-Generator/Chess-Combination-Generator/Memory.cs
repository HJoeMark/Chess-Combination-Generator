﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Combination_Generator
{
    public static class Memory
    {
        public static BoardNode Root;
    }

    public class BoardNode
    {
        public FieldType[] Board { get; set; }
        public HashSet<BoardNode> Nodes { get; set; }
    }
}
