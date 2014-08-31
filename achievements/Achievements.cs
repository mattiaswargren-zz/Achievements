// Copyright (c) 2014 Mattias Wargren

using System.Collections.Generic;
using System;

namespace achievements
{
	public class Achievements<T> where T : Achievement, new()
	{
		public Action<IEnumerable<T>> OnSave;

		List<T> achievements;
		Dictionary<string, bool> achievementsLookup = new Dictionary<string, bool>();

		public Achievements()
		{
			achievements = new List<T>();
		}

		public int Count {
			get { 
				return achievements.Count;
			}
		}

		public IEnumerable<T> GetAll()
		{
			for (var i = 0; i < achievements.Count; i++)
			{
				yield return achievements[i];
			}
		}

		public void Add(T pItem)
		{
			achievements.Add(pItem);
			achievementsLookup[pItem.identifier] = pItem.CheckComplete();
		}

		public void Add(string pIdentifier, Func<bool> pCheckComplete)
		{
			var item = new T()
			{
				identifier = pIdentifier,
				CheckComplete = pCheckComplete,
			};

			Add(item);
		}

//		public T GetAchievement(string pIdentifier)
//		{
//			foreach (var item in GetAll())
//			{
//				if (item.identifier == pIdentifier)
//				{
//					return item;
//				}
//			}
//
//			return default(T);
//		}

		public bool Check(string pIdentifier)
		{
			foreach (var item in GetAll())
			{
				if (item.identifier == pIdentifier)
				{
					return item.CheckComplete();
				}
			}

			return false;
		}

		public IEnumerable<T> GetChanged()
		{
			foreach (var item in GetAll())
			{
				if (!achievementsLookup[item.identifier] && item.CheckComplete())
				{
					achievementsLookup[item.identifier] = true;
					yield return item;
				}
			}
		}
	}
}

