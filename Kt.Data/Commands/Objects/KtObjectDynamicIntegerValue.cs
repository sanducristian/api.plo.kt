using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic integer value for an object.
/// </summary>
/// <param name="Value">The integer value.</param>
public sealed record KtObjectDynamicIntegerValue(long Value) : KtObjectDynamicValue;
