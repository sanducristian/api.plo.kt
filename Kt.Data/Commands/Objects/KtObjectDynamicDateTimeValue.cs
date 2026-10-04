using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic DateTime value.
/// </summary>
/// <param name="Value">The DateTime value.</param>
public sealed record KtObjectDynamicDateTimeValue(DateTime Value) : KtObjectDynamicValue;
