using Wazzy.IO;

namespace Wazzy.Sections
{
    public class FunctionSection : WASMSectionEnumerable<uint>
    {
        public FunctionSection(ref WASMReader input)
            : base(WASMSectionId.FunctionSection)
        {
            Subsections.Capacity = (int)input.ReadIntULEB128();
            for (int i = 0; i < Subsections.Capacity; i++)
            {
                Subsections.Add(input.ReadIntULEB128());
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Subsections.Count);
            foreach (uint typeIndex in Subsections)
            {
                size += WASMReader.GetULEB128Size(typeIndex);
            }
            return size;
        }
        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128((uint)Subsections.Count);
            foreach (uint typeIndex in Subsections)
            {
                output.WriteULEB128(typeIndex);
            }
        }
    }
}