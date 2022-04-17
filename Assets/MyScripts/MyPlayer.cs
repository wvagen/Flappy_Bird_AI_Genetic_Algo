using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public Rigidbody2D myRig;
    public SpriteRenderer mySpriteRend;

    public MyPlayerCollisionDetection myBird;

    public List<MyDecision> myDecisions = new List<MyDecision>();

    public Animator myAnim;

    public int previousScore = 0;

    public int maxFitness = 0;
    public int maxFitnessIndex = 0;

    MyManager myMan;

    float timePassing = 0;
    float nextDecisionTime = 0;

    int pointsOwned = 0;
    int indexCount = 0;

    void Start()
    {
        nextDecisionTime += myMan.decisionPeriod;
        mySpriteRend.color = Random.ColorHSV();
        if (MyManager.success)
        {
            myRig.GetComponent<Collider2D>().enabled = false;
        }
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
        byte randomDec = 0;
        if (indexCount < MyManager.bestDecisionsList.Count)
        {
            randomDec = (byte)MyManager.bestDecisionsList[indexCount].decisionTaken;
            indexCount++;
        }
        else
        {
            randomDec = (byte)Random.Range(0, 2);
        }

        if (randomDec == 1) Jump();

        int fitness = myMan.Get_Fitness(myBird.transform.position);

        if (maxFitness < fitness + previousScore)
        {
            maxFitness = fitness + previousScore;
            maxFitnessIndex = myDecisions.Count - 1;
        }

        MyDecision newDec = new MyDecision(fitness + previousScore, randomDec);
        myDecisions.Add(newDec);
    }

    public void Increment_Score()
    {
        pointsOwned++;
        maxFitness = pointsOwned * 10000;
        previousScore = maxFitness;
        myMan.Next_Pipe();
    }

    public void Die()
    {
        myMan.Player_Die(this);
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
