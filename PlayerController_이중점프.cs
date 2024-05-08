using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;          // Rigidbody2D ���� ����
    float axisH = 0.0f;         // �Է�
    public float speed = 3.0f;  // �̵��ӵ�

    public float jump = 9.0f;       // ������
    public LayerMask groundLayer;   // ������ �� �ִ� ���̾�
    bool goJump = false;            // ���� ���� �÷���
    bool onGround = false;          // ���鿡 ���ִ� �÷���

    // �ִϸ��̼� ó��
    Animator animator; // �ִϸ�����
    public string stopAnime = "PlayerIdle";
    public string moveAnime = "PlayerRun";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerClear";
    public string deadAnime = "GameOver";
    string nowAnime = "";
    string oldAnime = "";

    public static string gameState = "playing"; // ���� ����

    public int score = 0; // ����

    bool isMoving = false;

    // ��������
    public bool doubleJump = false;
    int jumpCount = 0;
    float jumpInterval = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D ��������
        rbody = this.GetComponent<Rigidbody2D>();
        // Animator ��������
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        gameState = "playing"; // ���� ��
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing")
        {
            return;
        }
        // �̵�
        if (isMoving == false)
        {
            // ���� ������ �Է� Ȯ��
            axisH = Input.GetAxisRaw("Horizontal");
        }
        // ���� ����
        if (axisH > 0.0f)
        {
            // ������ �̵�
            //Debug.Log("������ �̵�");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            // ���� �̵�
            //Debug.Log("���� �̵�");
            transform.localScale = new Vector2(-1, 1);   // �¿� ���� ��Ű��
        }

        jumpInterval = jumpInterval + Time.deltaTime;
        // ĳ���� �����ϱ�
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpInterval > 0.5)
            {
                jumpInterval = 0f;
                Jump(); // ����
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
        // ���� ����
        onGround = Physics2D.Linecast(transform.position,
                                      transform.position - (transform.up * 0.1f),
                                      groundLayer);
        if (onGround || axisH != 0)
        {
            // ������ or �ӵ��� 0 �ƴ�
            // �ӵ� ����
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        if ((doubleJump && goJump) || (!doubleJump && onGround && goJump))
        {
            // ���� ������ ����Ű ����
            // �����ϱ�
            Debug.Log(" ����! ");
            Vector2 jumpPw = new Vector2(0, jump);        // ������ ���� ���� ����
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);  // �������� �� ���ϱ�
            goJump = false; // ���� �÷��� ����
        }
        if (onGround)
        {
            jumpCount = 0;
            // ���� ��
            if (axisH == 0)
            {
                nowAnime = stopAnime;       // ����
            }
            else
            {
                nowAnime = moveAnime;       // �̵�
            }
        }
        else
        {
            // ����
            nowAnime = jumpAnime;
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);        // �ִϸ��̼� ���
        }
    }

    // ����
    public void Jump()
    {
        jumpCount = jumpCount + 1;
        if (jumpCount < 2)
        {
            goJump = true; // ���� �÷��� �ѱ�
        }        
        Debug.Log(" ���� ��ư ����! ");
    }

    // ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();        // ��
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();     // ���� ����
        }
        else if (collision.gameObject.tag == "ScoreItem")
        {
            // ���� ������
            // ItemData ��������
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            // ���� ���
            score = item.value;
            // ������ ����
            Destroy(collision.gameObject);
        }
    }
    // ��
    public void Goal()
    {
        animator.Play(goalAnime);
        gameState = "gameclear";
        GameStop(); // �÷��̾��� ������ ����(�ӵ� = 0)
    }
    // ���� ����
    public void GameOver()
    {
        animator.Play(deadAnime);
        gameState = "gameover";
        GameStop(); // �÷��̾��� ������ ����(�ӵ� = 0)
        // =====================
        // ���� ���� ����
        // =====================
        // �÷��̾��� �浹 ���� ��Ȱ��
        GetComponent<CapsuleCollider2D>().enabled = false;
        // �÷��̾ ���� Ƣ�� ������ �ϴ� ����
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    // ���� ����
    void GameStop()
    {
        // �ӵ��� 0���� �Ͽ� ���� ����
        rbody.velocity = new Vector2(0, 0);
    }

    // ��ġ ��ũ�� �߰� ����
    public void SetAxis(float h, float v)
    {
        axisH = h;
        if (axisH == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
