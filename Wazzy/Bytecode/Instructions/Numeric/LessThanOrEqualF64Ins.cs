namespace Wazzy.Bytecode.Instructions.Numeric;

public class LessThanOrEqualF64Ins : WASMInstruction
{
    public LessThanOrEqualF64Ins()
        : base(OPCode.LessThanOrEqualF64)
    { }
}