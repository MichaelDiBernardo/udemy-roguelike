using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxShrapnel;

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

            int piecesToDrop = Random.Range(1, maxShrapnel + 1);

            for (int i=0; i<piecesToDrop; i++)
            {
                int randomPiece = Random.Range(0, brokenPieces.Length);
                Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
            }
            
        }
    }
}
