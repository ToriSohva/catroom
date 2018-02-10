using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour {

    public int Speed;
    public int Damage;
    private Direction Direction;
    private Rigidbody2D body;

    // Use this for initialization
    private void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    void SetDirection(Direction direction)
    {
        Direction = direction;
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        switch (Direction) {
            case Direction.Left:
                body = Movement.MoveLeft(body, Speed);
                break;
            case Direction.Right:
                body = Movement.MoveRight(body, Speed);
                break;
            case Direction.Down:
                body = Movement.MoveDown(body, Speed);
                break;
            case Direction.Up:
                body = Movement.MoveUp(body, Speed);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Collider2D>().gameObject.GetComponent<Enemy>();
        if(!enemy.IsStunned())
        {
            var manager = collision.GetComponent<Collider2D>().gameObject.GetComponent<HealthManager>();
            manager.Hit(Damage);
            enemy.GetComponent<Animator>().SetTrigger("Hurt");
            if (manager.CheckHealth() <= 0)
            {
                enemy.Stun();
                //Destroy(collision.GetComponent<Collider2D>().gameObject);
            }
            Destroy(this.gameObject);
        }
        
    }

    
}
