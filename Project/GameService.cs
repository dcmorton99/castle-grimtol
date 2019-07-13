using System;
using System.Collections.Generic;
using System.Threading;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    private bool Running = true;


    public void GetUserInput()
    {
      string response = Console.ReadLine().ToLower();
      string[] inputs = response.Split(' ');
      string command = inputs[0];
      string option = "";
      if (inputs.Length > 1)
      {
        option = inputs[1];
      }
      switch (command)
      {
        case "go":
          Go(option);
          break;
        case "take":
          TakeItem(option);
          break;
        case "look":
          Console.Clear();
          Look();
          break;
        case "inventory":
          Console.WriteLine("You have the following: ");
          Inventory();
          break;
        case "help":
          Help();
          break;
        case "quit":
          Quit();
          break;

      }
    }

    public void Go(string direction)
    {
      CurrentRoom = (Room)CurrentRoom.LeaveRoom(direction);
    }

    public void Help()
    {
      Console.WriteLine("Please choose a command. Type 'quit' if you are done playing.");
    }

    public void Inventory()
    {
      int count = 1;
      foreach (var item in CurrentPlayer.Inventory)
      {
        Console.WriteLine($"{count}) {item.Name}");
        count++;
      }
    }

    public void Look()
    {
      Console.WriteLine("am I hitting the look function?");//I don't think I am hitting this
      Thread.Sleep(1000);
      Console.WriteLine($"{ CurrentRoom.Description}");
    }

    public void Quit()
    {
      Console.Clear();
      Console.WriteLine("You sure? y/n?");
      if (Console.ReadLine().ToLower() == "y")
      {
        Console.WriteLine($"Well, {CurrentPlayer.PlayerName}, you're done.");
        Running = false;
      }
      else
      {
        Look();
      }

    }

    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup()
    {

      //Rooms
      Room foyer = new Room("Foyer", "As you walk into the foyer, you see your favorite ficus overturned, soil spilled onto the Italian marble floor.There is an unopened bottle of evian sitting on your foyer table. You can hear the faint mewing of Blissa, but you aren't sure which direction it is coming from. To the north is entrance to the living room and to the east is an open window.");
      Room window = new Room("Window", "great job, you have accomplished nothing.");
      Room livRoom = new Room("Living Room", "Passing through the entry way, you hear dishes clanging from the kitchen. The door to the kitchen is north, but you still hear Blissa mewing, this time a bit more frantically. The sound could be coming from upstairs. Do you go to the kitchen to investigate, or to you take the stairs to the west?");
      Room kitchen = new Room("Kitchen", "Nothing to see hear, Skipper is just making a mess and some cookies. As you approach Skipper, you hear a loud crash come from upstairs and very clearly hear Blissa frantically shrieking!");
      Room landing = new Room("Upstairs Landing", "Upstairs, two doors, north to your bedroom or west to skippers room");
      Room bedroom = new Room("Bedroom", "fill this in later,be sure to use the keys you walked into the house with. Blissa is in the dream closet");
      Room skRoom = new Room("Skipper's Bedroom", "Wrong choice, loud crash, cat shrieking, fabric shredding noise");
      Room closet = new Room("Dream Closet", "rush into room pick blissa up and give her a big hug. How sweet!");
      Room bathroom = new Room("Bathroo", "Bathroom is the wrong choice!");

      //Items
      Item evian = new Item("Evian", "Unopened bottle sitting on the table in the foyer.");
      Item yarn = new Item("Ball of Yarn", "Pink ball of yarn sitting in the corner of the landing.");
      Item keys = new Item("Keys", "house keys and car keys on a key ring with a giant plastic 'B'");
      Item bags = new Item("Shopping Bags", "Two huge shopping bags filled with new clothes and some new toys for Blissa.");

      //Exits
      foyer.Exits.Add("east", window);//if you go to the window, nothing happens
      foyer.Exits.Add("north", livRoom);
      livRoom.Exits.Add("north", kitchen);//you lose
      livRoom.Exits.Add("west", landing);
      livRoom.Exits.Add("south", foyer);
      kitchen.Exits.Add("south", livRoom);
      landing.Exits.Add("north", bedroom);
      landing.Exits.Add("west", skRoom);
      bedroom.Exits.Add("south", landing);
      bedroom.Exits.Add("west", bathroom);//you lose
      bedroom.Exits.Add("east", closet);

      //Relationships
      //Items in Room
      foyer.Items.Add(evian);
      landing.Items.Add(yarn);

      //Adding exit options
      CurrentRoom = foyer;

      Console.WriteLine("Welcome to Barbie and the Life in the Dreamhouse! What is your name?");
      string name = Console.ReadLine();
      CurrentPlayer = new Player(name);

    }

    public void StartGame()
    {
      while (Running)
      {
        CurrentRoom.PrintRoom();
        Console.WriteLine("What would you like to do?<go 'direction', take 'item', look, use 'item', inventory, quit>");
        GetUserInput();
      }
    }

    public void TakeItem(string itemName)
    {
      Item foundItem = CurrentRoom.Items.Find(i => i.Name == itemName);
      if (foundItem != null)
      {
        CurrentRoom.Items.Remove(foundItem);
        CurrentPlayer.Inventory.Add(foundItem);
      }
      Console.WriteLine("That isn't even in the room");
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }


    public GameService()
    {

      Setup();
    }


  }
}