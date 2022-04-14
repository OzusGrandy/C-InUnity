namespace StudyGame
{
    public struct SaveStructure
    {
        private float xPos;
        private float yPos;
        private float zPos;

        private float xRot;
        private float yRot;
        private float zRot;
        private float wRot;

        private float energy;
        private float health;
        private float movementSpeed;
        private float jumpSpeed;

        private int[] levelVariants;
        private bool[] activated;

        private int activeLevel;

        public SaveStructure(LevelInfo[] tempLevels, PlayerInfo tempPlayer, int active)
        {
            xPos = tempPlayer.XPos;
            yPos = tempPlayer.YPos;
            zPos = tempPlayer.ZPos;

            xRot = tempPlayer.XRot;
            yRot = tempPlayer.YRot;
            zRot = tempPlayer.ZRot;
            wRot = tempPlayer.WRot;

            energy = tempPlayer.PlayerStats.Energy;
            health = tempPlayer.PlayerStats.Health;
            movementSpeed = tempPlayer.PlayerStats.MovementSpeed;
            jumpSpeed = tempPlayer.PlayerStats.JumpSpeed;

            levelVariants = new int[tempLevels.Length];
            activated = new bool[tempLevels.Length];
            for (int i = 0; i < tempLevels.Length; i++)
            {
                levelVariants[i] = tempLevels[i].LevelVariant;
                activated[i] = tempLevels[i].Activated;
            }

            activeLevel = active;
        }

        public float XPos { get { return xPos; } set { xPos = value; } }
        public float YPos { get { return yPos; } set { yPos = value; } }
        public float ZPos { get { return zPos; } set { zPos = value; } }

        public float XRot { get { return xRot; } set { xRot = value; } }
        public float YRot { get { return yRot; } set { yRot = value; } }
        public float ZRot { get { return zRot; } set { zRot = value; } }
        public float WRot { get { return wRot; } set { wRot = value; } }

        public float Energy { get { return energy; } set { energy = value; } }
        public float Health { get { return health; } set { health = value; } }
        public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
        public float JumpSpeed { get { return jumpSpeed; } set { jumpSpeed = value; } }

        public int[] LevelVariants { get { return levelVariants; } set { levelVariants = value; } }
        public bool[] Activated { get { return activated; } set { activated = value; } }

        public int ActiveLevel { get { return activeLevel; } set { activeLevel = value; } }

        public LevelInfo[] Levels 
        { 
            get 
            {
                LevelInfo[] temp = new LevelInfo[levelVariants.Length];
                for (int i = 0; i < levelVariants.Length; i++)
                {
                    temp[i].LevelVariant = levelVariants[i];
                    temp[i].Activated = activated[i];
                }
                return temp; 
            } 
        }
        public PlayerInfo Player 
        { 
            get 
            { 
                PlayerInfo temp = new PlayerInfo();
                UnitStructure tempStats = new UnitStructure();

                tempStats.Energy = energy;
                tempStats.Health = health;
                tempStats.MovementSpeed = movementSpeed;
                tempStats.JumpSpeed = jumpSpeed;
                temp.PlayerStats = tempStats;

                temp.XPos = xPos;
                temp.YPos = yPos;
                temp.ZPos = zPos;

                temp.XRot = xRot;
                temp.YRot = yRot;
                temp.ZRot = zRot;
                temp.WRot = wRot;

                return temp; 
            } 
        }
    }
}

