using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class BlockSyntax:SyntaxNonTerminal
  {
    public string Name { get; set; }

public List<SyntaxNonTerminal> Body { get; set; }

    public BlockSyntax(string name, List<SyntaxNonTerminal> body)
    {
      Name = name;
      Body = body;
    }
  }
}
