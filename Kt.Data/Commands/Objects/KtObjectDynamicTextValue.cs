using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic text value for an object.
/// </summary>
/// <param name="Value">The text value.</param>
public sealed record KtObjectDynamicTextValue(string? Value) : KtObjectDynamicValue;
