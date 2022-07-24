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

	private Dictionary<Person, Country> _expHt;

	[TestInitialize]
	public void Initialize()
	{
		_hf = new HashFunction<Person>(person => Math.Abs(person.Name.GetHashCode() * person.Age));
		_ht = new HashTable<Person, Country>(_hf);

		_expHt = new();

		var rand = new Random(323);
		_testData = Enumerable.Range(0, 50).
							   Select(i => new KeyValuePair<Person, Country>(new(RandomString(rand.Next(3, 20), rand),
																				 rand.Next(10, 80)),
																			 new(RandomString(rand.Next(3, 20), rand),
																				 rand.Next(10, 3000000)))).ToArray();
		foreach (var td in _testData)
		{
			_ht.Add(td);
			_expHt.Add(td.Key, td.Value);
		}
	}

	[TestMethod]
	public void ADD_SUCCESSFULLY()
	{
		foreach (var td in _testData)
		{
			Assert.IsTrue(_ht.Contains(td));
			Assert.AreEqual(_ht[td.Key], td.Value);
		}
	}

	[TestMethod]
	public void CLEAR()
	{
		_ht.Clear();
		foreach (var td in _testData) Assert.IsFalse(_ht.Contains(td));
		Assert.AreEqual(_ht.Count, 0);
	}

	[TestMethod]
	public void ADD_CLEAR_ADD()
	{
		_ht.Clear();
		foreach (var td in _testData) _ht.Add(td);
		foreach (var td in _testData) Assert.IsTrue(_ht.Contains(td));
		Assert.AreEqual(_ht.Count, _testData.Count());
	}

	[TestMethod]	
	public void REMOVE_SUCCESSFUL_KEY()
	{
		Assert.IsTrue(_ht.Remove(_testData.First().Key));
		Assert.IsFalse(_ht.Contains(_testData.First()));
		Assert.AreEqual(_ht.Count, _testData.Count() - 1);
	}

	[TestMethod]
	public void REMOVE_NOT_EXISTING_KEY() => Assert.IsFalse(_ht.Remove(new Person("nobody", 123)));

	[TestMethod]
	public void REMOVE_NULL_KEY() => Assert.ThrowsException<ArgumentNullException>(() => _ht.Remove(null));
	
	[TestMethod]
	public void REMOVE_SUCCESSFUL_KV_PAIR()
	{
		var toRemove = _testData.First();
		Assert.IsTrue(_ht.Remove(toRemove));
		Assert.IsFalse(_ht.Contains(toRemove));
	}

	[TestMethod]
	public void REMOVE_NULL_KV_PAIR() =>
		Assert.ThrowsException<ArgumentNullException>(() => _ht.Remove(new KeyValuePair<Person, Country>(null, null)));

	[TestMethod]
	public void TRY_GET_NOT_EXISTING_KEY() => Assert.IsFalse(_ht.TryGetValue(new Person("Vova", 34), out Country val));

	[TestMethod]
	public void TRY_GET_SUCCESSFUL()
	{
		var     testKv = _testData.First();

		Assert.IsTrue(_ht.TryGetValue(testKv.Key, out var val));
		Assert.AreEqual(testKv.Value, val);
		_expHt.TryGetValue(testKv.Key, out var valExp);
		Assert.AreEqual(valExp, val);
	}

	[TestMethod]
	public void SETTER_SUCCESSFUL()
	{
		var ht = new HashTable<Person, Country>(_hf);

		foreach (var td in _testData) ht[td.Key] = td.Value;
		foreach (var td in _testData)
		{
			Assert.IsTrue(ht.Contains(td));
			Assert.IsTrue(_expHt[td.Key] == ht[td.Key]);
			Assert.AreEqual(_expHt.Count, ht.Count);
		}
	}

	[TestMethod]
	public void SETTER_UNSUCCESSFUL() => Assert.ThrowsException<ArgumentNullException>(() => _ht[null] = null);

	[TestMethod]
	public void GETTER_UNSUCCESSFUL() => Assert.ThrowsException<KeyNotFoundException>(() => _ht[new Person("asd11", 222)]);

	[TestMethod]
	public void GETTER_NULL() => Assert.ThrowsException<ArgumentNullException>(() => _ht[null]);

}

