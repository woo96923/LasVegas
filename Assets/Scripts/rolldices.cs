using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolldices : MonoBehaviour
{
    public string PlayerColor = "red";
    int ColorDice;
    int whiteDice;
    int[] dices;

    public string getPlayerColor()
    {
        return PlayerColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        rectModeSelect = new Rect(10, 10, 180, 80);
        
    }

    private GameObject spawnPoint = null;
    private Rect rectModeSelect;
    private Vector3 Force()
    {
        Vector3 rollTarget = Vector3.zero + new Vector3(2 + 7 * Random.value, .5F + 4 * Random.value, -2 - 3 * Random.value);
        return Vector3.Lerp(spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
    }

    // check if a point is within a rectangle
    private bool PointInRect(Vector2 p, Rect r)
    {
        return (p.x >= r.xMin && p.x <= r.xMax && p.y >= r.yMin && p.y <= r.yMax);
    }

    // translate Input mouseposition to GUI coordinates using camera viewport
    private Vector2 GuiMousePosition()
    {
        Vector2 mp = Input.mousePosition;
        Vector3 vp = Camera.main.ScreenToViewportPoint(new Vector3(mp.x, mp.y, 0));
        mp = new Vector2(vp.x * Camera.main.pixelWidth, (1 - vp.y) * Camera.main.pixelHeight);
        return mp;
    }

    string randomColor
    {
        get
        {
            string _color = "blue";
            int c = System.Convert.ToInt32(Random.value * 6);
            switch (c)
            {
                case 0: _color = "red"; break;
                case 1: _color = "green"; break;
                case 2: _color = "blue"; break;
                case 3: _color = "yellow"; break;
                case 4: _color = "white"; break;
                
            }
            return _color;
        }
    }

    // Update is called once per frame
    //private void OnCollisionEnter(Collision collision)
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Bluecube")
        {
            PlayerColor = "blue";//블루큐브랑 충돌시 플레이어색 변경
        }
        else if (collision.gameObject.name == "Redcube")
        {
            PlayerColor = "red";//블루큐브랑 충돌시 플레이어색 변경
        }
        else if (collision.gameObject.name == "Yellowcube")
        {
            PlayerColor = "yellow";//블루큐브랑 충돌시 플레이어색 변경
        }
        
    }
    void Update()
    {
        spawnPoint = GameObject.Find("spawnPoint");
        // check if we have to roll dice
        if (Input.GetMouseButtonDown(Dice.MOUSE_RIGHT_BUTTON) && !PointInRect(GuiMousePosition(), rectModeSelect))
        {
            GameObject manager = GameObject.Find("GameManager"); ;
            GameManager GameManager;
            GameManager = manager.GetComponent<GameManager>();

            if (PlayerColor == "red")
                dices = GameManager.getRedDiceCount();
            else if (PlayerColor == "blue")
                dices = GameManager.getBlueDiceCount();

            ColorDice = dices[0];
            whiteDice = dices[1];

            // left mouse button clicked so roll random colored dice 2 of each dieType
            Dice.Clear();
            
                Dice.Roll(ColorDice + "d6", "d6-"+ PlayerColor +"-dots", spawnPoint.transform.position, Force());
                Dice.Roll(whiteDice + "d6", "d6-"+"white"+"-dots", spawnPoint.transform.position, Force());
            StartCoroutine("Timer");
            

        }

       
    }
    IEnumerator Timer() 
    {

        yield return new WaitForSeconds(5f);

        Dice.reArrangeAllDice();

        

    }
    void OnGUI()
    {

      
            //case MODE_ROLL:
                // display rolling message on bottom
                //GUI.Box(new Rect((Screen.width - 520) / 2, Screen.height - 40, 520, 25), "");
                //GUI.Label(new Rect(((Screen.width - 520) / 2) + 10, Screen.height - 38, 520, 22), "Click with the left (all die types) or right (gallery die) mouse button in the center to roll.");
                if (Dice.Count("") > 0)
                {
                    // we have rolling dice so display rolling status
                    GUI.Box(new Rect(10, Screen.height - 75, Screen.width - 20, 30), "");
                    GUI.Label(new Rect(20, Screen.height - 70, Screen.width, 20), Dice.CountAsString(""));
                }

              
        }
    }

