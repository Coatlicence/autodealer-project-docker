using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class AppQuery : ObjectGraphType
{
    public AppQuery(ICarRepository carRepository)
    {
        Name = "Query";

        AddField(new FieldType
        {
            Name = "cars",
            Type = typeof(ListGraphType<CarType>),
            Resolver = new FuncFieldResolver<IEnumerable<Car>>(context => carRepository.GetAll().GetAwaiter().GetResult())
        });
    }
}