using System;
namespace Kt.Data.Commands.Objects;

public sealed record KtObjectDynamicTextValue(string? Value) : KtObjectDynamicValue;
