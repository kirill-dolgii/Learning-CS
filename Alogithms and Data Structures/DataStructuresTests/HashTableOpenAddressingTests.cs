using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DataStructures.HashTable;

namespace DataStructuresTests;

[TestClass]
public class HashTableTests
{
	private record Person(string Name, int Age);

	private record City(string Name, int Population);

	private IEnumerable<KeyValuePair<Person, City>> _testData;

	private HashFunction<Person> _hf   = new((pers) => pers.Age * pers.Name.Length * 300000);
	private Random               _rand = new Random(22);

	private string RandomString(int length, Random random)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		return new string(Enumerable.Repeat(chars, length)
									.Select(s => s[random.Next(s.Length)]).ToArray());
	}

	private IEnumerable<KeyValuePair<Person, City>> GenerateTestData(Random rand, int size)
	{
		return Enumerable.Range(0, size).Select(i => new KeyValuePair<Person, City>(
													new Person(RandomString(rand.Next(8, 10), rand), rand.Next(50)),
													new City(RandomString(rand.Next(8, 10), rand), rand.Next(50))));
	}

	[TestInitialize]
	public void Initialize() => _testData = GenerateTestData(_rand, 100).ToArray();

	[TestMethod]
	public void CONSTRUCTION_FROM_ENUMERABLE()
	{
		var testData = GenerateTestData(_rand, 100).ToArray();
		var hfLinPr  = new HashTableLinearProbing<Person, City>(testData, _hf);
		var hfSepCh  = new HashTable<Person, City>(testData, _hf);

		TestHelperIDictionary.CONSTRUCTION_FROM_ENUMERABLE(Enumerable.Empty<KeyValuePair<Person, City>>(),
														   hfLinPr,
														   new Dictionary<Person, City>(testData));

		TestHelperIDictionary.CONSTRUCTION_FROM_ENUMERABLE(Enumerable.Empty<KeyValuePair<Person, City>>(),
														   hfSepCh,
														   new Dictionary<Person, City>(testData));
	}

	[TestMethod]
	public void CONSTRUCTION_BY_ADDING()
	{
		var testData = GenerateTestData(_rand, 100).ToArray();
		var hfLinPr  = new HashTableLinearProbing<Person, City>(_hf);
		var hfSepCh  = new HashTable<Person, City>(_hf);

		TestHelperIDictionary.CONSTRUCTION_BY_ADDING(testData,
													 hfSepCh,
													 new Dictionary<Person, City>());

		TestHelperIDictionary.CONSTRUCTION_BY_ADDING(testData,
													 hfLinPr,
													 new Dictionary<Person, City>());
	}

	[TestMethod]
	public void ADD_DUPLICATE_KEY()
	{
		var sepCh = new HashTable<Person, City>(_hf);
		var linPr = new HashTableLinearProbing<Person, City>(_hf);

		TestHelperIDictionary.ADD_DUPLICATE_KEY(_testData, sepCh);
		TestHelperIDictionary.ADD_DUPLICATE_KEY(_testData, linPr);
	}

	[TestMethod]
	public void ADD_NULL()
	{
		var sepCh = new HashTable<Person, City>(_hf);
		var linPr = new HashTableLinearProbing<Person, City>(_hf);

		TestHelperIDictionary.ADD_NULL(sepCh);
		TestHelperIDictionary.ADD_NULL(linPr);
	}

	[TestMethod]
	public void REMOVE_NULL()
	{
		var sepCh = new HashTable<Person, City>(_hf);
		var linPr = new HashTableLinearProbing<Person, City>(_hf);

		TestHelperIDictionary.REMOVE_NULL(sepCh);
		TestHelperIDictionary.REMOVE_NULL(linPr);
	}

	[TestMethod]
	public void REMOVE_ALL()
	{
		var sepCh    = new HashTable<Person, City>(_hf);
		var linPr    = new HashTableLinearProbing<Person, City>(_hf);
		var testData = GenerateTestData(_rand, 100).ToArray();

		TestHelperIDictionary.REMOVE_ALL(testData, linPr, new Dictionary<Person, City>());
		TestHelperIDictionary.REMOVE_ALL(testData, sepCh, new Dictionary<Person, City>());
	}

	[TestMethod]
	public void ADD_NULL_KV()
	{
		var sepCh    = new HashTable<Person, City>(_hf);
		var linPr    = new HashTableLinearProbing<Person, City>(_hf);
		var testData = GenerateTestData(_rand, 100).ToArray();

		TestHelperIDictionary.ADD_NULL_KV(linPr);
		TestHelperIDictionary.ADD_NULL_KV(sepCh);
	}
}

