using Wazzy.IO;

namespace Wazzy.Sections.Subsections
{
    public class ExportSubsection : WASMObject
    {
        private readonly IFunctionIndexAdjuster _functionIndexAdjuster;

        public uint Index { get; }
        public string Name { get; set; }
        public ImpexDesc Description { get; set; }

        public ExportSubsection(ref WASMReader input, IFunctionIndexAdjuster functionIndexAdjuster = null)
        {
            _functionIndexAdjuster = functionIndexAdjuster ?? default;

            Name = input.ReadString();
            Description = (ImpexDesc)input.ReadByte();
            Index = input.ReadIntULEB128();
        }

        public override int GetSize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size(Name);
            size += sizeof(byte);
            size += WASMReader.GetULEB128Size(Index + _functionIndexAdjuster?.GetFunctionIndexOffset(Description) ?? 0);
            return size;
        }
        public override void WriteTo(ref WASMWriter output)
        {
            output.WriteString(Name);
            output.Write((byte)Description);
            output.WriteULEB128(Index + _functionIndexAdjuster?.GetFunctionIndexOffset(Description) ?? 0);
        }
    }
}