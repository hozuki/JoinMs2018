using System.Linq;

namespace JoinMs {
    public static class Problem1 {

        public static int DeleteCharacters(string source, int numCharsToDelete) {
            // 一个简单的哈希表。这里已知输入全是小写英文字母，所以可以用定长数组。
            var hashTable = new int[26];
            // 构建哈希表。
            foreach (var ch in source) {
                ++hashTable[ch - 'a'];
            }

            // original:
            //var s = source.ToList();

            while (numCharsToDelete > 0) {
                var minIndex = -1;
                // 找到有最少项的字母的索引。
                for (var i = 1; i < hashTable.Length; ++i) {
                    if (hashTable[i] == 0) {
                        continue;
                    }

                    if (minIndex < 0) {
                        minIndex = i;
                    } else {
                        if (hashTable[i] < hashTable[minIndex]) {
                            minIndex = i;
                        }
                    }
                }

                // 如果实在没找到，说明要删除的字符数量大于字符串长度，此时直接返回0即可。
                if (minIndex < 0) {
                    return 0;
                }

                // 然后删除指定量的字符。注意不要删多（直接Replace()）了。
                var toDelete = numCharsToDelete > hashTable[minIndex] ? hashTable[minIndex] : numCharsToDelete;
                numCharsToDelete -= toDelete;
                hashTable[minIndex] -= toDelete;

                // original:
                //for (var i = 0; i < toDelete; ++i) {
                //    s.Remove((char)('a' + minIndex));
                //}

                // original:
                //var s2 = string.Joint("", s);
                //s = s2;
            }

            // original:
            //return s.Distinct().Count();

            // 看看那些字符是仍然存在的。
            return hashTable.Count(c => c > 0);
        }

    }
}
