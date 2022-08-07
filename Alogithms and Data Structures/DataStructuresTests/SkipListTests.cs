using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;

namespace DataStructuresTests;

[TestClass]
public class SkipListTests
{
	private record Planet(int Size, int Distance);

	private SkipList<Planet> _testingSl;
	private IEnumerable<Planet> _testData;

	private Random _rand;

	private static readonly Func<Planet, Planet, int> _compFunc = (planet1, planet2) =>
	{
		int size = planet1.Size.CompareTo(planet2.Size);
		int distance = planet1.Distance.CompareTo(planet2.Distance);

		if (size != 0) return size;
		return distance;
	};

	private static readonly Comparer<Planet> Comparer = Comparer<Planet>.Create(_compFunc.Invoke);

	[TestInitialize]
	public void Initialize()
	{
		_rand = new Random(323);
		var size = 100;
		_testingSl = new();
		_testData = Enumerable.Range(0, size)
							  .Select(_ => new Planet(_rand.Next(10, 100), 
													  _rand.Next(10, 100)))
							  .OrderBy(pl => pl, Comparer)
							  .ToArray();
	}

	[TestMethod]
	public void CONSTRUCTION_FROM_ENUMERABLE()
	{
		_testingSl = new(_testData, _compFunc);
		TestHelperICollection.CONSTRUCTION_FROM_ENUMERABLE(_testingSl, _testData);
	}

	[TestMethod]
	public void CONSTRUCTION_BY_ADDING()
	{
		_testingSl = new(Enumerable.Empty<Planet>(), _compFunc);
		TestHelperICollection.CONSTRUCTION_BY_ADDING(_testingSl, _testData);
	}

    [TestMethod]
	public void CONSTRUCTION_NULL_ENUMERABLE() 
		=> TestHelperICollection.CONSTRUCTION_NULL_ENUMERABLE(
				(IEnumerable<Planet> data) => new SkipList<Planet>(data, _compFunc));
}
