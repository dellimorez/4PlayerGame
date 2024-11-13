using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
{
    public GameObject MapRoom;

    private static GameObject MapRoomPrefab;
    private static GameObject self;
    public static uint boardSize;
    private static float roomScale;
    private static RectTransform rt;
    public static Dictionary<Tuple<int, int>, GameObject> roomDictionary;
    public Color normalRoomColor;
    public Color redRoomColor;
    public Color yellowRoomColor;
    public Color greenRoomColor;
    public Color blueRoomColor;

    private static Color staticNormalRoomColor;
    private static Color staticRedRoomColor;
    private static Color staticYellowRoomColor;
    private static Color staticGreenRoomColor;
    private static Color staticBlueRoomColor;

    private Tuple<int, int> previousRoom;

    private void Awake()
    {
        self = gameObject;
        staticNormalRoomColor = normalRoomColor;
        staticRedRoomColor = redRoomColor;
        staticYellowRoomColor = yellowRoomColor;
        staticGreenRoomColor = greenRoomColor;
        staticBlueRoomColor = blueRoomColor;

        staticRedRoomColor.a = 1;
        staticYellowRoomColor.a = 1;
        staticBlueRoomColor.a = 1;
        staticGreenRoomColor.a = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        roomScale = (1f / boardSize) * 0.75f;
        MapRoomPrefab = MapRoom;
        rt = GetComponent<RectTransform>();
        
        roomDictionary = new Dictionary<Tuple<int, int>, GameObject>();
        NewRoomVisited(new Tuple<int, int>(0,0), LevelGenerator.RoomTypes.Normal);
    }

    public static void NewRoomVisited(Tuple<int,int> pos, LevelGenerator.RoomTypes type)
    {
        if (roomDictionary.ContainsKey(pos)) { return; }

        GameObject mr = Instantiate(MapRoomPrefab, self.transform);
        float offset = rt.rect.x * roomScale * 2;

        mr.transform.localScale = new Vector3(roomScale, roomScale, 1);
        mr.transform.localPosition = new Vector3(-offset * pos.Item1, offset * pos.Item2);

        Image mapRoomImage = mr.transform.GetChild(0).gameObject.GetComponent<Image>();
        switch(type)
        {
            case LevelGenerator.RoomTypes.Red:
                mapRoomImage.color = staticRedRoomColor;
                break;
            case LevelGenerator.RoomTypes.Yellow:
                mapRoomImage.color = staticYellowRoomColor;
                break;
            case LevelGenerator.RoomTypes.Green:
                mapRoomImage.color = staticGreenRoomColor;
                break;
            case LevelGenerator.RoomTypes.Blue:
                mapRoomImage.color = staticBlueRoomColor;
                break;
            default:
                mapRoomImage.color = staticNormalRoomColor;
                break;
        }

        roomDictionary[pos] = mr;
    }
}
