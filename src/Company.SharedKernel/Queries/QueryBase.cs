namespace Company.SharedKernel.Queries;

public interface IQuery<TDTOSpecification, TDTO> : IRequest<QueryResponse<TDTO>>
    where TDTO : class
{
    public TDTOSpecification Specification { get; }
}

public abstract class QueryBase<TDTOSpecification, TDTO> : IQuery<TDTOSpecification, TDTO>
    where TDTOSpecification : ISpecification<TDTO>
    where TDTO : class
{
    public QueryBase(TDTOSpecification specification) => Specification = specification;

    public TDTOSpecification Specification { get; }
}
