using Xunit;

namespace JoinMs.Tests {
    public sealed class Interview2Tests {

        [Fact]
        public void TestNull() {
            string input = null;
            var r = Interview2.RevertString(input);
            Assert.Equal(null, r);
        }

        [Fact]
        public void TestNull2() {
            string input = null;
            var r = Interview2.RevertString2(input);
            Assert.Equal(null, r);
        }

        [Fact]
        public void TestEmpty() {
            var input = string.Empty;
            var r = Interview2.RevertString(input);
            Assert.Equal(string.Empty, r);
        }

        [Fact]
        public void TestEmpty2() {
            var input = string.Empty;
            var r = Interview2.RevertString2(input);
            Assert.Equal(string.Empty, r);
        }

        [Fact]
        public void TestWhitespace() {
            var input = "        ";
            var r = Interview2.RevertString(input);
            Assert.Equal(string.Empty, r);
        }

        [Fact]
        public void TestWhitespace2() {
            var input = "        ";
            var r = Interview2.RevertString2(input);
            Assert.Equal(string.Empty, r);
        }

        [Fact]
        public void TestEvenWords() {
            var input = "this contains four alphas";
            var r = Interview2.RevertString(input);
            Assert.Equal("alphas four contains this", r);
        }
        
        [Fact]
        public void TestEvenWords2() {
            var input = "this contains four alphas";
            var r = Interview2.RevertString2(input);
            Assert.Equal("alphas four contains this", r);
        }
        
        [Fact]
        public void TestOddWords() {
            var input = "every three words";
            var r = Interview2.RevertString(input);
            Assert.Equal("words three every", r);
        }
        
        [Fact]
        public void TestOddWords2() {
            var input = "every three words";
            var r = Interview2.RevertString2(input);
            Assert.Equal("words three every", r);
        }
        
        [Fact]
        public void TestMixedWords() {
            var input = "it has more than three words";
            var r = Interview2.RevertString(input);
            Assert.Equal("words three than more has it", r);
        }
        
        [Fact]
        public void TestMixedWords2() {
            var input = "it has more than three words";
            var r = Interview2.RevertString2(input);
            Assert.Equal("words three than more has it", r);
        }

    }
}
