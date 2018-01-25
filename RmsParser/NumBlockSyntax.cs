using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class NumBlockSyntax:BlockSyntax
  {
    public int Num { get; set; }

    public NumBlockSyntax(string name, int num, List<SyntaxNonTerminal> body)
      : base(name, body)
    {
      Num = num;
    }
  }
}
