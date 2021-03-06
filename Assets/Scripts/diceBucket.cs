using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceBucket : MonoBehaviour//주사위 자동이동하는 오브젝트가 주사위 몇개 들고있는지에대한 정보
{
    public int blue;
    public int red;
    public int yellow;
    public int white;

    public void initBuckit()
    {
        red = 0;
        blue = 0;
        yellow = 0;
        white = 0;
    }
    public void plusBlue()
    {
        blue++;
    }
    public void plusRed()
    {

        red++;
    }
    public void plusYellow()
    {
        yellow++;
    }
    public void plusWhite()
    {

        white++;
    }
}
