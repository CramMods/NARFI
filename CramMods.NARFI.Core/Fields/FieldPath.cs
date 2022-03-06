namespace CramMods.NARFI.Fields
{
    public class FieldPath : List<Field>
    {
        public Field Dequeue()
        {
            if (Count == 0) return new("EditorId");
            Field value = this[0];
            RemoveAt(0);
            return value;
        }

        public FieldPath AddToFront(Field field)
        {
            Insert(0, field);
            return this;
        }

        public FieldPath Clone() => new FieldPath(this);

        public override string ToString() => string.Join('.', this);

        public FieldPath(IEnumerable<Field> fields) : base(fields) { }
        public FieldPath(params Field[] fields) : this(fields.ToList()) { }

        public FieldPath(IEnumerable<string> fieldNames) : base(fieldNames.Select(fn => new Field(fn))) { }
        public FieldPath(params string[] fieldNames) : this(fieldNames.ToList()) { }

        public FieldPath(string pathString) : this(pathString.Split('.')) { }

        public static implicit operator FieldPath(string s) => new(s);
    }
}
