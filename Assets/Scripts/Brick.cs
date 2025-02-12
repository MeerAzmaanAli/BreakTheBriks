using UnityEngine;

public class Brick : MonoBehaviour
{

    [SerializeField] int hp; //1, 2, 3 : weak, mid, strong
    [SerializeField] string brickType;
    public GameObject crack1, crack2,brokenBrick;
    

    BrickSpawner brickSpawner;

    AudioSource Audio;
    public AudioClip sound;
    private void Start()
    {
        brickSpawner =GameObject.FindAnyObjectByType<BrickSpawner>();
        Audio = FindAnyObjectByType<GameManager>().GetComponent<AudioSource>();
    }
    void Update()
    {
        if (brickType=="strong" && hp == 2)
        {
            crack1.SetActive(true);
        }else if((brickType == "strong"|| brickType == "mid") && hp == 1)
        {
            crack2.SetActive(true);
        }
        else if(hp <= 0)
        {
            Break();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            if (hp > 0) 
            {
                hp -= 1;
            }
            
        }
    }

    public void Break()
    {
        Instantiate(brokenBrick, transform.position, Quaternion.identity);
        brickSpawner.spawnedBricks.Remove(gameObject);
        brickSpawner.brickCount--;
        Destroy(gameObject);
        Audio.PlayOneShot(sound);
    }
}
