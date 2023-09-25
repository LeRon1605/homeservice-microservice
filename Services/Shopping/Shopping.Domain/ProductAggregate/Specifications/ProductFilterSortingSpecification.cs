using BuildingBlocks.Domain.Specification;

namespace Shopping.Domain.ProductAggregate.Specifications;

public class ProductFilterSortingSpecification: Specification<Product>
{
    public ProductFilterSortingSpecification(
        Guid? productGroupId,
        decimal? minPrice,
        decimal? maxPrice,
        double? rating,
        bool isDescending,
        string? orderBy,
        int pageIndex,
        int pageSize,
        string? search)
    {
        AddInclude(x=> x.Reviews);
        ApplyPaging(pageIndex, pageSize);
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearchField(nameof(Product.Name));
            AddSearchTerm(search);
        }

        if (productGroupId.HasValue)
        {
            AddFilter(x=>x.ProductGroupId == productGroupId);   
        }

        if (maxPrice.HasValue)
        {
            AddFilter(x => x.Price <= maxPrice);
        }

        if (minPrice.HasValue)
        {
            AddFilter(x => x.Price >= minPrice);
        }

        if (rating.HasValue)
        {
            var (min, max) = ThresholdRating(rating.Value);
            AddFilter(x => 
                (!x.Reviews.Any() && min == 0) || 
                (x.Reviews.Any() && 
                 x.Reviews.Average(r => r.Rating) >= min &&
                 x.Reviews.Average(r => r.Rating) <= max));
        }
        
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            AddOrderByField(orderBy);
            ApplyDescending(isDescending);
        }
    }

    private static (double, double) ThresholdRating(double value)
    {
        if (value <= 0) return (0, 0);
        if (value >= 10) return (10, 10);
        
        const double maxRating = 10.0; 
        const double range = 2.5;
    
        var min = Math.Floor(value / range) * range;
        
        var max = min + range;
        if (max > maxRating)
        {
            max = maxRating;
        }

        return (min, max);
    }
}