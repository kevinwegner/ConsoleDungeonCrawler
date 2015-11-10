﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemLibrary
{
    private static ItemLibrary instance;
    public List<Item> items = new List<Item>();

    public void Init()
    {
        items.Add(new Item("med_kit", "cons"));
        items.Add(new Weapon("new_weap", "weap", 10, 5, -1, -1, -1, "none", "none", -1));
        items.Add(new Armor());
        items.Add(new Item("test_ammo", "ammo"));
        items.Add(new Item("infrared_keycard", "key"));
        items.Add(new Throwable());
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
