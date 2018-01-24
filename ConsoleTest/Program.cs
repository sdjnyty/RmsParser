using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RmsParser;

namespace RmsParser.Test
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var syntaxTree = SyntaxTree.Parse(@"<LAND_GENERATION>


  #define DESERT_MAP

  #define DESERT_MAP



  base_terrain                     DIRT
         
  base_terrain                     GRASS3

");
      Console.ReadLine();
    }
  }
}
