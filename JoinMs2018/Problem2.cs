using System.Collections.Generic;

namespace JoinMs {
    public static class Problem2 {

        // 矩阵自乘。输入是连通矩阵，所以保证是方阵，这里就不检查了。
        private static int[,] SelfMul(int[,] m) {
            var n1 = m.GetLength(0);
            var n2 = m.GetLength(1);

            // original:
            //var ret = (int[,])Array.CreateInstance(typeof(int), n1, n2);
            var ret = new int[n1, n2];

            for (var i = 0; i < n1; ++i) {
                for (var j = 0; j < n2; ++j) {
                    for (var p = 0; p < n1; ++p) {
                        ret[i, j] += m[i, p] * m[p, j];
                    }
                }
            }

            return ret;
        }

        // 统计矩阵中非零项的数量。
        private static int CountNonZero(int[,] m) {
            var n1 = m.GetLength(0);
            var n2 = m.GetLength(1);
            var total = 0;

            for (var i = 0; i < n1; ++i) {
                for (var j = 0; j < n2; ++j) {
                    if (i != j && m[i, j] != 0) {
                        ++total;
                    }
                }
            }

            return total;
        }

        // 求两个连通矩阵的差。
        // 输出是一个数组，每个元素为连通性发生了变化的顶点的起始、终止索引（(x,y)）。
        private static int[][] Sub(int[,] m1, int[,] m2) {
            var n1 = m1.GetLength(0);
            var n2 = m1.GetLength(1);

            var ret = new List<int[]>();

            for (var i = 0; i < n1; ++i) {
                for (var j = 0; j < n2; ++j) {
                    if (i != j && m1[i, j] == 0 && m2[i, j] != 0) {
                        ret.Add(new[] {i, j});
                    }
                }
            }

            return ret.ToArray();
        }

        // 为了测试方便写的。
        public static int CandyGiver(int studentNum, int[,] friendPairList) {
            return CandyGiver(studentNum, friendPairList.GetLength(0), friendPairList);
        }

        // 这是考题给出的函数签名。friendPairNum 参数其实是冗余的（建议使用而不是自己算，不然容易像我一样gg），见上一个函数。
        private static int CandyGiver(int studentNum, int friendPairNum, int[,] friendPairList) {
            // 构建连通矩阵。
            var m = new int[studentNum, studentNum];
            for (var i = 0; i < studentNum; ++i) {
                m[i, i] = 1;
            }
            // original: friendPairNum -> friendPairList.Length
            for (var i = 0; i < friendPairNum; ++i) {
                m[friendPairList[i, 0], friendPairList[i, 1]] = 1;
            }

            // 看看第一次给糖之后会发生什么。
            var m2 = SelfMul(m);
            var zc = CountNonZero(m);
            var zc2 = CountNonZero(m2);

            int[][] delta = null;

            // 如果有新的小朋友得到了糖，那我们就继续尝试给，看看有没有更多的小朋友得到糖。
            while (zc != zc2) {
                delta = Sub(m, m2);
                m = m2;
                m2 = SelfMul(m);
                zc = CountNonZero(m);
                zc2 = CountNonZero(m2);
            }

            if (delta == null) {
                // 如果第一轮就没给过，那这群小朋友人与人之间就太冷漠了……
                if (friendPairNum == 0) {
                    return 0;
                } else {
                    return friendPairList[0, 0];
                }
            } else {
                // 输出最长路径的第一个项索引。
                return delta[0][0];
            }
        }

    }
}
