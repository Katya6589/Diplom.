using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour
{
    private RawImage _image;

    [SerializeField, Range(0, 10)] private float _scrollSped = 0.1f;

    [SerializeField, Range(-1, 1)] private float _xDirection = 1;
    [SerializeField, Range(-1, 1)] private float _yDirection = 1;
    // Start is called before the first frame update

    private void Awake() => _image = GetComponent<RawImage>();

    private void Update()
        => _image.uvRect = new Rect(_image.uvRect.position + new Vector2(-_xDirection * _scrollSped, _yDirection * _scrollSped) * Time.deltaTime, _image.uvRect.size);
}
