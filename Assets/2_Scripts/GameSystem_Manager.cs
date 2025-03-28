using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem_Manager : MonoBehaviour
{
    public static GameSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private DataBase_Manager dbManager = null; // 데이터베이스 매니저
    [SerializeField] private ObstacleSystem_Manager obstacleSystem_Manager = null; // 장애물 시스템 매니저
    [SerializeField] private PlayerSystem_Manager playerSystem_Manager = null; // 플레이어 시스템 매니저
    [SerializeField] private CameraSystem_Manager cameraSystem_Manager = null; // 카메라 시스템 매니저
    [SerializeField] private IngameSystem_Manager ingameSystem_Manager = null; // 인게임 시스템 매니저
    [SerializeField] private GameObject titleObj = null; // 타이틀 오브젝트
    [SerializeField] private TextMeshProUGUI scoreTmp = null; // 점수 텍스트
    [SerializeField] private GameObject retryObj = null; // 재시도 오브젝트
    [SerializeField] private SoundSystem_Manager soundSystem_Manager = null; // 사운드 시스템 매니저
    [SerializeField] private Animation scoreRenewAnim = null; // 애니메이션 컴포넌트

    private int score = 0; // 점수

    public void Awake()
    {
        this.Init_Func(); // 초기화 함수 호출
    }

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        // 각 시스템 매니저 초기화 함수 호출
        this.dbManager.Init_Func();
        this.obstacleSystem_Manager.Init_Func();
        this.playerSystem_Manager.Init_Func();
        this.cameraSystem_Manager.Init_Func();
        this.ingameSystem_Manager.Init_Func();
        this.soundSystem_Manager.Init_Func();

        this.titleObj.SetActive(true); // 타이틀 오브젝트 활성화

        SoundSystem_Manager.Instance.PlayBgm_Func(BgmType.Title); // 타이틀 배경음악 재생

        this.Deactivate_Func(true); // 초기화 시 비활성화 함수 호출
    }

    // 활성화 함수
    public void Activate_Func()
    {
        // 각 시스템 매니저 활성화 함수 호출
        this.obstacleSystem_Manager.Activate_Func();
        this.playerSystem_Manager.Activate_Func();
        this.cameraSystem_Manager.Activate_Func();
        this.ingameSystem_Manager.Activate_Func();

        this.score = 0; // 점수 초기화
        this.scoreTmp.text = "0"; // 점수 텍스트 초기화

        this.retryObj.SetActive(false); // 재시도 오브젝트 비활성화
    }

    // 점수 추가 함수
    public void AddScore_Func(int _score)
    {
        this.score += _score; // 점수 추가

        this.scoreTmp.text = this.score.ToString(); // 점수 텍스트 업데이트

        this.scoreRenewAnim.Play(); // 점수 애니메이션 재생

        // 랜덤으로 군중 효과음 재생
        float _crowdSfxPer = _score / 7f;
        if (Random.value < _crowdSfxPer)
        {
            int _sfxID = Random.Range(0, 3);
            SfxType _sfxType = SfxType.None;
            switch (_sfxID)
            {
                case 0:
                    _sfxType = SfxType.Crowd1;
                    break;
                case 1:
                    _sfxType = SfxType.Crowd2;
                    break;
                case 2:
                    _sfxType = SfxType.Crowd3;
                    break;
            }

            SoundSystem_Manager.Instance.PlaySfx_Func(_sfxType); // 효과음 재생
        }
    }

    // 비활성화 함수
    public void Deactivate_Func(bool _isInit = false)
    {
        if (!_isInit)
        {
            // 각 시스템 매니저 비활성화 함수 호출
            this.obstacleSystem_Manager.Deactivate_Func();
            this.playerSystem_Manager.Deactivate_Func();
            this.cameraSystem_Manager.Deactivate_Func();
            this.ingameSystem_Manager.Deactivate_Func();
        }
    }

    // 게임 오버 함수
    public void OnGameOver_Func()
    {
        this.retryObj.SetActive(true); // 재시도 오브젝트 활성화
    }

    // 게임 시작 버튼 함수
    public void CallBtn_GameStart_Func()
    {
        this.Activate_Func(); // 활성화 함수 호출

        GameObject.Destroy(this.titleObj); // 타이틀 오브젝트 삭제
    }

    // 재시도 버튼 함수
    public void CallBtn_Retry_Func()
    {
        SceneManager.LoadScene(0); // 씬 다시 로드
    }
}