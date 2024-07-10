public abstract class PlayerAction
{
<<<<<<< Updated upstream
=======
PlayerController playerCtrl;
    public PlayerAction(PlayerController ctlr){
	playerCtrl=ctlr;
	Init();
}
	protected abstract void Init();
>>>>>>> Stashed changes
    public abstract void Update();
    public abstract void FixedUpdate();
}
