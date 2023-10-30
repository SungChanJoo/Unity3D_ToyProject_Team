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
    public bool isjumping = false;
    private bool isdead = false;
    //�߰��� �ð�
    private float walksoundtime = 0.5f;
    private float walksoundaccumulatetime = 0f;

    //�÷��̾� x�� �̵����ɹ���
    public float player_limitleft = -4.6f;
    public float player_limitright = 4.6f;

    //����
    public bool isinvincible = false;
    private Vector3 scaleup = new Vector3(2, 2, 2);

    //����
    private int coin_count = 0;

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
        if (!isdead)
        {
            PlayerMove();
            PlayerJump();
            PlayerRayToGround();
        }
    }

    private void PlayerMove()
    {
        

        
        if (!isjumping)
        {
            float x = Input.GetAxisRaw("Horizontal");

            if (!(transform.position.x < player_limitleft || transform.position.x > player_limitright))
            {
                transform.Translate(new Vector3(x, 0, 0) * movespeed * Time.deltaTime);
            }
            else
            {
                if (transform.position.x <= player_limitleft)
                {
                    transform.position += new Vector3(0.1f, 0, 0);
                }
                else if (transform.position.x >= player_limitright)
                {
                    transform.position += new Vector3(-0.1f, 0, 0);
                }
            }

            //�ȴ� ����
            walksoundaccumulatetime += Time.deltaTime;
            if (walksoundaccumulatetime > walksoundtime)
            {
                walksoundaccumulatetime = 0f;
                PlaySound(0);
            }

        }

    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isjumping)
        {
            //StartCoroutine(PlayerJump_co());
            isjumping = true;
            player_ani.SetBool("isJump", true);
            player_rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

            //���� ����
            PlaySound(1);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Object"))
        {
            if (!isinvincible)
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
            if (col.gameObject.name == "Star") // ����
            {
                // 3�ʰ� ����
                isinvincible = true;
                transform.localScale = scaleup;
                // ���� bool ���� false �����
                Invoke("ChangeInvincibility", 3f);
            }

            if (col.gameObject.name == "CoinGold") // ����
            {
                //���� �߰�
                coin_count++;
                uimanager.AddCoinScore(coin_count * 10);
                coin_count = 0;
            }
        }
    }

    private void PlayerRayToGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            if (hit.transform.gameObject.CompareTag("Ground"))
            {
                isjumping = false;
                player_ani.SetBool("isJump", false);
            }
        }
        else
        {
            isjumping = true;
            player_ani.SetBool("isJump", true);
        }

    }


    private void ChangeInvincibility()
    {
        isinvincible = false;
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Die()
    {
        PlaySound(3);
        isdead = true;
        this.gameObject.GetComponent<Animator>().enabled = false;
        Invoke("DestroyPlayer", 1f);
        uimanager.HandleGameOver();
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