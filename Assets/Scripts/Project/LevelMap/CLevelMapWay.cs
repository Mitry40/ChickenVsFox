using System.Collections.Generic;
using UnityEngine;

namespace LevelMap
{
    public class CLevelMapWay
    {
        private bool _IsWay;
        private TLevelMapData _Map;
        private List<Vector2Int> _Points;
        
        public bool IsWay { get => _IsWay; }
        private List<Vector2Int> Points
        {
            get
            {
                if (_Points == null) _Points = new List<Vector2Int>();
                return _Points;
            }
        }


        public List<Vector2Int> Create(TLevelMapData map, Vector2Int from, Vector2Int to)
        {
            _Map = map;
            _IsWay = false;
            Points.Clear();
            if (_Map.IsCorrect(from) == false || _Map.IsCorrect(to) == false) return null;
            ClearMap();
            WriteCell(to.x, to.y, 1);
            return CreateWay(from, to);
        }

        
        private void ClearMap()
        {
            int a = (int)ECellType.None;
            for (int x = 0; x < _Map.Width; x++)
            for (int y = 0; y < _Map.Height; y++)
            {
                if(_Map.Cells[x, y] != (int)ECellType.Wall) _Map.Cells[x, y] = a;
            }
        }

        private void WriteCell(int x, int y, int value)
        {
            if (_Map.IsCorrect(x, y))
            {
                int nextvalue = value + 1;
                int c = _Map.Cells[x, y];
                if (c == 0 || c > value)
                {
                    _Map.Cells[x, y] = value;
                    WriteCell(x - 1, y, nextvalue);
                    WriteCell(x + 1, y, nextvalue);
                    WriteCell(x, y - 1, nextvalue);
                    WriteCell(x, y + 1, nextvalue);
                }
            }
        }

        private List<Vector2Int> CreateWay(Vector2Int from, Vector2Int to)
        {
            int c = _Map.Cells[from.x, from.y];
            if (c == 0) return null;
            AddCell(from, c + 1, to);
            _IsWay = true;
            return Points;
        }
        private void AddCell(Vector2Int cell, int value, Vector2Int to)
        {
            Points.Add(cell);
            if (cell != to && value > 0)
            {
                int c = _Map.Cells[cell.x, cell.y];
                if (TryAddCell(cell.x - 1, cell.y, c)) AddCell(new Vector2Int(cell.x - 1, cell.y), c, to);
                else if (TryAddCell(cell.x + 1, cell.y, c)) AddCell(new Vector2Int(cell.x + 1, cell.y), c, to);
                else if (TryAddCell(cell.x, cell.y - 1, c)) AddCell(new Vector2Int(cell.x, cell.y - 1), c, to);
                else if (TryAddCell(cell.x, cell.y + 1, c)) AddCell(new Vector2Int(cell.x, cell.y + 1), c, to);
            }
        }
        private bool TryAddCell(int x, int y, int value)
        {
            if (_Map.IsCorrect(x, y))
            {
                int c = _Map.Cells[x, y];
                if (c > 0 && c < value) return true;
            }
            return false;
        }

    }
}