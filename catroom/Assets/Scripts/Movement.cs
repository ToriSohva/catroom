using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public static Rigidbody2D MoveLeft(Rigidbody2D body, int speed)
    {
        body.velocity = new Vector2(-speed * Time.deltaTime, body.velocity.y);
        return body;
    }
    public static Rigidbody2D MoveRight(Rigidbody2D body, int speed)
    {
        body.velocity = new Vector2(speed * Time.deltaTime, body.velocity.y);
        return body;
        //return body += Vector2.right * speed * Time.deltaTime;
    }
    public static Rigidbody2D MoveUp(Rigidbody2D body, int speed)
    {
        body.velocity = new Vector2(body.velocity.x, speed * Time.deltaTime);
        return body;
    }
    public static Rigidbody2D MoveDown(Rigidbody2D body, int speed)
    {
        body.velocity = new Vector2(body.velocity.x, -speed * Time.deltaTime); 
        return body;
    }

    public static Rigidbody2D StopHorizontal(Rigidbody2D body)
    {
        body.velocity = Vector2.Scale(body.velocity, new Vector2(0,1));
        return body;
    }

    public static Rigidbody2D StopVertical(Rigidbody2D body)
    {
        body.velocity = Vector2.Scale(body.velocity, new Vector2(1, 0));
        return body;
    }

    public static Rigidbody2D Stop(Rigidbody2D body)
    {
        body.velocity = Vector2.Scale(body.velocity, new Vector2(0, 0));
        return body;
    }
}
