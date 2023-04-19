using System;
using MapReduce;

namespace Dijkstra {
    public class Dijkstra
    {

        static readonly int INFINITY = -2;

        bool _Parallel = false;

        public Dijkstra(bool parallel) {
            this._Parallel = parallel;
        }

        private class Mapper<InKey, InValue, TmpKey, TmpValue> : IMapper<int, int, int, int>
        {
            int[,] adjacencyMatrix;

            public Mapper(int[,] adjacencyMatrix)
            {
                this.adjacencyMatrix = adjacencyMatrix;
            }


            public IList<Pair<int, int>> Map(int node, int cost)
            {
                List<Pair<int, int>> result = new();
                for (int neighbour = 0; neighbour < adjacencyMatrix.GetLength(0); neighbour++)
                {
                    if (adjacencyMatrix[node, neighbour] == -1) continue;
                    if (cost == INFINITY || node == neighbour) result.Add(new Pair<int, int>(neighbour, cost));
                    else
                    {
                        result.Add(new Pair<int, int>(neighbour, adjacencyMatrix[node, neighbour] + cost));
                    }
                }
                return result;
            }
        }

        

        private class Reducer<TmpKey, TmpValue, OutKey, OutValue> : IReducer<int, int, int, int>
        {


            public IList<Pair<int, int>> Reduce(int node, ICollection<int> costs)
            {
                List<Pair<int, int>> result = new();

                int minCost = costs.First();

                foreach (int cost in costs)
                {
                    minCost = minCost == INFINITY ? cost : cost == INFINITY ? minCost : Math.Min(cost, minCost);
                }
                result.Add(new Pair<int, int>(node, minCost));

                return result;
            }
        }

        public bool Parallel {
            get; set;
        }

        public int[] FindShortestPaths(int[,] adjacencyMatrix, int startNode)

        {

            IMapReduce<int, int, int, int, int, int> mapReduce;

            if (Parallel) {
                mapReduce = new ParallelMapReduce<int, int, int, int, int, int>(new Mapper<int, int, int, int>(adjacencyMatrix), new Reducer<int, int, int, int>());
            }
            else {

                mapReduce = new SequentialMapReduce<int, int, int, int, int, int>(new Mapper<int, int, int, int>(adjacencyMatrix), new Reducer<int, int, int, int>());
            }


            List<Pair<int, int>> initialList = new();

            int[] result = new int[adjacencyMatrix.GetLength(0)];

            initialList.Add(new Pair<int, int>(startNode, 0));

            for (int i = 1; i < adjacencyMatrix.GetLength(0); i++)
            {
                initialList.Add(new Pair<int, int>(i, INFINITY));
            }

            List<Pair<int, int>> tmp;

            bool modified;

            do
            {
                modified = false;
                tmp = new List<Pair<int, int>>(initialList);
                foreach (Pair<int, int> pair in mapReduce.Submit(tmp))
                {
                    if (!Equals(initialList[pair.Key].Value, pair.Value))
                    {
                        initialList[pair.Key] = pair;
                        modified = true;
                    }
                }
            } while (modified);

            for (int i = 0; i < initialList.Count; i++)
            {
                result[initialList[i].Key] = initialList[i].Value;
            }


            return result;

        }

        public static void Main(string[] arga) { }

    }
}




