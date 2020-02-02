using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    public int owner; // Which player fired this knife

    public bool pierce = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!pierce) Destroy(gameObject);
    }
}
