using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airship : MonoBehaviour
{
    Rigidbody2D rb;
    
    bool isFlying = false;
    bool isShooting = false;

    public GameObject gameover;
    public GameObject restart;
    
    public GameObject laserBluPrefab;
    public GameObject fuoco_propulsori;
    
    public Transform shootingPoint;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            isFlying = true;
            fuoco_propulsori.SetActive(true);
        }

        if (isFlying && !GameController.gameover && transform.position.y <= 4f  && !isShooting)
        {
            rb.velocity = new Vector2(0f, 4f);
	}
        if (Input.GetMouseButtonUp(0))
        {
            isFlying = false;
            isShooting = false;
            fuoco_propulsori.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.gameover = true;
        gameover.SetActive(true);
        restart.SetActive(true);
    }

    public void shoot()
    {
        Instantiate(this.laserBluPrefab, shootingPoint.position, Quaternion.Euler(0f, 0f, 90f));
    }
    
    public void SetIsShooting(bool value)
    {
        isShooting = value;
    }
}

