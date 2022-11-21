using FaqService.ComponentTests.ExternalEnvironment;
using TechTalk.SpecFlow;

namespace FaqService.ComponentTests.Hooks;

[Binding]
public static class ScenarioBeforeAndAfter
{
    private static ExtEnvironment? _extEnvironment;

    /// <summary>
    /// Выполняется перед всеми тестами
    /// </summary>
    [BeforeTestRun]
    public static void BeforeTestRun() => _extEnvironment = new ExtEnvironment();

    /// <summary>
    /// Выполняется перед каждым сценарием
    /// </summary>
    [BeforeScenario]
    public static void BeforeScenario()
    {
        ExtEnvironment.Clean();
        Common.Common.ClearState();
    }
}