using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gethit()
    {
        EnemyAI.getHit = false;
        EnemyAI.attack = true;
        EnemyAI.walking = true;
        Debug.Log("from event");
    }
}
