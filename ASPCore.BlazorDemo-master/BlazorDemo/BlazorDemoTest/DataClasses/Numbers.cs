using System.Collections;
using System.Collections.Generic;

namespace BlazorDemoTest.DataClasses
{
    public class Numbers : IEnumerable<object[]>
    {
        private static IEnumerable<object[]> Data => new[]
        {
            new object[] {"81", "mega garbage input", "9"},
            new object[] {"36", "Mike's behårede ben", "6"},
            new object[] {"25", "Kaare's Seng", "5"}
        };
        public IEnumerator<object[]> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}