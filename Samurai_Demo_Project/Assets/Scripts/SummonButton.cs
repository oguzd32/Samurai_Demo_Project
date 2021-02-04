using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonButton : MonoBehaviour
{
    [SerializeField] private int needKilledEnemy = 2;
    public int KilledEnemy { get; set; } = 0;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void ButtonWhite()
    {
        image.color = Color.white;
    }

    public void ButtonBlack()
    {
        image.color = Color.black;
    }

    public void EnemyKillded()
    {
        KilledEnemy++;
        Debug.Log("Killed enemy " + KilledEnemy);
        if (KilledEnemy >= needKilledEnemy) // if enough enemy have killed then button seems active
        {
            ButtonWhite();
        }
    }

    // this method using from Summon Button in hieararchy(canvas)
    public void SummonAllie()
    {
        if(KilledEnemy >= needKilledEnemy)
        {
            FindObjectOfType<AllieSpawner>().Spawn = true;
        }
        else
        {
            return;
        }
        
    }
}
