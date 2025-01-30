using log4net;
using log4net.Config;
using System.IO;

namespace SauceLabsAutomationPOM.Tests;

public class DryRunTest
{
    // Initialize logger
    private static readonly ILog logger = LogManager.GetLogger(typeof(DryRunTest));

    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        // Ensure 'Logs' directory exists
        string logDirectory = "Logs";
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
            logger.Info("Logs directory created.");
        }

        // Load log4net configuration from the config file
        XmlConfigurator.Configure(new FileInfo("log4net.config"));
        logger.Info("Test Suite Started.");
    }

    [SetUp]
    public void SetUp()
    {
        logger.Info("Test started.");
    }

    [Test]
    public void Add_TwoNumbers_ReturnsCorrectSum()
    {
        int result = Add(2, 3);
        logger.Info("Test Add_TwoNumbers_ReturnsCorrectSum passed.");
    }

    [Test]
    public void Subtract_TwoNumbers_ReturnsCorrectDifference()
    {
        int result = Subtract(5, 3);
        logger.Info("Test Subtract_TwoNumbers_ReturnsCorrectDifference passed.");
    }

    [TearDown]
    public void TearDown()
    {
        logger.Info("Test finished.");
    }

    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        logger.Info("Test Suite Completed.");
    }

    // Simple methods to test
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
}