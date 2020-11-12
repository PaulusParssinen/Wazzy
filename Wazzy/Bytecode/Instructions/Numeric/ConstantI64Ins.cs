﻿using System.Collections.Generic;

using Wazzy.IO;

namespace Wazzy.Bytecode.Instructions.Numeric
{
    public class ConstantI64Ins : WASMInstruction
    {
        public long Constant { get; set; }

        public ConstantI64Ins(ref WASMReader input)
            : this(input.ReadLongLEB128())
        { }
        public ConstantI64Ins(long constant = 0)
            : base(OPCode.ConstantI64)
        {
            Constant = constant;
        }

        public override void Execute(Stack<object> stack, WASMModule context)
        {
            stack.Push(Constant);
        }

        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteLEB128(Constant);
        }

        protected override int GetBodySize() => WASMReader.GetLEB128Size(Constant);
    }
}