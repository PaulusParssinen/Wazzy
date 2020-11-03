﻿using Wazzy.IO;
using Wazzy.Types;

namespace Wazzy.Sections
{
    public class TableSection : WASMSectionEnumerable<TableType>
    {
        public TableSection(WASMModule module)
            : base(module, WASMSectionId.TableSection)
        {
            Subsections.Capacity = module.Input.Read7BitEncodedInt();
            for (int i = 0; i < Subsections.Capacity; i++)
            {
                Add(new TableType(module));
            }
        }

        protected override void WriteBodyTo(WASMWriter output, int globalPosition)
        {
            output.Write7BitEncodedInt(Subsections.Count);
            foreach (TableType table in Subsections)
            {
                table.WriteTo(output);
            }
        }
    }
}