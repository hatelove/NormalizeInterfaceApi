using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NormalizeInterfaceApiTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void Test黑貓只用重量計算運費()
		{
			var target = new MyScenario();
			var actual = target.CalculateFee(1);
			var expected = 50;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Test郵局使用重量跟長寬高算運費_取便宜的()
		{
			var target = new MyScenario();
			var actual = target.CalculateFee(2);
			var expected = 60;
			Assert.AreEqual(expected, actual);
		}
	}

	internal class MyScenario
	{
		public double CalculateFee(int shipperId)
		{
			//product
			var weight = 10;
			var length = 30;
			var width = 20;
			var height = 10;

			if (shipperId == 1)
			{
				var blackcat = new Blackcat();

				//黑貓只用商品重量來決定運費
				var result = blackcat.GetFee(weight);
				return result;
			}
			else if (shipperId == 2)
			{
				var postOffice = new PostOffice();

				// 需要重量跟長寬高
				var result = postOffice.GetFee(weight, length, width, height);
				return result;
			}
			else
			{
				throw new NotImplementedException();
			}
			
		}
	}

	internal class Blackcat
	{
		public Blackcat()
		{
		}

		internal double GetFee(int weight)
		{
			return weight * 5;
		}
	}

	internal class PostOffice
	{
		public PostOffice()
		{
		}

		internal double GetFee(int weight, int length, int width, int height)
		{
			var feeByWeight = weight * 7;
			var feeBySize = length * width * height / 100;

			var result = feeByWeight < feeBySize ? feeByWeight : feeBySize;

			return result;
		}
	}
}
