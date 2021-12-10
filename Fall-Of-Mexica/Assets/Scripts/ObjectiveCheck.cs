using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCheck : MonoBehaviour
{
    private GameManager gm;
    private BoxCollider2D col; 
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.name == "VCheck" && gm.villageObjectiveCompleted == true)
        {
            col.isTrigger = true; 
        }

        else if (this.name == "MCheck" && gm.maizeObjectiveCompleted == true)
        {
            col.isTrigger = true;
        }

        else if (this.name == "TCheck" && gm.templeObjectiveCompleted == true)
        {
            col.isTrigger = true;
        }
    }

}
