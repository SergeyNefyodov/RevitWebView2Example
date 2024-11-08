﻿using System.Text.RegularExpressions;

sealed partial class Build
{
    readonly Regex ArgumentsRegex = ArgumentsRegexGenerator();
    readonly Regex YearRegex = YearRegexGenerator();

    [GeneratedRegex(@"\d{4}")]
    private static partial Regex YearRegexGenerator();

    [GeneratedRegex("'(.+?)'", RegexOptions.Compiled)]
    private static partial Regex ArgumentsRegexGenerator();
}