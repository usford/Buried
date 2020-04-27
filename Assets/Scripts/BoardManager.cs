using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public class Map
    {
        public int columns;
        public int rows;
        public List<Tales> tales;
        public List<Vector3> gridPositions = new List<Vector3>();
        public bool passed = false;

        public Map(int _x, int _y, List<Tales> _tales, List<Vector3> _gridPositions)
        {
            columns = _x;
            rows = _y;
            tales = _tales;
            gridPositions = _gridPositions;
        }
    }


    public class Tales
    {
        public int x;
        public int y;
        public GameObject tale;

        public Tales(int _x, int _y, GameObject _tale)
        {
            x = _x;
            y = _y;
            tale = _tale;
        }
    }

    public int maxColumns = 13;
    public int maxRows = 13;
    public int minColumns = 6;
    public int minRows = 6;

    public Count wallCount = new Count(5, 9);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject exit;
    public GameObject player;
    public Map[,] map = new Map[100,100]; //Все комнаты на уровне
    private Camera mainCamera;

    private Transform boardHolder;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    //Позиция каждого элемента внутри комнаты (без внешних стен)
    List<Vector3> InitialiseList(int columns, int rows)
    {
        List<Vector3> gridPositions = new List<Vector3>();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }

        return gridPositions;
    }

    //Отрисовка первой комнаты
    void BoardSetup(int columns, int rows)
    {
        int centreColumns = (int)Mathf.Floor(columns / 2);
        int centreRows = (int)Mathf.Floor(rows / 2);

        List<Tales> tales = new List<Tales>();

        for (int x = - 1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                //Создание внешних стен
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                //Создание выходов
                //if (x == -1 && y == centreRows || x == centreColumns && y == -1 || x == centreColumns && y == rows || x == columns && y == centreRows)
                //{
                //    GameObject cloneExit = Instantiate(exit, new Vector3(1000, 1000,-10f), Quaternion.identity);
                //    if (x == - 1 && y == centreRows)
                //    {
                //        cloneExit.GetComponent<Exit>().roomX = 4;
                //        cloneExit.GetComponent<Exit>().roomY = 5;
                //    }

                //    if (x == centreColumns && y == -1)
                //    {
                //        cloneExit.GetComponent<Exit>().roomX = 5;
                //        cloneExit.GetComponent<Exit>().roomY = 4;
                //    }

                //    if (x == centreColumns && y == rows)
                //    {
                //        cloneExit.GetComponent<Exit>().roomX = 5;
                //        cloneExit.GetComponent<Exit>().roomY = 6;
                //    }

                //    toInstantiate = cloneExit;
                //}

                Tales taleObject = new Tales(x, y, toInstantiate);

                tales.Add(taleObject);


                //Местоположение персонажа
                if (x == centreColumns & y == centreRows)
                {
                    toInstantiate = player;

                    taleObject = new Tales(centreColumns, centreRows, toInstantiate);

                    tales.Add(taleObject);
                }
            }
        }

        Map mapObject = new Map(columns, rows, tales, InitialiseList(columns, rows));
        map[6,6] = mapObject;
    }

    //Отрисовка магазина
    void BoardShop(int columns, int rows)
    {
        int centreColumns = (int)Mathf.Floor(columns / 2);
        int centreRows = (int)Mathf.Floor(rows / 2);
        List<Tales> tales = new List<Tales>();

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                //Создание внешних стен
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                //Создание выхода
                //if (x == columns && y == centreRows)
                //{
                //    GameObject cloneExit = Instantiate(exit, new Vector3(1000, 1000, -10f), Quaternion.identity);
                //    cloneExit.GetComponent<Exit>().roomX = 5;
                //    cloneExit.GetComponent<Exit>().roomY = 5;

                //    toInstantiate = cloneExit;
                //}

                Tales taleObject = new Tales(x, y, toInstantiate);

                tales.Add(taleObject);


                //Местоположение персонажа
                if (x == (columns - 1) & y == centreRows)
                {
                    toInstantiate = player;

                    taleObject = new Tales(x, y, toInstantiate);

                    tales.Add(taleObject);
                }
            }
        }

        Map mapObject = new Map(columns, rows, tales, InitialiseList(columns, rows));
        map[5,6] = mapObject;
    }

    //Отрисовка комнаты испытаний
    void BoardСhallenge(int columns, int rows)
    {
        int centreColumns = (int)Mathf.Floor(columns / 2);
        int centreRows = (int)Mathf.Floor(rows / 2);
        List<Tales> tales = new List<Tales>();

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                //Создание внешних стен
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                //Создание выхода
                //if (x == centreColumns && y == rows)
                //{
                //    GameObject cloneExit = Instantiate(exit, new Vector3(1000, 1000, -10f), Quaternion.identity);
                //    cloneExit.GetComponent<Exit>().roomX = 5;
                //    cloneExit.GetComponent<Exit>().roomY = 5;

                //    toInstantiate = cloneExit;
                //}

                Tales taleObject = new Tales(x, y, toInstantiate);

                tales.Add(taleObject);


                //Местоположение персонажа
                if (x == centreColumns & y == (rows - 1))
                {
                    toInstantiate = player;

                    taleObject = new Tales(x, y, toInstantiate);

                    tales.Add(taleObject);
                }
            }
        }

        Map mapObject = new Map(columns, rows, tales, InitialiseList(columns, rows));
        map[6, 5] = mapObject;
    }

    //Отрисовка простой комнаты
    void BoardRoom(int columns, int rows, int posX, int posY)
    {
        int centreColumns = (int)Mathf.Floor(columns / 2);
        int centreRows = (int)Mathf.Floor(rows / 2);
        List<Tales> tales = new List<Tales>();

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                //Создание внешних стен
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                //Создание выхода
                //if (x == centreColumns && y == rows)
                //{
                //    GameObject cloneExit = Instantiate(exit, new Vector3(1000, 1000, -10f), Quaternion.identity);
                //    //cloneExit.GetComponent<Exit>().roomId = 0;

                //    toInstantiate = cloneExit;
                //}

                Tales taleObject = new Tales(x, y, toInstantiate);

                tales.Add(taleObject);


                //Местоположение персонажа
                if (x == centreColumns & y == centreRows)
                {
                    toInstantiate = player;

                    taleObject = new Tales(centreColumns, centreRows, toInstantiate);

                    tales.Add(taleObject);
                }
            }
        }

        Map mapObject = new Map(columns, rows, tales, InitialiseList(columns, rows));
        //map.Add(mapObject);
        //Debug.Log("Комната создана по координатам: " + posX + "-" + posY);
        map[posX, posY] = mapObject;
    }

    //Построение комнат
    void BoardBuilding(int countRooms)
    {
        int x = 6;
        int y = 6;
        int randomColumns;
        int randomRows;
        List<Vector2Int> vacantPlaces = new List<Vector2Int>();

        for (int i = 0; i < countRooms; i++)
        {
            randomColumns = Random.Range(minColumns, maxColumns);
            if (randomColumns % 2 == 0) randomColumns++;

            randomRows = Random.Range(minRows, maxRows);
            if (randomRows % 2 == 0) randomRows++;

            vacantPlaces.Clear();

            //Debug.Log("x: " + x + "     y: " + y);

            if (x < 100 && map[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
            if (x > 0 && map[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
            if (y < 100 && map[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            if (y > 0 && map[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));

            Vector2Int position = vacantPlaces[Random.Range(0, vacantPlaces.Count)];

            //Debug.Log("x: " + position.x + "     y: " + position.y);

            BoardRoom(randomColumns, randomRows, position.x, position.y);

            x = position.x;
            y = position.y;
           
        }
    }

    //Построение выходов
    private void BuildExit()
    {
        int posX = 0;
        int posY = 0;
        int roomX = 0;
        int roomY = 0;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == null) continue;

                int centreColumns = (int)Mathf.Floor(map[x,y].columns / 2);
                int centreRows = (int)Mathf.Floor(map[x, y].rows / 2);

                //Из магазина дверь ведёт только в стартовую комнату
                //if (x == 4 && y == 5)
                //{
                //    roomX = x + 1;
                //    roomY = y;

                //    posX = map[x, y].columns;
                //    posY = centreRows;

                //    CreateExit(x, y, roomX, roomY, posX, posY);
                //    continue;
                //}

                //Из комнаты испытаний дверь ведёт только в стартовую комнату
                //if (x == 5 && y == 4)
                //{
                //    roomX = x;
                //    roomY = y + 1;

                //    posX = centreColumns;
                //    posY = map[x, y].rows;
                //    CreateExit(x, y, roomX, roomY, posX, posY);
                //    continue;
                //}

                if (x <100 && map[x + 1, y] != null)
                { 
                    roomX = x + 1;
                    roomY = y;

                    posX = map[x, y].columns;
                    posY = centreRows;

                    CreateExit(x, y, roomX, roomY, posX, posY);
                }

                if (x > 0 && map[x - 1, y] != null)
                {
                    roomX = x - 1;
                    roomY = y;

                    posX = -1;
                    posY = centreRows;
                    CreateExit(x, y, roomX, roomY, posX, posY);
                }

                if (y < 100 && map[x, y + 1] != null)
                {
                    roomX = x;
                    roomY = y + 1;

                    posX = centreColumns;
                    posY = map[x, y].rows;
                    CreateExit(x, y, roomX, roomY, posX, posY);
                }

                if (y > 0 && map[x, y - 1] != null)
                {
                    roomX = x;
                    roomY = y - 1;

                    posX = centreColumns;
                    posY = -1;
                    CreateExit(x, y, roomX, roomY, posX, posY);
                }
            }
        }
    }

    //Создание выхода
    private void CreateExit(int x, int y, int roomX, int roomY, int posX, int posY)
    {
        GameObject cloneExit = Instantiate(exit, new Vector3(1000, 1000, -10f), Quaternion.identity);
        cloneExit.GetComponent<Exit>().roomX = roomX;
        cloneExit.GetComponent<Exit>().roomY = roomY;

        GameObject toInstantiate = cloneExit;

        Tales taleObject = new Tales(posX, posY, toInstantiate);

        map[x, y].tales.Add(taleObject);
    }

    //Отрисовка текущей комнаты
    public void MapRendering(int x, int y)
    {
        if (boardHolder != null)
        {
            Destroy(GameObject.Find("Board"));
        }
        boardHolder = new GameObject("Board").transform;

        map[x,y].tales.ForEach((t) =>
        {
            GameObject instance = Instantiate(t.tale, new Vector3(t.x, t.y, 0f), Quaternion.identity) as GameObject;

            instance.transform.SetParent(boardHolder);
        });

        int centreColumns = (int)Mathf.Floor(map[x, y].columns / 2);
        int centreRows = (int)Mathf.Floor(map[x,y].rows / 2);

        Vector3 vect3 = new Vector3(centreColumns, centreRows, -10);

        mainCamera.transform.position = vect3; //Центровка камеры
    }


    //Vector3 RandomPosition()
    //{
    //    int randomIndex = Random.Range(0, gridPositions.Count);
    //    Vector3 randomPosition = gridPositions[randomIndex];
    //    gridPositions.RemoveAt(randomIndex);
    //    return randomPosition;
    //}

    //void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    //{
    //    int objectCount = Random.Range(minimum, maximum + 1);

    //    for (int i = 0; i < objectCount; i++)
    //    {
    //        Vector3 randomPosition = RandomPosition();
    //        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
    //        Instantiate(tileChoice, randomPosition, Quaternion.identity);
    //    }
    //}

    //Загрузка сцены
    public void SetupScene (int level)
    {
        map = new Map[100, 100];
        BoardSetup(9, 9);
        BoardShop(5, 5);
        BoardСhallenge(7, 7);
        BoardBuilding(7);
        BuildExit();
        MapRendering(6, 6);

        int count = 0;

        foreach (Map m in map)
        {
            if (m == null) continue;
            count++;
        }

        //Debug.Log("Кол-во комнат: " + count);
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
    }
}
