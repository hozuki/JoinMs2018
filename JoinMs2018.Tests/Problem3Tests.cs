using Xunit;

namespace JoinMs.Tests {
    public class Problem3Tests {

        // 这就是考题自带的例子。
        // 预期：7(=9-2) + 4(=7-3) = 11
        [Fact]
        public void Input1() {
            var billList = new[] {
                2, 3, 4, 9
            };
            var r = Problem3.AllVouchers(billList);
            Assert.Equal(11, r);
        }

        // 测试：只有一个顾客。
        // 预期：不发生变化，没有礼券产生，直接输出0。
        [Fact]
        public void Input2() {
            var billList = new[] {
                1
            };
            var r = Problem3.AllVouchers(billList);
            Assert.Equal(0, r);
        }

        // 测试：所有金额相等。
        // 预期：整个过程中都没有礼券产生，输出0。
        [Fact]
        public void Input3() {
            var billList = new[] {
                3, 3, 3, 3, 3
            };
            var r = Problem3.AllVouchers(billList);
            Assert.Equal(0, r);
        }

        // 测试：每步都有礼券产生。
        // 预期：4(=5-1) + 2(=4-2) + 1(=3-2) = 7
        [Fact]
        public void Input4() {
            var billList = new[] {
                1, 2, 3, 5
            };
            var r = Problem3.AllVouchers(billList);
            Assert.Equal(7, r);
        }

    }
}
