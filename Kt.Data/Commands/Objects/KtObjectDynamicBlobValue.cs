using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic blob value for an object.
/// </summary>
/// <param name="Value">The blob value.</param>
public sealed record KtObjectDynamicBlobValue(byte[]? Value) : KtObjectDynamicValue;
