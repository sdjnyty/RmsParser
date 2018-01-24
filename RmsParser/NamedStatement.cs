using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class NoArgStatement:SyntaxNonTerminal
  {
    public string Name { get; set; }

    public NoArgStatement(string name)
    {
      Name = name;
    }
  }
}
