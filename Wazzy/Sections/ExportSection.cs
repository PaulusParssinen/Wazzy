using Wazzy.IO;
using Wazzy.Sections.Subsections;

namespace Wazzy.Sections
{
    public class ExportSection : WASMSectionEnumerable<ExportSubsection>
    {
        public ExportSection(ref WASMReader input)
            : base(WASMSectionId.ExportSection)
        {
            Subsections.Capacity = (int)input.ReadIntULEB128();
            for (int i = 0; i < Subsections.Capacity; i++)
            {
                Subsections.Add(new ExportSubsection(ref input));
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Subsections.Count);
            foreach (ExportSubsection export in Subsections)
            {
                size += export.GetSize();
            }
            return size;
        }
        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128((uint)Subsections.Count);
            foreach (ExportSubsection export in Subsections)
            {
                export.WriteTo(ref output);
            }
        }
    }
}