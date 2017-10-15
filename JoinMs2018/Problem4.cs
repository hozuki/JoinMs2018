using System;
using System.Collections.Generic;
using System.Linq;

namespace JoinMs {
    public static class Problem4 {

        public static int MinCoverSet(NAryNode root) {
            // 如果这棵树只有孤零零的根节点，那只需要覆盖这个节点就行了。
            if (root.Children == null || root.Children.Count == 0) {
                return 1;
            }

            // 查找节点的父节点用。
            var parentDict = new Dictionary<NAryNode, NAryNode>();
            // 判断节点是否为叶节点用。
            var isLeafDict = new Dictionary<NAryNode, bool>();
            var q = new Queue<NAryNode>();

            q.Enqueue(root);

            // 首先遍历这棵树，填充 parentDict 和 isLeafDict。
            // 非递归 BFS 我就不用解释了吧。
            while (q.Count > 0) {
                var node = q.Dequeue();
                if (node.Children != null && node.Children.Count > 0) {
                    isLeafDict[node] = false;
                    foreach (var child in node.Children) {
                        parentDict[child] = node;
                        q.Enqueue(child);
                    }
                } else {
                    isLeafDict[node] = true;
                }
            }

            q.Clear();

            // 第一步，将所有的叶节点的父节点添加到待处理队列。
            foreach (var leaf in isLeafDict.Where(kv => kv.Value).Select(kv => kv.Key)) {
                if (parentDict.ContainsKey(leaf)) {
                    q.Enqueue(parentDict[leaf]);
                }
            }

            var selected = new List<NAryNode>();

            // 然后将这些节点放入预选列表，同时将它们的的爷爷节点加入待处理队列。
            while (q.Count > 0) {
                var node = q.Dequeue();
                if (!selected.Contains(node)) {
                    selected.Add(node);
                }

                if (parentDict.ContainsKey(node)) {
                    var parent = parentDict[node];
                    if (parentDict.ContainsKey(parent)) {
                        q.Enqueue(parentDict[parent]);
                    }
                }
            }

            // 这个数组的作用是防止枚举时更改集合（selected）内容导致抛出 InvalidOperationException。
            var tmpArray = selected.ToArray();
            // 简化预选列表，删除所有符合以下两条的节点：
            // 1. 其父节点在列表中；
            // 2. 其所有子节点在列表中。
            foreach (var node in tmpArray) {
                bool isParentInList;
                bool areAllChildrenInList;

                if (parentDict.ContainsKey(node)) {
                    var parent = parentDict[node];
                    isParentInList = selected.Contains(parent);
                } else {
                    // root
                    isParentInList = true;
                }

                if (node.Children != null && node.Children.Count > 0) {
                    // 你看 LINQ 是不是可以极大地简化代码呢？
                    areAllChildrenInList = node.Children.All(child => selected.Contains(child));
                } else {
                    // Not likely.
                    throw new ApplicationException();
                }

                if (isParentInList && areAllChildrenInList) {
                    selected.Remove(node);
                }
            }

            // 最后看看还剩下多少咯。
            return selected.Count;
        }

        public sealed class NAryNode {

            public NAryNode() {
            }

            public NAryNode(int key) {
                Children = new List<NAryNode>();
                Key = key;
            }

            public void AddChild(NAryNode node) {
                if (!Children.Contains(node)) {
                    Children.Add(node);
                }
            }

            // original name: child
            public List<NAryNode> Children { get; set; }

            public int Key { get; set; }

            // For debug only.
            public override string ToString() {
                if (Children == null || Children.Count == 0) {
                    return "N";
                }

                var childrenString = string.Join(",", Children.Select(n => n.ToString()));
                return string.Format("N({0})", childrenString);
            }

        }

    }
}