//public class HashTableTestHelper<T, TKey, TValue>
//where T : IDictionary<TKey, TValue>
//{
//	private IDictionary<TKey, TValue> _testingHashTable;

//	private readonly Func<IEnumerable<KeyValuePair<TKey, TValue>>, int, double,
//		HashFunction<TKey>, IDictionary<TKey, TValue>> _creator;

//	private          Func<Random, int, IEnumerable<KeyValuePair<TKey, TValue>>> _generator;
//	private readonly HashFunction<TKey>                                         _hashFunc;

//	public HashTableTestHelper(Func<IEnumerable<KeyValuePair<TKey, TValue>>, int, double,
//								   HashFunction<TKey>, IDictionary<TKey, TValue>> creator,
//							   Func<Random, int, IEnumerable<KeyValuePair<TKey, TValue>>> generator,
//							   HashFunction<TKey> hf)
//	{
//		_creator = creator;
//		_generator = generator;
//		_hashFunc = hf;
//	}

//	public void CONSTRUCTION_FROM_ENUMERABLE() { }

	//public void CONSTRUCTION_BY_ADDING()
	//{
	//	foreach (var td in _testData) 
	//		_testDict.Add(td);
	//	foreach (var kv in _testData) Assert.IsTrue(_testDict.Contains(kv));
	//	Assert.AreEqual(_testDict.Count, _testData.Count());
	//}

	//public void ADD_DUPLICATE_KEY()
	//{
	//	_testDict.Add(_testData.First());
	//	Assert.ThrowsException<NotSupportedException>(() => _testDict.Add(_testData.First()));
	//}

	//public void ADD_NULL()
	//{
	//	Assert.ThrowsException<ArgumentNullException>(() => 
	//		_testDict.Add(new KeyValuePair<Person, City>(null, null)));
	//}

	//public void REMOVE_NULL()
	//{
	//	_testDict = new HashTableLinearProbing<Person, City>(_testData, _hf);
	//	Assert.ThrowsException<ArgumentNullException>(() => _testDict.Remove(new KeyValuePair<Person, City>(null, null)));
	//}

	//public void REMOVE_ALL()
	//{
	//	_testDict = new HashTableLinearProbing<Person, City>(_testData, _hf);
	//	foreach (var td in _testData)
	//	{
	//		Assert.IsTrue(_testDict.Remove(td));
	//		Assert.IsFalse(_testDict.Contains(td));
	//	}

	//	Assert.AreEqual(_testDict.Count, 0);
	//}

	//public void ADD_KV_NULL() => 
	//	Assert.ThrowsException<ArgumentNullException>(() => _testDict.Add(null, null));

	//public void ADD_KV_SUCCESSFUL()
	//{
	//	var addition = new KeyValuePair<Person, City>(new Person("asd", 33), new City("sss", 888));
	//	_testDict.Add(addition.Key, addition.Value);
	//	Assert.IsTrue(_testDict.Contains(addition));
	//	Assert.IsTrue(_testDict.ContainsKey(addition.Key));
	//}

	//public void CONTAINS_KEY_SUCCESSFUL()
	//{
	//	foreach (var td in _testData)
	//	{
	//		_testDict.Add(td);
	//		_testDict.ContainsKey(td.Key);
	//	}
	//	foreach (var td in _testData) _testDict.ContainsKey(td.Key);
	//}

	//public void CONTAINS_KEY_NULL() => Assert.ThrowsException<ArgumentNullException>(() => _testDict.ContainsKey(null));

	//public void CONTAINS_KEY_NOT_EXISTING() => Assert.IsFalse(_testDict.ContainsKey(new Person("asd", 13)));

	//public void REMOVE_KEY_SUCCESSFUL()
	//{
	//	foreach (var td in _testData.Take(5)) _testDict.Add(td);
	//	foreach (var td in _testData.Take(5)) Assert.IsTrue(_testDict.Remove(td.Key));
	//       foreach (KeyValuePair<Person, City> td in _testData) Assert.IsFalse(_testDict.ContainsKey(td.Key));
	//	Assert.AreEqual(0, _testDict.Count);
	//}

	//public void REMOVE_KEY_NULL() => Assert.ThrowsException<ArgumentNullException>(() => _testDict.Remove(null));

	//public void REMOVE_KEY_NOT_EXISTING()
	//{
	//	foreach (var td in _testData) _testDict.Add(td);
	//	Assert.IsFalse(_testDict.Remove(new Person("---aaa222", 12)));
	//}

	//public void GET_VALUE_OPERATOR_SUCCESSFUL()
	//{
	//	foreach (var td in _testData) _testDict.Add(td);
	//	foreach (var td in _testData)
	//	{
	//		var valFromDict = _testDict[td.Key];
	//		Assert.AreEqual(valFromDict.Name, td.Value.Name);
	//		Assert.AreEqual(valFromDict.Population, td.Value.Population);
	//	}
	//}

	//public void GET_VALUE_OPERATOR_NOT_EXISTING() => 
	//	Assert.ThrowsException<KeyNotFoundException>(() => _testDict[new Person("asd", 3222)]);

	//public void SET_VALUE_SUCCESSFUL()
	//{
	//	var rand = new Random(323);
	//	foreach (var td in _testData) _testDict.Add(td);

	//	var newValues = Enumerable.Range(0, _testDict.Count)
	//							.Select(i => new City(RandomString(10, rand), rand.Next(500))).ToList();
	//	var tdata = _testData.ToList();

	//	for (int i = 0; i < newValues.Count; i++) _testDict[tdata[i].Key] = newValues[i];
	//	for (int i = 0; i < newValues.Count; i++) Assert.AreEqual(_testDict[tdata[i].Key], newValues[i]);
	//}

	//public void SET_VALUE_NULL()
	//{
	//	foreach (var td in _testData.Take(5)) _testDict.Add(td);
	//	Assert.ThrowsException<ArgumentNullException>(() => _testDict[null] = new City("asd", 455));
	//}

	//public void COPY_TO_SUCCESSFUL()
	//{
	//	var emptyArray = new KeyValuePair<Person, City>[_testData.Count()];
	//	foreach (var td in _testData) _testDict.Add(td);

	//	_testDict.CopyTo(emptyArray, 0);

	//	foreach (var kv in emptyArray) Assert.AreEqual(_testDict[kv.Key], kv.Value);
	//}

	//public void ENUMERATION()
	//{
	//	foreach (var kv in _testData) _testDict.Add(kv);
	//	Assert.IsTrue(_testData.SequenceEqual(_testDict));
	//}

	//public void ENUMERATION_AFTER_REMOVAL()
	//{
	//	var rand = new Random(22);
	//	int size = 55;

	//	var init    = _testData.ToList();
	//	var removed = Enumerable.Range(0, size).Select(i => init[i]).ToList();

	//	init = init.Except(removed).ToList();

	//	foreach (var kv in _testData) _testDict.Add(kv);
	//	foreach (var kv in removed) _testDict.Remove(kv);

	//	Assert.IsTrue(init.SequenceEqual(_testDict));
	//}
}