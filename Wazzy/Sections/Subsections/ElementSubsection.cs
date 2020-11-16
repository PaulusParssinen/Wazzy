using System.Collections.Generic;

using Wazzy.IO;
using Wazzy.Bytecode;

namespace Wazzy.Sections.Subsections
{
    public class ElementSubsection : WASMObject
    {
        private readonly IFunctionIndexAdjuster _functionIndexAdjuster;

        public uint TableIndex { get; set; }
        public List<uint> FunctionIndices { get; }
        public List<WASMInstruction> Expression { get; }

        public ElementSubsection(uint tableIndex = 0)
        {
            TableIndex = tableIndex;
            FunctionIndices = new List<uint>();
            Expression = new List<WASMInstruction>(3);
        }
        public ElementSubsection(ref WASMReader input, IFunctionIndexAdjuster functionIndexAdjuster = null)
        {
            _functionIndexAdjuster = functionIndexAdjuster;

            TableIndex = input.ReadIntULEB128();
            Expression = input.ReadExpression();
            FunctionIndices = new List<uint>((int)input.ReadIntULEB128());
            for (int i = 0; i < FunctionIndices.Capacity; i++)
            {
                FunctionIndices.Add(input.ReadIntULEB128());
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

            size += WASMReader.GetULEB128Size((uint)FunctionIndices.Count);
            foreach (uint functionIndex in FunctionIndices)
            {
                size += WASMReader.GetULEB128Size(functionIndex + (_functionIndexAdjuster?.GetFunctionIndexOffset() ?? 0));
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

            output.WriteULEB128((uint)FunctionIndices.Count);
            foreach (uint functionIndex in FunctionIndices)
            {
                output.WriteULEB128(functionIndex + (_functionIndexAdjuster?.GetFunctionIndexOffset() ?? 0));
            }
        }
    }
}