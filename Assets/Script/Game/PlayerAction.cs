public abstract class PlayerAction
{
    public PlayerController playerCtrl;

    public int moveCnt;//�ώ@�p

    public PlayerAction(PlayerController ctlr)
    {
        playerCtrl = ctlr;
        Init();
    }

    protected abstract void Init();
    public abstract void Reset();
    public abstract void Update();
    public abstract void FixedUpdate();
}