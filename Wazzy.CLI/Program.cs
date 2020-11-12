using System;
using System.IO;

namespace Wazzy.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            byte[] originalModuleData = File.ReadAllBytes(args[0]);
            var originalModule = new WASMModule(originalModuleData);
            originalModule.Disassemble();

            byte[] assembledModuleData = originalModule.ToArray();

            string modifiedPath = Path.GetFullPath(args[0]).Replace("original_", string.Empty);
            File.WriteAllBytes(modifiedPath, assembledModuleData);

            for (int i = 4; i < Math.Min(originalModuleData.Length, assembledModuleData.Length); i++)
            {
                if (originalModuleData[i] == assembledModuleData[i]) continue;
                string originalChunkHex = BitConverter.ToString(originalModuleData, i, 10);
                string modifiedChunkHex = BitConverter.ToString(assembledModuleData, i, 10);
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}