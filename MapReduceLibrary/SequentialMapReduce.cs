/*
 * @author: Jay Junior
 * @date : 04.2023
*/

using System;

namespace MapReduce
{
	sealed public class SequentialMapReduce<InKey, InValue, TmpKey, TmpValue, OutKey, OutValue> : IMapReduce<InKey, InValue, TmpKey, TmpValue, OutKey, OutValue>
	{

        private IMapper<InKey, InValue, TmpKey, TmpValue> _Mapper;
        private IReducer<TmpKey, TmpValue, OutKey, OutValue> _Reducer;

        public SequentialMapReduce(IMapper<InKey,InValue,TmpKey,TmpValue> Mapper , IReducer<TmpKey,TmpValue,OutKey,OutValue> Reducer)
		{
            this._Mapper = Mapper;
            this._Reducer = Reducer;
        }

        public IMapper<InKey, InValue, TmpKey, TmpValue> Mapper
        {
            get => this._Mapper; set => this._Mapper = value;
        }

        public IReducer<TmpKey, TmpValue, OutKey, OutValue> Reducer {
            get => this._Reducer; set => this._Reducer = value;
        }
        
        public IList<Pair<OutKey, OutValue>> Submit(IList<Pair<InKey, InValue>> values)
        {
            if (values == null) throw new ArgumentNullException($"{values}");
            
            Dictionary<TmpKey, List<TmpValue>> combinedTemporaryValues = new();


            #region Map Phase


            foreach (Pair<InKey,InValue> pair in values) {


                foreach(Pair<TmpKey,TmpValue> mappedPair in this._Mapper.Map(pair.Key,pair.Value) ) {
                    if (!combinedTemporaryValues.ContainsKey(mappedPair.Key)) {
                        combinedTemporaryValues.Add(mappedPair.Key, new List<TmpValue>());
                    }
                    // ignore Warning ! Can never be null ! 
                    combinedTemporaryValues.GetValueOrDefault(mappedPair.Key).Add(mappedPair.Value);
                }
            }

            #endregion Map Phase


            List<Pair<OutKey, OutValue>> result = new List<Pair<OutKey, OutValue>>();

            #region Reduce Phase

            foreach (KeyValuePair<TmpKey,List<TmpValue>> pair in combinedTemporaryValues) {
                result.AddRange(this._Reducer.Reduce(pair.Key, pair.Value));
            }

            #endregion Reduce Phase

            return CollectionExtensions.AsReadOnly(result);
            
        }


    }
}

