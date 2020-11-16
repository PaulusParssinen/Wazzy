using Wazzy.Sections;

namespace Wazzy
{
    public interface IFunctionIndexAdjuster
    {
        public uint GetFunctionIndexOffset() => 0;
        public uint GetFunctionIndexOffset(ImpexDesc description) => description == ImpexDesc.Function ? GetFunctionIndexOffset() : 0;
    }
}