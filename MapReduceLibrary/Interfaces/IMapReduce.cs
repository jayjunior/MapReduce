using System;


namespace MapReduce
{
     public interface IMapReduce<InKey,InValue,TmpKey,TmpValue,OutKey,OutValue>
	{
        IMapper<InKey, InValue, TmpKey, TmpValue> Mapper
        {
            get;set;
        }
        IReducer<TmpKey,TmpValue,OutKey,OutValue> Reducer {
            get;set;
        }

        IList<Pair<OutKey, OutValue>> Submit(IList<Pair<InKey,InValue>> values);

    }
}

