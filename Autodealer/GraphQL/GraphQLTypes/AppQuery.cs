using Autodealer.Repositories.Interfaces;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public class AppQuery : ObjectGraphType
{
    public AppQuery(ICarRepository carRepository)
    {
        Field<ListGraphType<CarType>>("cars", resolve: context => carRepository.GetAll());
    }
}