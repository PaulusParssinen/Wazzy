﻿namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class TruncateF32IntoI64_SIns : WASMInstruction
    {
        public TruncateF32IntoI64_SIns()
            : base(OPCode.TruncateF32IntoI64_S)
        { }
    }
}