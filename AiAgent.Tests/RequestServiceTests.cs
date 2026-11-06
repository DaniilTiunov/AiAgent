using System.Text.Json;
using AiAgent.Services.Client;
using AiAgent.Services.Request.Models;
using AiAgent.Services.Request.Services;
using Xunit.Abstractions;

namespace AiAgent.Tests;

/// <summary>
/// Подготовка тестов сервиса запросов.
/// </summary>
public class RequestServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    /// <summary>
    /// Инициализирует тестовый класс и вывод результатов.
    /// </summary>
    public RequestServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
}   
    