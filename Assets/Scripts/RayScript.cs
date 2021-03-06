using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    RaycastHit hit;
    float MaxDistance = 15f;
    GameObject player;
    GameObject playerEquipPoint;
    bool itemGrab;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");

    }
    void Start()
    {
        
    }
    void Setgrab(GameObject item, bool isgrab)
    {
        Collider itemCollider = item.GetComponent<Collider>();
        Rigidbody itemRigidebody = item.GetComponent<Rigidbody>();

        itemCollider.enabled = !isgrab;
        itemRigidebody.isKinematic = isgrab;

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {   
            if(itemGrab == false) 
            { 
                Debug.DrawRay(transform.position, transform.forward, Color.red, 0.3f);
                if(Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
                {
                    Setgrab(hit.transform.gameObject, true);
                    itemGrab = true;
                    hit.transform.SetParent(playerEquipPoint.transform);
                }
            }
            else
            {
                Setgrab(hit.transform.gameObject, false);
                playerEquipPoint.transform.DetachChildren();
                itemGrab = false;
            }
        }
    }
}
