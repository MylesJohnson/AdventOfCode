namespace AdventOfCode
{
    public class Day2025_08 : PuzzleDay
    {
        private readonly string[] _input;

        public Day2025_08()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"{Solve_1_First_Approach()}");
        public override ValueTask<string> Solve_2() => new($"{Solve_2_First_Approach()}");

        private int Solve_1_First_Approach()
        {
            DisjointSet<JunctionBox> disjointSet = new(_input.Select(JunctionBox.Parse));

            // Precompute all distances and store them in a priority queue
            var distances = new PriorityQueue<(JunctionBox BoxA, JunctionBox BoxB), long>(500000);
            for (int i = 0; i < disjointSet.Values.Count; i++)
            {
                for (int j = i + 1; j < disjointSet.Values.Count; j++)
                {
                    var dist = disjointSet.Values[i].DistanceSquared(disjointSet.Values[j]);
                    distances.Enqueue((disjointSet.Values[i], disjointSet.Values[j]), dist);
                }
            }

            // Process the 1000 shortest distances and union the corresponding junction boxes
            for (int i = 0; i < 1000; i++)
            {
                var (boxA, boxB) = distances.Dequeue();
                disjointSet.Union(boxA, boxB);
            }

            // Count the size of each network
            var networkSizes = new Dictionary<JunctionBox, int>();
            foreach (JunctionBox box in disjointSet.Values)
            {
                JunctionBox root = disjointSet.Find(box).Data;
                if (!networkSizes.ContainsKey(root))
                    networkSizes[root] = 0;
                networkSizes[root]++;
            }

            // Get the sizes of the three largest networks and return their product
            return networkSizes.Values.OrderDescending().Take(3).Aggregate((a, b) => a * b);
        }

        private int Solve_2_First_Approach()
        {
            DisjointSet<JunctionBox> disjointSet = new(_input.Select(JunctionBox.Parse));

            // Precompute all distances and store them in a priority queue
            var distances = new PriorityQueue<(JunctionBox BoxA, JunctionBox BoxB), long>(500000);
            for (int i = 0; i < disjointSet.Values.Count; i++)
            {
                for (int j = i + 1; j < disjointSet.Values.Count; j++)
                {
                    var dist = disjointSet.Values[i].DistanceSquared(disjointSet.Values[j]);
                    distances.Enqueue((disjointSet.Values[i], disjointSet.Values[j]), dist);
                }
            }

            int boxesRemaining = disjointSet.Values.Count;
            while (distances.Count > 0)
            {
                var (boxA, boxB) = distances.Dequeue();
                if (disjointSet.Union(boxA, boxB))
                {
                    if (--boxesRemaining == 1)
                    {
                        // When all the boxes are connected, return the product of the X coordinates of the last merged boxes
                        return boxA.X * boxB.X;
                    }
                }
            }

            return 0;
        }

        record JunctionBox(int X, int Y, int Z)
        {
            public long DistanceSquared(JunctionBox other)
            {
                // Using squared distance to avoid floating point operations. Performance optimization.
                long dx = X - other.X;
                long dy = Y - other.Y;
                long dz = Z - other.Z;
                return dx * dx + dy * dy + dz * dz;
            }

            public static JunctionBox Parse(string input)
            {
                var parts = input.Split(',', StringSplitOptions.TrimEntries);
                return new JunctionBox(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
            }
        }

        class Node<T> where T : IEquatable<T>
        {
            public T Data { get; set; }
            public Node<T> Parent { get; set; }
            public int Rank { get; set; }

            public Node(T data)
            {
                Data = data;
                Parent = this;
                Rank = 0;
            }
        }

        class DisjointSet<T> where T : IEquatable<T>
        {
            private readonly Dictionary<T, Node<T>> _nodes;
            private readonly IList<T> _values;

            public IList<T> Values => _values;

            public DisjointSet(IEnumerable<T> values)
            {
                _values = values.ToList();
                _nodes = _values.ToDictionary(v => v, v => new Node<T>(v));
            }

            public Node<T> Find(T data)
            {
                if (!_nodes.TryGetValue(data, out Node<T>? node))
                    return null;
                if (!node.Parent.Equals(node))
                {
                    node.Parent = Find(node.Parent.Data);
                }
                return node.Parent;
            }
            public bool Union(T data1, T data2)
            {
                var node1 = Find(data1);
                var node2 = Find(data2);
                if (node1 == null || node2 == null || node1.Equals(node2))
                    return false;
                if (node1.Rank < node2.Rank)
                {
                    node1.Parent = node2;
                }
                else if (node1.Rank > node2.Rank)
                {
                    node2.Parent = node1;
                }
                else
                {
                    node2.Parent = node1;
                    node1.Rank++;
                }
                return true;
            }
        }
    }
}
