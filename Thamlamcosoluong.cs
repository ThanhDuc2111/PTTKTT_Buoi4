using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thamlamcosoluong
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new List<Item>
        {
            new Item { Value = 60, Weight = 10, Quantity = 2 },
            new Item { Value = 100, Weight = 20, Quantity = 1 },
            new Item { Value = 120, Weight = 30, Quantity = 3 }
        };

            int capacity = 50;

            double maxValue = GreedyKnapsack(capacity, items);
            Console.WriteLine("Maximum value in Knapsack = " + maxValue);
            Console.ReadKey();

        }

        public class Item
        {
            public int Value { get; set; }
            public int Weight { get; set; }
            public int Quantity { get; set; }
            public double Ratio { get { return (double)Value / Weight; } }
        }

        public static double GreedyKnapsack(int capacity, List<Item> items)
        {
            // Sắp xếp các mặt hàng theo tỷ lệ giá trị/trọng lượng giảm dần
            items.Sort((x, y) => y.Ratio.CompareTo(x.Ratio));

            double totalValue = 0;
            int currentWeight = 0;

            foreach (var item in items)
            {
                if (currentWeight + item.Weight <= capacity)
                {
                    // Tính số lượng mặt hàng có thể thêm vào ba lô
                    int maxQuantity = (capacity - currentWeight) / item.Weight;
                    int quantityToAdd = Math.Min(item.Quantity, maxQuantity);

                    // Thêm số lượng mặt hàng vào ba lô
                    currentWeight += quantityToAdd * item.Weight;
                    totalValue += quantityToAdd * item.Value;
                }
                else
                {
                    // Tính phần còn lại của mặt hàng có thể thêm vào ba lô
                    int remainingWeight = capacity - currentWeight;
                    if (remainingWeight > 0)
                    {
                        double fraction = (double)remainingWeight / item.Weight;
                        totalValue += item.Value * fraction;
                        currentWeight += remainingWeight;
                    }
                    break;
                }
            }

            return totalValue;
        }
    }
}
