using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllieSpawner : MonoBehaviour
{
    [SerializeField] GameObject alliePrefab;

    public bool Spawn { get; set; } = false;

    private void Update()
    {
        while (Spawn)
        {
            SpawnAllie();
        }
    }

    public void SpawnAllie()
    {
        Instantiate(alliePrefab, transform.position, transform.rotation);
        Debug.Log("Allice has spawned");
        Spawn = false;
        FindObjectOfType<SummonButton>().ButtonBlack();
        FindObjectOfType<SummonButton>().KilledEnemy -= 2; // it decrase killed enemy prevent for multi calling allie
    }
}
