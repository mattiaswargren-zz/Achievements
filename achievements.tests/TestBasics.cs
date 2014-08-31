// Copyright (c) 2014 Mattias Wargren

using NUnit.Framework;
using System;

namespace achievements.tests
{
	[TestFixture()]
	public class TestBasics
	{

		[Test()]
		public void TestCount()
		{
			var achievements = new Achievements<Achievement>();
			Assert.AreEqual(0, achievements.Count);
		}

		[Test()]
		public void TestAddAndSizes()
		{
			var achievements = new Achievements<Achievement>();

			achievements.Add("Achievement 1", () => true);
			achievements.Add("Achievement 2", () => true);
			achievements.Add("Achievement 3", () => false);

			Assert.AreEqual(3, achievements.Count);

			Assert.IsTrue(achievements.Check("Achievement 1"));
			Assert.IsTrue(achievements.Check("Achievement 2"));
			Assert.IsFalse(achievements.Check("Achievement 3"));
		}

		[Test()]
		public void TestLists()
		{
			var achievements = new Achievements<Achievement>();

			achievements.Add("Achievement 1", () => true);
			achievements.Add("Achievement 2", () => false);
			achievements.Add("Achievement 3", () => false);

			foreach (var item in achievements.GetAll())
			{
				Console.WriteLine("{0} -> {1}", item.identifier, item.CheckComplete());
			}
		}

		[Test()]
		public void TestSave()
		{
			var achievements = new Achievements<Achievement>();
		
			var gold = 0;
		
			achievements.Add("10 gold", () => gold >= 10);
			achievements.Add("20 gold", () => gold >= 20);
			achievements.Add("All gold", () => achievements.Check("10 gold") && achievements.Check("20 gold"));
		
			while (gold < 20)
			{
				gold++;
			}

			Assert.IsTrue(achievements.Check("10 gold"));
			Assert.IsTrue(achievements.Check("20 gold"));
			Assert.IsTrue(achievements.Check("All gold"));

			foreach(var item in achievements.GetChanged())
			{
				Console.WriteLine("{0} -> just completed {1}", gold, item.identifier);
			}
		
		}


	}
}

