using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class PercentChanceSyntax:SyntaxNonTerminal
  {
    public int PercentChance { get; set; }

    public List< SyntaxNonTerminal> Statements { get; set; }

    public PercentChanceSyntax(int percentChance,List< SyntaxNonTerminal> statements)
    {
      PercentChance = percentChance;
      Statements = statements;
    }
  }
}
