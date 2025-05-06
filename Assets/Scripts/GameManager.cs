using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerController playerPrefabs;

    public Transform[] spawnPositions;
    public PlayerController[] playerReferences;
    public BossStateController BossReference;


    public string[] hAxisNames;
    public string[] vAxisNames;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPlayers();

        BossReference = GameObject.FindFirstObjectByType<BossStateController>();
        BossReference.SetupBoss(playerReferences);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayers()
    {
        List<PlayerController> list = new List<PlayerController>();

        for(int i = 0; i < 2; i++)
        {
            GameObject tempplayer = Instantiate(playerPrefabs.gameObject, spawnPositions[i].position, Quaternion.identity);
            
            PlayerController tempcontroller = tempplayer.GetComponent<PlayerController>();
            tempcontroller.horizontalName = hAxisNames[i];
            tempcontroller.verticalName = vAxisNames[i];

            list.Add(tempplayer.GetComponent<PlayerController>());

            tempplayer.SetActive(true);
        }
        playerReferences = list.ToArray();
    }
}
