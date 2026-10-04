using System;
namespace Kt.Data.Commands.Objects;

public sealed record KtObjectDynamicDoubleValue(double? Value) : KtObjectDynamicValue;
