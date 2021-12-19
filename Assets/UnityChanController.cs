using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody MyRigidbody;
    private float velocityZ = 16f;
    private float velocityX = 10f;
    private float velocityY = 10f;
    private float movableRange = 3.4f;
    private float coefficient = 0.99f;
    private bool isEnd = false;
    private GameObject stateText;
    private GameObject scoreText;
    private int score = 0;
    private bool isLButtonDown = false;
    private bool isRButtonDown = false;
    private bool isJButtonDown = false;
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
        this.myAnimator.SetFloat("Speed", 1);
        this.MyRigidbody = GetComponent<Rigidbody>();
        this.stateText = GameObject.Find("GameResultText");
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isEnd)
        {
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.velocityZ *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }
        float inputvelocityX = 0f;
        float inputvelocityY = 0f;
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            inputvelocityX = -this.velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            inputvelocityX = this.velocityX;
        }
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            inputvelocityY = velocityY;
        }
        else
        {
            inputvelocityY = this.MyRigidbody.velocity.y;
        }
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        this.MyRigidbody.velocity = new Vector3(inputvelocityX, inputvelocityY, this.velocityZ);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAMEOVER";
        }
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }
        if (other.gameObject.tag == "CoinTag")
        {
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";
        }
    }
        public void GetMyJumpButtonDown()
        {
            this.isJButtonDown = true;
        }
        public void GetMyJumpButtonUp()
        {
            this.isJButtonDown = false;
        }
        public void GetMyLeftButtonDown()
        {
            this.isLButtonDown = true;
        }
        public void GetMyLeftButtUp()
        {
            this.isLButtonDown = false;
        }
        public void GetMyRightButtonDown()
        {
            this.isRButtonDown = true;
        }
        public void GetMyRightButtonUp()
        {
            this.isRButtonDown = false;
        }

}
