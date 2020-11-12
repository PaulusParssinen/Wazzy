﻿using Wazzy.IO;
using Wazzy.Types;

namespace Wazzy.Sections
{
    /// <summary>
    /// Represents a list of function signatures.
    /// </summary>
    public class TypeSection : WASMSectionEnumerable<FuncType>
    {
        public TypeSection(ref WASMReader input)
            : base(WASMSectionId.TypeSection)
        {
            Subsections.Capacity = (int)input.ReadIntULEB128();
            for (int i = 0; i < Subsections.Capacity; i++)
            {
                input.ReadByte(); // FUNCTION_TYPE = 0x60
                Add(new FuncType(ref input));
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Subsections.Count);
            foreach (FuncType functionType in Subsections)
            {
                size += sizeof(byte);
                size += functionType.GetSize();
            }
            return size;
        }
        protected override void WriteBodyTo(ref WASMWriter output)
        {
            const byte FUNCTION_TYPE = 0x60;

            output.WriteULEB128((uint)Subsections.Count);
            foreach (FuncType functionType in Subsections)
            {
                output.Write(FUNCTION_TYPE);
                functionType.WriteTo(ref output);
            }
        }
    }
}