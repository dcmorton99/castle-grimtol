using System;
using System.Collections.Generic;
using System.Threading;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }

    public bool Unlocked { get; set; }


    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }

    public void PrintRoom()
    {
      Console.WriteLine($"You are now in the {Name}");
      Console.WriteLine(Description);
    }



    public IRoom LeaveRoom(string dir)
    {
      if (Exits.ContainsKey(dir))
      {
        return Exits[dir];

      }
      Console.Clear();
      Console.WriteLine("You can't go that way, sorry.");
      Thread.Sleep(2000);
      return this;
    }



    public Room(string name, string description, bool unlocked)
    {
      Name = name;
      Description = description;
      Unlocked = unlocked;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }
  }
}