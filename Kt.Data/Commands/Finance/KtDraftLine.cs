namespace Kt.Data.Commands.Finance;


/// <summary>
/// Represents a line item in a draft invoice or financial document.
/// </summary>
/// <param name="Description">The description of the line item.</param>
/// <param name="Quantity">The quantity of the line item.</param>
/// <param name="UnitPrice">The unit price of the line item.</param>
/// <param name="DiscountAmount">The discount amount applied to the line item.</param>
/// <param name="ChargeAmount">The charge amount applied to the line item.</param>
/// <param name="TaxAmount">The tax amount applied to the line item.</param>
/// <param name="LineType">The type of the line item.</param>
/// <param name="UnitOfMeasureCode">The unit of measure code for the line item.</param>
public sealed record KtDraftLine(string Description, decimal Quantity, decimal UnitPrice,
    decimal DiscountAmount = 0, decimal ChargeAmount = 0, decimal TaxAmount = 0,
    string LineType = "ITEM", string? UnitOfMeasureCode = null);