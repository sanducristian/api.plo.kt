using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic double value for an object.
/// </summary>
/// <param name="Value">The double value.</param>
public sealed record KtObjectDynamicDoubleValue(double? Value) : KtObjectDynamicValue;
