namespace CupheadRLPlugin.Models.PlayerState
{
    public class PlayerState
    {
        public int HP { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public bool Dashing { get; set; }
        public bool Grounded { get; set; }
        public bool Locked { get; set; }
    }
}