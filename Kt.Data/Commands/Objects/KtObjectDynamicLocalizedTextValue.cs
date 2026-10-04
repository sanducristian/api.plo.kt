using System;
namespace Kt.Data.Commands.Objects;

public sealed record KtObjectDynamicLocalizedTextValue(ulong LanguageId, string? Value) : KtObjectDynamicValue;
