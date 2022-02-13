using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public Rigidbody2D myRig;
    public SpriteRenderer mySpriteRend;

    public List<MyDecision> myDecisions = new List<MyDecision>();

    MyManager myMan;

    float timePassing = 0;
    float nextDecisionTime = 0;

    void Start()
    {
        nextDecisionTime += myMan.decisionPeriod;
        mySpriteRend.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyManager.canPlay)
        {
            timePassing += Time.deltaTime;
            if (timePassing >= nextDecisionTime)
            {
                nextDecisionTime += myMan.decisionPeriod;
                Take_Decision();
            }
        }

        if (MyManager.canPlay)
        {
            myRig.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            myRig.bodyType = RigidbodyType2D.Static;
        }
    }

    void Take_Decision()
    {
        byte randomDec = (byte) Random.Range(0, 2);
        if (randomDec == 1) Jump();

        MyDecision newDec = new MyDecision(myMan.Get_Fitness(transform.position), randomDec);
        myDecisions.Add(newDec);
    }

    public void Jump()
    {
        myRig.velocity = Vector2.zero;
        myRig.AddForce(Vector2.up * myMan.jumpForce);
    }

    public void Set_My_Manager(MyManager myMan)
    {
        this.myMan = myMan;
    }

}
