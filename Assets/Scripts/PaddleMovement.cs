using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaddleMovement : MonoBehaviour
{
    public bool usetilt = false;
    public bool useSlider = false;
    public float speed;
    public float tilt;

    public Slider paddleSlider;
    public Transform leftWall, rightWall;
    public float wallWidth;
    private void Start()
    {
        paddleSlider.minValue=leftWall.position.x+ wallWidth;
        paddleSlider.maxValue=rightWall.position.x- wallWidth;
    }
    void FixedUpdate()
    {
        TouchInput();
        TiltInput();
        sliderInput();

    }
    void TouchInput()
    {
        if (usetilt)return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            float clampedX = Mathf.Clamp(touchPos.x, leftWall.transform.position.x+ wallWidth, rightWall.transform.position.x- wallWidth);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        }
    }
    void TiltInput()
    {
        if (!usetilt)return;
        tilt = Input.acceleration.x * speed * Time.deltaTime;
        transform.position += new Vector3(tilt, 0, 0) ;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftWall.transform.position.x+ wallWidth, rightWall.transform.position.x- wallWidth), transform.position.y, transform.position.z);
    }

    void sliderInput()
    {
        if (!useSlider)return;
        transform.position = new Vector3(-paddleSlider.value, transform.position.y, transform.position.z);
    }
}
