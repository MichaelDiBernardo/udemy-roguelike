using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxShrapnel;

    public GameObject[] drops;
    public float dropPercent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerController.instance.IsDashing)
        {
            Destroy(gameObject);
            BlowUp();
            MaybeDropItem();
        }
    }

    private void BlowUp()
    {
        int piecesToDrop = Random.Range(1, maxShrapnel + 1);

        for (int i = 0; i < piecesToDrop; i++)
        {
            int randomPiece = Random.Range(0, brokenPieces.Length);
            Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
        }
    }


    private void MaybeDropItem()
    {
        if (drops.Length == 0)
        {
            return;
        }

        if (Random.Range(0f, 100f) >= dropPercent)
        {
            return;
        }

        int dropIndex = Random.Range(0, drops.Length);
        Instantiate(drops[dropIndex], transform.position, transform.rotation);
    }
}
