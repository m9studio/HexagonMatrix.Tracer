using m9studio.HexagonMatrix;
using System.Collections.Generic;

namespace m9studio.HexagonMatrix.Tracer
{
    public class HexagonMatrixTracer<T> : HexagonMatrix<T> where T : ITracer
    {
        /*
        /// <summary>
        /// Метод поиска пути Алгоритмом A*
        /// </summary>
        /// <param name="Begin">Начальная позиция.</param>
        /// <param name="End">Конечная позиция.</param>
        /// <returns></returns>
        public NodeHexagonPosition AStar(HexagonPosition Begin, HexagonPosition End, int Step)
        {

            return null;
        }
        */

        /// <summary>
        /// Метод поиска пути алгоритмом Ли
        /// </summary>
        /// <param name="Begin">Начальная позиция.</param>
        /// <param name="End">Конечная позиция.</param>
        /// <returns></returns>
        public NodeHexagonPosition Lee(HexagonPosition Begin, HexagonPosition End, int Step)
        {
            //проверяем на то, существуют ли точки и проходимы ли они, исключая что LeeWave вернет null
            if (isLocated(Begin) && Get(Begin).isPasseble() &&
                isLocated(End) && Get(End).isPasseble())
            {
                HexagonMatrix<int> Map = LeeWave(Begin, Step);
                NodeHexagonPosition node = new NodeHexagonPosition(End);
                int count = Map.Get(End);
                if (count != 0)
                {
                    count--;
                    for (;count != 1; count--)
                    {
                        List<HexagonPosition> positions = GetNeighbors(node.Position);
                        foreach(HexagonPosition position in positions)
                        {
                            if(Map.Get(position) == count)
                            {
                                node = new NodeHexagonPosition(position, node);
                                break;
                            }
                        }
                    }
                    node = new NodeHexagonPosition(Begin, node);
                    if(count == 1)
                        return node;
                }
            }
            return null;
        }
        /// <summary>
        /// Волна, необходимая для алгоритма Ли.
        /// </summary>
        /// <param name="Begin">Начальная позиция</param>
        /// <returns></returns>
        public HexagonMatrix<int> LeeWave(HexagonPosition Begin, int Step)
        {
            HexagonMatrix<int> Map = new HexagonMatrix<int>(Radius, 0);
            //точки в которые будем записывать значение волны
            List<HexagonPosition> All = new List<HexagonPosition>();
            //если точка Begin есть и она проходимая
            if (isLocated(Begin) && Get(Begin).isPasseble())
            {
                All.Add(Begin);
                Map.Set(1, Begin);
                //повтор пока Step не будет 0, либо пока мы не переберем всё в All
                for (int i = 2; Step != 0 && All.Count > 0; Step--, i++)
                {
                    //новый All, в который добавляются все соседние пустые проходимые клетки
                    List<HexagonPosition> _All = new List<HexagonPosition>();
                    foreach(HexagonPosition position in All)
                    {
                        //все соседи от точки position
                        List<HexagonPosition> Neighbors = GetNeighbors(position);
                        //перебор соседий от position
                        foreach (HexagonPosition pos in Neighbors)
                        {
                            //проверка, является ли сосед проходимым
                            if (Get(pos).isPasseble())
                            {
                                //если сосед пустой, то добавляем его для дальнейшей проверки
                                if (Map.Get(pos) == 0)
                                {
                                    _All.Add(pos);
                                    Map.Set(i, pos);
                                }
                            }
                        }
                    }
                    All = _All;
                }
                return Map;
            }
            return null;
        }
        /// <summary>
        /// Получение позиций соседних точек, которые могут быть на матрице.
        /// </summary>
        /// <param name="Matrix">Матрица по которой смотрим.</param>
        /// <param name="Position">Позиция, от которых будем искать соседей.</param>
        /// <returns>Коллекция соседий.</returns>
        public List<HexagonPosition> GetNeighbors(HexagonPosition Position)
        {
            List<HexagonPosition> Neighbors = new List<HexagonPosition>();

            Position.MoveX(1);
            if(isLocated(Position)) 
                Neighbors.Add((HexagonPosition)Position.Clone());

            Position.MoveX(-2);
            if (isLocated(Position))
                Neighbors.Add((HexagonPosition)Position.Clone());
            Position.MoveX(1);

            Position.MoveY(1);
            if (isLocated(Position))
                Neighbors.Add((HexagonPosition)Position.Clone());

            Position.MoveY(-2);
            if (isLocated(Position))
                Neighbors.Add((HexagonPosition)Position.Clone());
            Position.MoveY(1);

            Position.MoveZ(1);
            if (isLocated(Position))
                Neighbors.Add((HexagonPosition)Position.Clone());

            Position.MoveZ(-2);
            if (isLocated(Position))
                Neighbors.Add((HexagonPosition)Position.Clone());
            Position.MoveZ(1);

            return Neighbors;
        }


        #region Конструктор
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="radius">Радиус создаваемой матрицы.</param>
        public HexagonMatrixTracer(int radius) : base(radius) { }
        /// <summary>
        /// Конструктор, с предварительным заполенением.
        /// </summary>
        /// <param name="radius">Радиус создаваемой матрицы.</param>
        /// <param name="obj">Объект, которой будет заполнена матрица.</param>
        public HexagonMatrixTracer(int radius, T obj) : base(radius, obj) { }
        #endregion
    }
}
