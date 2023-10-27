using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     �÷��̾� �����ϴ� ��ũ��Ʈ
     ���� ��ũ�Ѹ��ϹǷ� �÷��̾�� �¿츸 �����̵��� �� ����
     Rigidbody, Collider, Animator ������Ʈ �߰�
     */

    [SerializeField] private Animator player_ani;
    [SerializeField] private Rigidbody player_rb;

    [SerializeField] public float movespeed = 15f;
    private bool isjumping = false;
    [SerializeField] public float jumpforce = 8f;

    //UIManager ������
    private UiManager uimanager;

    private void Awake()
    {
        player_ani = GetComponent<Animator>();
        player_rb = GetComponent<Rigidbody>();
        uimanager = FindObjectOfType<UiManager>();
    }

    private void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    private void PlayerMove()
    {
        if(!isjumping)
        {
            float x = Input.GetAxisRaw("Horizontal");
            //Vector3 playerpos = transform.position + new Vector3(x, 0, 0);

            transform.Translate(new Vector3(x, 0, 0) * movespeed * Time.deltaTime);
        }
    }

    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isjumping)
        {
            //StartCoroutine(PlayerJump_co());
            isjumping = true;
            player_ani.SetBool("isJump", true);
            player_rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            isjumping = false;
            player_ani.SetBool("isJump", false);
        }

        if(col.gameObject.CompareTag("Object"))
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
        uimanager.HandleGameOver();
    }

}
