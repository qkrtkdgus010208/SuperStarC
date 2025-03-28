using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public partial class DataBase_Manager : DB_Manager
{
    // �̱��� �ν��Ͻ�
    public static DataBase_Manager Instance;

    // �ʱ�ȭ �Լ�
    public override void Init_Func()
    {
        // �̱��� �ν��Ͻ� ����
        Instance = this;

        // �θ� Ŭ������ �ʱ�ȭ �Լ� ȣ��
        base.Init_Func();
    }
}