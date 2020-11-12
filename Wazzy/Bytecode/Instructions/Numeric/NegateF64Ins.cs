namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class NegateF64Ins : WASMInstruction
    {
        public NegateF64Ins()
            : base(OPCode.NegateF64)
        { }
    }
}