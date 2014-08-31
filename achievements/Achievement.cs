// Copyright (c) 2014 Mattias Wargren
using System;

namespace achievements
{
	public class Achievement
	{

		public string identifier { get; set; }
		public Func<bool> CheckComplete = () => false;

		public bool Validate()
		{
			return CheckComplete();
		}


	}

}

