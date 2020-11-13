namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class NotEqualI64Ins : WASMInstruction
    {
        public NotEqualI64Ins()
            : base(OPCode.NotEqualI64)
        { }
    }
}