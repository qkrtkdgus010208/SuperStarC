using System.Collections.Generic;
using UnityEngine;

public abstract class DB_Manager : ScriptableObject
{
    public static DB_Manager Instance; // �̱��� �ν��Ͻ�

    [Header("����")]
    [SerializeField] private BgmData[] bgmDataArr = null; // ������� ������ �迭
    [SerializeField] private SfxData[] sfxDataArr = null; // ȿ���� ������ �迭
    private Dictionary<BgmType, BgmData> bgmDataDic; // ������� ������ ��ųʸ�
    private Dictionary<SfxType, SfxData> sfxDataDic; // ȿ���� ������ ��ųʸ�

    // �����ͺ��̽� �ʱ�ȭ �Լ�
    public virtual void Init_Func()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����

        // ������� ������ ��ųʸ� �ʱ�ȭ
        this.bgmDataDic = new Dictionary<BgmType, BgmData>();
        foreach (BgmData _bgmData in this.bgmDataArr)
            this.bgmDataDic.Add(_bgmData.bgmType, _bgmData);

        // ȿ���� ������ ��ųʸ� �ʱ�ȭ
        this.sfxDataDic = new Dictionary<SfxType, SfxData>();
        foreach (SfxData _sfxData in this.sfxDataArr)
            this.sfxDataDic.Add(_sfxData.sfxType, _sfxData);
    }

    // bgmType�� �ش��ϴ� BgmData�� ��ȯ�ϴ� �Լ�
    public BgmData GetBgmData_Func(BgmType _bgmType)
    {
        return this.bgmDataDic[_bgmType];
    }

    // sfxType�� �ش��ϴ� SfxData�� ��ȯ�ϴ� �Լ�
    public SfxData GetSfxData_Func(SfxType _sfxType)
    {
        return this.sfxDataDic[_sfxType];
    }

    // ���� ������ Ŭ����
    [System.Serializable]
    public class SoundData
    {
        public AudioClip clip; // ����� Ŭ��
        public float volume = 1f; // ����
    }

    // ������� ������ Ŭ����
    [System.Serializable]
    public class BgmData : SoundData
    {
        public BgmType bgmType; // ������� Ÿ��
    }

    // ȿ���� ������ Ŭ����
    [System.Serializable]
    public class SfxData : SoundData
    {
        public SfxType sfxType; // ȿ���� Ÿ��
    }
}