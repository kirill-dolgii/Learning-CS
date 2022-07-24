using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.HashTable;

namespace DataStructuresTests;

[TestClass]
public class HashTableTests
{
	private record Person(string Name, int Age);

	private record Country(string Name, int Population);

	private string RandomString(int length, Random random)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		return new string(Enumerable.Repeat(chars, length)
									.Select(s => s[random.Next(s.Length)]).ToArray());
	}

	private KeyValuePair<Person, Country>[] _testData;

	private HashFunction<Person> _hf;

	private HashTable<Person, Country> _ht;

	[TestInitialize]
	public void Initialize()
	{
		_hf = new HashFunction<Person>(person => Math.Abs(person.Name.GetHashCode() * person.Age));
		_ht = new HashTable<Person, Country>(_hf);

		var rand = new Random(323);
		_testData = Enumerable.Range(0, 50).
							   Select(i => new KeyValuePair<Person, Country>(new(RandomString(rand.Next(3, 20), rand),
																				 rand.Next(10, 80)),
																			 new(RandomString(rand.Next(3, 20), rand),
																				 rand.Next(10, 3000000)))).ToArray();
	}

	[TestMethod]
	public void ADD_SUCCESSFULLY()
	{
		foreach (var td in _testData) _ht.Add(td);

		foreach (var td in _testData)
		{
			Assert.IsTrue(_ht.Contains(td));
            Assert.AreEqual(_ht[td.Key], td.Value);
		}
	}
}

