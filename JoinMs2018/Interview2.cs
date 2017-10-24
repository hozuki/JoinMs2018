using System;
using System.Linq;

namespace JoinMs {
    public static class Interview2 {

        // 形如 "who am i" -> "i am who" 的字符串倒序。C# 简明解法。
        public static string RevertString(string input) {
            if (string.IsNullOrEmpty(input)) {
                return input;
            }
            if (string.IsNullOrWhiteSpace(input)) {
                return string.Empty;
            }

            var words = input.Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Reverse());
        }

        // 模拟 C 的解法。由于我不想单独开工程给 Travis，所以就用 C# 来模拟一下指针访问。我也没开/unsafe，
        // 数组就能看出想表达的意思，不是吗。
        // 要求只能在原字符串空间上操作。
        // 假设输入前后无空格，而且没有多个连续空格。（如果有的话就要考虑空格数的对称性，就没法用存储空间优化解。）
        public static string RevertString2(string input) {
            if (string.IsNullOrEmpty(input)) {
                return input;
            }
            if (string.IsNullOrWhiteSpace(input)) {
                return string.Empty;
            }

            var chars = input.ToCharArray();
            RevertSubstring(chars, 0, chars.Length);

            var start = 0;
            var length = 0;
            for (var i = 0; i < chars.Length; ++i) {
                if (i == chars.Length - 1) {
                    ++length;
                    if (length > 0) {
                        RevertSubstring(chars, start, length);
                    }
                } else {
                    if (chars[i] == ' ') {
                        if (length > 0) {
                            RevertSubstring(chars, start, length);
                        }

                        start = i + 1;
                        length = 0;
                    } else {
                        ++length;
                    }
                }
            }

            return new string(chars);
        }

        // 面试官：抽象，抽象，抽象。将这部分分离成独立的函数。
        // 于是我又囧了。明明开始之前一直提醒自己要优先考虑自顶向下的代码编写（而且当初在重写 ACB 的组件的时候我不就是这么干的嘛），
        // 结果因第一面的冲击变得如履薄冰，后场都乱了。
        private static void RevertSubstring(char[] str, int start, int length) {
            var middle = length / 2;
            for (var i = 0; i < middle; ++i) {
                var tmp = str[start + i];
                str[start + i] = str[start + length - 1 - i];
                str[start + length - 1 - i] = tmp;
            }
        }

    }
}
