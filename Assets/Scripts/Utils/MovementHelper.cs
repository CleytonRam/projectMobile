using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> postions;
    public float duration = 1f;
    private int _index = 0;

    private void Start()
    {
        _index = Random.Range(0, postions.Count);
        transform.position = postions[_index].transform.position;
        NextIndex();
        StartCoroutine(StartMovement()); 
    }

    private void NextIndex() 
    {
        _index++;
        if (_index >= postions.Count) _index = 0;
    }
    IEnumerator StartMovement()
    {
        float time = 0;
        while (true) 
        {
            var currentPosition = transform.position;

            while(time < duration) 
            {
                transform.position = Vector3.Lerp(currentPosition, postions[_index].transform.position, (time/duration));

                time += Time.deltaTime;
                yield return null;
            }
            NextIndex();

            time = 0;
            yield return null;
        }
    }
}
