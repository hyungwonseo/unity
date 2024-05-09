using System.Collections.Generic;
using UnityEngine;

//BGM 종류
public enum BGMType
{
    None,       //없음
    Title,      //타이틀
    InGame,     //게임 중
    InBoss,     //보스전
}
//SE 종류
public enum SEType
{
    GameClear,  //게임 클리어
    GameOver,   //게임 오버
    Shoot,      //활 쏘기

    DoorOpen,   //문 열기
    DoorClosed, //닫힌 문
    ItemGet,    //아이템 획득
    GetDamage,  //데미지
    EnemyKilled,//적 사망
    BossKilled, //보스 사망
    Button,     //버튼 눌림
}

public class SoundManager : MonoBehaviour
{
    public AudioClip bgmInTitle;    //타이틀 BGM
    public AudioClip bgmInGame;     //게임 중 BGM
    public AudioClip bgmInBoss;     //보스전 BGM

    public AudioClip meGameClear;   //게임 클리어
    public AudioClip meGameOver;    //게임 오버
    public AudioClip seShoot;       //활 쏘기

    public AudioClip seDoorOpen;    //문 열기
    public AudioClip seDoorClosed;  //닫힌 문
    public AudioClip seItemGet;     //아이템 획득
    public AudioClip seDamage;      //데미지
    public AudioClip seEnemyDead;   //적 사망
    public AudioClip seBossDead;    //보스 사망
    public AudioClip seButton;      //버튼 눌림

    public static SoundManager soundManager;    //첫 SoundManager를 갖는 변수

    public static BGMType plyingBGM = BGMType.None;    //재생 중인 BGM

    private void Awake()
    {
        //BGM 재생
        if (soundManager == null)
        {
            soundManager = this;  //static 변수에 자기 자신을 저장
            //씬이 바뀌어도 게임 오브젝트를 파기하지 않음
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//게임 오브젝트르 파기
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //BGM 설정
    public void PlayBgm(BGMType type)
    {
        if (type != plyingBGM)
        {
            plyingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.Title)
            {
                audio.clip = bgmInTitle;    //타이틀
            }
            else if (type == BGMType.InGame)
            {
                audio.clip = bgmInGame;     //게임 중
            }
            else if (type == BGMType.InBoss)
            {
                audio.clip = bgmInBoss;     //보스전
            }
            audio.Play();
        }
    }
    //BGM정지
    public void StopBgm()
    {
        GetComponent<AudioSource>().Stop();
        plyingBGM = BGMType.None;
    }

    //SE재생
    public void SEPlay(SEType type)
    {
        if (type == SEType.GameClear)
        {
            GetComponent<AudioSource>().PlayOneShot(meGameClear);   //게임 클리어
        }
        else if (type == SEType.GameOver)
        {
            GetComponent<AudioSource>().PlayOneShot(meGameOver);   //게임 오버
        }
        else if (type == SEType.Shoot)
        {
            GetComponent<AudioSource>().PlayOneShot(seShoot);       //활 쏘기
        }
        else if (type == SEType.DoorClosed)
        {
            GetComponent<AudioSource>().PlayOneShot(seDoorClosed);  //닫힌 문
        }
        else if (type == SEType.DoorOpen)
        {
            GetComponent<AudioSource>().PlayOneShot(seDoorOpen);    //문 열기
        }
        else if (type == SEType.ItemGet)
        {
            GetComponent<AudioSource>().PlayOneShot(seItemGet);     //아이템 획득
        }
        else if (type == SEType.GetDamage)
        {
            GetComponent<AudioSource>().PlayOneShot(seDamage);      //데미지
        }
        else if (type == SEType.EnemyKilled)
        {
            GetComponent<AudioSource>().PlayOneShot(seEnemyDead);   //적 사망
        }
        else if (type == SEType.BossKilled)
        {
            GetComponent<AudioSource>().PlayOneShot(seBossDead);    //보스 사망
        }
        else if (type == SEType.Button)
        {
            GetComponent<AudioSource>().PlayOneShot(seButton);      //버튼 눌림
        }
    }

}
