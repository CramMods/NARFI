namespace CramMods.NARFI.Filters
{
    public static class FilterUtils
    {
        public static IFilter? Merge(IFilter? filter1, IFilter? filter2, GroupFilterOperator op = GroupFilterOperator.AND)
        {
            if (filter1 == null) return filter2;
            if (filter2 == null) return filter1;

            GroupFilter output = new GroupFilter(op);

            if ((filter1 is GroupFilter) && (((GroupFilter)filter1).Operator == op)) output.Filters.AddRange(((GroupFilter)filter1).Filters);
            else output.Filters.Add(filter1);

            if ((filter2 is GroupFilter) && (((GroupFilter)filter2).Operator == op)) output.Filters.AddRange(((GroupFilter)filter2).Filters);
            else output.Filters.Add(filter2);

            return output;
        }
    }
}
