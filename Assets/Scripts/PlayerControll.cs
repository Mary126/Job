using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed;
    public float timeToTakeResources;
    public Dictionary<string, int> playerResources = new Dictionary<string, int>();

    private void Start()
    {
        playerResources.Add("resource1", 0);
        playerResources.Add("resource2", 0);
        playerResources.Add("resource3", 0);
    }
    public void MoveUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed);
    }
    public void MoveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed);
    }
    public void MoveLeft()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y);
    }
    public void MoveRight()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
