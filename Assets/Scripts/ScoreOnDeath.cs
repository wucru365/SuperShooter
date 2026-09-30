using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public int amount;
    private void OnDestroy()
    {
        ScoreManager.instance.amount += amount;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
