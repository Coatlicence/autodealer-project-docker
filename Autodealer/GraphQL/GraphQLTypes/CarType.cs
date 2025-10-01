using Autodealer.Entities;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class CarType : ObjectGraphType<Car>
{
    public CarType()
    {
        Field(x => x.Id, type: typeof(IdGraphType));
        Field(x => x.Brand);
        Field(x => x.Model);
        Field(x => x.Generation);
        Field(x => x.EngineId);
    }
}