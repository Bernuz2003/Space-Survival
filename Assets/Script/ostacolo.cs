using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ostacolo : MonoBehaviour
{
    bool giaContato = false;

    void Update()
    {
        if (!GameController.gameover) {
            transform.position = new Vector2(transform.position.x - 4f * Time.deltaTime, transform.position.y);
        }
        if (this.transform.position.x <= -16.5f)
            Destroy(gameObject);

        if(!giaContato && this.transform.position.x < -14f) {
            giaContato = true;
            GameController.ostacoli_passati++;       
        }
    }
}
