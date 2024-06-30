using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaitonEventHandler : MonoBehaviour
{

    Animator myAnimator;
    public static bool ismovable = true;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makenotmovable()
    {
        ismovable = false;
        Player.numOfCliks = 0;
    }

    public void makeMovable()
    {
        ismovable = true;

    }
    public void setAttackingFalse()
    {
        Player.isAttacking = false;
    }
    public void setAttackingTrue()
    {
        Player.isAttacking = true;
    }


}
