﻿using System;

namespace Wazzy.Types
{
    public abstract class WASMType
    {
        private static readonly Type[] _supportedTypes;

        static WASMType()
        {
            _supportedTypes = new[] { typeof(int), typeof(long), typeof(float), typeof(double) };
        }

        public static Type GetType(byte valueTypeId) => _supportedTypes[127 - valueTypeId];
    }
}