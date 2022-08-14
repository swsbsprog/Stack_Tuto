using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube baseCube;
    public MovingCube movingCube; // 마지막에 움직이고 있던 큐브
    public MovingCube stackedCube;  // 마지막에 쌓여 있던 큐브
    IEnumerator Start()
    {
        baseCube.gameObject.SetActive(false);   
        cubeHeight = baseCube.transform.localScale.y;
        CreateCube();

        yield return new WaitForSeconds(waitSeconds);
        OnKeyDown();
        Debug.Break();
    }
    public int level;
    public float distance = 3;
    private float cubeHeight;
    public float waitSeconds = 0.6f;

    public bool IsEven => level % 2 == 0;

    private void CreateCube()
    {
        // 큐브 생성 위치 지정하자.
        var newCube = 
            Instantiate(baseCube, baseCube.transform.parent);
        newCube.gameObject.SetActive(true);
        newCube.name = level.ToString();
        Vector3 newPos = 
            new Vector3(distance, level * cubeHeight, distance);
        if (IsEven) // 짝수면
            newPos.z = -newPos.z;
        newCube.transform.localPosition = newPos;
        if (this.movingCube != null)
        {
            newCube.pivot = this.movingCube.transform.localPosition;
            newCube.Start();
            newCube.Update();
        }
        movingCube = newCube;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnKeyDown();
        }
    }

    private void OnKeyDown()
    {
        level++;
        // 움직이고 있던 큐브멈추고
        //새 큐브 만들자.
        StopCube();
        CreateCube();
    }

    private void StopCube()
        => movingCube.GetComponent<MovingCube>()
        .enabled = false;
}
