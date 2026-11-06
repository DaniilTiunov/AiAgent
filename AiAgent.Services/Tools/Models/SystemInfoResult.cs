namespace AiAgent.Services.Tools.Models;

/// <summary>
/// Сведения об ОС, архитектуре, процессорах и версии .NET.
/// </summary>
public sealed record SystemInfoResult(
    string OperatingSystem,
    string Architecture,
    int ProcessorCount,
    string DotNetVersion);