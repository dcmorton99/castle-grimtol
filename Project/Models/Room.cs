using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }


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
      Console.WriteLine("You can't go that way, sorry.");
      return this;
    }



    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }
  }
}