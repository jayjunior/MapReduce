
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

            int[] actual1 = sut.FindShortestPaths(test1, 4);

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

            int[] actual2 = sut.FindShortestPaths(test2, 0);

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

            int[] actual3 = sut.FindShortestPaths(test3, 0);

            CollectionAssert.AreEqual(expected3, actual3, "Error!");


            int[,] test4 = {
                {0, 10 , 20 , -1 , -1 , -1 },
                {-1, 0 , -1 , 50 , 10 , -1 },
                {-1, -1, 0  , 20 , 33 , -1 },
                {-1, -1, -1 , 0  , 20 , 2  },
                {-1, -1, -1 , -1 , 0  , 1  },
                {-1, -1, -1 , -1 , -1 , 0  },
            };

            int[] expected4 = { -2, -2, 0, 20, 33, 22 };

            int[] actual4 = sut.FindShortestPaths(test4, 2);

            CollectionAssert.AreEqual(expected4, actual4, "Error!");

            int[] expected5 = { -2, -2, -2, 0, 20, 2 };

            int[] actual5 = sut.FindShortestPaths(test4, 3);

            CollectionAssert.AreEqual(expected5, actual5, "Error!");


            int[,] test5Loop = {
                {0, 10 , -1 , -1 , -1 , -1},
                {-1, 0 , 1  , -1 , -1 , -1},
                {-1, -1, 0  , -1 , 3  , -1},
                {-1, 4 , -1 , 0  , -1 , -1},
                {-1, -1, -1 , 10, 0   , 22},
                {-1, -1, -1 , -1, -1  , 0 },
            };

            int[] expected5Loop = { -2, 0, 1, 14, 4, 26 };

            int[] actual5Loop = sut.FindShortestPaths(test5Loop, 1);

            CollectionAssert.AreEqual(expected5Loop, actual5Loop, "Error!");

            // a b c d e f g h i j
            
            int[,] test6 = {
                
                { 0,-1 ,-1 ,-1 ,-1 , 343 , -1 ,1435 , 464 ,-1 }, // f h i
                {-1 , 0, -1 , -1 ,-1 ,879,954,811,-1,524 }, // h g j ,f
                {-1 , -1 , 0 , -1 , 1364 , 1054 , -1 ,-1 , -1 , -1 },//e f
                { -1 , -1 , -1 , 0 , -1 , -1, 433 , -1 , -1 , 1053},// g j
                {-1 , -1 ,1364 , -1 , 0 , 1106 , -1 ,-1 , -1 ,766 }, // // c f j
                {343 , 879 , 1054 ,-1 , 1106 , 0 , -1 , -1 , -1 , -1 }, // a  b c e
                {-1 , 954 , -1 , 433 , -1 , -1 ,0 ,837 , -1 , -1 }, // b d h
                { 1435 , 811 , -1 , -1 , -1 ,-1 , 837  , 0 , -1 ,-1}, // a b g
                {464 , -1 ,-1 ,-1 , -1 ,-1 ,-1 ,-1 , 0 ,-1 },
                {-1 , 524 , -1 , 1053 , 766 , -1 , -1 ,-1 , -1 , 0 }, // b d e

            };

            int[] expected6 = { 2176, 954, 2887, 433, 2244, 1833, 0, 837, 2640, 1478 };

            int[] actual6 = sut.FindShortestPaths(test6, 6);

            CollectionAssert.AreEqual(expected6, actual6, "Error!");

            int[] expected7 = { 343, 879, 1054, 2266, 1106, 0, 1833, 1690, 807, 1403 };

            int[] actual7 = sut.FindShortestPaths(test6, 5);

            CollectionAssert.AreEqual(expected7, actual7, "Error!");


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
