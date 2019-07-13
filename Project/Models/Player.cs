using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }


    public void holding()
    {
      Inventory = new List<Item>(){
            new Item("keys", "the keys can unlock doors"),
            new Item("bags", "shopping bag is filled with new shoes and toys for Blissa!")
        };
    }
    public void PrintInventory()//can't even hit this...
    {
      Console.WriteLine("do I even have inventory?");

    }



    public Player(string playerName)
    {
      PlayerName = playerName;
      Inventory = new List<Item>();
      holding();
    }
  }
}