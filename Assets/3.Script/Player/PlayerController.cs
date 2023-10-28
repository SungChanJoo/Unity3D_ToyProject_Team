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
    [SerializeField] public float jumpforce = 8f;
    private bool isjumping = false;
    private bool isdead = false;
    //�߰��� �ð�
    private float walksoundtime = 0.5f;
    private float walksoundaccumulatetime = 0f;

    //����
    public bool isinvincible = false;

    //UIManager ������
    private UiManager uimanager;

    //���� Ŭ�� ������ �־����
    [SerializeField] private AudioSource player_audiosource;
    [SerializeField] private AudioClip[] player_clips; // ���� ������ 0:�߰���, 1:����, 2:������ ȹ��, 3:�÷��̾� ���


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

            //�ȴ� ����
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

            //���� ����
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

        //������
        if (col.gameObject.CompareTag("Item"))
        {
            if(col.gameObject.name == "Invincibility") // ����
            {
                // 3�ʰ� ����
                isinvincible = true;
                // ���� bool ���� false �����
                Invoke("ChangeInvincibility", 3f);
            }

            if(col.gameObject.name == "Coin") // ����
            {
                //���� �߰�
                
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
