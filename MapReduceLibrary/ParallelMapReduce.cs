/*
 * @author: Jay Junior
 * @date : 04.2023
*/


using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MapReduce
{
	sealed public class ParallelMapReduce<InKey, InValue, TmpKey, TmpValue, OutKey, OutValue> : IMapReduce<InKey, InValue, TmpKey, TmpValue, OutKey, OutValue>
    {

        private IMapper<InKey, InValue, TmpKey, TmpValue> _Mapper;
        private IReducer<TmpKey, TmpValue, OutKey, OutValue> _Reducer;

        private static readonly object _AccessLock = new();
        
        public ParallelMapReduce(IMapper<InKey, InValue, TmpKey, TmpValue> Mapper, IReducer<TmpKey, TmpValue, OutKey, OutValue> Reducer)
		{
            this._Mapper = Mapper;
            this._Reducer = Reducer;
        }

        public IMapper<InKey, InValue, TmpKey, TmpValue> Mapper { get => this._Mapper; set => this._Mapper = value; }

        public IReducer<TmpKey, TmpValue, OutKey, OutValue> Reducer { get => this._Reducer; set => this._Reducer = value; }


        public IList<Pair<OutKey, OutValue>> Submit(IList<Pair<InKey, InValue>> values)

        {
            if (values == null) throw new ArgumentNullException($"{values}");

            ConcurrentDictionary<TmpKey, List<TmpValue>> combinedTemporaryValues = new();

            #region Map Phase


            Parallel.ForEach(values, (pair) =>
            {
                
                foreach (Pair<TmpKey, TmpValue> item in this._Mapper.Map(pair.Key, pair.Value))

                {
                    /*
                     * Locking here may seem unneeded due to the use of a Thread-safe dictionaray .
                     * But race conditions may still occur between line 46 and 48 . Tho the need of the lock :) . 
                     */
                    lock (_AccessLock)
                    {
                        if (!combinedTemporaryValues.ContainsKey(item.Key))
                        {
                            combinedTemporaryValues.TryAdd(item.Key, new List<TmpValue>());
                        }
                        // ignore Warning ! Can never be null ! 
                        combinedTemporaryValues.GetValueOrDefault(item.Key).Add(item.Value);
                    }
                }
               
            });

            #endregion Reduce Phase

            List<Pair<OutKey, OutValue>> result = new();

            #region Reduce Phase

            Parallel.ForEach(combinedTemporaryValues, (pair) =>
            {
                lock (_AccessLock)
                {
                    result.AddRange(this._Reducer.Reduce(pair.Key, pair.Value));
                }
            });

            #endregion Reduce Phase

            return result;
            
        }


    }

}

