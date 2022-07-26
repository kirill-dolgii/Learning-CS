using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

using DataStructures.HashTable;

namespace DataStructuresTests;

[TestClass]
public class HashTableOpenAddressingTests
{
	private record Person(string Name, int Age);

	private record City(string Name, int Population);

	private IDictionary<Person, City>                 _expectedDict;
	private HashTableOpenAddressingBase<Person, City> _testDict;
	private IEnumerable<KeyValuePair<Person, City>>   _testData;
	private HashFunction<Person>                      _hf;

	private string RandomString(int length, Random random)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		return new string(Enumerable.Repeat(chars, length)
									.Select(s => s[random.Next(s.Length)]).ToArray());
	}

	[TestInitialize]
	public void Initialize()
	{
		var rand = new Random(323);
		int size = 100;

		_testData = Enumerable.Range(0, size).Select(i => new KeyValuePair<Person, City>(
															  new Person(RandomString(rand.Next(10), rand), rand.Next()),
															  new City(RandomString(rand.Next(10), rand), rand.Next()))).
							   ToArray();
        _testDict = new HashTableLinearProbing<Person, City>(_testData);
        _expectedDict = new Dictionary<Person, City>(_testData);
		_hf = new HashFunction<Person>(p => p.Age * p.Name.GetHashCode());
	}

	[TestMethod]
	public void CONSTRUCTOR_FROM_ENUMERABLE()
	{
		_testDict = new HashTableLinearProbing<Person, City>(_testData, _hf);
		foreach (var kv in _testData) Assert.IsTrue(_testDict.Contains(kv));
	}

}	
	