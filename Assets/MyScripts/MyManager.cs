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
        for (int i = 0; i < agentNumber; i++)
        {
            MyPlayer playerScript = Instantiate(player, playerSpawnPos.position, Quaternion.identity, playerSpawnPos).
                GetComponent<MyPlayer>();
            playerScript.Set_My_Manager(this);
            players.Add(playerScript);
        }
    }

    // Update is called once per frame
    void Update()
    {
        canPlay = canStart;
    }

    public int Get_Fitness(Vector2 playerPos)
    {
        return  (int)(100 * (1 / Vector2.Distance(pipesPos[nextPipe].position, playerPos)));
    }

}
