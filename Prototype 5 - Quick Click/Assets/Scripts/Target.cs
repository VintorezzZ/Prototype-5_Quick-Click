using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private GameManager _gameManager;
    
    private Rigidbody rb;
    private float xRange = 4;
    private float ySapwnPos = 0;
    private float minSpeed = 10;
    private float maxSpeed = 13;
    private float maxTorque = 10;

    public int pointValue;
    public ParticleSystem explosionParticle;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    private float RandomTorque()
    {
        return Random.Range(maxTorque, maxTorque);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySapwnPos);
    }

    private void OnMouseDown()
    {
        if(!_gameManager.isGameActive)
            return;
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        _gameManager.UpdateScore(pointValue);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad"))
        {
            _gameManager.GameOver();
        }
        Destroy(gameObject);
        
    }
}
