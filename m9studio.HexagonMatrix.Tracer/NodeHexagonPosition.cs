using m9studio.HexagonMatrix;

namespace m9studio.HexagonMatrix.Tracer
{
    public class NodeHexagonPosition
    {
        public HexagonPosition Position;
        public NodeHexagonPosition Next;
        public NodeHexagonPosition(HexagonPosition position)
        {
            Position = position;
        }
        public NodeHexagonPosition(HexagonPosition position, NodeHexagonPosition next) : this(position)
        {
            Next = next;
        }
    }
}
