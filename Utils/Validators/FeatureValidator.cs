using Back_End_Dot_Net.Data;
using Back_End_Dot_Net.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

public class FeaturesValidator
{
    private readonly ApplicationDBContext _dbContext;

    public FeaturesValidator(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(bool, ModelStateDictionary)> ValidateFeatureAsync(Features feature)
    {
        var isValid = true;
        var errors = new ModelStateDictionary();

        // Check if name and type combination already exists in database
        var existingFeature = await _dbContext.Features
            .FirstOrDefaultAsync(f =>
                f.Name == feature.Name &&
                f.Type == feature.Type &&
                f.Category == feature.Category
            );

        if (existingFeature != null)
        {
            return (isValid, errors); // If already exist, let it pass
        }

        // Check if type is valid
        if (!IsValidType(feature.Type))
        {
            errors.AddModelError(nameof(feature.Type), "Type must be Performance, Screen, Design, or Default");
            isValid = false;
        }

        // Check if category is valid
        if (!IsValidCategory(feature.Category))
        {
            errors.AddModelError(nameof(feature.Category), "Category must be Chipset, Phone, or Laptop");
            isValid = false;
        }

        // Check if name already exists with a different type
        var conflictingFeature = await _dbContext.Features
            .FirstOrDefaultAsync(f =>
                f.Name == feature.Name &&
                f.Type != feature.Type &&
                f.Category == feature.Category
            );

        if (conflictingFeature != null)
        {
            errors.AddModelError("Name", $"A feature with the name '{feature.Name}' already exists with a different type.");
            isValid = false;
        }

        _dbContext.Features.Add(feature);
        await _dbContext.SaveChangesAsync();

        return (isValid, errors);
    }

    private bool IsValidType(string? type)
    {
        return Enum.IsDefined(typeof(FeaturesType), type);
    }

    private bool IsValidCategory(string? category)
    {
        return Enum.IsDefined(typeof(FeaturesCategory), category);
    }
}