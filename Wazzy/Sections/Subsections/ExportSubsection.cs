using Wazzy.IO;

namespace Wazzy.Sections.Subsections
{
    public class ExportSubsection : WASMObject
    {
        public uint Index { get; }
        public string Name { get; set; }
        public ImpexDesc Description { get; set; }

        public ExportSubsection(ref WASMReader input)
        {
            Name = input.ReadString();
            Description = (ImpexDesc)input.ReadByte();
            Index = input.ReadIntULEB128();
        }

        public override int GetSize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size(Name);
            size += sizeof(byte);
            size += WASMReader.GetULEB128Size(Index);
            return size;
        }
        public override void WriteTo(ref WASMWriter output)
        {
            output.WriteString(Name);
            output.Write((byte)Description);
            output.WriteULEB128(Index);
        }
    }
}