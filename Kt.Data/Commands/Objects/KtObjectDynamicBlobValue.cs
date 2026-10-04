using System;
namespace Kt.Data.Commands.Objects;

public sealed record KtObjectDynamicBlobValue(byte[]? Value) : KtObjectDynamicValue;
