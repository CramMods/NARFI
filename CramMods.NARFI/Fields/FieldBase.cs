using System.Reflection;

namespace CramMods.NARFI.Fields
{
    public abstract class FieldBase : IField
    {
        protected string _id;
        public string Id => _id;

        public FieldBase(string id) => _id = id;

        public override string ToString() => _id;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!obj.GetType().IsAssignableTo(typeof(FieldBase))) return false;
            return ((FieldBase)obj).Id.Equals(_id, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode() => _id.GetHashCode();

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
                .Where(v => v.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase) || v.Value.Id.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .Select(v => v.Value)
                .FirstOrDefault();

        public int CompareTo(object? obj) => _id.CompareTo(((FieldBase?)obj)?.Id);
    }

}
