using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public bool[,] gameBoard;
    public uint boardSize = 13;
    public uint maxRoomCount = 40;
    public int roomScale = 3;
    public GameObject roomPrefab;

    private int roomFootprint = 16;

    // Start is called before the first frame update
    void Start()
    {
        gameBoard = new bool[boardSize,boardSize];
        uint startX = boardSize / 2;
        uint startY = boardSize / 2;

        roomFootprint = 16 * roomScale;

        // Generate random bools
        generateMap(startX, startY, 0);

        // Create room prefabs at each area
        for(int i = 0; i < boardSize; i++)
        {
            for(int j = 0; j < boardSize; j++)
            {
                if(!gameBoard[i,j]) { continue; }
                
                GameObject newRoom = Instantiate(roomPrefab);
                RoomScript newRoomScript = newRoom.GetComponent<RoomScript>();
                int xPos = (i - (int)startX) * roomFootprint;
                int yPos = (j - (int)startY) * roomFootprint * -1;
                newRoom.transform.position = new Vector3(xPos, yPos, 0);
                
                if (i > 0 && gameBoard[i - 1,j]) { newRoomScript.roomToLeft = true; }
                if (i < boardSize - 1 && gameBoard[i + 1, j]) { newRoomScript.roomToRight = true; }
                if (j > 0 && gameBoard[i, j - 1]) { newRoomScript.roomToTop = true; }
                if (j < boardSize - 1 && gameBoard[i, j + 1]) { newRoomScript.roomToBottom = true; }

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
        gameBoard[x, y] = true;
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
            if (roomNumber == 0 && x > 0 && !gameBoard[x - 1, y])
            {
                chosenRoom = 0;
                break;
            }
            else if (roomNumber == 0)
            {
                roomNumber++;
            }
            if (roomNumber == 1 && x < boardSize - 1 && !gameBoard[x + 1, y])
            {
                chosenRoom = 1;
                break;
            }
            else if (roomNumber == 1)
            {
                roomNumber++;
            }
            if (roomNumber == 2 && y > 0 && !gameBoard[x, y - 1])
            {
                chosenRoom = 2;
                break;
            }
            else if (roomNumber == 2)
            {
                roomNumber++;
            }
            if (roomNumber == 3 && y > 0 && !gameBoard[x, y - 1])
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
            // Return to beginning to branch out from start
            if (!gameBoard[boardSize / 2 - 1, boardSize / 2]) { 
                generateMap(boardSize / 2 - 1, boardSize / 2, currentRoomCount - 1); 
            } else if (!gameBoard[boardSize / 2 + 1, boardSize / 2])
            {
                generateMap(boardSize / 2 + 1, boardSize / 2, currentRoomCount - 1);
            }
            else if (!gameBoard[boardSize / 2, boardSize / 2 - 1])
            {
                generateMap(boardSize / 2, boardSize / 2 - 1, currentRoomCount - 1);
            }
            else if (!gameBoard[boardSize / 2, boardSize / 2 + 1])
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

    #endregion Private functions
}
