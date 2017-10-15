using System;
using System.Linq;

namespace JoinMs {
    public static class Problem3 {

        public static int AllVouchers(int[] billList) {
            // 明显如果我们有一个有序列表，那处理就方便了。
            // 既然有 List<T>.Sort() 方法，Int32 实现了 IComparable<Int32>，为什么不直接用呢？
            var bills = billList.ToList();
            bills.Sort();

            // 如果还能给礼券那就继续给吧。
            var total = 0;
            while (bills.Count > 1) {
                // 这个计算就是题目给定的，很简单。
                var min = bills[0];
                var max = bills[bills.Count - 1];
                var delta = max - min;

                if (delta == 0) {
                    bills.RemoveAt(0);
                } else {
                    bills.RemoveAt(0);
                    bills.RemoveAt(bills.Count - 1);
                    total += delta;

                    if (bills.Count == 0) {
                        bills.Add(delta);
                        // 此时我们可以直接退出循环。
                        break;
                    }

                    // original:
                    //bills.Add(delta);
                    //bills.Sort();

                    // 添加新的金额时要注意维护列表的有序性。
                    if (delta <= bills[0]) {
                        bills.Insert(0, delta);
                    } else if (delta >= bills[bills.Count - 1]) {
                        bills.Add(delta);
                    } else {
                        var inserted = false;
                        for (var i = 1; i < bills.Count; ++i) {
                            if (bills[i - 1] < delta && delta <= bills[i]) {
                                bills.Insert(i, delta);
                                inserted = true;
                                break;
                            }
                        }

                        if (!inserted) {
                            // Not likely.
                            throw new ApplicationException();
                        }
                    }
                }
            }

            return total;
        }

    }
}
