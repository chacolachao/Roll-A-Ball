using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    private Rigidbody rb;
    private int count;
    public Text winText;
    public Text PointCount;
    public Text loseText;
    private int pickups;
    public Text pickupsTotal;
    public GameObject secondSpawn;
    private int lives;
    public Text livesText;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        pickups = 0;
        lives = 3;
        SetCountText ();
        winText.text = "";
        PointCount.text = "";
        loseText.text = "";
        
        
    }
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        if (lives == 0 || count >= 20 ) 
        {
            moveHorizontal = 0;
            moveVertical = 0;
            rb.velocity = Vector3.zero;
        }
        
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);

        if (Input.GetKey("escape"))
            Application.Quit();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            pickups = pickups + 1;
            SetCountText ();
        }
        if (other.gameObject.CompareTag("Anti"))
        {
            other.gameObject.SetActive (false);
            count = count - 1;
            pickups = pickups + 1;
            lives = lives - 1;
            SetCountText ();
           
        }
    }
    void SetCountText ()
    {
       countText.text = "Points: " + count.ToString ();
       pickupsTotal.text = "Pickup Total: " + pickups.ToString ();
       livesText.text = "Lives: " + lives.ToString ();
       if (count == 12)
       {
            rb.MovePosition(secondSpawn.transform.position);

       }
       if (count >= 20)
       {
          winText.text = "Wow! YOU WON!!"; 
          
          PointCount.text = "Total Points: " + count;
       }
       if (lives == 0)
       {
         loseText.text = "GAME OVER";
         PointCount.text = "Total Points: " + count;  
       }
    }
}
