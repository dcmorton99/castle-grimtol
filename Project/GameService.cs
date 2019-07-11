using System;
using System.Collections.Generic;
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
      throw new System.NotImplementedException();
    }

    public void Go(string direction)
    {
      throw new System.NotImplementedException();
    }

    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }

    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup()
    {

      //Rooms
      Room foyer = new Room("Foyer", "As you walk into the foyer, you see your favorite ficus overturned, soil spilled onto the Italian marble floor. You can hear the faint mewing of Blissa, but you aren't sure which direction it is coming from. To the north is entrance to the living room and to the east is an open window.");
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
      livRoom.Exits.Add("north", kitchen);
      livRoom.Exits.Add("west", landing);
      landing.Exits.Add("north", bedroom);
      landing.Exits.Add("west", skRoom);
      bedroom.Exits.Add("west", bathroom);
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
      StartGame();
    }

    public void StartGame()
    {
      while (Running)
      {
        Console.Clear();
        CurrentRoom.PrintRoom();
        Console.WriteLine("What would you like to do?<go 'direction', take 'item', look, use 'item'>"); //clarify through text later
        string response = Console.ReadLine().ToLower();
        string[] inputs = response.Split(' ');
        string command = inputs[0];
        // string option = "";
        // if (inputs.Length > 1)
        // {
        //   option = inputs[1];
        // }
        // switch (command)
        // {

        // }



      }
    }

    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
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