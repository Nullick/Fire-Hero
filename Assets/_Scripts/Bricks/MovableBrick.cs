using UnityEngine;

public class MovableBrick : Brick
{
    private int direction;

    public override void Initialize(Health health, float maxHealthPossible)
    {
        base.Initialize(health, maxHealthPossible);
    }

    public override void Move()
    {
        base.Move();

        if(CurrentHealth <= MaxHealth / 2)
        {
            Vector2 tempPos = transform.position;
            tempPos.x += _speed * Time.deltaTime;
            transform.position = tempPos;
        }
    }

}
