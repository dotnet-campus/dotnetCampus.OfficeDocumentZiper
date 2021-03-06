﻿using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class Inch : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.Inch, Inch>
    {
        public Inch(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public static implicit operator Inch(dotnetCampus.OpenXmlUnitConverter.Inch newUnit)
        {
            return new Inch(newUnit.Value);
        }
    }
}