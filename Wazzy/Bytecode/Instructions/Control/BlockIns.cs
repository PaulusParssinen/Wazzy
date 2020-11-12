using System;
using System.Collections.Generic;

using Wazzy.IO;
using Wazzy.Types;

namespace Wazzy.Bytecode.Instructions.Control
{
    public class BlockIns : WASMInstruction
    {
        public Type BlockType { get; set; }
        public int? FunctionTypeIndex { get; set; }
        public List<WASMInstruction> Expression { get; }

        public BlockIns()
            : base(OPCode.Block)
        {
            Expression = new List<WASMInstruction>();
        }
        public BlockIns(ref WASMReader input)
            : base(OPCode.Block)
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
            Expression = input.ReadExpression();
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
        }

        protected override int GetBodySize()
        {
            int size = 0;
            if (FunctionTypeIndex != null)
            {
                size += WASMReader.GetLEB128Size((int)FunctionTypeIndex);
            }
            else size += 1; // Type
            foreach (var instruction in Expression)
            {
                size += instruction.GetSize();
            }
            return size;
        }
    }
}