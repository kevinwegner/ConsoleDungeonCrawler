﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TriggerObject : GameObject, ITrigger
{
    public TriggerObject()
    {
        Application.GetData().level.trigger.Add(this);
    }
    public TriggerObject(string name, Vector2 position)
    {
        this.name = name;
        this.position = position;
        Application.GetData().level.trigger.Add(this);
    }

    public string OnTriggerEnter()
    {
        Console.Write("TRIGGERED");
        Environment.Exit(0);
        return name;
    }
}