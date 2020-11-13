using Wazzy.IO;

namespace Wazzy.Bytecode.Instructions.Control
{
    public class CallIns : WASMInstruction
    {
        public uint FunctionIndex { get; set; }

        public CallIns(ref WASMReader input)
            : this(input.ReadIntULEB128())
        { }
        public CallIns(uint functionIndex = 0)
            : base(OPCode.Call)
        {
            FunctionIndex = functionIndex;
        }

        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128(FunctionIndex);
        }
        protected override int GetBodySize() => WASMReader.GetULEB128Size(FunctionIndex);
    }
}