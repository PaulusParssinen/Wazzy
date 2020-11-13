namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class ReinterpretF64IntoI64Ins : WASMInstruction
    {
        public ReinterpretF64IntoI64Ins()
            : base(OPCode.ReinterpretF64IntoI64)
        { }
    }
}