using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public Rigidbody2D myRig;

    public MyManager myMan;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    public void Jump()
    {
        myRig.velocity = Vector2.zero;
        myRig.AddForce(Vector2.up * myMan.jumpForce);
    }

}
