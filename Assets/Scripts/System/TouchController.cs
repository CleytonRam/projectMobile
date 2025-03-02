using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public Vector2 pastPosition;
    public float velocity = 1f;
    public float leftLimit = -5f;
    public float rightLimit = 5f;
    void Start()
    {
        
    }

 
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            //mousePosition AGORA - mousePosition passado
            Move(Input.mousePosition.x - pastPosition.x);
        }
        pastPosition = Input.mousePosition;
    }


    public void Move(float speed)
    {
        Vector3 currentPosition = transform.position;
        currentPosition += Vector3.right * Time.deltaTime * speed * velocity;

        currentPosition.x = Mathf.Clamp(currentPosition.x, leftLimit, rightLimit);

        transform.position = currentPosition;   
    }
}
