using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCharecter : MonoBehaviour
{
    public int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log($"Player Health: {health}");
    }
}
