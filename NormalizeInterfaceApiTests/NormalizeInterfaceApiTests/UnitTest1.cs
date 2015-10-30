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

			var product = new Product { Weight = weight, Length = length, Width = width, Height = height };

			IShipper shipper = GetShipper(shipperId, product);

			if (shipper != null)
			{
				var result = shipper.GetFee();

				return result;
			}
			else
			{
				throw new NotImplementedException();
			}
			
			//if (shipperId == 1)
			//{
			//	IShipper shipper = new Blackcat();

			//	//黑貓只用商品重量來決定運費
			//	double result = shipper.GetFee();
			//	return result;
			//}
			//else if (shipperId == 2)
			//{
			//	IShipper shipper = new PostOffice();

			//	// 需要重量跟長寬高
			//	double result = shipper.GetFee();
			//	return result;
			//}
			//else
			//{
			//	throw new NotImplementedException();
			//}

		}

		private IShipper GetShipper(int shipperId, Product product)
		{
			switch (shipperId)
			{
				case 1:
					return new Blackcat(product.Weight);
				case 2:
					return new PostOffice(product);
				default:
					break;
			}
		}
	}

	internal class Blackcat : IShipper
	{
		public Blackcat()
		{
		}

		public double GetFee()
		{
			throw new NotImplementedException();
		}

		internal double GetFee(int weight)
		{
			return weight * 5;
		}
	}

	internal class PostOffice : IShipper
	{
		public PostOffice()
		{
		}

		public double GetFee()
		{
			throw new NotImplementedException();
		}

		internal double GetFee(int weight, int length, int width, int height)
		{
			var feeByWeight = weight * 7;
			var feeBySize = length * width * height / 100;

			var result = feeByWeight < feeBySize ? feeByWeight : feeBySize;

			return result;
		}
	}

	internal interface IShipper
	{
		double GetFee();
	}

	internal class Product
	{
		public int Height { get; set; }
		public int Length { get; set; }
		public int Weight { get; set; }
		public int Width { get; set; }
	}
}
