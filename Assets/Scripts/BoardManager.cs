using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelRooms
{
    public List<GameObject> rooms = new List<GameObject>();
}
public class BoardManager : MonoBehaviour
{
    public GameObject exit; //Префаб выхода

    public Sprite[] exitStates; //Состояние выходов
    public GameObject player;
    private GameObject _player;
    public GameObject[,] rooms = new GameObject[100,100]; //Все комнаты
    
    public List<LevelRooms> roomsSimple;
    public GameObject[] roomsBoss;
    public GameObject[] roomSpawn;
    public GameObject[] roomShop;
    public GameObject[] roomChallenge;
    public GameObject chest; //Сундук с сокровищами
    public GameObject currentRoom;
    private int level = 1;


    //public Map currentRoom;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    //Постройка определённой комнаты
    private void RoomBuild(GameObject room, int posX, int posY, Color color, bool target)
    {
        int centreColumns = (int)Mathf.Floor(room.GetComponent<Room>().columns / 2);
        int centreRows = (int)Mathf.Floor(room.GetComponent<Room>().rows / 2);

        GameObject newRoom = Instantiate(room, new Vector3(0, 0, 0f), Quaternion.identity);
        newRoom.SetActive(false);
        newRoom.GetComponent<Room>().color = color;
        newRoom.GetComponent<Room>().posX = posX;
        newRoom.GetComponent<Room>().posY = posY;

        //Создание цели для комнаты
        if (target)
        {
            float chance = 0.0f; //Шанс на сундук

            if (newRoom.GetComponent<Room>().spawnChest)
                chance = newRoom.GetComponent<Room>().chanceSpawnChest + 0.1f;

            float random = Random.Range(0.0f, 1.0f);

            if (random <= chance)
            {
                GameObject newChest = Instantiate(chest, new Vector3(centreColumns, centreRows, 0.0f), Quaternion.identity);
                newChest.transform.SetParent(newRoom.transform);
                newChest.AddComponent<Target>();
                newChest.GetComponent<Target>().type = "chest";
                newChest.GetComponent<Target>().posX = posX;
                newChest.GetComponent<Target>().posY = posY;
            }else
            {
                bool isEnemy = false;
                foreach (Transform child in newRoom.GetComponentsInChildren<Transform>())
                {
                    if(child.tag == "Enemy")
                    {
                        isEnemy = true;
                    }
                    
                }

                if (!isEnemy)
                {
                    int minEnemy = newRoom.GetComponent<Room>().minEnemy;
                    int maxEnemy = newRoom.GetComponent<Room>().maxEnemy + 1;

                    int count = Random.Range(minEnemy, maxEnemy);
                    List<GameObject> enemies = new List<GameObject>();
                    enemies = newRoom.GetComponent<Room>().enemies;

                    List<Vector2> spawnedPositions = new List<Vector2>();
                    spawnedPositions = newRoom.GetComponent<Room>().spawnedPositions;

                    for (int i = 0; i < count; i++)
                    {
                        int randomSpawn = Random.Range(0, spawnedPositions.Count); 
                        GameObject newEnemy = Instantiate(enemies[enemies.Count - 1], spawnedPositions[randomSpawn], Quaternion.identity);
                        newEnemy.transform.SetParent(newRoom.transform);
                        spawnedPositions.Remove(spawnedPositions[randomSpawn]);
                    }
                }
                
                newRoom.AddComponent<Target>().type = "enemies";
                newRoom.GetComponent<Target>().posX = posX;
                newRoom.GetComponent<Target>().posY = posY;
            }
        }

        rooms[posX, posY] = newRoom;
    }

    //Отрисовка комнаты
    public void RoomRendering(int posX, int posY, int spawnX, int spawnY)
    {   
        if (currentRoom != null) currentRoom.SetActive(false);

        

        rooms[posX, posY].SetActive(true);

        

        foreach (Transform child in rooms[posX, posY].GetComponentsInChildren<Transform>())
        {
            if (child.tag == "Exit")
            {
                if (!rooms[posX,posY].GetComponent<Room>().passed)
                {
                    child.GetComponent<Animator>().SetBool("passed", false);
                }else
                {
                    child.GetComponent<Animator>().SetBool("passed", true);
                    child.GetComponent<Animator>().speed = 100;
                }
            }
        }

        Vector3 vec3 = new Vector3(spawnX, spawnY, 0f);
        _player.transform.position = vec3;

        int centreColumns = (int)Mathf.Floor(rooms[posX, posY].GetComponent<Room>().columns / 2);
        int centreRows = (int)Mathf.Floor(rooms[posX, posY].GetComponent<Room>().rows / 2);

        Vector3 vect3 = new Vector3(centreColumns, centreRows, -10);

        mainCamera.transform.position = vect3; //Центровка камеры

        currentRoom = rooms[posX, posY];

        
    }

