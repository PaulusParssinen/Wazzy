using System.Collections.Generic;

namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class EqualZeroI32Ins : WASMInstruction
    {
        public EqualZeroI32Ins()
            : base(OPCode.EqualZeroI32)
        { }

        public override void Execute(Stack<object> stack, WASMModule context, params object[] parameters) => stack.Push((int)stack.Pop() == 0);
    }
}