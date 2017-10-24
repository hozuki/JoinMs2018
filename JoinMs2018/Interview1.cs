using System;
using System.Collections.Generic;
using System.Linq;

namespace JoinMs {
    public static class Interview1 {

        /// <param name="depends">依赖图。每一项表示depends[i,0]到depends[i,1]。</param>
        /// <param name="n">总顶点数。</param>
        public static int[] GetCompilationOrder(int[,] depends, int n) {
            var dependsLength = depends.GetLength(0);

            for (var i = 0; i < dependsLength; ++i) {
                if (depends[i, 0] == depends[i, 1]) {
                    throw new ArgumentException();
                }
            }

            // 面试官指出我没有处理闭环。所以在完成的代码中我加入了处理闭环的部分。
            // 有环的时候返回 null，表示错误，而且适用于 Assert。
            if (CycleExistsInGraph(depends, n)) {
                return null;
            }

            var leafHashTable = new bool[n];
            for (var i = 0; i < n; ++i) {
                leafHashTable[i] = true;
            }
            for (var i = 0; i < dependsLength; ++i) {
                leafHashTable[depends[i, 0]] = false;
            }

            // BFT

            // 首先先查找所有出度为0的顶点。
            var q = new Queue<int>();
            for (var i = 0; i < n; ++i) {
                if (leafHashTable[i]) {
                    q.Enqueue(i);
                }
            }

            var compiled = new List<int>();

            while (q.Count > 0) {
                var index = q.Dequeue();
                if (compiled.Contains(index)) {
                    continue;
                }

                var areAllDependenciesCompiled = true;
                for (var i = 0; i < dependsLength; ++i) {
                    if (depends[i, 0] == index && !compiled.Contains(depends[i, 1])) {
                        areAllDependenciesCompiled = false;
                        break;
                    }
                }

                if (areAllDependenciesCompiled) {
                    compiled.Add(index);
                }

                // 添加依赖于此项的项。
                for (var i = 0; i < dependsLength; ++i) {
                    if (depends[i, 1] == index) {
                        q.Enqueue(depends[i, 0]);
                    }
                }
            }

            return compiled.ToArray();
        }

        private static bool CycleExistsInGraph(int[,] depends, int n) {
            return Enumerable.Range(0, n).Any(i => CycleExistsInSubGraph(depends, i, n));
        }

        private static bool CycleExistsInSubGraph(int[,] depends, int root, int n) {
            // 本质上是DFS。
            var visited = new bool[n];
            var stack = new Stack<int>();
            var dependsLength = depends.GetLength(0);

            stack.Push(root);

            while (stack.Count > 0) {
                var current = stack.Pop();
                if (visited[current]) {
                    return true;
                }

                visited[current] = true;

                for (var i = 0; i < dependsLength; ++i) {
                    if (depends[i, 0] != current) {
                        continue;
                    }
                    if (visited[depends[i, 1]]) {
                        return true;
                    }
                    stack.Push(depends[i, 1]);
                }
            }

            return false;
        }

    }
}
