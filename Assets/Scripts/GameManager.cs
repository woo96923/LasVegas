using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    #region Public Variables
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    #endregion


    #region Photon Callbacks


    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }


    #endregion


    #region Public Methods


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    #endregion

    #region Private Methods


    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            UnityEngine.Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        UnityEngine.Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    }


    #endregion

    #region Photon Callbacks


    public override void OnPlayerEnteredRoom(Player other)
    {
        UnityEngine.Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            UnityEngine.Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        UnityEngine.Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            UnityEngine.Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }


    #endregion

    public Text casino1;
    public Text casino2;
    public Text casino3;
    public Text casino4;
    public Text casino5;
    public Text casino6;

    private List<int> casinosMoney1;
    private List<int> casinosMoney2;
    private List<int> casinosMoney3;
    private List<int> casinosMoney4;
    private List<int> casinosMoney5;
    private List<int> casinosMoney6;

    private List<int>[] casinosMoneys;


    public int[] blueDice;
    public int[] redDice;
    public int[] yellowDice;
    public int[] whiteDice;
    public int[] nowDice;// 현재 플레이어의 주사위

    private int playerTurn;
    int[] playerScore = new int[4] { 0, 0, 0,0 };

    string nowplayerColor;//현재 플레이어의 색




    // Start is called before the first frame update
    void Start()
    {
         casinosMoney1 = new List<int>();
        casinosMoney2 = new List<int>();
        casinosMoney3 = new List<int>();
        casinosMoney4 = new List<int>();
        casinosMoney5 = new List<int>();
        casinosMoney6 = new List<int>();

        diceinint(1);
        initCard();
        initMoney();
        playerScore = new int[4] { 0, 0, 0, 0 };

        if (playerPrefab == null)
        {
            UnityEngine.Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            UnityEngine.Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        rolldices player = GameObject.Find("Player").GetComponent<rolldices>();
        nowplayerColor = player.getPlayerColor();
        //현재 주사위 변경
        if (nowplayerColor == "blue")
        {
            nowDice = blueDice;
        }
        else if (nowplayerColor == "red")
        {
            nowDice = redDice;
        }
        else if (nowplayerColor == "yellow")
        {
            nowDice = yellowDice;
        }
        


        if (redDice[0] == 0 && redDice[1] == 0 && blueDice[0] ==0 && blueDice[1] ==0 && yellowDice[0]==0&&yellowDice[1]==0)//게임이 끝났을 때(모든 주사위가 다 사용되었을 때)
        {
            
            for (int i =0; i < 6; i++)
            {
                GameObject casinos = GameObject.Find("casinos").gameObject;
                GameObject current = casinos.transform.Find("casino" + (1 + i)).gameObject;
                GameObject a = current.transform.Find("woodbox").gameObject;
                GameObject b = a.transform.Find("Canvas").gameObject;
                GameObject c = b.transform.Find("Panel").gameObject;

                GameObject d1 = c.transform.Find("red").gameObject;
                GameObject red = d1.transform.Find("diceCount").gameObject;

                GameObject d2 = c.transform.Find("blue").gameObject;
                GameObject blue = d2.transform.Find("diceCount").gameObject;

                GameObject d3 = c.transform.Find("yellow").gameObject;
                GameObject yellow = d3.transform.Find("diceCount").gameObject;

                GameObject d4 = c.transform.Find("white").gameObject;
                GameObject white = d4.transform.Find("diceCount").gameObject;


                //GameObject current = casinos.transform.Find("reddiceCount").gameObject;
                //각 카지노 우승자 가려내고 위에 점수판에 숫자 올림
                int redCount = int.Parse(red.GetComponent<Text>().text);
                int blueCount = int.Parse(blue.GetComponent<Text>().text);
                int yellowCount = int.Parse(yellow.GetComponent<Text>().text);
                int whiteCount = int.Parse(white.GetComponent<Text>().text);
                //각 텍스트 값을 가져와서 정수로 변환

                int[] counts = new int[4] { redCount, blueCount, yellowCount, whiteCount };
                               
                int max = -1;
                int maxcolor;

                if (counts[0] == counts[1]) { counts[0] = -1; counts[1] = -1; }
                if (counts[0] == counts[2]) { counts[0] = -1; counts[2] = -1; }
                if (counts[0] == counts[3]) { counts[0] = -1; counts[3] = -1; }
                if (counts[1] == counts[2]) { counts[1] = -1; counts[2] = -1; }
                if (counts[1] == counts[3]) { counts[1] = -1; counts[3] = -1; }
                if (counts[2] == counts[3]) { counts[2] = -1; counts[3] = -1; }//중복 제거

                while (true)
                {
                    max = -1;
                    maxcolor = 0;
                    for (int j = 0; j < 4; j++) 
                    {

                        if (counts[j] > max) { max = counts[j]; maxcolor = j; }
                    }
                    if (max == -1) break;
                    //if (maxcolor == 3) break;//하얀색이 1등일 때 넘기기, 나중에 4인기능 추가할 때 지워야함
                    counts[maxcolor] = -1;


                    max = 0;
                    foreach (int ele in casinosMoneys[i])//카지노중 최댓값 찾는거
                    {
                        if (ele > max) max = ele;
                    }
                        
                     playerScore[maxcolor] += max;
                     casinosMoneys[i].Remove(max);
                        
                    


                }

                
               
                
            }


        }
    }

    public void diceinint(int playerCount)
    {
        if(playerCount < 4)
        {

            blueDice = new int[2] { 2, 0 };
            redDice = new int[2] { 1, 0 }; 
            yellowDice = new int[2] { 0, 0 };//일단은 2인용이니까 이거 0으로 해둠
        }
        else
        {
            blueDice = new int[2] { 8, 0 };
            redDice = new int[2] { 8, 0 };
            yellowDice = new int[2] { 8, 0 }; 
            whiteDice = new int[2] { 8, 0 }; 
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 100), "");

        GUI.Label(new Rect(20, 12, 100, 22), " PlayerScore ");
        GUI.Label(new Rect(20, 32, 100, 22), "   Red : " + playerScore[0]);
        GUI.Label(new Rect(20, 52, 100, 22), "  Blue : " + playerScore[1]);
        GUI.Label(new Rect(20, 72, 100, 22), "Yellow : "+ playerScore[2]);

        GUI.Box(new Rect((Screen.width - 520) / 2, Screen.height - 40, 520, 25), "");

        GUI.Label(new Rect(((Screen.width - 520) / 2) + 10, Screen.height - 38, 520, 22), "Player's Dices - " + nowplayerColor + " : " + nowDice[0] + " White : " + nowDice[1]);

        if (nowDice[0] == 0 && nowDice[1] == 0)
        {
            GUI.Box(new Rect(10, Screen.height - 75, Screen.width - 20, 30), "");
            GUI.Label(new Rect(20, Screen.height - 70, Screen.width, 20), "주사위 다 사용했어요!!!");
        }
        //case MODE_ROLL:
        // display rolling message on bottom
        //GUI.Box(new Rect((Screen.width - 520) / 2, Screen.height - 40, 520, 25), "");
        //GUI.Label(new Rect(((Screen.width - 520) / 2) + 10, Screen.height - 38, 520, 22), "Click with the left (all die types) or right (gallery die) mouse button in the center to roll.");



    }
    //일단 빨간색으로만 작동하게 야매로 해둠
    public int[] getRedDiceCount()
    {
        return redDice;
    }
    public void redmin()
    {
        redDice[0]--;
    }
    public void redswhitemin()
    {
        redDice[1]--;
    }

    public int[] getBlueDiceCount()
    {
        return blueDice;
    }
    public void bluemin()
    {
        blueDice[0]--;
    }
    public void blueswhitemin()
    {
        redDice[1]--;
    }


    public int[] getYellowDiceCount()
    {
        return yellowDice;
    }
    public void yellowmin()
    {
        yellowDice[0]--;
    }
    public void yellowswhitemin()
    {
        redDice[1]--;
    }

    private System.Random rand = new System.Random();
    private List<int> money = new List<int>();

    public void initCard()//money에다가 카드를 섞어주는 역할
    {
        List<int> tempmoney = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            tempmoney.Add(6);
            tempmoney.Add(7);
            tempmoney.Add(8);
            tempmoney.Add(9);
        }
        for (int i = 0; i < 6; i++)
        {
            tempmoney.Add(1);
            tempmoney.Add(4);
            tempmoney.Add(5);
        }
        for (int i = 0; i < 8; i++)
        {
            tempmoney.Add(2);
            tempmoney.Add(3);
        }

        //카드 섞기
        int random1;
        int random2;

        int tmp;

        for (int index = 0; index < tempmoney.Count; ++index)
        {
            random1 = this.rand.Next(0, tempmoney.Count);
            random2 = this.rand.Next(0, tempmoney.Count);

            tmp = tempmoney[random1];
            tempmoney[random1] = tempmoney[random2];
            tempmoney[random2] = tmp;
        }
        //카드 섞기 출처: https://minhyeokism.tistory.com/16 [programmer-dominic.kim]
        this.money = tempmoney;
        //return this.money;

    }

    public List<List<int>> initMoney()//initCard에서 섞어준 카드들을 이용해서 알맞게 돈 카드를 뽑아서 각 카지노에 분배
    {
        List<List<int>> temp = new List<List<int>>();
        int sum = 0;

        Text[] casinos = new Text[6] { casino1, casino2, casino3, casino4, casino5, casino6 };
        casinosMoneys = new List<int>[6] { casinosMoney1, casinosMoney2, casinosMoney3, casinosMoney4, casinosMoney5, casinosMoney6 };
        for ( int i=0; i<6;i++)//카지노 위 돈 숫자들 제거
        {
            casinos[i].text = "";
        }

        for (int i = 0; i < 6; i++)
        {
                  

            // temp[i].Clear();
            temp.Add(new List<int>());
            sum = 0;
            while (sum < 5)
            {
                temp[i].Add(this.money[0]);

                casinos[i].text += (this.money[0].ToString()+"M ");//뽑은 순서대로 추가
                casinosMoneys[i].Add(this.money[0]);

                sum += this.money[0];
                this.money.RemoveAt(0);//pop money
            }

        }
        return temp;
    }
}


