using Ardalis.Specification;

namespace Company.Videomatic.Domain.Abstractions;

//public interface IPaginationOptions
//{
//    int Page { get; }
//    int PageSize { get; }
//}

public interface IPaginatedSpecification<T> : ISpecification<T>
{
    int Page { get; }
    int PageSize { get; }
}

//public class PaginatedSpecification<T> : Specification<T>, IPaginatedSpecification<T>
//{
//    public int Page { get; }
//    public int PageSize { get; }
    
//    public PaginatedSpecification(int? page, int? pageSize)
//    {
//        Page = page ?? 1;
//        PageSize = pageSize ?? 10;

//        Query.Skip((Page - 1) * PageSize);
//        Query.Take(PageSize);        
//    }
//}   