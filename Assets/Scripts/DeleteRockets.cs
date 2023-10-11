using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRockets : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteCluster());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DeleteCluster(){
        yield return new WaitForSeconds(5.5f);
        Destroy(gameObject);
    }
}