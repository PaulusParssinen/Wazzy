namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class TruncateF64IntoI64_SIns : WASMInstruction
    {
        public TruncateF64IntoI64_SIns()
            : base(OPCode.TruncateF64IntoI64_S)
        { }
    }
}