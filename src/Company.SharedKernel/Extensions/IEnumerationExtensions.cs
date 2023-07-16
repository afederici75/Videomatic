namespace System.Collections.Generic;

public static class IEnumerationExtensions
{
    public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> source, int pageSize)
    {
        // See http://stackoverflow.com/questions/2380413/paging-with-linq-for-objects
        using (var enumerator = source.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                var currentPage = new List<T>(pageSize)
                    {
                        enumerator.Current
                    };

                while (currentPage.Count < pageSize && enumerator.MoveNext())
                {
                    currentPage.Add(enumerator.Current);
                }

                yield return new List<T>(currentPage);
            }
        }
    }

    //public static async IAsyncEnumerable<IEnumerable<T>> PageAsync<T>(this IAsyncEnumerable<T> source, int pageSize)
    //{
    //    // See http://stackoverflow.com/questions/2380413/paging-with-linq-for-objects
    //    using (var enumerator = source.GetAsyncEnumerator())
    //    {
    //        while (enumerator.MoveNextAsync())
    //        {
    //            var currentPage = new List<T>(pageSize)
    //                {
    //                    enumerator.Current
    //                };

    //            while (currentPage.Count < pageSize && enumerator.MoveNextAsync())
    //            {
    //                currentPage.Add(enumerator.Current);
    //            }

    //            yield return new List<T>(currentPage);
    //        }
    //    }
    //}
}
