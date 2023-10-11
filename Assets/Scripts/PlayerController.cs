using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public GameObject rocketCluster;
    private Rigidbody playerRb;
    public float speed = 3.5f;

    float powerupStrength = 30.0f;
    bool hasPowerup=false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0,0.77f,0);
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(/*Vector3.forward*/focalPoint.transform.forward*speed*forwardInput);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup")){
            hasPowerup = true;
            Destroy(other.gameObject);
            playerRb.AddForce(Vector3.up*speed, ForceMode.Impulse);
            //playerRb.mass = 25;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutin());
        }else if(other.CompareTag("Rockets")){
            Instantiate(rocketCluster, transform.position, rocketCluster.transform.rotation);
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy")&&hasPowerup){
            Debug.Log("Collided with " + other.gameObject.name + " with powerup se to " + hasPowerup);
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer*powerupStrength, ForceMode.Impulse);
            //playerRb.AddForce(awayFromPlayer*1.5f, ForceMode.Impulse);
        }    
    }

    IEnumerator PowerupCountdownRoutin(){
        yield return new WaitForSeconds(7.5f);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        Debug.Log("Powerup has ran out!!!");
    }
}
