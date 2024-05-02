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

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D ��������
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ������ �Է� Ȯ��
        axisH = Input.GetAxisRaw("Horizontal");
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

        // ĳ���� �����ϱ�
        if (Input.GetButtonDown("Jump"))
        {
            Jump(); // ����
        }
    }

	private void FixedUpdate()
	{
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
        if (onGround && goJump)
        {
            // ���� ������ ����Ű ����
            // �����ϱ�
            Debug.Log(" ����! ");
            Vector2 jumpPw = new Vector2(0, jump);        // ������ ���� ���� ����
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);  // �������� �� ���ϱ�
            goJump = false; // ���� �÷��� ����
        }
    }

    // ����
    public void Jump()
    {
        goJump = true; // ���� �÷��� �ѱ�
        Debug.Log(" ���� ��ư ����! ");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
}
