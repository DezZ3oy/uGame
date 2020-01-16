using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public void AddCoin()
    {
        MainCharacter.coins += 5;
        Destroy(gameObject);
    }

    public void AddHeal()
    {
        if (MainCharacter.lives >= 90)
            MainCharacter.lives = 100;
        else
            MainCharacter.lives += 10;

        Destroy(gameObject);
    }
}
