namespace Wazzy.Bytecode.Instructions.Numeric;

public class CountLeadingZeroesI32Ins : WASMInstruction
{
    public CountLeadingZeroesI32Ins()
        : base(OPCode.CountLeadingZeroesI32)
    { }
}