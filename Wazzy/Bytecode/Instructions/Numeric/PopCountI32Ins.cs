namespace Wazzy.Bytecode.Instructions.Numeric;

public class PopCountI32Ins : WASMInstruction
{
    public PopCountI32Ins()
        : base(OPCode.PopCountI32)
    { }
}