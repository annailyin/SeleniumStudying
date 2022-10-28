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
                    yield return new TestFixtureData("Firefox");
                    yield return new TestFixtureData("Chrome");
                    yield return new TestFixtureData("Edge");
                }
            }
        }
    }
}
