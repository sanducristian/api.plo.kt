$records = @(
    "public sealed record KtObjectDynamicIntegerValue(long Value) : KtObjectDynamicValue;",
    "public sealed record KtObjectDynamicDoubleValue(double? Value) : KtObjectDynamicValue;",
    "public sealed record KtObjectDynamicTextValue(string? Value) : KtObjectDynamicValue;",
    "public sealed record KtObjectDynamicLocalizedTextValue(ulong LanguageId, string? Value) : KtObjectDynamicValue;",
    "public sealed record KtObjectDynamicAttributeValue(string Name, string Value) : KtObjectDynamicValue;",
    "public sealed record KtObjectDynamicBlobValue(byte[]? Value) : KtObjectDynamicValue;",
    "public sealed record KtObjectDynamicDateValue(DateOnly? Value) : KtObjectDynamicValue;",
    "public sealed record KtObjectDynamicDateTimeValue(DateTime Value) : KtObjectDynamicValue;",
    "public sealed record KtObjectDynamicTimeValue(TimeSpan Value) : KtObjectDynamicValue;"
)

foreach ($record in $records) {
    # Use Regex to extract the name immediately following the word 'record'
    $match = [regex]::Match($record, "record (\w+)")
    
    if ($match.Success) {
        $className =$match.Groups[1].Value
        $fileName = "$className.cs"
        
        $content = "using System;`n`n$record"
        
        Set-Content -Path $fileName -Value $content
        Write-Host "Created $fileName"
    }
}