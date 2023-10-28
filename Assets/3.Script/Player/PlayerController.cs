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

    [SerializeField] public float movespeed = 15f;
    [SerializeField] public float jumpforce = 8f;
    private bool isjumping = false;
    private bool isdead = false;
    //발걸음 시간
    private float walksoundtime = 0.5f;
    private float walksoundaccumulatetime = 0f;

    //무적
    public bool isinvincible = false;

    //UIManager 참조함
    private UiManager uimanager;

    //사운드 클립 가지고 있어야함
    [SerializeField] private AudioSource player_audiosource;
    [SerializeField] private AudioClip[] player_clips; // 직접 참조함 0:발걸음, 1:점프, 2:아이템 획득, 3:플레이어 사망


    private void Awake()
    {
        player_ani = GetComponent<Animator>();
        player_rb = GetComponent<Rigidbody>();
        uimanager = FindObjectOfType<UiManager>();
        player_audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!isdead)
        {
            PlayerMove();
            PlayerJump();
        }
    }

    private void PlayerMove()
    {
        if(!isjumping)
        {
            float x = Input.GetAxisRaw("Horizontal");

            transform.Translate(new Vector3(x, 0, 0) * movespeed * Time.deltaTime);

            //걷는 사운드
            walksoundaccumulatetime += Time.deltaTime;
            if(walksoundaccumulatetime > walksoundtime)
            {
                Debug.Log(walksoundaccumulatetime);
                walksoundaccumulatetime = 0f;
                PlaySound(0);
            }
            
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

            //점프 사운드
            PlaySound(1);
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
            if(!isinvincible)
            {
                Die();
            }
            else
            {
                return;
            }
        }

        //아이템
        if (col.gameObject.CompareTag("Item"))
        {
            if(col.gameObject.name == "Invincibility") // 무적
            {
                // 3초간 무적
                isinvincible = true;
                // 무적 bool 변수 false 만들기
                Invoke("ChangeInvincibility", 3f);
            }

            if(col.gameObject.name == "Coin") // 코인
            {
                //점수 추가
                
            }
        }
    }

    private void ChangeInvincibility()
    {
        isinvincible = false;
    }

    private void Die()
    {
        PlaySound(3);
        isdead = true;
        this.gameObject.GetComponent<Animator>().enabled = false;
        uimanager.HandleGameOver();
        Invoke("DestroyPlayer", 1f);
    }

    private void PlaySound(int index)
    {
        player_audiosource.clip = player_clips[index];
        player_audiosource.Play();
    }

    private void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }


}
