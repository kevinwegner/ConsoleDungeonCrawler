
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Actor : GameObject
{

    public Actor()
    {
        this.name = "player";
        this.position = new Vector2();
        this.selector = new GameObject();
        this.selector.position = new Vector2(0, 0);
    }

    public int health;
    public int speed;
    public int actions;
    public float vision;
    public Slot Weapon;
    public Slot Armour;
    private GameData data;
    public GameObject selector;

    public void Move(Direction dir)
    {
        Vector2 pos = new Vector2();
        data = Application.GetData();
        switch(dir)
        {
            case Direction.UP:

                if (position.x-1 < 0)
                {
                     return;
                }
                pos.x -= 1;
                Console.WriteLine("move up? " + position.x + " " + position.y);

                break;

            case Direction.DOWN:

                if (position.x+1 > data.level.structure.GetLength(0)-1)
                {
                    return;
                }
                pos.x += 1;
                Console.WriteLine("move down? " + position.x + " " + position.y);

                break;

            case Direction.LEFT:

                if (position.y - 1 < 0)
                {
                    return;
                }
                pos.y -= 1;
                Console.WriteLine("move left? " + position.x + " " + position.y);

                break;

            case Direction.RIGHT:

                if (position.y + 1 > data.level.structure.GetLength(1) - 1)
                {
                    return;
                }
                pos.y += 1;
                Console.WriteLine("move right? " + position.x + " " + position.y);

                break;
        }

        if (!data.combat)
        {
            position = new Vector2((int)(position.x + pos.x), (int)(position.y + pos.y));
        }

        if (data.combat)
        {
            selector.position = new Vector2((int)(selector.position.x + pos.x), (int)(selector.position.y + pos.y));
        }

        for (int i = 0; i < data.level.pickUps.Count; i++)
        {
            //Console.WriteLine("move to pickup debug: " + position.x + " " + data.level.pickUps[i].position.y);
            if (position.x == data.level.pickUps[i].position.x && position.y == data.level.pickUps[i].position.y)
            {
                data.level.pickUps[i].OnPickup();
            }
        }
    }

    public void EnterCombat()
    {
        selector = new GameObject();
        selector.position = new Vector2();

        selector.position = this.position;
    }

    public void TakeDamage(int value)
    {
        // TODO implement here
    }

}