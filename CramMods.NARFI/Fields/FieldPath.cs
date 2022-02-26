namespace CramMods.NARFI.Fields
{
    public class FieldPath : List<Field>
    {
        public Field Dequeue()
        {
            Field field = Field.EditorID;
            if (Count > 0)
            {
                field = this[0];
                RemoveAt(0);
            }
            return field;
        }

        public FieldPath AddToFront(Field field)
        {
            Insert(0, field);
            return this;
        }

        public FieldPath Clone() => new(this);

        public FieldPath(IEnumerable<Field> fields) : base(fields) { }
        public FieldPath(IEnumerable<string> fieldNames) : this(fieldNames.Select(fn => Field.Parse(fn) ?? throw new Exception($"Unable to parse field: \"{fn}\""))) { }
        public FieldPath(string[] fieldNames) : this(fieldNames.ToList()) { }
        public FieldPath(string fieldPath) : this(fieldPath.Split('.')) { }

        public override string ToString() => string.Join('.', this);
    }
}
