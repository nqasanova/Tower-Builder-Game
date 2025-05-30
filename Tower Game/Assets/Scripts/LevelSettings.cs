[System.Serializable]
public class LevelSettings
{
    public float swingAmplitude;
    public float swingSpeed;
    public float swingHeight;
    public int scoreToWin;

    public bool hasMovingGround;

    // NEW moving ground config
    public float groundMoveRange;
    public float groundMoveSpeed;
}