using Wazzy.IO;
using Wazzy.Sections.Subsections;

namespace Wazzy.Sections
{
    public class DataSection : WASMSectionEnumerable<DataSubsection>
    {
        public DataSection(ref WASMReader input)
            : base(WASMSectionId.DataSection)
        {
            Subsections.Capacity = (int)input.ReadIntULEB128();
            for (int i = 0; i < Subsections.Capacity; i++)
            {
                Add(new DataSubsection(ref input));
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Subsections.Count);
            foreach (DataSubsection data in Subsections)
            {
                size += data.GetSize();
            }
            return size;
        }
        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128((uint)Subsections.Count);
            foreach (DataSubsection data in Subsections)
            {
                data.WriteTo(ref output);
            }
        }
    }
}