using UnityEngine;

public class brockenBricks : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject,3f);
    }
    private void FixedUpdate()
    {
        
        transform.position=new Vector3(transform.position.x,transform.position.y-Time.deltaTime*3,transform.position.z);
    }


}
