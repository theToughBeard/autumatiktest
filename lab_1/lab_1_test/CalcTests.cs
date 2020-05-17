using System;
using lab_1;
using lab_1_test.Sources;
using static lab_1_test.Sources.CalcTestCaseSource;
using NUnit.Framework;

namespace lab_1_test
{
	[TestFixture]
	public class CalcTests
	{
		private Calc _calc;

		[SetUp]
		public void Setup()
		{
			_calc = new Calc();
		}

		[TearDown]
		public void Cleanup()
		{
			_calc.Dispose();
		}

		[TestCase(0, 0, 0)]
		[TestCase(1, 2, 3)]
		[TestCase(-1, 2, 1)]
		[TestCase(-1, -2, -3)]
		public void AddTest(int numberFirst, int numberSecond, int expected)
		{
			var result = _calc.Add(numberFirst, numberSecond);

			Assert.NotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestCase(0, 0, 0)]
		[TestCase(1, 2, -1)]
		[TestCase(-1, 2, -3)]
		[TestCase(-1, -2, 1)]
		public void DecrementTest(int numberFirst, int numberSecond, int expected)
		{
			var result = _calc.Decrement(numberFirst, numberSecond);

			Assert.NotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestCaseSource(typeof(CalcTestCaseSource), nameof(DivideCaseSources))]
		public void DivideTest(DivideCaseSource testData)
		{
			switch (testData.TestCase)
			{
				case 1:
				case 2:

					var ex = Assert
						.Throws<DivideByZeroException>(() => _calc.Divide(testData.NumberFirst, testData.NumberSecond));

					break;
				case 3:

					var result = _calc.Divide(testData.NumberFirst, testData.NumberSecond);

					Assert.NotNull(result);
					Assert.AreEqual(testData.ExpectedNumber, result);
					break;
			}
		}

		[TestCase(0, 0, 0)]
		[TestCase(1, 2, 2)]
		[TestCase(-1, 2, -2)]
		[TestCase(-2, -2, 4)]
		public void MultipleTest(int numberFirst, int numberSecond, int expected)
		{
			var result = _calc.Multiple(numberFirst, numberSecond);

			Assert.NotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestCaseSource(typeof(CalcTestCaseSource), nameof(SqrtCaseSources))]
		public void SqrtTest(SqrtCaseSource testData)
		{
			switch (testData.TestCase)
			{
				case 1:
				case 2:

					var ex = Assert.Throws<ArgumentException>(() => _calc.Sqrt(testData.Number));

					Assert.AreEqual(testData.ExpectedThrowMessage, ex.Message);

					break;
				case 3:

					var result = _calc.Sqrt(testData.Number);

					Assert.NotNull(result);
					Assert.AreEqual(testData.ExpectedNumber, result);
					break;
			}
		}

		[TestCase(0, 0)]
		[TestCase(1, 1)]
		[TestCase(3, 9)]
		[TestCase(-1, 1)]
		[TestCase(-2, 4)]
		public void SquareTest(int number, double expected)
		{
			var result = _calc.Square(number);

			Assert.NotNull(result);
			Assert.AreEqual(expected, result);
		}

		[Timeout(100)]
		[Ignore("Ignore a test")]
		public void IgnoreWithTimeoutTest()
		{

		}

		[Test]
		public void FailedTest()
		{
			Assert.Fail();
		}
	}
}