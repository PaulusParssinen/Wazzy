namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class CopysignF64Ins : WASMInstruction
    {
        public CopysignF64Ins()
            : base(OPCode.CopysignF64)
        { }
    }
}