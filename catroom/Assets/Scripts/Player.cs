using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int Speed;
    private Direction Direction;
    public int attackTime = 500;
    private AttackManager attackManager;
    private HealthManager healthManager;

    public Clips clips;

    private DateTime AttackCooldownTimer;
    private bool AttackCooldown;
    private Rigidbody2D body;
    private bool dying;

    // Use this for initialization
    void Start () {
        StaticValues.Player = gameObject;
        StaticValues.PlayerPosition = transform.position;
        this.Direction = Direction.Down;
        attackManager = gameObject.AddComponent<AttackManager>();
        healthManager = gameObject.GetComponent<HealthManager>();
        AttackCooldown = false;
        dying = false;
        body = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (healthManager.CheckHealth() <= 0)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0) && dying)
            {
                Destroy(gameObject);
            }
            else if (!dying)
            {
                if (!clips.catDieSource.isPlaying)
                    clips.catDieSource.Play();
                dying = true;
                StaticValues.PlayerDead = true;
                GetComponent<Animator>().SetTrigger("Dead");
            }

        } else
        {
            StaticValues.PlayerPosition = transform.position;
            CheckMovement();
            CheckActions();
        }
        
	}

    void CheckActions()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !AttackCooldown)
        {
            AttackCooldown = true;
            AttackCooldownTimer = DateTime.Now;
            clips.RandomPitchPlay(clips.catAttackSource);
            attackManager.PlayerAttack(transform.position, Direction);
        }
        else
        {
            TimeSpan cooldownTime = DateTime.Now - AttackCooldownTimer;
            if (cooldownTime.Milliseconds > attackTime)
            {
                AttackCooldown = false;
            }
        }
        
    }

    void CheckMovement()
    {
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            body = Movement.MoveLeft(body, Speed);
            Direction = Direction.Left;
            GetComponent<Animator>().SetTrigger("Left");
            if (!clips.walkingSource.isPlaying)
                clips.walkingSource.Play();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            body = Movement.MoveRight(body, Speed);
            Direction = Direction.Right;
            GetComponent<Animator>().SetTrigger("Right");
            if (!clips.walkingSource.isPlaying)
                clips.walkingSource.Play();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            body = Movement.MoveDown(body, Speed);
            Direction = Direction.Down;
            GetComponent<Animator>().SetTrigger("Down");
            if (!clips.walkingSource.isPlaying)
                clips.walkingSource.Play();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            body = Movement.MoveUp(body, Speed);
            Direction = Direction.Up;
            GetComponent<Animator>().SetTrigger("Up");
            if (!clips.walkingSource.isPlaying)
                clips.walkingSource.Play();
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow) ){
            body = Movement.StopVertical(body);
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow))
        {
            body = Movement.StopHorizontal(body);
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow))
        {
            if (clips.walkingSource.isPlaying)
                clips.walkingSource.Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Direction == Direction.Up || Direction == Direction.Down)
        {
            body = Movement.StopVertical(body);
        }
        if(Direction == Direction.Left || Direction == Direction.Right)
        {
            body = Movement.StopHorizontal(body);
        }
    }

    
}
