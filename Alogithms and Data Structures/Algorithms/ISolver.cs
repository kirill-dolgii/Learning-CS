using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms;
public interface ISolver<TResult>
{
	public bool IsSolved { get; }
    public TResult Solve();
    public TResult Result { get; }
}

