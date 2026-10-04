namespace Kt.Data.Commands.Finance;


public sealed record KtDraftLine(string Description, decimal Quantity, decimal UnitPrice,
    decimal DiscountAmount = 0, decimal ChargeAmount = 0, decimal TaxAmount = 0,
    string LineType = "ITEM", string? UnitOfMeasureCode = null);