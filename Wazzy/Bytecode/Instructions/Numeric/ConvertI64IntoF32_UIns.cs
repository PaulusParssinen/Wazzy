﻿namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class ConvertI64IntoF32_UIns : WASMInstruction
    {
        public ConvertI64IntoF32_UIns()
            : base(OPCode.ConvertI64IntoF32_U)
        { }
    }
}