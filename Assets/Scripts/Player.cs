using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    private Rigidbody2D rb;
    private bool moving;
    public float  movingSpeed = 1.0f;
    public float  turnSpeed = 1.0f;
    private float turnDirection;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        moving = Input.GetKey(KeyCode.W);
        if(Input.GetKey(KeyCode.A)){
            turnDirection = 1.0f;
        }else if(Input.GetKey(KeyCode.D)){
            turnDirection = -1.0f;
        }else{
            turnDirection = 0.0f;   
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }
    }

    private void FixedUpdate() {
        if(moving){
            rb.AddForce(this.transform.up * movingSpeed);
        }

        if(turnDirection != 0.0f){
            rb.AddTorque(this.turnDirection * turnSpeed); 
        }
    }

    private void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Asteroid"){
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
