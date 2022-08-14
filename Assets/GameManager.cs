using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube baseCube;
    public MovingCube movingCube; // 마지막에 움직이고 있던 큐브
    public MovingCube stackedCube;  // 마지막에 쌓여 있던 큐브
    [SerializeField] float hue;
    [SerializeField] float s;
    [SerializeField] float v;
    [SerializeField] float changeHue = 10/256f;
    [SerializeField] Material bgMaterial;
    [SerializeField] float bgBottomColorOffset = 0.15f;
    IEnumerator Start()
    {
        Color firstColor = baseCube.GetComponent<Renderer>().material.GetColor("_ColorTop");
        Color.RGBToHSV(firstColor, out hue, out s, out v);

        baseCube.gameObject.SetActive(false);   
        cubeHeight = baseCube.transform.localScale.y;
        CreateCube();

        //yield return new WaitForSeconds(waitSeconds);
        //OnKeyDown();
        //Debug.Break();
        yield return null;
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
            //Debug.Break();
            //newCube.Start();
            //newCube.Update();
        }
        movingCube = newCube;

        CameraMove();
        ChangeColor();
    }

    private void ChangeColor()
    {
        //movingCube. 색지정.
        var cubeMaterial = movingCube.GetComponent<Renderer>().material;
        hue += changeHue;
        if (hue > 1)
            hue = 1 - hue;
        cubeMaterial.SetColor("_ColorTop", Color.HSVToRGB(hue, s, v));
        cubeMaterial.SetColor("_ColorBottom", Color.HSVToRGB(hue, s, v));

        // 백그라운드 색지정.
        bgMaterial.SetColor("_ColorTop", Color.HSVToRGB(hue, s, v));
        float bottomColorHue = hue + bgBottomColorOffset;
        if (bottomColorHue > 1)
            bottomColorHue = 1 - bottomColorHue;
        bgMaterial.SetColor("_ColorBottom", Color.HSVToRGB(bottomColorHue, s, v));
    }

    private void CameraMove()
    {
        Camera.main.transform.Translate(0, cubeHeight, 0, Space.World);
    }

    void Update()
    {
        if (Input.anyKeyDown)
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
