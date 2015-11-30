
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PickUp : GameObject
{
    private Random rng = new Random();

    public PickUp()
    {
        this.item = new Item();
        this.count = 1;
    }

    public PickUp(Item item, int count)
    {
        this.item = item;
        this.count = count;
        this.type = item.type;
    }

    public PickUp(Item item, string type, int count)
    {
        this.item = item;
        this.type = type;
        this.count = count;
    }

    public Item item;
    public string type;
    public int count;

    public void OnPickup()
    {
        GameData data = Application.GetData();
        //Sequence spawn conditions
        #region Ammo
        if (item.type == "ammo")
        {
            Console.WriteLine("AMMO FOUND");
            for (int i = 0; i < data.inventory.content.Count; i++)
            {
                if (data.inventory.content[i].item.type == "weap")
                {
                    Console.WriteLine("WEAPON FOUND");
                    Weapon temp = (Weapon)data.inventory.content[i].item;

                    if (temp.ammo == temp.maxAmmo)
                        continue;

                    temp.ammo += (int)(temp.clipsize * 1.5);

                    if (temp.ammo > temp.maxAmmo)
                        temp.ammo = temp.maxAmmo;

                    data.inventory.content[i].item = temp;

                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */"Ammunition crate found. Weapon ammunition restored.");
                }
            }
        }
        #endregion
        #region Medkit
        else if (item.type == "med")
        {
            //Console.WriteLine("MEDKIT FOUND");
            data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */"Medkit found. Player health restored.");
            data.player.health = data.player.maxHealth;
        }
        #endregion
        #region Weapon
        else if (item.type == "weap")
        {
            bool added = false;
            bool NotAllWeapons = true;
            int count = 0;

            for (int i = 0; i < data.inventory.content.Count; i++)
            {
                if (data.inventory.content[i].item.type == "weap") count++;
                if (count == ItemLibrary.Get().weaponList.Count) NotAllWeapons = false;
            }

            count = 0;

            while (NotAllWeapons)
            {
                int current = rng.Next(0, ItemLibrary.Get().weaponList.Count);
                this.item = ItemLibrary.Get().weaponList[current];

                if (!data.inventory.Contains(this.item))
                {
                    data.inventory.Add(this.item, this.count);
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Weaponcase found. " + this.item.name + " added to inventory.");

                    added = true;
                    break;
                }

                count++;
            }
                if (!added)
                {
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Empty Weaponcase found. Proceeding...");
                }
        }
        #endregion
        #region Armor
        else if (item.type == "armor")
        {
            bool added = false;
            int count = 0;

            for (int i = 0; i < data.inventory.content.Count; i++)
            {
                if (data.inventory.content[i].item.type == "armor") count++;
                if (count == ItemLibrary.Get().armorList.Count) return;
            }

            count = 0;

            while (true)
            {
                int current = rng.Next(0, ItemLibrary.Get().armorList.Count);
                this.item = ItemLibrary.Get().armorList[current];

                if (!data.inventory.Contains(this.item))
                {
                    data.inventory.Add(this.item, this.count);
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Armor found. " + this.item.name + " was added to the inventory.");

                    added = true;
                    break;
                }

                count++;
            }
            if (!added)
            {
                data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Debug no armor found. Proceeding...");
            }
        }
        #endregion
        #region Grenades
        else if (item.type == "grenade")
        {
            bool added = false;
            int count = 0;

            while (true)
            {
                int current = rng.Next(0, ItemLibrary.Get().grenadeList.Count);
                this.item = ItemLibrary.Get().grenadeList[current];

                if (!data.inventory.Contains(this.item))
                {
                    data.inventory.Add(this.item, this.count);
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Grenade found. " + this.item.name + " was added to the inventory.");

                    added = true;
                    break;
                }

                count++;
            }
            if (!added)
            {
                data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Empty grenade found. Proceeding...");
            }
        }
        #endregion

        else
        {
            //Console.WriteLine("SECOND");

            data.inventory.Add(this.item, this.count);
        }

        data.level.pickUps.Remove(this);
        //Console.WriteLine("item picked up " + item.name);
    }

}