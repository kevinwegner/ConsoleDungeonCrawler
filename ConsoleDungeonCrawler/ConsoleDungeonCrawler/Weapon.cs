
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Weapon : Item
{
    Random rng = new Random();

    public Weapon()
    {
        this.name = "empty_weap";
    }
    public Weapon(Weapon weapon)
    {
        this.name = weapon.name;
        this.type = weapon.type;
        this.damage = weapon.damage;
        this.range = weapon.range;

        if (weapon.accuracy > 1) weapon.accuracy = 1;
        this.accuracy = weapon.accuracy;

        this.ammo = weapon.ammo;
        this.currentammo = 0;
        this.maxAmmo = weapon.maxAmmo;
        this.clipsize = weapon.clipsize;
        this.ammotype = weapon.ammotype;
        this.damagetype = weapon.damagetype;
        this.penetration = weapon.penetration;
    }

    public Weapon(string name, string type)
    {
        this.name = name;
        this.type = type;
        this.damage = 0;
        this.range = 0;
        this.accuracy = 0;
        this.ammo = 0;
        this.clipsize = 0;
        this.ammotype = "none";
        this.damagetype = "none";
        this.penetration = 0;
    }

    public Weapon(string n, string t, int d, float r, float a, int ammo, int maxAmmo, int clip, string ammotype, string damagetype, float pen)
    {
        this.name = n;
        this.type = t;
        this.damage = d;
        this.range = r;

        if (a > 1) a = 1;
        this.accuracy = a;

        this.ammo = ammo;
        this.currentammo = 0;
        if (ammo == -1)
        {
            this.currentammo = -1;
        }

        this.maxAmmo = maxAmmo;
        this.clipsize = clip;
        this.ammotype = ammotype;
        this.damagetype = damagetype;
        this.penetration = pen;
    }

    public int damage;
    public float range;
    public float accuracy;
    public int ammo;
    public int currentammo;
    public int maxAmmo;
    public int clipsize;
    public string ammotype;
    public string damagetype;
    public float penetration;

    public void Attack()
    {
        GameData data = Application.GetData();
        int hits = 1;

        if (data.player.actions <= 0)
        {
            return;
        }

        if (currentammo == 0)
        {
            return;
        }

        if (CheckTarget(new Vector2(data.player.selector.position.x, data.player.selector.position.y)) != null)
        {
            if (this.name == "submachine_gun")
            {
                hits = 5;
            }
            for (int i = 0; i < hits; i++)
            {
                if (CheckAccuracy())
                {
                    if (CheckTarget(new Vector2(data.player.selector.position.x, data.player.selector.position.y)) != null)
                    {
                        data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */"Target found. Dealing damage...");
                        CheckTarget(new Vector2(data.player.selector.position.x, data.player.selector.position.y)).TakeDamage(damage, damagetype, penetration);
                    }

                }
                else
                {
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */" Target missed. No damage dealt.");
                }

                data.player.actions -= 1;
                if (data.player.actions <= 0)
                {
                    data.combat = false;
                }

                currentammo -= 1;
                data.player.RemoveTrait("acc");
            }
        }
    }

    public void Attack(Vector2 target)
    {
        GameData data = Application.GetData();
        if (CheckTarget(target) != null)
        {
            if (CheckAccuracy())
            {
                CheckTarget(target).TakeDamage(damage, damagetype, penetration);
            }
            else
            {
                data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */" enemy missed. No damage taken.");
            }

            data.player.actions -= 1;

            for (int i = 0; i < data.level.enemies.Count; i++)
            {
                data.level.enemies[i].RemoveTrait("acc");
            }
        }
    }

    public void Reload()
    {
        GameData data = Application.GetData();

        if (data.player.actions <= 0)
        {
            return;
        }

        if (Application.GetData().combat)
        {
            Application.GetData().combat = false;
        }

        if (ammo <= 0)
        {
            Application.GetData().combatlog.Add("No ammunition left in system. Unable to reload weapon.");
            return;
        }

        else if (currentammo == clipsize)
        {
            Application.GetData().combatlog.Add("Weapon system fully loaded. Unable to reload weapon.");
            return;
        }

        else if (ammo >= clipsize)
        {
            int storage = currentammo;
            currentammo = clipsize;
            ammo -= clipsize - storage;

            Application.GetData().combatlog.Add("Weapon system reloaded.");
        }

        else if (ammo > 0 && ammo < clipsize)
        {
            currentammo = ammo;
            ammo = 0;

            Application.GetData().combatlog.Add("Weapon system reloaded. Please refill ammunition.");
        }

        for (int i = 0; i < data.player.traits.Count; i++)
        {
            if (data.player.traits[i].name == "ammo_mod")
            {
                data.player.RemoveTrait(data.player.traits[i]);
            }
        }

        Application.GetData().player.actions -= 1;
    }

    public Actor CheckTarget(Vector2 target)
    {
        GameData data = Application.GetData();
        Actor actor = new Actor();
        bool targetExists = false;

        for (int i = 0; i < data.collision.Count; i++)
        {
            if (data.collision[i].position.x == target.x && data.collision[i].position.y == target.y)
            {
                if ((ConsolePseudoRaycast.CastRay(new Vector2(data.player.position.x, data.player.position.y), new Vector2(target.x, target.y))))
                {
                    return null;
                }
                /**/
                targetExists = true;
                actor = data.collision[i];
            }
        }

        if (targetExists)
        {
            return actor;
        }

        return null;
    }

    public bool CheckAccuracy()
    {
        float rngF = (rng.Next(-50, 100));
        float acc = rngF / 100.0f;
        if (acc < 0.01f) acc = 0.01f;

        if (acc <= this.accuracy) return true;
        else return false;
    }

}