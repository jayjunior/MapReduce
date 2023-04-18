using System;
/*
 * Interface for Reducer Instance , for implementing the reduce function
 */
namespace MapReduce
{
	public interface IReducer<TmpKey,TmpValue,OutKey,OutValue>


	{

		public IList<Pair<OutKey,OutValue>> Reduce(TmpKey key, ICollection<TmpValue> values);
	}
}

