using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public int score;

    public void NewInt(int n)
    {
        score = n;
    }

    public void AddInt(int n)
    {
        score += n;
    }
    public void IncInt()
    {
        score++;
    }

    public void DecInt()
    {
        score--;
    }
}