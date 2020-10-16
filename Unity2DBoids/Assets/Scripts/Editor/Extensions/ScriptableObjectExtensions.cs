using System;
using System.Reflection;
using UnityEngine;

public static class ScriptableObjectExtensions
{
    /// <summary>
    /// Should NOT be used at runtime. Uses Reflection
    /// </summary>
    /// <param name="component"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    public static void SetField(this ScriptableObject component, string fieldName, object value)
    {
        FieldInfo fInfo = component.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (fInfo == null)
            throw new InvalidOperationException($"Field {fieldName} could not be found");
        fInfo.SetValue(component, value);
    }
    /// <summary>
    /// Should NOT be used at runtim. Uses Reflection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="component"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static T GetField<T>(this ScriptableObject component, string fieldName)
    {
        FieldInfo fInfo = component.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (fInfo == null)
            throw new InvalidOperationException($"Field {fieldName} could not be found");
        return (T)fInfo.GetValue(component);
    }
}