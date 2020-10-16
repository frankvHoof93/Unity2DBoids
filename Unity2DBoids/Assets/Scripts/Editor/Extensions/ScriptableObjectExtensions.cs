using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Extensions for ScriptableObject to set Private Values in Editor
/// </summary>
public static class ScriptableObjectExtensions
{
    /// <summary>
    /// Should NOT be used at runtime. Uses Reflection
    /// </summary>
    /// <param name="component">Component to set Field on</param>
    /// <param name="fieldName">Name of Field in Component</param>
    /// <param name="value">Value to Set</param>
    public static void SetField(this ScriptableObject component, string fieldName, object value)
    {
        FieldInfo fInfo = component.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (fInfo == null)
            throw new InvalidOperationException($"Field {fieldName} could not be found");
        fInfo.SetValue(component, value);
    }
    /// <summary>
    /// Should NOT be used at runtime. Uses Reflection
    /// </summary>
    /// <typeparam name="T">Type of Field to get</typeparam>
    /// <param name="component">Component to get Field from</param>
    /// <param name="fieldName">Name of Field in Component</param>
    /// <returns>Value of Field. Throws InvalidOperationException if Field could not be found</returns>
    public static T GetField<T>(this ScriptableObject component, string fieldName)
    {
        FieldInfo fInfo = component.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (fInfo == null)
            throw new InvalidOperationException($"Field {fieldName} could not be found");
        return (T)fInfo.GetValue(component);
    }
}