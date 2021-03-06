using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boxDiceCheck : MonoBehaviour
{
    public Text blue;
    public Text red;
    public Text yellow;
    public Text white;
    string nowplayerColor;

    private void Update()
    {
        rolldices player = GameObject.Find("Player").GetComponent<rolldices>();
        nowplayerColor = player.getPlayerColor();
    }
    

    private void OnTriggerEnter(Collider other) 
    {// 주사위 바구니와 상자 위쪽에 있는 평면이 부딛히게 되면 주사위 바구니의 주사위갯수 정보를 이용하여 표시되는 주사위 갯수 변경
        if(other.gameObject.tag == "idceposition")
        {

            diceBucket arrangePointbuctet;
            arrangePointbuctet = other.GetComponent<diceBucket>();


            GameObject manager = GameObject.Find("GameManager"); ;
            GameManager GameManager;
            GameManager = manager.GetComponent<GameManager>();



            //주사위 갯수 처리하는 부분
            int[] diceCount = { arrangePointbuctet.blue, arrangePointbuctet.red, arrangePointbuctet.yellow, arrangePointbuctet.white };
            int number;
            int temp;

            //일단 야매로 해둠 지금은빨간색 관련으로만 동작함
            temp = diceCount[0];
            if (temp > 0)
            {
                for (int i = 0; i < temp; i++)
                {
                    GameManager.bluemin();
                }
            }
            number = int.Parse(blue.text);
            number += temp;
            blue.text = number.ToString();

            temp = diceCount[1];
            if (temp > 0)
            {
                for (int i = 0; i < temp; i++)
                {
                    GameManager.redmin();
                }
            }
            number = int.Parse(red.text);
            number += temp;
            red.text = number.ToString();

            temp = diceCount[2];
            if (temp > 0)
            {
                for (int i = 0; i < temp; i++)
                {
                    GameManager.yellowmin();
                }
            }
            number = int.Parse(yellow.text);
            number += temp;
            yellow.text = number.ToString();

            temp = diceCount[3];
            if (temp > 0)
            {
                if (nowplayerColor == "red") {
                    for (int i = 0; i < temp; i++)
                    {
                        GameManager.redswhitemin();
                    }
                }

                else if (nowplayerColor == "blue") {
                    for (int i = 0; i < temp; i++)
                    {
                        GameManager.blueswhitemin();
                    }
                }

                else if (nowplayerColor == "yellow") {
                    for (int i = 0; i < temp; i++)
                    {
                        GameManager.yellowswhitemin();
                    }
                }

                    
            }
            number = int.Parse(white.text);
            number += temp;
            white.text = number.ToString();

            Dice.Clear();
            
        }


    }
}
