using System.Text.Json;

namespace AiAgent.Services.Tools.Models;

/// <summary>
/// Описание инструмента, доступного модели.
/// </summary>
public sealed class ToolDefinition
{
    public string Type => "function";

    public required ToolFunctionDefinition Function { get; init; }
}

/// <summary>
/// Описание функции инструмента и её параметров.
/// </summary>
public sealed class ToolFunctionDefinition
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public ToolParameters Parameters { get; init; } = new();
}

/// <summary>
/// Схема аргументов функции инструмента.
/// </summary>
public sealed class ToolParameters
{
    public string Type => "object";

    public Dictionary<string, ToolParameter> Properties { get; init; } = [];

    public List<string> Required { get; init; } = [];
}

/// <summary>
/// Описание отдельного аргумента инструмента.
/// </summary>
public sealed class ToolParameter
{
    public required string Type { get; init; }

    public required string Description { get; init; }
}

/// <summary>
/// Вызов инструмента, запрошенный моделью.
/// </summary>
public sealed class ToolCallModel
{
    public required ToolFunctionCallModel Function { get; init; }
}

/// <summary>
/// Имя и аргументы вызываемой функции.
/// </summary>
public sealed class ToolFunctionCallModel
{
    public required string Name { get; init; }

    public JsonElement Arguments { get; init; }
}