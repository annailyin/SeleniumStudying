using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Homework3
{
    public partial class Tests
    {
        public class TestFixtureSource
        {
            public static IEnumerable FixtureParams
            {
                get
                {
                    yield return new TestFixtureData("Edge", "latest", "Windows 10");
                    yield return new TestFixtureData("Firefox", "latest", "Windows 8.1");
                    yield return new TestFixtureData("Chrome", "latest", "Windows 11");
                }
            }
        }
    }
}
