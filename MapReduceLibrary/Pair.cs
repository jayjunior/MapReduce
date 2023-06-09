﻿/**
 * A pair of two values.  Helper Class for the MapReduceLibrary .
 *
 * @param K the type of the first element of the pair
 * @param V the type of the second element of the pair
 */

using System;

namespace MapReduce
{
	public class Pair<InKey,InValue>
	{

		public Pair(InKey Key , InValue Value)
		{
			this.Key = Key;
			this.Value = Value;
		}

		public InKey Key {
			get;set;
		}
		public InValue Value {
			get;set;
		}
	}
}

