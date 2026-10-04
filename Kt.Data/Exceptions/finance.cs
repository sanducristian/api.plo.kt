namespace Kt.Data.Exceptions;

public sealed class KtInvoiceConflictException : Exception {
    public KtInvoiceConflictException() : base("Invoice changed, is not an editable draft, or is unavailable in this scope.") { }
}

