﻿using Wazzy.IO;
using Wazzy.Sections.Subsections;

namespace Wazzy.Sections
{
    public class ElementSection : WASMSectionEnumerable<ElementSubsection>
    {
        public ElementSection(ref WASMReader input)
            : base(WASMSectionId.ElementSection)
        {
            Capacity = (int)input.ReadIntULEB128();
            for (int i = 0; i < Capacity; i++)
            {
                Add(new ElementSubsection(ref input));
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Count);
            foreach (ElementSubsection element in this)
            {
                size += element.GetSize();
            }
            return size;
        }
        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128((uint)Count);
            foreach (ElementSubsection element in this)
            {
                element.WriteTo(ref output);
            }
        }
    }
}