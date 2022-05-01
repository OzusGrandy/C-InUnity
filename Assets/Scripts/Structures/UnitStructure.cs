namespace StudyGame
{
    public struct UnitStructure
    {
        //private float crystallite;
        private float energy;
        private float health;
        private float movementSpeed;
        private float jumpSpeed;
        //private float castSpeed;
        //private float density;
        //private float thermalCrystallite;
        //private float electricCrystallite;
        //private float kineticCrystallite;
        //private float radiateCrystallite;
        //private float energyStorageRate;
        //private float energySpeedFactor;

        //public float Crystallite { get { return crystallite; } set { crystallite = value; } }
        public float Energy { get { return energy; } set { energy = value; } }
        public float Health { get { return health; } set { health = value; } }
        public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
        public float JumpSpeed { get { return jumpSpeed; } set { jumpSpeed = value; } }
        //public float CastSpeed { get { return castSpeed; } }
        //public float Density { get { return density; } }
        //public float ThermalCrystallite { get { return thermalCrystallite; } }
        //public float ElectricCrystallite { get { return electricCrystallite; } }
        //public float KineticCrystallite { get { return kineticCrystallite; } }
        //public float RadiateCrystallite { get { return radiateCrystallite; } }
        //public float EnergyStorageRate { get { return energyStorageRate; } }

        //private float EnergySpeedFactor { get { return energySpeedFactor; } set { energySpeedFactor = 0.1f; } }

        //private void SetEnergy()
        //{
        //    energy = crystallite * 100.0f;
        //}

        //private void SetHealth()
        //{
        //    health = crystallite * 10.0f;
        //}

        //private void SetEnergyStorageRate()
        //{
        //    energyStorageRate = 100.0f;
        //}

        public UnitStructure (float energy, float health, float movementSpeed, float jumpSpeed) : this()
        {
            this.energy = energy;
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.jumpSpeed = jumpSpeed;
        }
    }
}

