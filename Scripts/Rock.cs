using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //khi khong con cai gi do~ nua~ thi delay 1s r rot
        {                                              //Neu cham nhan vat trong 1s thi heal -= 1
            StartCoroutine("fallDown");
        }
    }

    IEnumerator fallDown()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("cham nhau");
        rb.isKinematic = false;
        
    }
}
