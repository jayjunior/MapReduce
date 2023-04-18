
namespace DijstraTest {


    [TestClass]
    public class DijstraTest
    {

        public Dijkstra.Dijkstra sut;


        public static void RunTestCases(Dijkstra.Dijkstra sut)
        {
            int[,] test1 = {
            {  0, -1,  3,  8,  5 },
            { -1,  0,  2,  1, 12 },
            {  3,  2,  0,  4, -1 },
            {  8,  1,  4,  0, -1 },
            {  5, 12, -1, -1,  0 }
        };

            int[] expected1 = { 5, 10, 8, 11, 0 };

            int[] actual1 = sut.FindShortestPathsSequential(test1, 4);

            CollectionAssert.AreEqual(expected1, actual1, "Error!");

            int[,] test2 = {
            {  0, 10,  1,  8, -1, -1 },
            { 10,  0,  7, -1, -1,  2 },
            {  1,  7,  0,  4, -1,  6 },
            {  8, -1,  4,  0,  6, -1 },
            { -1, -1, -1,  6,  0,  3 },
            { -1,  2,  6, -1,  3,  0 }
        };

            int[] expected2 = { 0, 8, 1, 5, 10, 7 };

            int[] actual2 = sut.FindShortestPathsSequential(test2, 0);

            CollectionAssert.AreEqual(expected2, actual2, "Error!");

            int[,] test3 = {
            { 0, 7, 12, -1 ,-1 , -1 },
            {-1, 0, 2, 9, -1, -1    },
            {-1, -1, 0 , -1, 10 ,-1 },
            {-1, -1, -1 ,0, -1, 1   },
            { -1,-1 ,-1 ,4 ,0 ,5    },
            {-1 , -1 , -1, -1,-1 ,0 },
        };

            int[] expected3 = { 0, 7, 9, 16, 19, 17 };

            int[] actual3 = sut.FindShortestPathsSequential(test3, 0);

            CollectionAssert.AreEqual(expected3, actual3, "Error!");
        }


        
        [TestMethod]
        public void DijkstraWithParallel()
        {
            sut = new Dijkstra.Dijkstra(true);
            RunTestCases(sut);
        }
        

        [TestMethod]
        public void DijkstraWithSequential()
        {
            sut = new Dijkstra.Dijkstra(false);
            RunTestCases(sut);
        }
    }

}
