using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public enum RoomTypes
    {
        None,
        Normal,
        Boss,
        Red,
        Yellow,
        Green,
        Blue,
        RedSpawn,
        YellowSpawn,
        GreenSpawn,
        BlueSpawn
    }

    public RoomTypes[,] gameBoard;
    public uint boardSize = 13;
    public uint maxRoomCount = 40;
    public float roomScale = 3;
    public GameObject[] roomPrefabs;
    public GameObject[] keys;
    public GameObject[] powerups;

    public static GameObject[] staticKeys;
    public static GameObject[] staticPowerups;

    public float roomFootprint = 16;
    private float bossRoomFootprint = 16;
    private const int minimumRoomCount = ((int)RoomTypes.BlueSpawn - (int)RoomTypes.Red) + 5;

    private void Awake()
    {
        MapScript.boardSize = boardSize;
        staticKeys = keys; 
        staticPowerups = powerups;
    }

    // Start is called before the first frame update
    private void Start()
    {
        maxRoomCount = (uint)Math.Max(maxRoomCount, minimumRoomCount);
        if (boardSize - 2 * boardSize - 2 < minimumRoomCount) { return; }
        gameBoard = new RoomTypes[boardSize, boardSize];
        uint startX = boardSize / 2;
        uint startY = startX;

        roomFootprint = 16 * roomScale;
        bossRoomFootprint = 16 * roomScale;

        // Generate random bools
        generateMap(startX, startY, 0);

        // TODO: Change the method to generate colored rooms cause it's slow
        for (int i = (int)RoomTypes.Red; i <= (int)RoomTypes.BlueSpawn; i++)
        {
            bool found = false;
            while(!found)
            {
                int randomX = UnityEngine.Random.Range(1, (int)boardSize - 1);
                int randomY = UnityEngine.Random.Range(1, (int)boardSize - 1);

                if(randomX == startX || randomY == startY || gameBoard[randomX, randomY] != RoomTypes.Normal)
                {
                    continue;
                }

                gameBoard[randomX, randomY] = (RoomTypes)i;
                found = true;
            }
        }

        // TODO: Generate boss room
        // Get left most room
        Tuple<int, int> leftMostRoom = getLeftMostRoom();
        gameBoard[leftMostRoom.Item1 - 1, leftMostRoom.Item2] = RoomTypes.Boss;

        // Create room prefabs at each area
        for(int i = 0; i < boardSize; i++)
        {
            for(int j = 0; j < boardSize; j++)
            {
                if(gameBoard[i, j] == RoomTypes.None) { continue; }

                GameObject newRoom;
                if ((int)gameBoard[i, j] >= (int)RoomTypes.Red && (int)gameBoard[i, j] <= (int)RoomTypes.Blue)
                {
                    newRoom = Instantiate(roomPrefabs[(int)gameBoard[i, j] - 1]);
                }
                else if (gameBoard[i,j] == RoomTypes.Boss)
                {
                    newRoom = Instantiate(roomPrefabs[(int)RoomTypes.Boss - 1]);
                }
                else
                {
                    newRoom = Instantiate(roomPrefabs[(int)RoomTypes.Normal - 1]);
                }
                

                RoomScript newRoomScript = newRoom.GetComponent<RoomScript>();
                float xPos = (i - (int)startX) * roomFootprint;

                if (gameBoard[i, j] == RoomTypes.Boss)
                {
                    xPos -= roomFootprint / 2 - (0.5f * roomScale);
                }

                float yPos = (j - (int)startY) * roomFootprint * -1;
                newRoom.transform.position = new Vector3(xPos, yPos, 0);
                newRoom.transform.localScale = new Vector3(roomScale, roomScale, roomScale);
                
                if (i > 0 && gameBoard[i - 1,j] != RoomTypes.None) { newRoomScript.roomToLeft = true; }
                if (i < boardSize - 1 && gameBoard[i + 1, j] != RoomTypes.None) { newRoomScript.roomToRight = true; }
                if (j > 0 && gameBoard[i, j - 1] != RoomTypes.None) { newRoomScript.roomToTop = true; }
                if (j < boardSize - 1 && gameBoard[i, j + 1] != RoomTypes.None) { newRoomScript.roomToBottom = true; }

                newRoomScript.isStartingRoom = xPos == 0 && yPos == 0;
                newRoomScript.pos = new Tuple<int,int>(i - (int)startX, j - (int)startY);
                newRoomScript.roomType = gameBoard[i, j];
            }
        }

        // Finished... hopefully 
        // TODO: Add more customization to rooms
        // Treasure rooms, boss rooms, etc.
    }

    #region Private functions

    private void generateMap(uint x, uint y, uint currentRoomCount)
    {
        // Set bool to active
        gameBoard[x, y] = RoomTypes.Normal;
        currentRoomCount++;
        if(currentRoomCount >= maxRoomCount)
        {
            return; // Finished generating
        }

        // check if rooms active
        int chosenRoom = -1;
        bool looped = false;
        int roomNumber = UnityEngine.Random.Range(0, 4);
        while (chosenRoom == -1)
        {
            if (roomNumber == 0 && x > 1 && gameBoard[x - 1, y] == RoomTypes.None)
            {
                chosenRoom = 0;
                break;
            }
            else if (roomNumber == 0)
            {
                roomNumber++;
            }
            if (roomNumber == 1 && x < boardSize - 2 && gameBoard[x + 1, y] == RoomTypes.None)
            {
                chosenRoom = 1;
                break;
            }
            else if (roomNumber == 1)
            {
                roomNumber++;
            }
            if (roomNumber == 2 && y > 1 && gameBoard[x, y - 1] == RoomTypes.None)
            {
                chosenRoom = 2;
                break;
            }
            else if (roomNumber == 2)
            {
                roomNumber++;
            }
            if (roomNumber == 3 && y < boardSize - 2 && gameBoard[x, y + 1] == RoomTypes.None)
            {
                chosenRoom = 3;
                break;
            }
            else if (roomNumber == 3)
            {
                roomNumber = 0;
            }

            if(looped)
            {
                break;
            }
            looped = true;
        }

        // Add logic if there's no chosen room
        if(chosenRoom == -1)
        {
            if (boardSize <= 1) return;
            // Return to beginning to branch out from start
            if (gameBoard[boardSize / 2 - 1, boardSize / 2] == RoomTypes.None) { 
                generateMap(boardSize / 2 - 1, boardSize / 2, currentRoomCount - 1); 
            } else if (gameBoard[boardSize / 2 + 1, boardSize / 2] == RoomTypes.None)
            {
                generateMap(boardSize / 2 + 1, boardSize / 2, currentRoomCount - 1);
            }
            else if (gameBoard[boardSize / 2, boardSize / 2 - 1] == RoomTypes.None)
            {
                generateMap(boardSize / 2, boardSize / 2 - 1, currentRoomCount - 1);
            }
            else if (gameBoard[boardSize / 2, boardSize / 2 + 1] == RoomTypes.None)
            {
                generateMap(boardSize / 2, boardSize / 2 + 1, currentRoomCount - 1);
            }

            return;
        }

        if(chosenRoom == 0)
        {
            generateMap(x - 1, y, currentRoomCount);
        } else if (chosenRoom == 1)
        {
            generateMap(x + 1, y, currentRoomCount);
        }
        else if (chosenRoom == 2)
        {
            generateMap(x, y - 1, currentRoomCount);
        }
        else
        {
            generateMap(x, y + 1, currentRoomCount);
        }
    }

    Tuple<int, int> getLeftMostRoom()
    {
        Tuple<int, int> position = Tuple.Create(1000,1000);
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (gameBoard[i, j] != RoomTypes.None && i < position.Item1)
                {
                    position = Tuple.Create(i, j);
                }
            }
        }
        return position;
    }

    #endregion Private functions
}
