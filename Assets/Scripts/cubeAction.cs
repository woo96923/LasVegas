using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class cubeAction : MonoBehaviour
{
    GameObject player;
    GameObject playerEquipPoint;

    Vector3 forceDirection;
    bool isPlayerEnter;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1") && isPlayerEnter)
        {
            transform.SetParent(playerEquipPoint.transform);
            transform.localPosition = Vector3.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);

            isPlayerEnter = false;
        }*/
    }

    void OnTirggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            isPlayerEnter = false;
        }
    }
}
