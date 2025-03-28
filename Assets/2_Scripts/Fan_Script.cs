using System.Collections;
using UnityEngine;

public class Fan_Script : MonoBehaviour
{
    [SerializeField] private SpriteRenderer srdr = null; // 스프라이트 렌더러
    private DataBase_Manager.FanData fanData = null; // 팬 데이터

    // 초기화 함수
    public void Init_Func()
    {
        this.Deactivate_Func(true); // 초기화 시 비활성화 함수 호출
    }

    // 활성화 함수
    public void Activate_Func(DataBase_Manager.FanData _fanData, Vector2 _fanSpawnPos)
    {
        this.fanData = _fanData; // 팬 데이터 설정
        this.transform.position = _fanSpawnPos; // 팬 생성 위치 설정
    }

    // 아이들 애니메이션 함수
    public void OnIdleAni_Func(int _spriteID)
    {
        this.srdr.sprite = this.fanData.idleSpriteArr[_spriteID]; // 아이들 스프라이트 설정
    }

    // 게임 오버 함수
    public void OnGameOver_Func()
    {
        this.srdr.sprite = this.fanData.gameoverSprite; // 게임 오버 스프라이트 설정
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