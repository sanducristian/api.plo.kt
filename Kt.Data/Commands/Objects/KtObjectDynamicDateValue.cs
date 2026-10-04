using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic date value for an object.
/// </summary>
/// <param name="Value">The date value.</param>
public sealed record KtObjectDynamicDateValue(DateOnly? Value) : KtObjectDynamicValue;
