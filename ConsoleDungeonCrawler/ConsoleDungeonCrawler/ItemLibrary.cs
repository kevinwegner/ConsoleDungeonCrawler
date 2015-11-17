﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemLibrary
{
    private static ItemLibrary instance;
    public List<Item> items = new List<Item>();
    public List<Item> generics = new List<Item>();
    public List<Weapon> weaponList = new List<Weapon>();
    public List<Armor> armorList = new List<Armor>();
    public List<Throwable> grenadeList = new List<Throwable>();
    public List<List<Item>> itemLists = new List<List<Item>>();

    public void Init()
    {
        items.Add(new Item("med_kit", "med"));
        items.Add(new Weapon("new_weap", "weap", 10, 5, 1, 5, 20, 5, "none", "none", -1));
        items.Add(new Weapon("other_weap", "weap", 7, 7, 0, 3, 15, 3, "none", "none", -1));
        items.Add(new Armor());
        items.Add(new Item("test_ammo", "ammo"));
        items.Add(new Item("infrared_keycard", "key"));
        items.Add(new Throwable("frag_grenade", "frag", new Damage(4.0f, 8.0f)));
    }

    public static ItemLibrary Get()
    {
        if (instance == null)
        {
            instance = new ItemLibrary();
            instance.Init();
        }

        return instance;
    }
}
