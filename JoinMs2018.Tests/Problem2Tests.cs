using Xunit;

namespace JoinMs.Tests {
    public class Problem2Tests {

        // 常规测试。
        // 预期：2->3->4->8，输出2。
        [Fact]
        public void Input1() {
            var studentCount = 10;
            var pairs = new[,] {
                {5, 1},
                {2, 3},
                {3, 4},
                {4, 8},
                {3, 9}
            };
            var r = Problem2.CandyGiver(studentCount, pairs);
            Assert.Equal(2, r);
        }

        // 测试：冷漠的幼儿园。
        // 预期：没给糖，输出0。
        [Fact]
        public void Input2() {
            var studentCount = 10;
            var pairs = new int[,] { };
            var r = Problem2.CandyGiver(studentCount, pairs);
            Assert.Equal(0, r);
        }

        // 测试：带有局部环的图。
        // 预期：9->1->2，输出9。
        [Fact]
        public void Input3() {
            var studentCount = 10;
            var pairs = new[,] {
                {9, 1},
                {1, 9},
                {1, 2}
            };
            var r = Problem2.CandyGiver(studentCount, pairs);
            Assert.Equal(9, r);
        }

        // 测试：仅有环的图。
        // 预期：3->1，输出3。
        [Fact]
        public void Input4() {
            var studentCount = 10;
            var pairs = new[,] {
                {3, 1},
                {1, 3}
            };
            var r = Problem2.CandyGiver(studentCount, pairs);
            Assert.Equal(3, r);
        }

        // 测试：只有一条边的图。
        // 预期：1->2，输出1。
        [Fact]
        public void Input5() {
            var studentCount = 4;
            var pairs = new[,] {
                {1, 2}
            };
            var r = Problem2.CandyGiver(studentCount, pairs);
            Assert.Equal(1, r);
        }

    }
}
