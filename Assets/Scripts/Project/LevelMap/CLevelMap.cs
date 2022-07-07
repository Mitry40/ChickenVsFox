using System.Collections.Generic;
using UnityEngine;

namespace LevelMap
{
    public enum ECellType
    {
        None = 0,
        Wall = -1,
        Start = -10,
        Exit = -11,
        Enemy = -12
    }

    public struct TLevelMapData
    {
        public int Width;
        public int Height;
        public int[,] Cells;

        public TLevelMapData(uint width, uint height)
        {
            Width = (int)width;
            Height = (int)height;
            Cells = new int[Width, Height];
        }

        public bool IsCorrect(Vector2Int cell) => IsCorrect(cell.x, cell.y);
        public bool IsCorrect(int x, int y)
        {
            if (x < 0 || x >= Width) return false;
            if (y < 0 || y >= Height) return false;
            return true;
        }
    }

    public class CLevelMap
    {
        private TLevelMapData Data;
        private CLevelMapWay _Way;
        private Vector2Int CellFrom;
        private Vector2Int CellTo;


        public int Width { get => Data.Width; }
        public int Height { get => Data.Height; }
        private CLevelMapWay Way
        {
            get
            {
                if(_Way == null) _Way = new CLevelMapWay();
                return _Way;
            }
        }


        public CLevelMap(uint width, uint height, Vector2Int from, Vector2Int to)
        {
            Data = new TLevelMapData(width, height);
            CellFrom = from;
            CellTo = to;
        }

        public ECellType Cell(int x, int y)
        {
            if (Data.IsCorrect(x, y)) return (ECellType)Data.Cells[x, y];
            return ECellType.None;
        }

        public void Create()
        {
            CreateWalls();
        }

        private void Clear()
        {
            int a = (int)ECellType.None;
            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                Data.Cells[x, y] = a;
            }
        }

        private void CreateWalls()
        {
            int count;
            int a = (int)ECellType.Wall;
            Clear();
            for (int x = 1; x < Width - 1; x++)
            {
                count = Random.Range(1, 6);
                for (int i = 0; i < count; i++)
                {
                    Data.Cells[x, Random.Range(1, Height - 1)] = a;
                }
            }
        }

        public List<Vector2Int> CreateWay(Vector2Int from, Vector2Int to)
        {
            CLevelMapWay way = new CLevelMapWay();
            return way.Create(Data, from, to);
        }

        public Vector2Int GetRandomFreeCell()
        {
            Vector2Int v;
            do
            {
                v = GetRandomCell();
                Way.Create(Data, v, CellTo);
            } while (Way.IsWay == false);
            return v;
        }

        private Vector2Int GetRandomCell()
        {
            int x, y;
            do {
                x = Random.Range(0, Width);
                y = Random.Range(0, Width);
            } while (Data.Cells[x, y] == (int)ECellType.Wall);
            return new Vector2Int(x, y);
        }

    }
}