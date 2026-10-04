using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic time value for an object.
/// </summary>
/// <param name="Value">The time value.</param>
public sealed record KtObjectDynamicTimeValue(TimeSpan Value) : KtObjectDynamicValue;
