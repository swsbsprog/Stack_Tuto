using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public Vector3 pivot;
    [SerializeField] Vector3 desPoint;    // 목표지점.
    [SerializeField] Vector3 startPoint;  // 시작 지점.
    float startTime;
    public void Start()
    {
        startPoint = new Vector3(transform.localPosition.x, transform.position.y, transform.localPosition.z);
        desPoint = new Vector3(-startPoint.x, startPoint.y, -startPoint.z);
        pivot.y = 0;
        startPoint += pivot;
        desPoint += pivot;
        startTime = Time.time;
    }

    public float elapsTime;
    public float 나머지빼기1;
    public float time;
    public void Update()
    {
        elapsTime = Time.time - startTime;
        나머지빼기1 = elapsTime % 2 - 1f;
        time = Mathf.Abs(나머지빼기1); // 1 ~ 0 ~ 1
        //a + (b - a) * t.
        Vector3 pos = Vector3.Lerp(desPoint, startPoint, time);
        transform.localPosition = pos;
    }
}
