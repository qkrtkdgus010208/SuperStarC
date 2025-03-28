using UnityEngine;

public partial class DataBase_Manager : DB_Manager
{
    [Header("플레이어")]
    public float gravityScale = 2f; // 중력 스케일
    public float jumpPower = 100f; // 점프 힘
    public float moveSpeed = 10f; // 이동 속도
    public float heightModifyValue = 1.7f; // 높이 보정 값

    [Header("장애물")]
    public float obstacleSpawnInterval = 1f; // 장애물 생성 간격
    public float obstacleSpawnOffsetX = 1f; // 장애물 생성 오프셋 X
    public Obstacle_Scripts[] ObstacleClassArr = null; // 장애물 클래스 배열
    public LevelData[] levelDataArr = null; // 레벨 데이터 배열

    [Header("팬")]
    public Fan_Script baseFanClass = null; // 기본 팬 클래스
    public FanData[] fanDataArr = null; // 팬 데이터 배열
    public float fanSpawnInterval = 1f; // 팬 생성 간격
    public float fanSpawnOffsetX = 10f; // 팬 생성 오프셋 X
    public float fanSpawnPosY = -6.5f; // 팬 생성 위치 Y
    public float fanAnimationInterval = 0.5f; // 팬 애니메이션 간격
    public int GetFanIdleAniSpriteNum => this.fanDataArr[0].idleSpriteArr.Length; // 팬 아이들 애니메이션 스프라이트 수

    [Header("슬로건")]
    public Slogan_Scripts baseSloganClass = null; // 기본 슬로건 클래스
    public SloganData[] sloganDataArr = null; // 슬로건 데이터 배열
    public float sloganSpawnInterval = 1f; // 슬로건 생성 간격
    public float sloganSpawnOffsetX = 1f; // 슬로건 생성 오프셋 X

    // 레벨 데이터 클래스
    [System.Serializable]
    public class LevelData
    {
        public int conditionNum; // 조건 번호
        public int[] obstacleIDArr; // 장애물 ID 배열
    }

    // 팬 데이터 클래스
    [System.Serializable]
    public class FanData
    {
        public Sprite gameoverSprite; // 게임 오버 스프라이트
        public Sprite[] idleSpriteArr; // 아이들 스프라이트 배열
    }

    // 슬로건 데이터 클래스
    [System.Serializable]
    public class SloganData
    {
        public Sprite sprite; // 슬로건 스프라이트
    }
}

// 배경음악 타입 열거형
public enum BgmType
{
    None = 0, // 없음

    Title = 10, // 타이틀
    Ingame = 20, // 인게임
}

// 효과음 타입 열거형
public enum SfxType
{
    None = 0, // 없음

    Crowd1 = 10, // 군중1
    Crowd2 = 20, // 군중2
    Crowd3 = 30, // 군중3
    Gameover = 40, // 게임 오버
    Jump = 50, // 점프
    Flicker = 60, // 깜빡임
}