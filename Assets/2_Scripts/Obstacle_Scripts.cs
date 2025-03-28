using UnityEngine;

public class Obstacle_Scripts : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCol = null; // 박스 콜라이더
    [SerializeField] private int score = 1; // 장애물 점수
    [SerializeField] private GameObject[] lightObjArr = null; // 라이트 오브젝트 배열
    private bool isPassed = false; // 장애물 통과 여부

    // 장애물 높이 반환
    public float GetHeight => this.boxCol.size.y;
    // 장애물 X 위치 반환
    public float GetPosX => this.transform.position.x;
    // 장애물 통과 여부 반환
    public bool IsPassed => isPassed;

    // 초기화 함수
    public void Init_Func()
    {
        this.Deactivate_Func(true); // 초기화 시 비활성화 함수 호출
    }

    // 활성화 함수
    public void Activate_Func(Vector2 _pos)
    {
        this.transform.position = _pos; // 장애물 위치 설정

        this.isPassed = false; // 장애물 통과 여부 초기화

        // 라이트 오브젝트 비활성화
        foreach (GameObject _lightObj in lightObjArr)
            _lightObj.SetActive(false);
    }

    // 트리거 충돌 처리 함수
    public void OnTriggerEnter_Func()
    {
        // 트리거 충돌 시 처리 로직 추가
    }

    // 장애물 통과 처리 함수
    public void OnPass_Func()
    {
        this.isPassed = true; // 장애물 통과 여부 설정

        GameSystem_Manager.Instance.AddScore_Func(this.score); // 점수 추가

        // 라이트 오브젝트 활성화
        foreach (GameObject _lightObj in lightObjArr)
            _lightObj.SetActive(true);

        SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.Flicker); // 효과음 재생
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