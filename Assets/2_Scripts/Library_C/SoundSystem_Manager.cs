using UnityEngine;

public class SoundSystem_Manager : MonoBehaviour
{
    public static SoundSystem_Manager Instance; // �̱��� �ν��Ͻ�

    [SerializeField] private AudioSource bgmAS = null; // �������(AudioSource) ������Ʈ
    [SerializeField] private AudioSource[] sfxAsArr = null; // ȿ����(AudioSource) ������Ʈ �迭

    private int SfxAsID; // ���� ��� ���� ȿ���� AudioSource�� �ε���

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����

        this.SfxAsID = 0; // ȿ���� �ε��� �ʱ�ȭ
    }

    // ������� ��� �Լ�
    public void PlayBgm_Func(BgmType _bgmType)
    {
        // BGM �����͸� ������
        DataBase_Manager.BgmData _bgmData = DataBase_Manager.Instance.GetBgmData_Func(_bgmType);
        this.bgmAS.clip = _bgmData.clip; // BGM Ŭ�� ����
        this.bgmAS.volume = _bgmData.volume; // BGM ���� ����
        this.bgmAS.Play(); // BGM ���
    }

    // ������� ���� �Լ�
    public void StopBgm_Func()
    {
        this.bgmAS.Stop(); // BGM ����
    }

    // ȿ���� ��� �Լ�
    public void PlaySfx_Func(SfxType _sfxType)
    {
        // SFX �����͸� ������
        DataBase_Manager.SfxData _sfxData = DataBase_Manager.Instance.GetSfxData_Func(_sfxType);

        // ���� �ε����� AudioSource�� SFX ���� �� ���
        AudioSource _sfxAS = this.sfxAsArr[this.SfxAsID];
        _sfxAS.volume = _sfxData.volume; // SFX ���� ����
        _sfxAS.PlayOneShot(_sfxData.clip); // SFX ���

        // ���� �ε����� �̵�, �迭�� ���� �����ϸ� �ٽ� ó������
        if (this.SfxAsID + 1 < this.sfxAsArr.Length)
            this.SfxAsID++;
        else
            this.SfxAsID = 0;
    }

    // �ʱ�ȭ �� ȣ��Ǵ� �Լ�
    private void Reset()
    {
        // ������� AudioSource�� �������� ���� ��� ���� ����
        if (this.bgmAS == null)
        {
            GameObject _bgmObj = new GameObject("BgmAS");
            _bgmObj.transform.SetParent(this.transform);
            this.bgmAS = _bgmObj.AddComponent<AudioSource>();
        }

        // ȿ���� AudioSource �迭�� �������� ���� ��� ���� ����
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

        // ���� ������Ʈ �̸� ����
        this.gameObject.name = typeof(SoundSystem_Manager).Name;
    }
}