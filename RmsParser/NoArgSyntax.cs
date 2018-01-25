using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class NoArgSyntax:SyntaxNonTerminal
  {
    public string Name { get; set; }

    public NoArgSyntax(string name)
    {
      Name = name;
    }
  }
}
