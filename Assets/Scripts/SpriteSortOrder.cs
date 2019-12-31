using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer _theSR;

    private void Awake()
    {
        _theSR = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _theSR.sortingOrder = Mathf.RoundToInt(-100 * transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
