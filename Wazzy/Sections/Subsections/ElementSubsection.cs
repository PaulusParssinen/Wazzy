using System.Collections.Generic;

using Wazzy.IO;
using Wazzy.Bytecode;

namespace Wazzy.Sections.Subsections
{
    public class ElementSubsection : WASMObject
    {
        public uint TableIndex { get; set; }
        public List<uint> FunctionTypeIndices { get; }
        public List<WASMInstruction> Expression { get; }

        public ElementSubsection(uint tableIndex = 0)
        {
            TableIndex = tableIndex;
            FunctionTypeIndices = new List<uint>();
            Expression = new List<WASMInstruction>(3);
        }
        public ElementSubsection(ref WASMReader input)
        {
            TableIndex = input.ReadIntULEB128();
            Expression = input.ReadExpression();
            FunctionTypeIndices = new List<uint>((int)input.ReadIntULEB128());
            for (int i = 0; i < FunctionTypeIndices.Capacity; i++)
            {
                FunctionTypeIndices.Add(input.ReadIntULEB128());
            }
        }

        public T EvaluateOffset<T>(WASMModule context)
        {
            return (T)WASMMachine.Execute(Expression, context).Pop();
        }

        public override int GetSize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size(TableIndex);
            foreach (WASMInstruction instruction in Expression)
            {
                size += instruction.GetSize();
            }

            size += WASMReader.GetULEB128Size((uint)FunctionTypeIndices.Count);
            foreach (uint functionTypeIndex in FunctionTypeIndices)
            {
                size += WASMReader.GetULEB128Size(functionTypeIndex);
            }
            return size;
        }
        public override void WriteTo(ref WASMWriter output)
        {
            output.WriteULEB128(TableIndex);
            foreach (WASMInstruction instruction in Expression)
            {
                instruction.WriteTo(ref output);
            }

            output.WriteULEB128((uint)FunctionTypeIndices.Count);
            foreach (uint functionTypeIndex in FunctionTypeIndices)
            {
                output.WriteULEB128(functionTypeIndex);
            }
        }
    }
}