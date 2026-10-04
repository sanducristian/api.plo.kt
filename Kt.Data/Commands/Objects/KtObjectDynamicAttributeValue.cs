using System;
namespace Kt.Data.Commands.Objects;

public sealed record KtObjectDynamicAttributeValue(string Name, string Value) : KtObjectDynamicValue;
