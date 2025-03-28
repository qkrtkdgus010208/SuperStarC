using UnityEngine;

public class PlayerSystem_Manager : MonoBehaviour
{
    public static PlayerSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private Rigidbody2D rigid = null; // 플레이어의 Rigidbody2D 컴포넌트
    private bool isAlive = false; // 플레이어의 생존 여부

    // 플레이어의 X 위치 반환
    public float GetPosX => this.transform.position.x;

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        this.rigid.gravityScale = 0f; // 중력 스케일 초기화

        this.Deactivate_Func(true); // 초기화 시 비활성화 함수 호출
    }

    // 활성화 함수
    public void Activate_Func()
    {
        this.isAlive = true; // 플레이어 생존 상태 설정
    }

    private void Update()
    {
        if (!isAlive) // 플레이어가 생존 상태가 아니면 업데이트 중지
            return;

        // 스페이스 키 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.rigid.linearVelocity.y == 0f) // 플레이어가 점프 중이 아니면
            {
                this.rigid.gravityScale = DataBase_Manager.Instance.gravityScale; // 중력 스케일 설정
                this.rigid.linearVelocity = Vector2.right * DataBase_Manager.Instance.moveSpeed; // 이동 속도 설정
            }

            this.rigid.linearVelocity = new Vector2(this.rigid.linearVelocity.x, 0f); // 수직 속도 초기화

            float _jumpPower = DataBase_Manager.Instance.jumpPower; // 점프 힘 가져오기
            this.rigid.AddForce(Vector2.up * _jumpPower); // 점프 힘 적용

            SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.Jump); // 점프 사운드 재생
        }

        float _playerPosY = this.transform.position.y; // 플레이어의 Y 위치

        float _heightModifyValue = DataBase_Manager.Instance.heightModifyValue; // 높이 보정 값
        if ((_playerPosY - _heightModifyValue) <= ObstacleSystem_Manager.Instance.GetBottomPosY) // 플레이어가 바닥에 닿았는지 확인
        {
            this.OnGameOver_Func(false); // 게임 오버 처리
        }
        else if ((_playerPosY + _heightModifyValue) >= ObstacleSystem_Manager.Instance.GetTopPosY) // 플레이어가 천장에 닿았는지 확인
        {
            ObstacleSystem_Manager.Instance.OnCollideTop_Func(); // 상단 충돌 처리

            this.OnGameOver_Func(true); // 게임 오버 처리
        }
    }

    // 게임 오버 처리 함수
    public void OnGameOver_Func(bool _isVelocityStop)
    {
        this.isAlive = false; // 플레이어 생존 상태 설정

        if (_isVelocityStop)
            this.rigid.linearVelocity = Vector2.zero; // 속도 초기화
        else
            this.rigid.linearVelocity = new Vector2(0f, this.rigid.linearVelocity.y); // 수평 속도 초기화

        IngameSystem_Manager.Instance.OnGameover_Func(); // 인게임 시스템 매니저의 게임 오버 함수 호출

        SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.Gameover); // 게임 오버 사운드 재생
    }

    // 트리거 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if (!this.isAlive) // 플레이어가 생존 상태가 아니면 처리 중지
            return;

        if (_coll.gameObject.CompareTag("Obstacle")) // 충돌한 오브젝트가 장애물인지 확인
        {
            Obstacle_Scripts _obstacleClass = _coll.gameObject.GetComponent<Obstacle_Scripts>();
            _obstacleClass.OnTriggerEnter_Func(); // 장애물의 트리거 충돌 처리 함수 호출

            this.OnGameOver_Func(true); // 게임 오버 처리
        }
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