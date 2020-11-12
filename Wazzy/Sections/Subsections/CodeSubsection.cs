﻿//#define Peanut_Debugging
using System.Collections.Generic;

using Wazzy.IO;
using Wazzy.Types;
using Wazzy.Bytecode;

namespace Wazzy.Sections.Subsections
{
    public class CodeSubsection : WASMObject
    {
        public byte[] Body { get; set; }
        public List<Local> Locals { get; }
        public List<WASMInstruction> Expression { get; }

        public CodeSubsection(ref WASMReader input)
        {
            int funcSize = (int)input.ReadIntULEB128();
#if Peanut_Debugging
        DataStart: // Are you judging me right now? 
#endif
            int startOfSubsection = input.Position;
            Locals = new List<Local>((int)input.ReadIntULEB128());
            for (int i = 0; i < Locals.Capacity; i++)
            {
                Locals.Add(new Local(ref input));
            }

            int startOfBytecode = input.Position;
            int sizeOfBytecode = funcSize - (startOfBytecode - startOfSubsection);
#if Peanut_Debugging
            try
            {
                Expression = input.ReadExpression();
            }
            catch(System.Exception e) { System.Diagnostics.Debugger.Break(); }
#else
            Body = input.ReadBytes(sizeOfBytecode).ToArray();
#endif
#if Peanut_Debugging
            int bytesRead = input.Position - startOfBytecode;
            if (bytesRead != sizeOfBytecode)
            {
                System.Diagnostics.Debugger.Break();
                input.Position = startOfSubsection;
                goto DataStart; // Abort Abort
            }
#endif
        }

        public override int GetSize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Locals.Count);
            foreach (Local local in Locals)
            {
                size += local.GetSize();
            }
            size += Body.Length;

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
            funcSize += Body.Length;

            output.WriteULEB128((uint)funcSize);
            output.WriteULEB128((uint)Locals.Count);
            foreach (Local local in Locals)
            {
                local.WriteTo(ref output);
            }
            output.Write(Body);
        }
    }
}