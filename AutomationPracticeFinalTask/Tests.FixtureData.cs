using NUnit.Framework;
using System.Collections;

namespace AutomationPracticeFinalTask
{
    public partial class Tests
    {
        public class TestFixtureSource
        {
            public static IEnumerable FixtureParams
            {
                get
                {
                    yield return new TestFixtureData("Local", "Chrome", null, null);
                    yield return new TestFixtureData("Local", "Edge", null, null);
                    yield return new TestFixtureData("Remote", "Chrome", "latest", "Windows 11");
                    yield return new TestFixtureData("Remote", "Edge", "latest", "Windows 10");
                }
            }
        }
    }
}
