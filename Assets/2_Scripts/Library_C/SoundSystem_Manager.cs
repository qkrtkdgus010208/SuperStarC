using UnityEngine;

public class SoundSystem_Manager : MonoBehaviour
{
    public static SoundSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private AudioSource bgmAS = null; // 배경음악(AudioSource) 컴포넌트
    [SerializeField] private AudioSource[] sfxAsArr = null; // 효과음(AudioSource) 컴포넌트 배열

    private int SfxAsID; // 현재 사용 중인 효과음 AudioSource의 인덱스

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        this.SfxAsID = 0; // 효과음 인덱스 초기화
    }

    // 배경음악 재생 함수
    public void PlayBgm_Func(BgmType _bgmType)
    {
        // BGM 데이터를 가져옴
        DataBase_Manager.BgmData _bgmData = DataBase_Manager.Instance.GetBgmData_Func(_bgmType);
        this.bgmAS.clip = _bgmData.clip; // BGM 클립 설정
        this.bgmAS.volume = _bgmData.volume; // BGM 볼륨 설정
        this.bgmAS.Play(); // BGM 재생
    }

    // 배경음악 정지 함수
    public void StopBgm_Func()
    {
        this.bgmAS.Stop(); // BGM 정지
    }

    // 효과음 재생 함수
    public void PlaySfx_Func(SfxType _sfxType)
    {
        // SFX 데이터를 가져옴
        DataBase_Manager.SfxData _sfxData = DataBase_Manager.Instance.GetSfxData_Func(_sfxType);

        // 현재 인덱스의 AudioSource에 SFX 설정 및 재생
        AudioSource _sfxAS = this.sfxAsArr[this.SfxAsID];
        _sfxAS.volume = _sfxData.volume; // SFX 볼륨 설정
        _sfxAS.PlayOneShot(_sfxData.clip); // SFX 재생

        // 다음 인덱스로 이동, 배열의 끝에 도달하면 다시 처음으로
        if (this.SfxAsID + 1 < this.sfxAsArr.Length)
            this.SfxAsID++;
        else
            this.SfxAsID = 0;
    }

    // 초기화 시 호출되는 함수
    private void Reset()
    {
        // 배경음악 AudioSource가 설정되지 않은 경우 새로 생성
        if (this.bgmAS == null)
        {
            GameObject _bgmObj = new GameObject("BgmAS");
            _bgmObj.transform.SetParent(this.transform);
            this.bgmAS = _bgmObj.AddComponent<AudioSource>();
        }

        // 효과음 AudioSource 배열이 설정되지 않은 경우 새로 생성
        if (this.sfxAsArr == null)
        {
            this.sfxAsArr = new AudioSource[10];

            for (int i = 0; i < this.sfxAsArr.Length; i++)
            {
                GameObject _sfxObj = new GameObject("SfxAS_" + i);
                _sfxObj.transform.SetParent(this.transform);
                this.sfxAsArr[i] = _sfxObj.AddComponent<AudioSource>();
            }
        }

        // 게임 오브젝트 이름 설정
        this.gameObject.name = typeof(SoundSystem_Manager).Name;
    }
}