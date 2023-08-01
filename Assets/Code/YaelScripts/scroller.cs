using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _scrollSpeedX, _scrollSpeedY;

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = _img.uvRect.position;
        offset.x += _scrollSpeedX * Time.deltaTime;
        offset.y += _scrollSpeedY * Time.deltaTime;
        _img.uvRect = new Rect(offset, _img.uvRect.size);
    }
}
