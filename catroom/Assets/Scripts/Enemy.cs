using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int speed;
    public int damage;
    private Direction direction;
    private AttackManager attackManager;
    private HealthManager healthManager;
    private DateTime attackCooldownTimer;
    private bool attackCooldown;
    private Rigidbody2D body;
    private CircleCollider2D detectArea;
    private DateTime moveTimer;
    public int attackTime = 1500;
    public float detectionRange = 30;
    public float attackRange = 10;

    public Clips clips;

    public int walkSeconds { get; private set; }
    public int waitSeconds { get; private set; }
    public DateTime stunTime { get; private set; }
    public bool followsPlayer { get; private set; }
    public GameObject player { get; private set; }

    private DateTime waitingTimer;
    private bool waiting;
    private bool moving;
    private bool stunned;
    public float followSpeed;
    private float directionVariable = 10;

    // Use this for initialization
    void Start()
    {
        this.direction = Direction.Down;
        attackManager = gameObject.AddComponent<AttackManager>();
        healthManager = gameObject.GetComponent<HealthManager>();
        attackCooldown = false;
        body = gameObject.GetComponent<Rigidbody2D>();
        detectArea = gameObject.GetComponent<CircleCollider2D>();
        moving = false;
        stunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticValues.PlayerDead)
        {

        } else
        {
            CheckMovement();

            CheckActions();
        }
        
    }

    void CheckActions()
    {
        if(CloseEnough() && !IsStunned())
        {
            if (!attackCooldown)
            {
                attackCooldown = true;
                attackCooldownTimer = DateTime.Now;
                GetComponent<Animator>().SetTrigger("Attack");
                clips.RandomPitchPlay(clips.bearAttacksSource);
                clips.RandomPitchPlay(clips.catMoansSource);
                attackManager.EnemyAttack(transform.position, direction, damage);
            }
            else
            {
                //Debug.Log(StaticValues.Player.GetComponent<HealthManager>().CheckHealth());
                TimeSpan cooldownTime = DateTime.Now - attackCooldownTimer;
                if (cooldownTime > TimeSpan.FromMilliseconds(attackTime))
                {
                    attackCooldown = false;
                }
            }
        }
        
    }

    void CheckMovement()
    {   
        if (IsStunned())
        {
            clips.bearFollowsSource.Stop();
            TimeSpan stunSpan = DateTime.Now - stunTime;
            if (stunSpan.Seconds > 5)
            {
                healthManager.ResetHealth();
                stunned = false;
                GetComponent<Animator>().SetTrigger("Down");
            }
        }
        else if (SeesPlayer() && !CloseEnough())
        {
            if(!clips.bearFollowsSource.isPlaying)
                clips.RandomPitchPlay(clips.bearFollowsSource);
            moving = false;
            waiting = false;
            Movement.Stop(body);
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                DetermineDirection();
            transform.position = Vector3.MoveTowards(transform.position, StaticValues.PlayerPosition, followSpeed * Time.deltaTime);
        }
        else if (!CloseEnough())
        {
            clips.bearFollowsSource.Stop();
            if (!moving)
            {
                moving = true;
                Array values = Enum.GetValues(typeof(Direction));
                System.Random random = new System.Random();
                Direction direction = (Direction)values.GetValue(random.Next(values.Length));
                this.direction = direction;
                moveTimer = DateTime.Now;
                walkSeconds = random.Next(1, 4);
                waitSeconds = random.Next(1, 4);
            } else if (!waiting)
            {
                TimeSpan moveSpan = DateTime.Now - moveTimer;
                if (moveSpan.Seconds >= walkSeconds)
                {
                    waitingTimer = DateTime.Now;
                    waiting = true;
                }
                switch (direction)
                {
                    case Direction.Left:
                        body = Movement.MoveLeft(body, speed);
                        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                            GetComponent<Animator>().SetTrigger("Left");
                        break;
                    case Direction.Right:
                        body = Movement.MoveRight(body, speed);
                        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                            GetComponent<Animator>().SetTrigger("Right");
                        break;
                    case Direction.Down:
                        body = Movement.MoveDown(body, speed);
                        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                            GetComponent<Animator>().SetTrigger("Down");
                        break;
                    case Direction.Up:
                        body = Movement.MoveUp(body, speed);
                        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                            GetComponent<Animator>().SetTrigger("Up");
                        break;
                }
            } else
            {
                body = Movement.Stop(body);
                TimeSpan moveSpan = DateTime.Now - waitingTimer;
                if (moveSpan.Seconds >= waitSeconds)
                {
                    waiting = false;
                    moving = false;
                }
            }
            
            
        }
        
    }

    private void DetermineDirection()
    {
        if(StaticValues.PlayerPosition.x < transform.position.x)
        {
            if(Math.Abs(StaticValues.PlayerPosition.x - transform.position.x) < directionVariable)
            {
                if (StaticValues.PlayerPosition.y < transform.position.y)
                {
                    direction = Direction.Down;
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                        GetComponent<Animator>().SetTrigger("Down");
                    // DOWN
                } else
                {
                    direction = Direction.Up;
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                        GetComponent<Animator>().SetTrigger("Up");
                    // UP
                }
            } else
            {
                direction = Direction.Left;
                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                    GetComponent<Animator>().SetTrigger("Left");
                // LEFT
            }
            
        } else
        {
            if (Math.Abs(StaticValues.PlayerPosition.x - transform.position.x) < directionVariable)
            {
                if (StaticValues.PlayerPosition.y < transform.position.y)
                {
                    direction = Direction.Down;
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                        GetComponent<Animator>().SetTrigger("Down");
                    // DOWN
                }
                else
                {
                    direction = Direction.Up;
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                        GetComponent<Animator>().SetTrigger("Up");
                    // UP
                }
            }
            else
            {
                direction = Direction.Right;
                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                    GetComponent<Animator>().SetTrigger("Right");
                // RIGHT
            }
        }
        var distance = new Vector2(StaticValues.PlayerPosition.x, StaticValues.PlayerPosition.y) - new Vector2(transform.position.x, transform.position.y);
        Debug.Log(Vector2.Angle(distance, transform.up));
    }

    public void Stun()
    {
        stunTime = DateTime.Now;
        stunned = true;
        clips.bearMoansSource.Play();
        GetComponent<Animator>().SetTrigger("Stun");
        body = Movement.Stop(body);
    }
    public bool IsStunned()
    {
        return stunned;
    }

    private bool SeesPlayer()
    {
        var distance = transform.position - StaticValues.PlayerPosition;
        if(distance.magnitude < detectionRange)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool CloseEnough()
    {
        var distance = transform.position - StaticValues.PlayerPosition;

        if (distance.magnitude < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (Direction == Direction.Up || Direction == Direction.Down)
        //{
        //    body = Movement.StopVertical(body);
        //}
        //if (Direction == Direction.Left || Direction == Direction.Right)
        //{
        //    body = Movement.StopHorizontal(body);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //followsPlayer = true;
        //player = collision.GetComponent<Collider2D>().gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //followsPlayer = false;
    }
}
