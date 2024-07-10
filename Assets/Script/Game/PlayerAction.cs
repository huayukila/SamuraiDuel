public abstract class PlayerAction
{
PlayerController playerCtrl;
    public PlayerAction(PlayerController ctlr){
	playerCtrl=ctlr;
}
    public abstract void Update();
    public abstract void FixedUpdate();
}
