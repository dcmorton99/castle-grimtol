﻿using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      GameService game = new GameService();
      game.Setup();

    }
  }
}
