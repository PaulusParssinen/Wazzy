﻿using Wazzy.IO;

namespace Wazzy.Bytecode.Instructions.Control
{
    public class LoopIns : BlockIns
    {
        public LoopIns()
            : base(OPCode.Loop)
        { }
        public LoopIns(ref WASMReader input)
            : base(ref input, OPCode.Loop)
        { }
    }
}