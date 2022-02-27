﻿using System;
using System.Reflection;

namespace CramMods.NARFI.Fields
{
    public abstract class FieldBase : IComparable<FieldBase>, IEquatable<FieldBase>
    {
        protected string _id;
        public string Id => _id;

        public FieldBase(string id) => _id = id;

        public static IReadOnlyList<T> GetAll<T>() =>
            typeof(T)
                .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(fi => fi.GetValue(null))
                .Where(o => o != null)
                .Select(o => o!)
                .Cast<T>()
                .ToList();

        public static T? Parse<T>(string name) where T : FieldBase =>
            typeof(T)
                .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(fi => new KeyValuePair<string, T>(fi.Name, (T)fi.GetValue(null)!))
                .Where(v => {
                    if (v.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase)) return true;
                    if (v.Value.Id.Equals(name, StringComparison.InvariantCultureIgnoreCase)) return true;
                    if (v.Key.Equals(name.Replace(':', '_'), StringComparison.InvariantCultureIgnoreCase)) return true;
                    if (v.Value.Id.Equals(name.Replace(':', '_'), StringComparison.InvariantCultureIgnoreCase)) return true;
                    return false;
                })
                .Select(v => v.Value)
                .FirstOrDefault();

        public override string ToString() => _id;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!obj.GetType().IsAssignableTo(typeof(FieldBase))) return false;
            return Equals((FieldBase)obj);
        }
        public override int GetHashCode() => _id.GetHashCode();

        public int CompareTo(FieldBase? obj) => _id.CompareTo(obj?.Id);
        public bool Equals(FieldBase? other) => other?.Id.Equals(_id, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }

}
