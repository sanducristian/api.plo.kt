using System;
namespace Kt.Data.Commands.Objects;

/// <summary>
/// Represents a dynamic attribute value for an object.
/// </summary>
/// <param name="Name">The name of the attribute.</param>
/// <param name="Value">The value of the attribute.</param>
public sealed record KtObjectDynamicAttributeValue(string Name, string Value) : KtObjectDynamicValue;
