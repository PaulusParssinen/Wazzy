namespace Wazzy.Bytecode.Instructions.Numeric;

public class TruncateF32IntoI64_UIns : WASMInstruction
{
    public TruncateF32IntoI64_UIns()
        : base(OPCode.TruncateF32IntoI64_U)
    { }
}