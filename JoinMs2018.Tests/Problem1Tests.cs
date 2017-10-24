using Xunit;

namespace JoinMs.Tests {
    public class Problem1Tests {

        // 常规测试。
        // 预期：剩余bcde，输出4。
        [Fact]
        public void Input1() {
            var str = "aabbbccccdddddeeeeee";
            var toDelete = 4;
            var r = Problem1.DeleteCharacters(str, toDelete);
            Assert.Equal(4, r);
        }

        // 测试：一个都不删。
        // 预期：剩余abcdef，输出6。
        [Fact]
        public void Input2() {
            var str = "abcdef";
            var toDelete = 0;
            var r = Problem1.DeleteCharacters(str, toDelete);
            Assert.Equal(6, r);
        }

        // 测试：删除数刚好为字符串长度。
        // 预期：一个不剩，输出0。
        [Fact]
        public void Input3() {
            var str = "abccddeeff";
            var toDelete = 10;
            var r = Problem1.DeleteCharacters(str, toDelete);
            Assert.Equal(0, r);
        }

        // 测试：常规测试的变种，不过有多个相同字符。
        // 预期：剩余cdefg（由于哈希表的访问顺序），输出5。
        [Fact]
        public void Input4() {
            var str = "abcdefgg";
            var toDelete = 2;
            var r = Problem1.DeleteCharacters(str, toDelete);
            Assert.Equal(5, r);
        }

        // 测试：空字符串，并要求删除字符。
        // 预期：本来就是空的，输出0。
        [Fact]
        public void Input5() {
            var str = "";
            var toDelete = 1;
            var r = Problem1.DeleteCharacters(str, toDelete);
            Assert.Equal(0, r);
        }

        // 测试：空字符串，但不要求删除字符。
        // 预期：本来就是空的，输出0。
        [Fact]
        public void Input6() {
            var str = "";
            var toDelete = 0;
            var r = Problem1.DeleteCharacters(str, toDelete);
            Assert.Equal(0, r);
        }

    }
}
