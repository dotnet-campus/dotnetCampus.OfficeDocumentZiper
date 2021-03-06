﻿using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class Pound : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.Pound, Pound>
    {
        public Pound(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public static implicit operator Pound(dotnetCampus.OpenXmlUnitConverter.Pound newUnit)
        {
            return new Pound(newUnit.Value);
        }
    }
}