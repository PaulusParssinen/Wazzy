namespace Wazzy.Bytecode.Instructions.Numeric;

public class ConvertI64IntoF64_UIns : WASMInstruction
{
    public ConvertI64IntoF64_UIns()
        : base(OPCode.ConvertI64IntoF64_U)
    { }
}