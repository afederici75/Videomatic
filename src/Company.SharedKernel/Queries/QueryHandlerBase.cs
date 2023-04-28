namespace Company.SharedKernel.Queries;

//public abstract class QueryHandlerBase<TQuery, TDTO> : 
//    IRequestHandler<IQuery<TQuery, TDTO>, QueryResponse<TDTO>>
//    where TQuery : IQuery<TDTO>
//    where TDTO : class
//{
//    public Task<QueryResponse<TDTO>> Handle(IQuery<TQuery, TDTO> request, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }
//
//    //protected abstract Task<IEnumerable<TDTO>> QueryResultsAsync(TDTOSpecification specification, CancellationToken cancellationToken);
//
//    //public async Task<QueryResponse<TDTO>> Handle(IQuery<TDTOSpecification, TDTO> request, CancellationToken cancellationToken)
//    //{
//    //    var results = await QueryResultsAsync(request.Specification, cancellationToken);
//    //
//    //    return new QueryResponse<TDTO>(results.Count(), results);
//    //}
//}
