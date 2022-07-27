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
															  new Person(RandomString(rand.Next(10), rand), rand.Next(50)),
															  new City(RandomString(rand.Next(10), rand), rand.Next(50)))).
							   ToArray();

		_hf = new HashFunction<Person>(p => p.Age * p.Name.Length * 33333);
		_testDict = new HashTableLinearProbing<Person, City>(_hf);
        _expectedDict = new Dictionary<Person, City>();
	}

	[TestMethod]
	public void CONSTRUCTION_FROM_ENUMERABLE()
	{
		_testDict = new HashTableLinearProbing<Person, City>(_testData, _hf);
		foreach (var kv in _testData) Assert.IsTrue(_testDict.Contains(kv));
	}

	[TestMethod]
	public void CONSTRUCTION_BY_ADDING()
	{
		foreach (var td in _testData) 
			_testDict.Add(td);
		foreach (var kv in _testData) Assert.IsTrue(_testDict.Contains(kv));
		Assert.AreEqual(_testDict.Count, _testData.Count());
	}

	[TestMethod]
	public void ADD_DUPLICATE_KEY()
	{
		_testDict.Add(_testData.First());
		Assert.ThrowsException<NotSupportedException>(() => _testDict.Add(_testData.First()));
	}

	[TestMethod]
	public void ADD_NULL()
	{
		Assert.ThrowsException<ArgumentNullException>(() => 
			_testDict.Add(new KeyValuePair<Person, City>(null, null)));
	}

	[TestMethod]
	public void REMOVE_NULL()
	{
		_testDict = new HashTableLinearProbing<Person, City>(_testData, _hf);
		Assert.ThrowsException<ArgumentNullException>(() => _testDict.Remove(new KeyValuePair<Person, City>(null, null)));
	}

	[TestMethod]
	public void REMOVE_ALL()
	{
		_testDict = new HashTableLinearProbing<Person, City>(_testData, _hf);
		foreach (var td in _testData)
		{
			Assert.IsTrue(_testDict.Remove(td));
			Assert.IsFalse(_testDict.Contains(td));
		}

		Assert.AreEqual(_testDict.Count, 0);
	}

	[TestMethod]
	public void ADD_KV_NULL() => 
		Assert.ThrowsException<ArgumentNullException>(() => _testDict.Add(null, null));

	[TestMethod]
	public void ADD_KV_SUCCESSFUL()
	{
		var addition = new KeyValuePair<Person, City>(new Person("asd", 33), new City("sss", 888));
		_testDict.Add(addition.Key, addition.Value);
		Assert.IsTrue(_testDict.Contains(addition));
		Assert.IsTrue(_testDict.ContainsKey(addition.Key));
	}

	[TestMethod]
	public void CONTAINS_KEY_SUCCESSFUL()
	{
		foreach (var td in _testData)
		{
			_testDict.Add(td);
			_testDict.ContainsKey(td.Key);
		}
		foreach (var td in _testData) _testDict.ContainsKey(td.Key);
	}

	[TestMethod]
	public void CONTAINS_KEY_NULL() => Assert.ThrowsException<ArgumentNullException>(() => _testDict.ContainsKey(null));

	[TestMethod]
	public void CONTAINS_KEY_NOT_EXISTING() => Assert.IsFalse(_testDict.ContainsKey(new Person("asd", 13)));

	[TestMethod]
	public void REMOVE_KEY_SUCCESSFUL()
	{
		foreach (var td in _testData.Take(5)) _testDict.Add(td);
		foreach (var td in _testData.Take(5)) Assert.IsTrue(_testDict.Remove(td.Key));
        foreach (KeyValuePair<Person, City> td in _testData) Assert.IsFalse(_testDict.ContainsKey(td.Key));
		Assert.AreEqual(0, _testDict.Count);
	}

	[TestMethod]
	public void REMOVE_KEY_NULL() => Assert.ThrowsException<ArgumentNullException>(() => _testDict.Remove(null));

	[TestMethod]
	public void REMOVE_KEY_NOT_EXISTING()
	{
		foreach (var td in _testData) _testDict.Add(td);
		Assert.IsFalse(_testDict.Remove(new Person("---aaa222", 12)));
	}

	[TestMethod]
	public void GET_VALUE_OPERATOR_SUCCESSFUL()
	{
		foreach (var td in _testData) _testDict.Add(td);
		foreach (var td in _testData)
		{
			var valFromDict = _testDict[td.Key];
			Assert.AreEqual(valFromDict.Name, td.Value.Name);
			Assert.AreEqual(valFromDict.Population, td.Value.Population);
		}
	}

	[TestMethod]
	public void GET_VALUE_OPERATOR_NOT_EXISTING() => 
		Assert.ThrowsException<KeyNotFoundException>(() => _testDict[new Person("asd", 3222)]);

	[TestMethod]
	public void SET_VALUE_SUCCESSFUL()
	{
		var rand = new Random(323);
		foreach (var td in _testData) _testDict.Add(td);

		var newValues = Enumerable.Range(0, _testDict.Count)
								.Select(i => new City(RandomString(10, rand), rand.Next(500))).ToList();
		var tdata = _testData.ToList();

		for (int i = 0; i < newValues.Count; i++) _testDict[tdata[i].Key] = newValues[i];
		for (int i = 0; i < newValues.Count; i++) Assert.AreEqual(_testDict[tdata[i].Key], newValues[i]);
	}
}	
	