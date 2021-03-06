﻿using System;
using System.ComponentModel;

namespace dotnetCampus.OpenXMLUnitConverter
{
    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("请使用 dotnetCampus.OpenXmlUnitConverter 命名空间下的同名类型。")]
    public class Pt : LegacyUnit<dotnetCampus.OpenXmlUnitConverter.Pt, Pt>
    {
        public Pt(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public static implicit operator Pt(dotnetCampus.OpenXmlUnitConverter.Pt newUnit)
        {
            return new Pt(newUnit.Value);
        }
    }
}