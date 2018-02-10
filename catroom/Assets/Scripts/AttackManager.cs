using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackManager : MonoBehaviour {

	public void PlayerAttack (Vector3 position, Direction direction)
    {
        
        CastMagicBall(position, direction);
    }

    public void EnemyAttack(Vector3 position, Direction direction, int damage)
    {
        var manager = StaticValues.Player.GetComponent<HealthManager>();
        manager.Hit(damage);
        //if(manager.CheckHealth() <= 0)
        //{
        //    Destroy(StaticValues.Player);
        //}
    }

    void CastMagicBall(Vector3 position, Direction direction)
    {
        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Magic Ball.prefab", typeof(GameObject));
        Vector3 ballPosition = new Vector3(position.x, position.y, -5);
        GameObject clone = Instantiate(prefab, ballPosition, Quaternion.identity) as GameObject;
        clone.SendMessage("SetDirection", direction);
        IEnumerator coroutine = HandleBallDestruction(clone);
        StartCoroutine(coroutine);
    }

    public IEnumerator HandleBallDestruction(GameObject ball)
    {
        yield return new WaitForSeconds(1f);
        DestroyImmediate(ball);
    }
}
