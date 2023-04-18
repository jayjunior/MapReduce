using System;

/*
 * Interface for Mapper Instance , for implementing the map function.
 */

namespace MapReduce
{
	public interface IMapper<InKey,InValue,TmpKey,TmpValue>
	{

		public IList<Pair<TmpKey, TmpValue>> Map(InKey key , InValue value);
	}
}

