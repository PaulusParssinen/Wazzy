using Wazzy.IO;
using Wazzy.Sections.Subsections;

namespace Wazzy.Sections
{
    public class ImportSection : WASMSectionEnumerable<ImportSubsection>
    {
        public ImportSection(ref WASMReader input)
            : base(WASMSectionId.ImportSection)
        {
            Subsections.Capacity = (int)input.ReadIntULEB128();
            for (int i = 0; i < Subsections.Capacity; i++)
            {
                Add(new ImportSubsection(ref input));
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Subsections.Count);
            foreach (ImportSubsection import in Subsections)
            {
                size += import.GetSize();
            }
            return size;
        }
        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128((uint)Subsections.Count);
            foreach (ImportSubsection import in Subsections)
            {
                import.WriteTo(ref output);
            }
        }
    }
}