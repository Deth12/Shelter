using System;
using UnityEngine;
using System.Data;

public class Test : MonoBehaviour
{
    public static Test Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    public PlayerCard p;
    public CatastropheCardInfo ci;
    
    public void Gen()
    {
        p.FillPlayerCard(Generator.GeneratePlayerCard());
    }
}
