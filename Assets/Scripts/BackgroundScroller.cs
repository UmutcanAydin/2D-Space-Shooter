using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.2f;
    [SerializeField] float starsScrollSpeed = 0.5f;
    Material material, starMaterial;
    Vector2 offSet, starOffset;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScrollSpeed);

        var child = transform.GetChild(0);
        starMaterial = child.GetComponent<Renderer>().material;
        starOffset = new Vector2(0f, starsScrollSpeed);
    }

    private void Update()
    {
        material.mainTextureOffset += offSet * Time.deltaTime;
        starMaterial.mainTextureOffset += starOffset * Time.deltaTime;
    }
}
