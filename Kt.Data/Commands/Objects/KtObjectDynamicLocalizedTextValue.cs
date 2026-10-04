using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic localized text value for an object.
/// </summary>
/// <param name="LanguageId">The language identifier.</param>
/// <param name="Value">The text value.</param>
public sealed record KtObjectDynamicLocalizedTextValue(ulong LanguageId, string? Value) : KtObjectDynamicValue;
