using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube baseCube;
    public MovingCube movingCube; // 마지막에 움직이고 있던 큐브
    public MovingCube stackedCube;  // 마지막에 쌓여 있던 큐브
    void Start()
    {
        baseCube.gameObject.SetActive(false);   
        cubeHeight = baseCube.transform.localScale.y;
        CreateCube();
    }
    public int level;
    public float distance = 3;
    private float cubeHeight;

    private void CreateCube()
    {
        movingCube = Instantiate(baseCube, baseCube.transform.parent);
        movingCube.gameObject.SetActive(true);
        movingCube.name = level.ToString();
        Vector3 newPos = new Vector3(distance, level * cubeHeight, distance);
        movingCube.transform.localPosition = newPos;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            level++;
            // 움직이고 있던 큐브멈추고
            //새 큐브 만들자.
            StopCube();
            CreateCube();
        }
    }
    private void StopCube() => movingCube.GetComponent<MovingCube>().enabled = false;
}
