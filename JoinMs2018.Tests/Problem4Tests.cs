using System;
using System.Collections.Generic;
using Xunit;

namespace JoinMs.Tests {
    public class Problem4Tests {

        // 测试：孤独的根节点。
        // 预期：明显是1。
        [Fact]
        public void Input1() {
            var root = ParseTree("*N");
            var r = Problem4.MinCoverSet(root);
            Assert.True(r == 1);
        }

        // 测试：带有一个子节点的根节点。
        // 预期：输出1。（算法筛选的关键节点已标出）
        [Fact]
        public void Input2() {
            var root = ParseTree("*N(N)");
            var r = Problem4.MinCoverSet(root);
            Assert.True(r == 1);
        }

        // 测试：带有两个子节点的根节点。
        // 预期：输出1。
        [Fact]
        public void Input3() {
            var root = ParseTree("*N(N)(N)");
            var r = Problem4.MinCoverSet(root);
            Assert.True(r == 1);
        }

        // 原题的测试树。
        // 预期：输出2。
        [Fact]
        public void Input4() {
            // original example
            var root = ParseTree("*N(*N(N,N),N)(N)");
            var r = Problem4.MinCoverSet(root);
            Assert.True(r == 2);
        }

        // 测试：对上个测试用例的树的扩展。
        // 预期：输出3。
        [Fact]
        public void Input5() {
            var root = ParseTree("*N(*N(N,N),*N(N,N,N))(N)(N)(N)");
            var r = Problem4.MinCoverSet(root);
            Assert.True(r == 3);
        }

        // 测试：根节点下的所有子节点都是关键节点，从而不应被选中。
        // 预期：输出5。
        [Fact]
        public void Input6() {
            var root = ParseTree("N(*N(N,N),*N(N,N,N))(*N(N,N,N,N))(*N(N,N))(*N(N))");
            var r = Problem4.MinCoverSet(root);
            Assert.True(r == 5);
        }

        // 测试：
        [Fact]
        public void Input7() {
            var root = ParseTree("*N(N(*N(N(*N(N)(N))(*N))(N))(*N))(N)");
            var r = Problem4.MinCoverSet(root);
            Assert.True(r == 5);
        }

        // Format example: N(N(N,N,N),N)
        private static Problem4.NAryNode ParseTree(string s) {
            if (string.IsNullOrEmpty(s)) {
                throw new ArgumentException("Invalid tree.", "s");
            }

            s = s.ToLowerInvariant();
            if (!s.StartsWith("n", StringComparison.Ordinal) && !s.StartsWith("*", StringComparison.Ordinal)) {
                throw new ArgumentException(null, "s");
            }

            var stack = new Stack<Problem4.NAryNode>();
            Problem4.NAryNode root = null;
            Problem4.NAryNode currentNode = null;

            foreach (var ch in s) {
                switch (ch) {
                    case 'n': {
                        var node = new Problem4.NAryNode();
                        if (stack.Count == 0) {
                            if (root == null) {
                                root = node;
                            } else {
                                throw new FormatException("Illegal input.");
                            }
                        } else {
                            var parent = stack.Peek();
                            parent.AddChild(node);
                        }
                        currentNode = node;
                        break;
                    }
                    case '(': {
                        if (currentNode == null) {
                            throw new FormatException("Illegal input.");
                        }
                        if (currentNode.Children == null) {
                            currentNode.Children = new List<Problem4.NAryNode>();
                        }
                        stack.Push(currentNode);
                        break;
                    }
                    case ')':
                        try {
                            currentNode = stack.Pop();
                        } catch (InvalidOperationException ex) {
                            throw new FormatException("Illegal input.", ex);
                        }
                        break;
                    case ',': // node separator (optional)
                    case '*': // key node marker (for humans)
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("ch", ch, null);
                }
            }

            if (stack.Count != 0) {
                throw new FormatException("Illegal input.");
            }

            return root;
        }

    }
}