    //Построение комнат
    private void RoomBuilding(int countRooms)
    {
        int x = 5;
        int y = 5;
        List<Vector2Int> vacantPlaces = new List<Vector2Int>();

        for (int i = 0; i < countRooms; i++)
        {
            vacantPlaces.Clear();

            //Debug.Log("x: " + x + "     y: " + y);

            if (x < 100 && rooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
            if (x > 0 && rooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
            if (y < 100 && rooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            if (y > 0 && rooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));

            Vector2Int position = vacantPlaces[Random.Range(0, vacantPlaces.Count)];

            //Debug.Log("x: " + position.x + "     y: " + position.y);
            GameObject room = roomsSimple[level - 1].rooms[Random.Range(0, roomsSimple[level - 1].rooms.Count)];

            RoomBuild(room, position.x, position.y, room.GetComponent<Room>().color, true);

            x = position.x;
            y = position.y;
           
           //Создание комнаты с боссом
           if (i + 1 == countRooms)
           {
                vacantPlaces.Clear();

                if (x < 100 && rooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (x > 0 && rooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y < 100 && rooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
                if (y > 0 && rooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));

                position = vacantPlaces[Random.Range(0, vacantPlaces.Count)];

                room = roomsBoss[level - 1];

                RoomBuild(room, position.x, position.y, room.GetComponent<Room>().color, true);

                x = position.x;
                y = position.y;
           }
        }
    }

    //Создание выходов в комнате
    private void RoomBuildExit()
    {
        float posX = 0;
        float posY = 0;
        int roomX = 0;
        int roomY = 0;
        for (int x = 0; x < rooms.GetLength(0); x++)
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                if (rooms[x, y] == null) continue;

                int centreColumns = (int)Mathf.Floor(rooms[x,y].GetComponent<Room>().columns / 2);
                int centreRows = (int)Mathf.Floor(rooms[x, y].GetComponent<Room>().rows / 2);

                //Правый выход
                if (x < 100 && rooms[x + 1, y] != null)
                { 
                    roomX = x + 1;
                    roomY = y;

                    posX = rooms[x, y].GetComponent<Room>().columns - 1;
                    posY = centreRows;

                    RoomCreateExit(x, y, roomX, roomY, posX, posY, -90.0f, "DoorR");
                }

                //Левый выход
                if (x > 0 && rooms[x - 1, y] != null)
                {
                    roomX = x - 1;
                    roomY = y;

                    posX = 0;
                    posY = centreRows;
                    RoomCreateExit(x, y, roomX, roomY, posX, posY, 90.0f, "DoorL");
                }

                //Верхний выход
                if (y < 100 && rooms[x, y + 1] != null)
                {
                    roomX = x;
                    roomY = y + 1;

                    posX = centreColumns;
                    posY = rooms[x, y].GetComponent<Room>().rows - 1;
                    RoomCreateExit(x, y, roomX, roomY, posX, posY, 0.0f, "DoorU");
                }

                //Нижний выход
                if (y > 0 && rooms[x, y - 1] != null)
                {
                    roomX = x;
                    roomY = y - 1;

                    posX = centreColumns;
                    posY = -1;
                    RoomCreateExit(x, y, roomX, roomY, posX, posY, 180.0f, "DoorD");
                }
            }
        }
    }

    //Создание выхода
    private void RoomCreateExit(int x, int y, int roomX, int roomY, float posX, float posY, float rotationZ, string nameDoor)
    {
        GameObject cloneExit = Instantiate(exit, new Vector3(posX, posY, 0f), Quaternion.identity);
        cloneExit.GetComponent<Exit>().roomX = roomX;
        cloneExit.GetComponent<Exit>().roomY = roomY;

        // if (nameDoor == "DoorU")
        // {
        //     cloneExit.GetComponent<BoxCollider2D>().offset = new Vector2(cloneExit.GetComponent<BoxCollider2D>().offset.x, 0.35f);
        //     cloneExit.GetComponent<BoxCollider2D>().size = new Vector2(cloneExit.GetComponent<BoxCollider2D>().size.x, 0.29f);
        // }

        if (!rooms[x, y].GetComponent<Room>().passed)
        {
             cloneExit.GetComponent<SpriteRenderer>().sprite = exitStates[0];
             cloneExit.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        

        //GameObject toInstantiate = cloneExit;

        // Tales taleObject = new Tales(posX, posY, cloneExit);

        // map[x, y].tales.Add(taleObject);

        cloneExit.transform.Rotate(new Vector3(0,0,rotationZ));
        
        cloneExit.transform.parent = rooms[x, y].GetComponent<Transform>();
        
        //Уничтожение стены в том месте, где появится дверь
        foreach (Transform child in rooms[x, y].GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.name == nameDoor)
            {
                Destroy(child.gameObject);
            }
        }
    }

    //Изменение состояние выходов
    public void ChangeExit(int x, int y)
    {
        foreach (Transform child in rooms[x, y].GetComponentsInChildren<Transform>())
        {
            if (child.tag == "Exit")
            {
                if (!rooms[x,y].GetComponent<Room>().passed)
                {
                    child.GetComponent<BoxCollider2D>().isTrigger = false;    
                    child.GetComponent<Animator>().SetBool("passed", false);
                }else
                {
                    
                    child.GetComponent<BoxCollider2D>().isTrigger = true;    
                    child.GetComponent<Animator>().SetBool("passed", true); 
                }
            }
        }
    }

    //Загрузка сцены
    public void SetupScene (int _level)
    {
        level = _level;
        rooms = new GameObject[100, 100];

        if (_player != null) Destroy(_player);

        _player = Instantiate(player, new Vector3(5, 5, 0f), Quaternion.identity);
        RoomBuild(roomSpawn[_level - 1], 5, 5, Color.yellow, false);
        RoomBuild(roomShop[_level - 1], 4, 5, Color.yellow, false);
        RoomBuild(roomChallenge[_level - 1], 5, 4, Color.yellow, false);
        try
        {
            RoomBuilding(7);
        }catch (Exception)
        {
            Debug.Log("ОШИБКА ГЕНЕРАЦИИ КОМНАТ");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        RoomBuildExit();
        RoomRendering(5, 5, 4, 4);

        //Debug.Log(level);

        currentRoom = rooms[5, 5];


        int count = 0;

        foreach (GameObject r in rooms)
        {
            if (r == null) continue;
            count++;
        }

        //Debug.Log("Кол-во комнат: " + count);
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
    }
}
