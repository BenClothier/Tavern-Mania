using System.Linq;
using System.Collections.Generic;

public struct DrinkMix
{
    public static DrinkMix Empty()
    {
        return new DrinkMix(new Liquid[] {});
    }

    public List<Liquid> Liquids;

    public int LiquidCount => Liquids.Count;

    public bool IsEmpty => Liquids.Count <= 0;

    public bool IsFull => Liquids.Count >= 3;

    public DrinkMix(params Liquid[] liquids)
    {
        Liquids = new List<Liquid>(liquids).OrderBy(x => x.ID).ToList();
    }

    public DrinkMix(List<Liquid> liquids)
    {
        Liquids = liquids.OrderBy(x => x.ID).ToList();
    }

    public void AddLiquid(Liquid liquid)
    {
        Liquids.Add(liquid);
        Liquids = Liquids.OrderBy(x => x.ID).ToList();
    }

    public bool Equals(DrinkMix other)
    {
        return Liquids.OrderBy(x => x.ID).SequenceEqual(other.Liquids.OrderBy(x => x.ID));
    }

    public void Clear()
    {
        Liquids.Clear();
    }
}
