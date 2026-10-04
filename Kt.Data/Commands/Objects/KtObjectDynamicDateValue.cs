using System;
namespace Kt.Data.Commands.Objects;

public sealed record KtObjectDynamicDateValue(DateOnly? Value) : KtObjectDynamicValue;
