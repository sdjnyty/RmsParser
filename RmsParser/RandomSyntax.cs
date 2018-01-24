using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class RandomSyntax:SyntaxNonTerminal
  {
    public List<PercentChanceSyntax> PercentChances { get; set; }

    public RandomSyntax(List<PercentChanceSyntax> percentChances)
    {
      PercentChances = percentChances;
    }
  }
}
