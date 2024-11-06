using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public bool roomToLeft;
    public bool roomToRight;
    public bool roomToTop;
    public bool roomToBottom;
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject doorUp;
    public GameObject doorDown;
    public GameObject leftWall1;
    public GameObject leftWall2;
    public GameObject rightWall1;
    public GameObject rightWall2;
    public GameObject topWall1;
    public GameObject topWall2;
    public GameObject bottomWall1;
    public GameObject bottomWall2;

    // Start is called before the first frame update
    void Start()
    {
        if(!roomToLeft)
        {
            leftWall1.transform.localScale = new Vector3(1, 15, 0);
            Vector3 newPosition = leftWall1.transform.localPosition;
            newPosition.y = 0;
            leftWall1.transform.localPosition = newPosition;
        }
        if (!roomToRight)
        {
            rightWall1.transform.localScale = new Vector3(1, 15, 0);
            Vector3 newPosition = rightWall1.transform.localPosition;
            newPosition.y = 0;
            rightWall1.transform.localPosition = newPosition;
        }
        if (!roomToTop)
        {
            topWall1.transform.localScale = new Vector3(1, 17, 0);
            Vector3 newPosition = topWall1.transform.localPosition;
            newPosition.x = 0;
            topWall1.transform.localPosition = newPosition;
        }
        if (!roomToBottom)
        {
            bottomWall1.transform.localScale = new Vector3(1, 17, 0);
            Vector3 newPosition = bottomWall1.transform.localPosition;
            newPosition.x = 0;
            bottomWall1.transform.localPosition = newPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
