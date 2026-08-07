namespace CupheadRLPlugin.Models.GameState
{
    public class GameState
    {
        public PlayerState.PlayerState playerState { get; set; } = new();
    }
}