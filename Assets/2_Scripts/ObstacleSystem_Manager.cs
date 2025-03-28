using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSystem_Manager : MonoBehaviour
{
    public static ObstacleSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private Transform[] pivotTrfArr = null; // 장애물 스폰 위치를 위한 피벗 트랜스폼 배열
    [SerializeField] private Animation topCollideAnim = null; // 상단 충돌 애니메이션

    private List<Obstacle_Scripts> obstacleList; // 장애물 리스트

    // 상단 피벗 위치 Y 반환
    public float GetTopPosY => this.pivotTrfArr[0].position.y;
    // 하단 피벗 위치 Y 반환
    public float GetBottomPosY => this.pivotTrfArr[1].position.y;

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        this.obstacleList = new List<Obstacle_Scripts>(); // 장애물 리스트 초기화

        this.Deactivate_Func(true); // 초기화 시 비활성화 함수 호출
    }

    // 활성화 함수
    public void Activate_Func()
    {
        StartCoroutine(this.OnSpawn_Cor()); // 장애물 스폰 코루틴 시작
    }

    // 장애물 스폰 코루틴
    private IEnumerator OnSpawn_Cor()
    {
        float _obstacleSpawnInterval = DataBase_Manager.Instance.obstacleSpawnInterval; // 장애물 스폰 간격
        float _interval = 0f; // 스폰 간격 초기값
        int _spawnCount = 0; // 스폰된 장애물 수

        while (true)
        {
            float _playerPosX = PlayerSystem_Manager.Instance.GetPosX; // 플레이어의 X 좌표

            // 장애물 스폰
            if (_interval < _playerPosX)
            {
                _interval += _obstacleSpawnInterval; // 다음 스폰 간격 설정

                // 현재 레벨 데이터 가져오기
                DataBase_Manager.LevelData[] _levelDataArr = DataBase_Manager.Instance.levelDataArr;
                DataBase_Manager.LevelData _currentLevelData = null;
                foreach (DataBase_Manager.LevelData _levelData in _levelDataArr)
                {
                    if (_levelData.conditionNum <= _spawnCount)
                    {
                        _currentLevelData = _levelData;
                        break;
                    }
                }

                // 랜덤한 장애물 ID 선택
                int _randID = Random.Range(0, _currentLevelData.obstacleIDArr.Length);
                int _obstacleID = _currentLevelData.obstacleIDArr[_randID];
                Obstacle_Scripts _baseObstacleClass = DataBase_Manager.Instance.ObstacleClassArr[_obstacleID];
                Obstacle_Scripts _obstacleClass = GameObject.Instantiate<Obstacle_Scripts>(_baseObstacleClass);
                this.obstacleList.Add(_obstacleClass);

                // 장애물 스폰 위치 설정
                float _obstacleSpawnPosX = _playerPosX + DataBase_Manager.Instance.obstacleSpawnOffsetX;

                int _pivotID = 0; // 기본 피벗 ID는 0 (상단)
                float _obstacleSpawnPosY = 0; // 기본 장애물 스폰 위치 Y는 0
                if (Random.value < 0.5f) // 50% 확률로 하단 피벗 선택
                {
                    _pivotID = 1; // 하단 피벗 ID는 1
                    _obstacleSpawnPosY = _obstacleClass.GetHeight; // 장애물 높이만큼 Y 위치 조정
                }

                Transform _pivotTrf = this.pivotTrfArr[_pivotID]; // 선택된 피벗 트랜스폼
                _obstacleSpawnPosY = _pivotTrf.position.y + _obstacleSpawnPosY; // 피벗 위치에 장애물 위치 더하기

                Vector2 _pos = new Vector2(_obstacleSpawnPosX, _obstacleSpawnPosY); // 최종 스폰 위치 설정
                _obstacleClass.Activate_Func(_pos); // 장애물 활성화

                _spawnCount++; // 스폰된 장애물 수 증가
            }

            // 장애물 통과 여부 확인
            foreach (Obstacle_Scripts _obstacleClass in this.obstacleList)
            {
                if (_obstacleClass.IsPassed) // 이미 통과한 장애물은 건너뜀
                    continue;

                float _obstaclePosX = _obstacleClass.GetPosX; // 장애물의 X 위치
                if (_obstaclePosX < _playerPosX) // 플레이어가 장애물을 통과했는지 확인
                {
                    _obstacleClass.OnPass_Func(); // 장애물 통과 처리 함수 호출
                }
            }

            yield return null; // 다음 프레임까지 대기
        }
    }

    // 상단 충돌 처리 함수
    public void OnCollideTop_Func()
    {
        this.topCollideAnim.Play(); // 상단 충돌 애니메이션 재생
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