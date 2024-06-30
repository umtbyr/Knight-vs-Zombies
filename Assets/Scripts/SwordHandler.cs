using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHandler : MonoBehaviour
{


    private float hitTimer;
    private float hitCooldown = 0.2f;
    private bool canHit = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - hitTimer > hitCooldown)
        {
            canHit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && Player.isAttacking)
        {
            hitTimer = Time.time;
            if (canHit)
            {
                Debug.Log("collision occured");
                canHit = false;
                //DestroyAnimation(other.gameObject);
                //Destroy(other.gameObject);
                EnemyAI.GetHit("normalhit");
            }
            
            
        }
    }
    private void DestroyAnimation(GameObject obj)
    {
        Vector3 tempPos = obj.transform.position;
        tempPos.z += 2f;
        tempPos.y += 0.5f;
        Vector3 cubesizes = new Vector3(0.1f, 0.1f, 0.1f);
        for (int i = 0; i < 40; i++)
        {

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Rigidbody rb = cube.AddComponent<Rigidbody>();
            cube.transform.localScale = cubesizes;
            
            if (i > 20)
            {
                cube.transform.position = tempPos;
            }
            else
            {
                cube.transform.position = obj.transform.position;
            }

            

        }

    }
}
