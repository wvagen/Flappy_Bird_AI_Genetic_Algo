using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyManager : MonoBehaviour
{
    public int agentNumber;

    public float jumpForce = 500;
    public float decisionPeriod = 0.25f;

    public GameObject player;
    public Transform playerSpawnPos;

    public bool canStart = false;

    public static bool canPlay = true;

    List<Transform> pipesPos = new List<Transform>();
    List<MyPlayer> players = new List<MyPlayer>();

    int nextPipe = 0;

    private void Awake()
    {
        canPlay = canStart;
    }

    void Start()
    {
        Fill_Pipes_List();
        Spawn_Agents();
    }

    void Fill_Pipes_List()
    {
        foreach(MyPipe mypipe in FindObjectsOfType<MyPipe>())
        {
            pipesPos.Add(mypipe.transform);
        }
    }

    void Spawn_Agents()
    {
        Vector2 nextSpawnPos = playerSpawnPos.position;
        for (int i = 0; i < agentNumber / 2 + 1; i++)
        {
            MyPlayer playerScript = Instantiate(player, nextSpawnPos, Quaternion.identity, playerSpawnPos).
                GetComponent<MyPlayer>();
            playerScript.Set_My_Manager(this);
            if (nextSpawnPos.y < 2)
            nextSpawnPos.y += 0.4f;
            players.Add(playerScript);
        }
        nextSpawnPos = playerSpawnPos.position;
        for (int i = agentNumber / 2 + 1; i < agentNumber; i++)
        {
            MyPlayer playerScript = Instantiate(player, nextSpawnPos, Quaternion.identity, playerSpawnPos).
                GetComponent<MyPlayer>();
            playerScript.Set_My_Manager(this);
            if (nextSpawnPos.y > -2)
                nextSpawnPos.y -= 0.4f;
            players.Add(playerScript);
        }
    }

    // Update is called once per frame
    void Update()
    {
        canPlay = canStart;

        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Agent " + i.ToString() + " max fitness: " + players[i].maxFitness);
        }

    }

    public void Next_Pipe()
    {
        nextPipe++;
    }

    public int Get_Fitness(Vector2 playerPos)
    {
        if (1 / Vector2.Distance(pipesPos[nextPipe].position, playerPos) > 10000) return 10000;
        else return  (int)(10000 * (1 / Vector2.Distance(pipesPos[nextPipe].position, playerPos)));
    }

}
