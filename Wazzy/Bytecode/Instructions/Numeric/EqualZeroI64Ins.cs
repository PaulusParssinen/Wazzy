namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class EqualZeroI64Ins : WASMInstruction
    {
        public EqualZeroI64Ins()
            : base(OPCode.EqualZeroI64)
        { }
    }
}