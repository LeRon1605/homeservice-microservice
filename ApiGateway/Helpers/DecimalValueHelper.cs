namespace ApiGateway.Helpers;

public static class DecimalValueHelper
{
    private const decimal NanoFactor = 1_000_000_000;

    public static decimal ToDecimal(Products.Application.Grpc.Proto.DecimalValue grpcDecimal)
    {
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }
    
    public static decimal? ToDecimal(Products.Application.Grpc.Proto.NullableDecimalValue grpcDecimal)
    {
        if (!grpcDecimal.Units.HasValue || !grpcDecimal.Nanos.HasValue)
        {
            return null;
        }
        
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }
    
    public static decimal ToDecimal(Shopping.Application.Grpc.Proto.DecimalValue grpcDecimal)
    {
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }
    
    public static decimal? ToDecimal(Shopping.Application.Grpc.Proto.NullableDecimalValue grpcDecimal)
    {
        if (!grpcDecimal.Units.HasValue || !grpcDecimal.Nanos.HasValue)
        {
            return null;
        }
        
        return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    }

    public static Shopping.Application.Grpc.Proto.NullableDecimalValue ToDecimalValue(decimal? value)
    {
        if (!value.HasValue)
        {
            return new Shopping.Application.Grpc.Proto.NullableDecimalValue()
            {
                Units = null,
                Nanos = null
            };
        }
        var units = decimal.ToInt64(value.Value);
        var nanos = decimal.ToInt32((value.Value - units) * NanoFactor);
        return new Shopping.Application.Grpc.Proto.NullableDecimalValue()
        {
            Units = units,
            Nanos = nanos
        };
    }
}