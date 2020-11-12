using System.Diagnostics;

using Wazzy.IO;

namespace Wazzy.Types
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Limits : WASMType
    {
        public uint Minimum { get; set; }
        public uint Maximum { get; set; }
        public bool HasMaximum { get; set; }

        internal string DebuggerDisplay => $"Min: {Minimum:n0}; Max: {(HasMaximum ? Maximum : uint.MaxValue):n0}";

        public Limits(ref WASMReader input)
        {
            HasMaximum = input.ReadBoolean();
            Minimum = input.ReadIntULEB128();
            if (HasMaximum)
            {
                Maximum = input.ReadIntULEB128();
            }
        }

        public override void WriteTo(ref WASMWriter output)
        {
            output.Write(HasMaximum);
            output.WriteULEB128(Minimum);
            if (HasMaximum)
            {
                output.WriteULEB128(Maximum);
            }
        }

        public override int GetSize()
        {
            int size = 0;
            size += sizeof(byte);
            size += WASMReader.GetULEB128Size(Minimum);
            if (HasMaximum)
                size += WASMReader.GetULEB128Size(Maximum);
            return size;
        }
    }
}