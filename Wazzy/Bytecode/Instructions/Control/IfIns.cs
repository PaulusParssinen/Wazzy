using System;
using System.Collections.Generic;

using Wazzy.IO;
using Wazzy.Types;

namespace Wazzy.Bytecode.Instructions.Control
{
    public class IfIns : WASMInstruction
    {
        public Type BlockType { get; set; }
        public int? FunctionTypeIndex { get; set; }
        public List<WASMInstruction> Expression { get; }
        public List<WASMInstruction> ElseExpression { get; }
        public bool HasElseExpression => ElseExpression?.Count > 0;

        public IfIns()
            : base(OPCode.If)
        {
            Expression = new List<WASMInstruction>();
            ElseExpression = new List<WASMInstruction>();
        }
        public IfIns(ref WASMReader input)
            : base(OPCode.If)
        {
            byte blockId = input.ReadByte();
            if (blockId == 0x40)
            {
                BlockType = typeof(void);
            }
            else if (WASMType.IsSupportedValueTypeId(blockId))
            {
                BlockType = WASMType.GetValueType(blockId);
            }
            else // This is an index to a function type.
            {
                input.Position--;
                FunctionTypeIndex = input.ReadIntLEB128();
            }

            Expression = input.ReadExpression(OPCode.Else);
            if (Expression[^1].OP == OPCode.Else)
            {
                ElseExpression = input.ReadExpression();
                Expression.RemoveAt(Expression.Count - 1);
            }
        }

        protected override void WriteBodyTo(ref WASMWriter output)
        {
            if (FunctionTypeIndex != null)
            {
                output.WriteLEB128((int)FunctionTypeIndex);
            }
            else output.Write(BlockType);
            foreach (WASMInstruction instruction in Expression)
            {
                instruction.WriteTo(ref output);
            }
            if (ElseExpression.Count > 0)
            {
                output.Write((byte)OPCode.Else);
                foreach (WASMInstruction instruction in ElseExpression)
                {
                    instruction.WriteTo(ref output);
                }
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            if (FunctionTypeIndex != null)
            {
                size += WASMReader.GetLEB128Size((int)FunctionTypeIndex);
            }
            else size += 1; // Type
            foreach (WASMInstruction instruction in Expression)
            {
                size += instruction.GetSize();
            }

            if (HasElseExpression)
            {
                foreach (WASMInstruction instruction in ElseExpression)
                {
                    size += instruction.GetSize();
                }
            }
            return size;
        }
    }
}