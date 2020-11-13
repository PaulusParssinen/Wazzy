using System;
using System.Collections.Generic;

using Wazzy.IO;
using Wazzy.Types;
using Wazzy.Bytecode;

namespace Wazzy.Sections.Subsections
{
    public class CodeSubsection : WASMObject
    {
        public List<Local> Locals { get; }
        public List<WASMInstruction> Expression { get; }

        public CodeSubsection(ref WASMReader input)
        {
            int funcSize = (int)input.ReadIntULEB128();
            int funcEnd = input.Position + funcSize;

            Locals = new List<Local>((int)input.ReadIntULEB128());
            for (int i = 0; i < Locals.Capacity; i++)
            {
                Locals.Add(new Local(ref input));
            }
            Expression = input.ReadExpression();
            if (funcEnd != input.Position)
            {
                throw new Exception("Malformed subsection, since the expected ending position did not match the actual ending position.");
            }
        }

        public override int GetSize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Locals.Count);
            foreach (Local local in Locals)
            {
                size += local.GetSize();
            }

            foreach (WASMInstruction instruction in Expression)
            {
                size += instruction.GetSize();
            }

            // Finally calculate the real size
            size += WASMReader.GetULEB128Size((uint)size);
            return size;
        }
        public override void WriteTo(ref WASMWriter output)
        {
            int funcSize = 0;
            funcSize += WASMReader.GetULEB128Size((uint)Locals.Count);
            foreach (Local local in Locals)
            {
                funcSize += local.GetSize();
            }

            foreach (WASMInstruction instruction in Expression)
            {
                funcSize += instruction.GetSize();
            }

            output.WriteULEB128((uint)funcSize);
            output.WriteULEB128((uint)Locals.Count);
            foreach (Local local in Locals)
            {
                local.WriteTo(ref output);
            }

            foreach (WASMInstruction instruction in Expression)
            {
                instruction.WriteTo(ref output);
            }
        }
    }
}