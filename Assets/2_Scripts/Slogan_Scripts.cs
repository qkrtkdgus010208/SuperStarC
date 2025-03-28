using UnityEngine;

public class Slogan_Scripts : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srdr = null; // 스프라이트 렌더러

    // 초기화 함수
    public void Init_Func()
    {
        this.Deactivate_Func(true); // 초기화 시 비활성화 함수 호출
    }

    // 활성화 함수
    public void Activate_Func(DataBase_Manager.SloganData _sloganData, Vector2 _sloganSpawnPos)
    {
        this.srdr.sprite = _sloganData.sprite; // 슬로건 스프라이트 설정

        this.transform.position = _sloganSpawnPos; // 슬로건 스폰 위치 설정
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