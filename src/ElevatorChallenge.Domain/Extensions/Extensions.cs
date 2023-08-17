namespace ElevatorChallenge.Domain.Extensions
{
    internal static class Extensions
    {
        internal static void AddRange<T>(this HashSet<T> set, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                set.Add(item);
            }
        }

        internal static void RemoveRange<T>(this List<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                list.Remove(item);
            }
        }
    }
}
