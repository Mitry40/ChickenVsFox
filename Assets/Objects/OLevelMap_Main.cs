using System.Collections.Generic;
using UnityEngine;
using Uliger;
using LevelMap;

public class OLevelMap_Main : MonoBehaviour
{
    [SerializeField] private readonly float Z = 0;
    private CLevelMap MapData;
    private Vector2Int CellFrom;
    private Vector2Int CellTo;
    private OPlayer_Main _Player;



    private void Awake()
    {
        CRandom.Randomize();
        
        CellFrom = new Vector2Int(0, 0);
        CellTo = new Vector2Int(9, 9);
        MapData = new CLevelMap(10, 10, CellFrom, CellTo);
        MapData.Create();

        CreateMap();
    }


    private void CreateMap()
    {
        CreateWalls();
        CreateBound();
        CreateCell(CellFrom.x, CellFrom.y, ECellType.Start);
        CreateCell(CellTo.x, CellTo.y, ECellType.Exit);
        CreateEnemy();
        CreateEnemy();
    }


    private void CreateWalls()
    {
        for (int x = 0; x < MapData.Width; x++)
        for (int y = 0; y < MapData.Height; y++)
        {
            if (MapData.Cell(x, y) == ECellType.Wall) CreateCell(x, y, ECellType.Wall);
        }
    }

    private void CreateBound()
    {
        ECellType cell = ECellType.Wall;
        float noizevalue = 0.4f; 

        for (int x = 0; x < MapData.Width; x++)
        {
            CreateCell(x, -1, cell);
            CreateCell(x, MapData.Height, cell);
            if (Random.value < noizevalue) CreateCell(x, -2, cell);
            if (Random.value < noizevalue) CreateCell(x, MapData.Height + 1, cell);
        }
        for (int y = -1; y <= MapData.Height; y++)
        {
            CreateCell(-1, y, cell);
            CreateCell(MapData.Width, y, cell);
            if (Random.value < noizevalue) CreateCell(-2, y, cell);
            if (Random.value < noizevalue) CreateCell(MapData.Width + 1, y, cell);
        }
    }

    private void CreateEnemy()
    {
        Vector2Int v;
        do
        {
            v = MapData.GetRandomFreeCell();
        } while (v.x < 4 && v.y < 4);
        CreateCell(v.x, v.y, ECellType.Enemy);
    }


    private void CreateCell(int x, int y, ECellType cell)
    {
        Vector3 position = new Vector3(x, y, Z);
        switch (cell)
        {
            case ECellType.Wall: CFactory.CreateWall(position, transform); break;
            case ECellType.Start: _Player = CFactory.CreatePlayer(position); break;
            case ECellType.Enemy:
                OEnemy_Main enemy = CFactory.CreateEnemy(position, CreateRandomWay(new Vector2Int(x, y)));
                enemy.DTransformWay += CreateTransformWay;
                _Player.DOnThreatMax += enemy.OnPlayerThreatMax;
                break;
            case ECellType.Exit: CFactory.CreateExitCell(position); break;
        }
    }


    private List<Vector2Int> CreateRandomWay(Vector2Int from)
    {
        Vector2Int v;
        List<Vector2Int> points = null;

        for(int i=0; i<10; i++)
        {
            v = MapData.GetRandomFreeCell();
            points = MapData.CreateWay(from, v);
            if (points != null)
            {
                if (points.Count > 6) break;
            }
        }
        return points;
    }


    private List<Vector2Int> CreateTransformWay(Transform from, Transform target)
    {
        Vector2Int v1 = new Vector2Int((int)(from.position.x + 0.5f), (int)(from.position.y + 0.5f));
        Vector2Int v2 = new Vector2Int((int)(target.position.x + 0.5f), (int)(target.position.y + 0.5f));
        return MapData.CreateWay(v1, v2);
    }

}