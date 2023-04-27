using Ardalis.Specification;
using Company.Videomatic.Domain.Model;
using Company.Videomatic.Domain.Queries;

namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;


public interface IQueryHandler<TSpecification, TEntity> : IRequestHandler<TSpecification, TEntity>
    where TSpecification : class, ISpecification<TEntity>, IRequest<TEntity>
    where TEntity : class
{}

public abstract class QueryHandler<TSpecification, TEntity> : IQueryHandler<TSpecification, TEntity>
    where TSpecification : class, ISpecification<TEntity>, IRequest<TEntity>
    where TEntity : class
{
    public Task<TEntity> Handle(TSpecification request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class GetVideosQueryHandler : QueryHandler<Domain.Queries.GetVideosQuery, Video>
{
}

//
//public abstract class QueryHandler<TQuery, TEntity, TSpecification> : 
//    IRequestHandler<TQuery, TEntity>
//    where TQuery : IQuery<TSpecification, TEntity>, IRequest<Video>
//    where TSpecification : ISpecification<TEntity>, IRequest<Video>
//    where TEntity : class
//{
//    public Task<TEntity> Handle(TQuery request, CancellationToken cancellationToken)
//    {        
//        throw new NotImplementedException();
//    }
//}

//public class GetVideosQueryHandler : QueryHandler<GetVideosSpecification, Video>
//{
//}
//
//public class GetVideoQueryHandler : QueryHandler<GetVideoSpecification, Video>
//{
//}


public partial class GetVideosQuery : IRequest<IEnumerable<VideoDTO>>
{
    public GetVideosQuery(Domain.Queries.GetVideosQuery specification)
    {
        Specification = specification;
    }

    public Domain.Queries.GetVideosQuery Specification { get; }
}
