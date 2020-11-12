using System;

using Wazzy.IO;

namespace Wazzy.Types
{
    public class Local : WASMType
    {
        public uint Rank { get; }
        public Type Type { get; }

        public Local(ref WASMReader input)
        {
            Rank = input.ReadIntULEB128();
            Type = input.ReadValueType();
        }

        public override void WriteTo(ref WASMWriter output)
        {
            output.WriteULEB128(Rank);
            output.Write(Type);
        }

        public override int GetSize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size(Rank);
            size += 1; // Type
            return size;
        }
    }
}