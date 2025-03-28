using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameSystem_Manager : MonoBehaviour
{
    public static IngameSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private Transform sloganGroupTrf = null; // 슬로건 그룹 트랜스폼
    private List<Fan_Script> fanClassList = new List<Fan_Script>(); // 팬 클래스 리스트
    private Coroutine idleCor = null; // 아이들 코루틴
    private int spriteID = 0; // 스프라이트 ID

    // 초기화 함수
    public void Init_Func()
    {
        IngameSystem_Manager.Instance = this; // 싱글톤 인스턴스 설정

        this.fanClassList = new List<Fan_Script>(); // 팬 클래스 리스트 초기화

        this.Deactivate_Func(true); // 초기화 시 비활성화 함수 호출
    }

    // 활성화 함수
    public void Activate_Func()
    {
        SoundSystem_Manager.Instance.PlayBgm_Func(BgmType.Ingame); // 인게임 배경음악 재생

        StartCoroutine(this.OnSpawn_Cor()); // 스폰 코루틴 시작

        this.idleCor = StartCoroutine(this.OnIdle_Cor()); // 아이들 코루틴 시작
    }

    // 팬과 슬로건을 스폰하는 코루틴
    private IEnumerator OnSpawn_Cor()
    {
        // 팬 스폰 관련 변수 초기화
        float _fanInterval = -DataBase_Manager.Instance.fanSpawnOffsetX - 2f; // 팬 스폰 간격 초기값 설정
        float _fanSpawnInterval = DataBase_Manager.Instance.fanSpawnInterval; // 팬 스폰 간격
        DataBase_Manager.FanData[] _fanDataArr = DataBase_Manager.Instance.fanDataArr; // 팬 데이터 배열

        // 슬로건 스폰 관련 변수 초기화
        float _sloganInterval = -DataBase_Manager.Instance.sloganSpawnOffsetX; // 슬로건 스폰 간격 초기값 설정
        float _sloganSpawnInterval = DataBase_Manager.Instance.sloganSpawnInterval; // 슬로건 스폰 간격
        DataBase_Manager.SloganData[] _sloganDataArr = DataBase_Manager.Instance.sloganDataArr; // 슬로건 데이터 배열

        while (true)
        {
            // 플레이어의 X 좌표를 가져옴
            float _playerPosX = PlayerSystem_Manager.Instance.GetPosX;

            // 팬 스폰
            if (_fanInterval < _playerPosX)
            {
                // 랜덤한 팬 데이터를 선택
                DataBase_Manager.FanData _fanData = _fanDataArr[Random.Range(0, _fanDataArr.Length)];

                // 팬 클래스 인스턴스 생성 및 리스트에 추가
                Fan_Script _baseFanClass = DataBase_Manager.Instance.baseFanClass;
                Fan_Script _fanClass = GameObject.Instantiate<Fan_Script>(_baseFanClass);
                this.fanClassList.Add(_fanClass);

                // 팬 스폰 위치 설정
                float _fanSpawnPosX = _fanInterval + DataBase_Manager.Instance.fanSpawnOffsetX;
                Vector2 _fanSpawnPos = new Vector2(_fanSpawnPosX, DataBase_Manager.Instance.fanSpawnPosY);
                _fanClass.Activate_Func(_fanData, _fanSpawnPos);

                // 다음 팬 스폰 간격 설정
                _fanInterval += _fanSpawnInterval;
            }

            // 슬로건 스폰
            if (_sloganInterval < _playerPosX)
            {
                // 랜덤한 슬로건 데이터를 선택
                DataBase_Manager.SloganData _sloganData = _sloganDataArr[Random.Range(0, _sloganDataArr.Length)];

                // 슬로건 클래스 인스턴스 생성
                Slogan_Scripts _baseSloganClass = DataBase_Manager.Instance.baseSloganClass;
                Slogan_Scripts _sloganClass = GameObject.Instantiate<Slogan_Scripts>(_baseSloganClass);

                // 슬로건 스폰 위치 설정
                float _sloganSpawnPosX = _sloganInterval + DataBase_Manager.Instance.sloganSpawnOffsetX;
                Vector2 _sloganSpawnPos = new Vector2(_sloganSpawnPosX, this.sloganGroupTrf.position.y);
                _sloganClass.Activate_Func(_sloganData, _sloganSpawnPos);

                // 다음 슬로건 스폰 간격 설정
                _sloganInterval += _sloganSpawnInterval;
            }

            yield return null; // 다음 프레임까지 대기
        }
    }

    // 팬의 아이들 애니메이션을 처리하는 코루틴
    private IEnumerator OnIdle_Cor()
    {
        while (true)
        {
            this.spriteID++; // 스프라이트 ID 증가

            // 스프라이트 ID가 최대값을 넘으면 0으로 초기화
            if (DataBase_Manager.Instance.GetFanIdleAniSpriteNum <= this.spriteID)
                this.spriteID = 0;

            // 모든 팬 클래스에 대해 아이들 애니메이션 업데이트
            foreach (Fan_Script _fanClass in this.fanClassList)
                _fanClass.OnIdleAni_Func(this.spriteID);

            yield return new WaitForSeconds(DataBase_Manager.Instance.fanAnimationInterval); // 애니메이션 간격만큼 대기
        }
    }

    // 게임 오버 처리 함수
    public void OnGameover_Func()
    {
        StopCoroutine(this.idleCor); // 아이들 코루틴 정지

        this.idleCor = null;

        // 모든 팬 클래스에 대해 게임 오버 애니메이션 업데이트
        foreach (Fan_Script _fanClass in fanClassList)
        {
            _fanClass.OnGameOver_Func();
        }

        GameSystem_Manager.Instance.OnGameOver_Func(); // 게임 시스템 매니저의 게임 오버 함수 호출

        SoundSystem_Manager.Instance.StopBgm_Func(); // 배경음악 정지
    }

    // 비활성화 함수
    public void Deactivate_Func(bool _isInit = false)
    {
        if (!_isInit)
        {
            // 초기화가 아닌 경우 추가적인 비활성화 로직을 여기에 추가
        }
    }
}