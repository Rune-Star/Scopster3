using System;
using System.Diagnostics;
using AYellowpaper.SerializedCollections;

[Conditional("UNITY_EDITOR")]
public class SerializedDictionaryAttributeOBs : Attribute
{
    public readonly string KeyName;
    public readonly string ValueName;

    public SerializedDictionaryAttributeOBs(string keyName = null, string valueName = null)
    {
        KeyName = keyName;
        ValueName = valueName;
    }
}