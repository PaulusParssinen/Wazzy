using System.Collections.Generic;

using Wazzy.IO;
using Wazzy.Sections.Subsections;

namespace Wazzy.Sections
{
    public class ImportSection : WASMSectionEnumerable<ImportSubsection>
    {
        private readonly List<ImportSubsection> _functionImports;
        private readonly List<ImportSubsection> _freshlyImportedFunctions;

        public IReadOnlyList<ImportSubsection> FreshlyImportedFunctions { get; }

        public ImportSection()
            : base(WASMSectionId.ImportSection)
        {
            _functionImports = new List<ImportSubsection>();
            _freshlyImportedFunctions = new List<ImportSubsection>();

            FreshlyImportedFunctions = _freshlyImportedFunctions.AsReadOnly();
        }
        public ImportSection(ref WASMReader input)
            : this()
        {
            Capacity = (int)input.ReadIntULEB128();
            for (int i = 0; i < Capacity; i++)
            {
                Add(new ImportSubsection(ref input));
            }
        }

        public uint Add(string module, string name, uint functionTypeIndex)
        {
            int functionImportIndex = FreshlyImportedFunctions.Count;
            var freshFunctionImport = new ImportSubsection(module, name, functionTypeIndex);

            _freshlyImportedFunctions.Add(freshFunctionImport);
            Insert(IndexOf(_functionImports[0]) + functionImportIndex, freshFunctionImport);

            return (uint)functionImportIndex;
        }

        protected override void Cleared()
        {
            _functionImports.Clear();
        }
        protected override void Added(int index, ImportSubsection subsection)
        {
            if (subsection.Description != ImpexDesc.Function) return;
            if (!_freshlyImportedFunctions.Contains(subsection))
            {
                _functionImports.Add(subsection);
            }
            else _functionImports.Insert(_freshlyImportedFunctions.Count - 1, subsection);
        }
        protected override void Removed(int index, ImportSubsection subsection)
        {
            if (subsection.Description == ImpexDesc.Function)
            {
                _functionImports.Remove(subsection);
                if (_freshlyImportedFunctions.Contains(subsection))
                {
                    _freshlyImportedFunctions.Remove(subsection);
                }
            }
        }

        protected override int GetBodySize()
        {
            int size = 0;
            size += WASMReader.GetULEB128Size((uint)Count);
            foreach (ImportSubsection import in this)
            {
                size += import.GetSize();
            }
            return size;
        }
        protected override void WriteBodyTo(ref WASMWriter output)
        {
            output.WriteULEB128((uint)Count);
            foreach (ImportSubsection import in this)
            {
                import.WriteTo(ref output);
            }
        }
    }
}