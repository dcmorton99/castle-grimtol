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
          Console.Clear();
          Go(option);
          CurrentRoom.PrintRoom();
          break;
        case "take":
          TakeItem(option);
          break;
        case "look":
          Look();
          break;
        case "purse":
          Inventory();
          break;
        case "help":
          Help();
          break;
        case "quit":
          Quit();
          break;
        case "restart":
          Reset();
          break;
        case "use":
          UseItem(option);
          break;
        default:
          Console.WriteLine("Invalid command, type 'help' if you are confused.");
          break;

      }
    }

    public void Go(string direction)
    {
      CurrentRoom = (Room)CurrentRoom.LeaveRoom(direction);
    }

    public void Help()
    {
      Console.WriteLine("Type 'go' and the direction to move to another room;");
      Console.WriteLine("Type 'take' and the item to add something to your purse;");
      Console.WriteLine("Type 'use' and the item to use an item in your purse;");
      Console.WriteLine("Type 'purse' to see what you have in your purse;");
      Console.WriteLine("Type 'inventory' to see what items are in the room;");
      Console.WriteLine("Type 'look' to see the room description again;");
      Console.WriteLine("Type 'restart' to start over;");
      Console.WriteLine("Type 'quit' if you are done playing.");
    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count > -1)
      {
        Console.WriteLine("You have the following: ");
        int count = 1;
        foreach (var item in CurrentPlayer.Inventory)
        {
          Console.WriteLine($"{count}) {item.Name}");
          count++;
        }
      }
      else
      {
        Console.WriteLine("Your Inventory is empty.");
      }
    }

    public void Look()
    {
      Console.Clear();
      Thread.Sleep(1000);
      Console.WriteLine($"{ CurrentRoom.Description}");
      Console.WriteLine();
      Console.WriteLine($"The room contains the following items:");
      int counter = 1;
      foreach (var item in CurrentRoom.Items)
      {
        Console.WriteLine($"{counter}) {item.Name}");
        counter++;
      }
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
      Console.Clear();
      Thread.Sleep(2000);
      GameService game = new GameService();
    }

    public void Setup()
    {

      Console.WriteLine(@"
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#(((((((#@@@@@@@@@@@@#((((((((((((((#@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@(((.   .((((((#@@@&((((/.         ./(((((#@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@((/      .((((((((((((.                ./(((((#@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&(((((((#@@@@@@@@@@@@(((      .(((((((((/                      ,((((((((#@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#((/.  ((((((((&@@@@@@@@/((*. ./(,.((((((,       ./(((((,       /((((((((((((#@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@(((,     .(((((((((((@@@@@@@(((((.   *((((.       (((((((,       (((((((((((((((((@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@/(((        ((((((((((((((((((((/       /(((      ,((((((*       *(((((((((((((((((((&@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@/((/       (((((((/.      ,/((((.      ((*     .(((((*       *((      .((((((((((((((@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@((,      .((((.            .(((      .(,     .(((/       .(((*        ((((((((((((((@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@(((.      *(.                .(/      */      /(.       /((((/        *(((((((((((((#@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#((((((/       ,       *//,        /       /               ((((((((/      *((((((((((((((@@@
@@@@@@@@@@@@@@@@#((((((((((((((%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&((((*                     .(((((*       *       ,            ((((((((((.      /((((((((((((((@@@
@@@@@@@@@@@@#((((((/*,...,*/((((((#@@@@@@@@@@@@@@@@@@@@@@@@@(((.            ,           ,(((((((/       *      *.         *(((((((((*       ,(((((((((((((((@@@
@@@@@@@@@#((((/.               ./(((((&@@@@@@@@&%((((((((#(((,              (.      *((((((((((((/       ,      /,          ./(((/.        ,(((((((((((((((/@@@
@@@@@@@#((((,                     .((((((#@@((((((//,..,*((/          (*,,/(((       /((((((((((((*      *       ((                       /((((((((((((((((@@@@
@@@@@@(((/           .,///,         ,(((((((/.             .((       ,((((((((/       (((((((((((((      .(      .(((/                 *((((((((((((((((((*@@@@
@@@@@(((,        *(((((((((((.       ,((((.                 .(/       (((((((((.      .((((((((((((      .(/      *(((((/.         .*((((((((((((((((((((*@@@@@
@@@@/((.       ((((/((((((((((,       ((/        ,/((/       ,(.      ,(((((((((       /(((((((((((      *((*      /((((((((((((((((((((((((((((((((((((@@@@@@@
@@@@((/      ,(((.  ,((((((((((       ((.      (((((((*       /(       (((((((((/       ((((((((((,      ((((.      *(((((((((((((((((((((((((((((((((/@@@@@@@@
@@@@((,     ,(*      *(((((((((       ((,      *(((((,         (/      .(((((((((*      .(((((((/       /((((/   ,/((((((((((((((((((((((((((((((//@@@@@@@@@@@@
@@@@((,     */        /(((((((*      *((/      ((/             ,(,      *(((((((((.      *(((/,        *(((((((((((((((((((((((((((((((((////@@@@@@@@@@@@@@@@@@
@@@@((/                (((((*       .(((((///((.                *(       /(((((((((                   /((((((((((((((((((((((((((((((((@@@@@@@@@@@@@@@@@@@@@@@@
@@@@/((                ,.                ./((,         /(/       //      .(((((((((/               ,((((((((((((((((((((((((((((/*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@((.      ,(                                   *(((((/       (*      ,(((((((((/        .,/(((((((((((((((((((((@@*((/*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@,/((((((((/              ....              *((((((((,      .(.      /(((((((((((((((((((((((((((((((((((((((@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@/((((((,        *((((((((((*          ,(((((((((*       *(       *(((((((((((((((((((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@/((((.      *((((((((((((*         ,((((((((*         /*    .*((((((((((((((((((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@((((       /((((((((((((*          *//*              (/(((((((((((((((((((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@((/       (((((((((((((                     * */((((((((((((((((((((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@%#((((((((((*      .((((((((((((                   /((((((((((((((((((((((((((((((((((//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@&(((/*..  ..*/((.      ,((((((((((*               */(((((((((((((((((((((((/////((@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@%((.                      *((((((((,       (((((((((((((((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@#((.                        */(((/.        /((((((((((((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@((,      ./((//.                         ,(((((((((((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@(((      /(((*     ./.                  *(((((((((((((((((((((((///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@(/      *(((       .((((((//,.  .,*/(((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@((        .,       ,(((((((((((((((((((((((((((((((((((@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@(((                (((((((((((((((((((((((((((((((((/@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@(((.            ,(((((((((((((((((/(((((((((((((((@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@(((/*.   ,/(((((((((((((((((((@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@(/((((((((((((((((((((((((@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@*(((((((((((((((((((*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@//(((((((((((,@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

      Console.WriteLine();

      //Rooms
      Room foyer = new Room("Foyer", "As you walk into the foyer, your drop your keys on the foyer table and set your shopping bags on the ground. You look around and see that your favorite ficus overturned, soil spilled onto the Italian marble floor.There is an unopened bottle of evian sitting on your foyer table. You can hear the faint mewing of Blissa (your favorite fluffy cat), but you aren't sure which direction it is coming from. To the north is the entrance to the living room and to the east is an open window.", true);
      Room window = new Room("Window", "great job, you have accomplished nothing.", true);
      Room livRoom = new Room("Living Room", "Passing through the entry way, you hear dishes clanging from the kitchen. The door to the kitchen is north, but you still hear Blissa mewing, this time a bit more frantically. The sound could be coming from upstairs. Do you go north to the kitchen to investigate those clanging noises, or do you take the stairs to the west?", true);
      Room kitchen = new Room("Kitchen", "Nothing to see hear, Skipper is just making a mess and some cookies. As you approach Skipper, you hear a loud crash come from upstairs and very clearly hear Blissa frantically shrieking!", true);
      Room landing = new Room("Upstairs Landing", "You've made it upstairs, and at first glance everything looks fine. You can go north to your bedroom or take the door to the west, which is Skipper's room. There is a bright pink ball of yarn sitting just outside your bedroom door. As you stand there thinking, you hear scratching at a door, but it sounds faint, like it might be coming from another room inside one of the bedrooms. Which way should you go?", true);
      Room bedroom = new Room("Bedroom", "As you rush into your room, you see the Dream Closet door to the east is closed, but so is the bathroom door on the west side of the room. You can hear more frantic scratching coming from behind one of those doors.", true);
      Room skRoom = new Room("Skipper's Bedroom", "Nope, Blissa isn't in here. You hear a very loud crash, Blissa shrieking, and the sickening sound of expensive fabric being shredded.", true);
      Room closet = new Room("Dream Closet", "Your try to open the door to the Dream Closet, but it is locked. Did you remember to bring your keys?", false);
      Room bathroom = new Room("Bathroom", "Bathroom is the wrong choice!", true);

      //Items
      Item evian = new Item("evian", "Unopened bottle sitting on the table in the foyer.");
      Item ficus = new Item("ficus", "The ficus spilled in your foyer.");
      Item yarn = new Item("yarn", "Pink ball of yarn sitting in the corner of the landing.");
      Item keys = new Item("keys", "house keys and car keys on a key ring with a giant plastic 'B'");
      Item bags = new Item("bags", "Two huge shopping bags filled with new clothes and some new toys for Blissa.");
      Item cookies = new Item("cookies", "The chocolate chip cookies Skipper is making.");

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
      foyer.Items.Add(keys);
      foyer.Items.Add(ficus);
      foyer.Items.Add(bags);
      landing.Items.Add(yarn);
      kitchen.Items.Add(cookies);

      //Adding exit options
      CurrentRoom = foyer;

      Console.WriteLine("Welcome to Barbie and the Life in the Dreamhouse! What is your name?");
      string name = Console.ReadLine();
      CurrentPlayer = new Player(name);
      StartGame();
    }

    public void StartGame()
    {
      CurrentRoom.PrintRoom();
      while (Running)
      {
        Console.WriteLine("What would you like to do? Type 'help' if you are unsure.");
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
        Console.Clear();
        Console.WriteLine($"{foundItem.Name} is now in your inventory.");
      }
      else
      {
        Console.WriteLine($"{itemName} isn't even in the room, maybe it's in your hand?");
      }
    }

    public void UseItem(string itemName)
    {
      if (itemName == "keys" && CurrentRoom.Unlocked == false)
      {
        CurrentRoom.Unlocked = true;
        Console.WriteLine();
        Console.WriteLine("You unlocked the door just in time! Blissa was about to shred everything in a frantic fit! You scoop Blissa into your arms and give her a big snuggle. Now it's time to put away those new shoes you bought, and give Blissa her new toys.");
        Console.WriteLine($"Congratulations {CurrentPlayer.PlayerName}, you've saved Blissa and won the game!");

      }
      else
      {
        Console.WriteLine("That doesn't do anything, but you like carrying it anyway.");
      }
    }


    public GameService()
    {
      Setup();
    }


  }
}