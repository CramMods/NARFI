namespace CramMods.NARFI.Fields
{
    public class Field : IEquatable<Field>, IEquatable<string>
    {
        protected string _name;
        public string Name => _name;

        protected List<string> _aliases = new();
        public IEnumerable<string> Aliases => _aliases;

        protected IEnumerable<string> _allNames => _aliases.Append(_name);
        public IEnumerable<string> AllNames => _allNames;

        public override string ToString() => _name;

        public bool Equals(Field? other) => _allNames.Any(n => other?.AllNames.Contains(n, StringComparer.InvariantCultureIgnoreCase) ?? false);
        public bool Equals(string? other) => _allNames.Any(n => n.Equals(other, StringComparison.InvariantCultureIgnoreCase));
        public override bool Equals(object? obj) => Equals(obj as Field);
        public override int GetHashCode() => _name.ToLowerInvariant().GetHashCode();

        public Field(string name, IEnumerable<string> aliases) => (_name, _aliases) = (name, aliases.ToList());
        public Field(string name, params string[] aliases) => (_name, _aliases) = (name, aliases.ToList());
        public Field(string name) => _name = name;
    }
}
