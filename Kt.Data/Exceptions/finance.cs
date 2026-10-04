namespace Kt.Data.Exceptions;

/// <summary>
/// Represents an exception that is thrown when there is a conflict with an invoice.
/// </summary>
public sealed class KtInvoiceConflictException : Exception {


    /// <summary>
    /// Initializes a new instance of the <see cref="KtInvoiceConflictException"/> class.
    /// </summary>
    public KtInvoiceConflictException() : base("Invoice changed, is not an editable draft, or is unavailable in this scope.") { }
}

