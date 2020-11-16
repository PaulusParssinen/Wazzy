using Wazzy.IO;

namespace Wazzy.Bytecode.Instructions.Control
{
    public class CallIns : WASMInstruction
    {
        private readonly IFunctionIndexAdjuster _functionIndexAdjuster;

        public uint FunctionIndex { get; set; }

        public CallIns(uint functionIndex = 0)
            : base(OPCode.Call)
        {
            FunctionIndex = functionIndex;
        }
        public CallIns(ref WASMReader input, IFunctionIndexAdjuster functionIndexAdjuster = null)
            : this(input.ReadIntULEB128())
        {
            _functionIndexAdjuster = functionIndexAdjuster;
        }

        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128(FunctionIndex + _functionIndexAdjuster?.GetFunctionIndexOffset() ?? 0);
        }
        protected override int GetBodySize() => WASMReader.GetULEB128Size(FunctionIndex + _functionIndexAdjuster?.GetFunctionIndexOffset() ?? 0);
    }
}