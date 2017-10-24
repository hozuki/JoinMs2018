using Xunit;

namespace JoinMs.Tests {
    public sealed class Interview1Tests {

        [Fact]
        public void TestSimpleCase() {
            var total = 10;
            var d = new[,] {
                {1, 2},
                {2, 3}
            };
            var r = Interview1.GetCompilationOrder(d, total);
            var e = new[] {0, 3, 4, 5, 6, 7, 8, 9, 2, 1};
            Assert.Equal(e, r);
        }

        [Fact]
        public void TestComplexCase() {
            var total = 10;
            var d = new[,] {
                {1, 2},
                {2, 3},
                {2, 4},
                {7, 1},
                {9, 3},
                {4, 5}
            };
            var r = Interview1.GetCompilationOrder(d, total);
            var e = new[] {0, 3, 5, 6, 8, 9, 4, 2, 1, 7};
            Assert.Equal(e, r);
        }

        [Fact]
        public void TestSmallCycle() {
            var total = 10;
            var d = new[,] {
                {1, 2},
                {2, 1}
            };
            var r = Interview1.GetCompilationOrder(d, total);
            Assert.Equal(null, r);
        }

        [Fact]
        public void TestLargeCycle() {
            var total = 10;
            var d = new[,] {
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 1}
            };
            var r = Interview1.GetCompilationOrder(d, total);
            Assert.Equal(null, r);
        }

        [Fact]
        public void TestComplexCycle() {
            var total = 10;
            var d = new[,] {
                {1, 2},
                {2, 3},
                {4, 5},
                {5, 2},
                {6, 9},
                {1, 7},
                {3, 4},
                {4, 1}
            };
            var r = Interview1.GetCompilationOrder(d, total);
            Assert.Equal(null, r);
        }

    }
}
