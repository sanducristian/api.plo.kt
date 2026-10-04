using System;
namespace Kt.Data.Commands.Objects;

public sealed record KtObjectDynamicTimeValue(TimeSpan Value) : KtObjectDynamicValue;
