using System;
namespace Kt.Data.Commands.Objects;

public sealed record KtObjectDynamicDateTimeValue(DateTime Value) : KtObjectDynamicValue;
