using UnityEngine;

public class Border : MonoBehaviour
{
    public RectTransform canvas;
    public Transform[] borders;
    void Start()
    {
        reSize();
    }
    void reSize()
    {
        
            Camera cam = Camera.main;
            float screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float screenTop = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            float screenBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

            // Set borders based on screen edges
            borders[3].transform.position = new Vector3(screenLeft, 0, 0);//left
            borders[2].transform.position = new Vector3(screenRight, 0, 0);//right
            borders[0].transform.position = new Vector3(0, screenTop, 0);//top
            borders[1].transform.position = new Vector3(0, screenBottom, 0);//bottom
        
    }
}
