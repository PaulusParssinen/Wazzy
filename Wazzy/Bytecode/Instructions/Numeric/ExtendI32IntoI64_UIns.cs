﻿namespace Wazzy.Bytecode.Instructions.Numeric;

public class ExtendI32IntoI64_UIns : WASMInstruction
{
    public ExtendI32IntoI64_UIns()
        : base(OPCode.ExtendI32IntoI64_U)
    { }
}