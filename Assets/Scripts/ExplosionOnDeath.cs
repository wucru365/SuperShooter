using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnDeath : MonoBehaviour
{
    public GameObject particlePrefab;
    // Start is called before the first frame update
    void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(OnDeath);

    }

    // Update is called once per frame
    void OnDeath()
    {
        Instantiate(particlePrefab, transform.position, transform.rotation);
    }
}
