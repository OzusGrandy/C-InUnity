namespace StudyGame
{
    public abstract class Unit : IAttack, IMove
    {
        public abstract void Attack();
        public abstract void Move();
    }
}

