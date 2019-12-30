using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float deceleration = 5f;
    public float lifetime = 3f;

    public SpriteRenderer theSR;
    public float fadeSpeed = 2.5f;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            float a = Mathf.MoveTowards(theSR.color.a, 0f, fadeSpeed * Time.deltaTime);
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, a);

            if (Mathf.Approximately(a, 0f))
            {
                Destroy(gameObject);
            }            
        }
    }
}
