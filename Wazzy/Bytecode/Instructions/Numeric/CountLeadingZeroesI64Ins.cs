namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class CountLeadingZeroesI64Ins : WASMInstruction
    {
        public CountLeadingZeroesI64Ins()
            : base(OPCode.CountLeadingZeroesI64)
        { }
    }
}