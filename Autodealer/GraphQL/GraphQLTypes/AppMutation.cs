using Autodealer.Dto;
using Autodealer.Entities;
using Autodealer.Repositories.Interfaces;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace Autodealer.GraphQL.GraphQLTypes;

public sealed class AppMutation : ObjectGraphType
{
    public AppMutation(ICarRepository carRepository)
    {
        Name = "Mutation";

        AddField(new FieldType
        {
            Name = "createCar",
            Type = typeof(CarType),
            Arguments = new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "brand" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "model" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "generation" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "engine" }),
            Resolver = new FuncFieldResolver<Car>(context =>
            {
                var brand = context.GetArgument<string>("brand");
                var model = context.GetArgument<string>("model");
                var generation = context.GetArgument<string>("generation");
                var engine = context.GetArgument<string>("engine");

                var carDto = new CarDto
                {
                    Brand = brand,
                    Model = model,
                    Generation = generation,
                    Engine = engine
                };

                var car = carRepository.Create(carDto);
                return car.Result;
            })
        });

        AddField(new FieldType
        {
            Name = "updateCar",
            Type = typeof(CarType),
            Arguments = new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                new QueryArgument<StringGraphType> { Name = "brand" },
                new QueryArgument<StringGraphType> { Name = "model" },
                new QueryArgument<StringGraphType> { Name = "generation" },
                new QueryArgument<StringGraphType> { Name = "engine" }),
            Resolver = new FuncFieldResolver<Car>(context =>
            {
                var id = context.GetArgument<string>("id");

                var car = carRepository.GetById(id);
                if (car == null)
                {
                    context.Errors.Add(new ExecutionError("Car not found"));
                }
                
                var brand = context.GetArgument<string>("brand");
                var model = context.GetArgument<string>("model");
                var generation = context.GetArgument<string>("generation");
                var engine = context.GetArgument<string>("engine");
                
                if(!string.IsNullOrEmpty(brand))
                    car.Brand = brand;
                
                if(!string.IsNullOrEmpty(model))
                    car.Model = model;
                
                if(!string.IsNullOrEmpty(generation))
                    car.Generation = generation;
                
                if(!string.IsNullOrEmpty(engine))
                    car.Engine = engine;

                return car;
            })
        });

        AddField(new FieldType
        {
            Name = "deleteCar",
            Type = typeof(StringGraphType),
            Arguments = new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
            Resolver = new FuncFieldResolver<string>(context =>
            {
                var id = context.GetArgument<string>("id");
                carRepository.Delete(id);
                return $"Car {id} deleted";
            })
        });
    }
}