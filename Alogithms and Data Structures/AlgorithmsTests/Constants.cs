using System.Reflection;

namespace AlgorithmsTests;

public static class Constants
{
    public static string TestDataLocalPath => "Data\\";

    public static string GraphTestDataPath =>
        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, 
                     TestDataLocalPath,
                     "GraphAlgorithmsTests");
}