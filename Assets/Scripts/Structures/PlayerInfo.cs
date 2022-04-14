namespace StudyGame
{
    public struct PlayerInfo
    {
        private float xPos;
        private float yPos;
        private float zPos;

        private float xRot;
        private float yRot;
        private float zRot;
        private float wRot;

        private UnitStructure playerStats;

        public float XPos { get { return xPos; } set { xPos = value; } }
        public float YPos { get { return yPos; } set { yPos = value; } }
        public float ZPos { get { return zPos; } set { zPos = value; } }

        public float XRot { get { return xRot; } set { xRot = value; } }
        public float YRot { get { return yRot; } set { yRot = value; } }
        public float ZRot { get { return zRot; } set { zRot = value; } }
        public float WRot { get { return wRot; } set { wRot = value; } }

        public UnitStructure PlayerStats { get { return playerStats; } set { playerStats = value; } }
    }
}

