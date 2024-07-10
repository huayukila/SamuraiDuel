public abstract class PlayerAction
{
    public PlayerController playerCtrl;

    public PlayerAction(PlayerController ctlr)
    {
        playerCtrl = ctlr;
        Init();
    }

    protected abstract void Init();
    public abstract void Update();
    public abstract void FixedUpdate();
}