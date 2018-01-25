using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmsParser
{
  public class NumSyntax:NoArgSyntax
  {
    public int Num { get; set; }

    public NumSyntax(string name, int num)
      :base(name)
    {
      Num = num;
    }
  }
}
