using System.Collections;
using UnityEngine;

public class CameraSystem_Manager : MonoBehaviour
{
    public static CameraSystem_Manager Instance; // 싱글톤 인스턴스

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        this.Deactivate_Func(true); // 초기화 시 비활성화 함수 호출
    }

    // 활성화 함수
    public void Activate_Func()
    {
        StartCoroutine(OnFollow_Cor()); // 카메라 추적 코루틴 시작
    }

    // 카메라 추적 코루틴
    private IEnumerator OnFollow_Cor()
    {
        while (true)
        {
            // 플레이어의 X 좌표를 가져와서 카메라 위치 설정
            float _playerPosX = PlayerSystem_Manager.Instance.GetPosX;
            this.transform.position = new Vector2(_playerPosX, this.transform.position.y);

            yield return null; // 다음 프레임까지 대기
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