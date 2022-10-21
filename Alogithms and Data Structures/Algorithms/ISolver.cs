using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms;
public interface ISolver
{
	public bool IsSolved { get; }

	public void Solve();
}

