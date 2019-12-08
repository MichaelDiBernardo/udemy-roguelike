using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
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
        doTrapEffect(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        doTrapEffect(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        doTrapEffect(other.collider);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        doTrapEffect(other.collider);
    }

    private static void doTrapEffect(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}
