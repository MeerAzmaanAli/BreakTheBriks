using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //script References
    [SerializeField] BrickSpawner brickSpawner;
    [SerializeField] BallMovement ballMovement;
    [SerializeField] PaddleMovement paddleMovement;

    //UI elements
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject tiltButton;
    [SerializeField] GameObject sliderButton;
    [SerializeField] GameObject Title;
    [SerializeField] GameObject tiltTick;
    [SerializeField] GameObject sliderTick;
    [SerializeField] GameObject WinText;
    [SerializeField] GameObject reStartButton;
    [SerializeField] TextMeshProUGUI brickLeft;

    //transforms
    [SerializeField] Transform ballStartPos;
    [SerializeField] Transform paddleStartPos;

    int level = 1;
    int r=0;
    int c=0;

    bool gameStarted = false;


    private void Update()
    {
        if (gameStarted && brickSpawner.spawnedBricks.Count <= 0)
        {
            winner();
        }
        brickLeft.text = "Bricks Left: "+ brickSpawner.brickCount.ToString();
    }
    public void StartGame()
    {
        gameStarted = false;
        Reset();
        if (PlayerPrefs.GetInt("level") == 0) level = 1;
        else level = PlayerPrefs.GetInt("level");

        if (level == 1)
        {
            PlayerPrefs.SetInt("level",level);
            r = 1; c = 2;
        }
        else
        {
            r = PlayerPrefs.GetInt("r", r); c = PlayerPrefs.GetInt("c", c);
        }
        BombBricksNo(level);
        brickSpawner.SpawnBricks(r, c);
        ComponentToggle(true);
        UItoggle(false);

        ballMovement.Launch();
        reStartButton.SetActive(true);
        gameStarted = true;  
    }
    public void NextGame()
    {
        Reset();
        level++;
        PlayerPrefs.SetInt("level", level);

        r = PlayerPrefs.GetInt("r")+1;
        c = PlayerPrefs.GetInt("c") + 2;

        if (r > 5) r = 5;
        if(c > 11)c=11;

        PlayerPrefs.SetInt("r", r);
        PlayerPrefs.SetInt("c", c);
        BombBricksNo(level);
        brickSpawner.SpawnBricks(r, c);
        ComponentToggle(true);

        UItoggle(false);
        nextButton.SetActive(false);
        WinText.SetActive(false);
        
        ballMovement.Launch();
        reStartButton.SetActive(true);
        gameStarted = true;
    }
    void winner()
    {
        Reset();
        ComponentToggle(false);

        UItoggle(true);

        startButton.SetActive(false);
        nextButton.SetActive(true);
        WinText.SetActive(true);
        reStartButton.SetActive(false);
        gameStarted = false;
    }

    public void GameOver()
    {
        Reset();
        UItoggle(true);
        ComponentToggle(false);

        startButton.SetActive(true);
        nextButton.SetActive(false);
        reStartButton.SetActive(false);
        gameStarted = false;
    }

    public void Tilt()
    {
        paddleMovement.usetilt = !paddleMovement.usetilt;
        paddleMovement.useSlider = false;

        if (paddleMovement.usetilt)
        {
            tiltTick.SetActive(true);
            sliderTick.SetActive(false);
        }
        else tiltTick.SetActive(false);     
    }

    public void slider()
    {
        paddleMovement.useSlider = !paddleMovement.useSlider;
        paddleMovement.usetilt = false;

        if (paddleMovement.useSlider)
        {
            sliderTick.SetActive(true);
            tiltTick.SetActive(false);
            paddleMovement.paddleSlider.gameObject.SetActive(true);

        }
        else 
        { 
            sliderTick.SetActive(false);
            paddleMovement.paddleSlider.gameObject.SetActive(false);
        }
    }

    void UItoggle(bool b)
    {
        
        startButton.SetActive(b);
        tiltButton.SetActive(b);
        sliderButton.SetActive(b);
        Title.SetActive(b);
        brickLeft.gameObject.SetActive(!b);
    }
    void ComponentToggle(bool b)
    {
        ballMovement.enabled = b;
        paddleMovement.enabled = b;
    }
    private void Reset()
    {
        ballMovement.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;             //stops the ball
        ballMovement.gameObject.transform.position = ballStartPos.position;                 // reset position of balll and paddle
        paddleMovement.gameObject.transform.position = paddleStartPos.transform.position;
        foreach (GameObject G in brickSpawner.spawnedBricks)
        {
            Destroy(G);
        }
        brickSpawner.spawnedBricks.Clear();
        brickSpawner.explosiveBrickCount = 0;
        brickSpawner.brickCount = 0;
    }

    void BombBricksNo(int level)
    {
        if(level <= 2)
        {
            brickSpawner.maxExplosiveBricks= 0;
        }else if(level > 2 && level <= 4)
        {
            brickSpawner.maxExplosiveBricks = 2;
        }else if (level > 4 && level <= 6)
        {
            brickSpawner.maxExplosiveBricks = 3;
        }
        else{
            brickSpawner.maxExplosiveBricks = 4;
        }
    }
}
