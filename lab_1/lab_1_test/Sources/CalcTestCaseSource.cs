using System.Collections.Generic;
using NUnit.Framework;

namespace lab_1_test.Sources
{
    public static class CalcTestCaseSource
    {
        public class DivideCaseSource
        {
            public int TestCase { get; set; }

            public int NumberFirst { get; set; }

            public int NumberSecond { get; set; }

            public int ExpectedNumber { get; set; }
        }

        public static List<DivideCaseSource> DivideCaseSources =>
            new List<DivideCaseSource>
            {
                new DivideCaseSource
                {
                    TestCase = 1,
                    NumberFirst = 0,
                    NumberSecond = 0
                },
                new DivideCaseSource
                {
                    TestCase = 2,
                    NumberFirst = 10,
                    NumberSecond = 0
                },
                new DivideCaseSource
                {
                    TestCase = 3,
                    NumberFirst = 10,
                    NumberSecond = -2,
                    ExpectedNumber = 10 / -2
                },
                new DivideCaseSource
                {
                    TestCase = 3,
                    NumberFirst = 10,
                    NumberSecond = 2,
                    ExpectedNumber = 10 / 2
                },
                new DivideCaseSource
                {
                    TestCase = 3,
                    NumberFirst = 2,
                    NumberSecond = 10,
                    ExpectedNumber = 2 / 10
                }
            };

        public class SqrtCaseSource
        {
            public int TestCase { get; set; }

            public int Number { get; set; }

            public int ExpectedNumber { get; set; }

            public string ExpectedThrowMessage { get; set; }
        }

        public static List<SqrtCaseSource> SqrtCaseSources
            => new List<SqrtCaseSource>
            {
                new SqrtCaseSource
                {
                    TestCase = 1,
                    Number = 0,
                    ExpectedThrowMessage = "0"
                },
                new SqrtCaseSource
                {
                    TestCase = 2,
                    Number = -1,
                    ExpectedThrowMessage = "-1"
                },
                new SqrtCaseSource
                {
                    TestCase = 3,
                    Number = 4,
                    ExpectedNumber = 2
                }
            };
    }
}