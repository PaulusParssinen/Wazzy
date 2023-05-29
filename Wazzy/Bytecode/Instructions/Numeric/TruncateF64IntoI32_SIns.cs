namespace Wazzy.Bytecode.Instructions.Numeric;

public class TruncateF64IntoI32_SIns : WASMInstruction
{
    public TruncateF64IntoI32_SIns()
        : base(OPCode.TruncateF64IntoI32_S)
    { }
}