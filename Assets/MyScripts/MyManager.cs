using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MyManager : MonoBehaviour
{
    public int agentNumber;

    public float jumpForce = 500;
    public float decisionPeriod = 0.25f;

    public GameObject player;
    public Transform playerSpawnPos;

    public bool canStart = false;

    public static bool canPlay = true;

    static bool directStart = false;
    static bool autoPlay = false;

    public Animator myAnim;
    public GameObject PlayerDashBoardGO;

    public Transform gameOverPlayersDashBoardPanel;

    public TextMeshProUGUI generationTxt, generationTxt1;

    List<Transform> pipesPos = new List<Transform>();
    List<MyPlayer> players = new List<MyPlayer>();

    int nextPipe = 0;
    static byte generation = 1;

    private void Awake()
    {
        if (directStart)
        {
            canStart = true;
        }
        canPlay = canStart;
        generationTxt1.text = "Generation: " + generation.ToString();
    }

    void Start()
    {
        Fill_Pipes_List();
        Spawn_Agents();
    }

    void Fill_Pipes_List()
    {
        foreach (MyPipe mypipe in FindObjectsOfType<MyPipe>())
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

    public void Player_Die(MyPlayer deadPlayer)
    {
        PlayerDashBoard tempPlayDash = Instantiate(PlayerDashBoardGO, gameOverPlayersDashBoardPanel).GetComponent<PlayerDashBoard>();

        string deadPlayerDecisions = "";

        for (int i = 0; i < deadPlayer.myDecisions.Count; i++)
        {
            deadPlayerDecisions += deadPlayer.myDecisions[i].decisionTaken.ToString();
        }

        tempPlayDash.Set_Me(deadPlayer.mySpriteRend.color, deadPlayer.maxFitness.ToString(), deadPlayerDecisions);
        players.Remove(deadPlayer);
        Destroy(deadPlayer.gameObject);
        if (players.Count == 0)
        {
            canStart = false;
            generationTxt.text = "Generation: " + generation.ToString();
            generation++;

            if (autoPlay)
            {
                Next_Gen_Btn_Auto();
            }
            else
            {
                myAnim.Play("Death_Anim");
            }
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            autoPlay = false;
        }
    }

    public void Next_Gen_Btn()
    {
        directStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Next_Gen_Btn_Auto()
    {
        directStart = true;
        autoPlay = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Next_Pipe()
    {
        nextPipe++;
    }

    public int Get_Fitness(Vector2 playerPos)
    {
        if (1 / Vector2.Distance(pipesPos[nextPipe].position, playerPos) > 10000) return 10000;
        else return (int)(10000 * (1 / Vector2.Distance(pipesPos[nextPipe].position, playerPos)));
    }

}
