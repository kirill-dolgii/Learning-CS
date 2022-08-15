using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresTests;

[TestClass]
public class HashSetTests
{
	private record Person(string Name, int Age);
	
	private IEnumerable<Person> _testData;

	private Random               _rand = new Random(222);

	private DataStructures.HashSet<Person> _hashSet;

	private Func<Person, int> _hf = pers => pers.GetHashCode();//pers.Age * pers.Name.Length * 300;

	private string RandomString(int length, Random random)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		return new string(Enumerable.Repeat(chars, length)
									.Select(s => s[random.Next(s.Length)]).ToArray());
	}

	private IEnumerable<Person> GenerateTestData(Random rand, int size) 
		=> Enumerable.Range(0, size).Select(i => new Person(RandomString(rand.Next(2, 10), rand), rand.Next(50)));


	[TestInitialize]
	public void Initialize()
	{
		_testData = GenerateTestData(_rand, 100).ToArray();
		_hashSet = new(_testData, null);
	}

	[TestMethod]
	public void CONSTRUCTION_FROM_ENUMERABLE() => TestHelperICollection.CONSTRUCTION_FROM_ENUMERABLE(_hashSet, _testData);

	[TestMethod]
	public void CONSTRUCTION_BY_ADDING() =>
		TestHelperICollection.CONSTRUCTION_BY_ADDING(
			new DataStructures.HashSet<Person>(Enumerable.Empty<Person>(), null, _hf), _testData);

	[TestMethod]
	public void CONSTRUCTION_NULL_ENUMERABLE() =>
		TestHelperICollection.CONSTRUCTION_NULL_ENUMERABLE(
			new Func<IEnumerable<Person>, ICollection<Person>>(ienum => new HashSet<Person>(ienum)));

	[TestMethod]
	public void ADD_NULL() => TestHelperICollection.ADD_NULL(_hashSet);

	[TestMethod]
	public void REMOVE_SUCCESSFUL() => TestHelperICollection.REMOVE_SUCCESSFUL(new HashSet<Person>(), _testData);

	[TestMethod]
	public void REMOVE_SUBSEQUENCE_FROM_MIDDLE() => TestHelperICollection.REMOVE_SUBSEQUENCE_FROM_MIDDLE(_hashSet, _testData);

	[TestMethod]
	public void REMOVE_NULL() => TestHelperICollection.REMOVE_NULL(_hashSet);

	[TestMethod]
	public void REMOVE_NOT_EXISTING() => TestHelperICollection.REMOVE_NOT_EXISTING(_hashSet, new Person("aoosooso", 12));
}

