﻿using Wazzy.IO;
using Wazzy.Types;

namespace Wazzy.Sections
{
    public class MemorySection : WASMSectionEnumerable<MemoryType>
    {
        public MemorySection(ref WASMReader input)
            : base(WASMSectionId.MemorySection)
        {
            Subsections.Capacity = (int)input.ReadIntULEB128();
            for (int i = 0; i < Subsections.Capacity; i++)
            {
                Add(new MemoryType(ref input));
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Subsections.Count);
            foreach (MemoryType memory in Subsections)
            {
                size += memory.GetSize();
            }
            return size;
        }
        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128((uint)Subsections.Count);
            foreach (MemoryType memory in Subsections)
            {
                memory.WriteTo(ref output);
            }
        }
    }
}