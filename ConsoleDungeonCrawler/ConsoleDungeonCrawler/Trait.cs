
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Trait
{
    public string name;
    public List<ITraitBehaviour> behaviour = new List<ITraitBehaviour>();

    public Trait()
    {

    }
    public Trait(ITraitBehaviour behaviour)
    {
        this.behaviour.Add(behaviour);
    }
    public Trait(string name, ITraitBehaviour behaviour)
    {
        this.name = name;
        this.behaviour.Add(behaviour);
    }
    public Trait(string name, List<ITraitBehaviour> behaviour)
    {
        this.name = name;
        this.behaviour.AddRange(behaviour);
    }
    public void AddTrait(ITraitBehaviour trait)
    {
        behaviour.Add(trait);
    }
    public void Remove(Actor actor)
    {
        for (int i = 0; i < behaviour.Count; i++)
        {
            behaviour[i].OnRemove(actor);
        }
    }
}