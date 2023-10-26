using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     플레이어 조작하는 스크립트
     맵이 스크롤링하므로 플레이어는 좌우만 움직이도록 할 것임
     Rigidbody, Collider, Animator 컴포넌트 추가
     
     */

    [SerializeField] private Animator player_ani;
    [SerializeField] private Rigidbody player_rb;

    private float movespeed = 2f;
    private bool isjumping = false;
    private float jumpforce = 8f;

    private void Awake()
    {
        player_ani = GetComponent<Animator>();
        player_rb = GetComponent<Rigidbody>();
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
            Vector3 playerpos = transform.position + new Vector3(x, 0, 0);

            player_rb.MovePosition(playerpos);
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
    }

}
