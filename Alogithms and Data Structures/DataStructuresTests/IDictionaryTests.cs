using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Linq;

namespace DataStructuresTests;

public class TestHelperIDictionary
{
	public static void CONSTRUCTION_FROM_ENUMERABLE<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> testData,
																  IDictionary<TKey, TValue> testingDict,
																  IDictionary<TKey, TValue> sampleDict)
	{
		foreach (var kv in sampleDict)
			Assert.IsTrue(testingDict.Contains(kv) && testingDict.ContainsKey(kv.Key));
		Assert.AreEqual(testingDict.Count, sampleDict.Count);

		Assert.IsTrue(Enumerable.SequenceEqual(testingDict, sampleDict));
	}

	public static void CONSTRUCTION_BY_ADDING<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> testData,
															IDictionary<TKey, TValue> testingDict,
															IDictionary<TKey, TValue> sampleDict)
	{
		foreach (var kv in testData)
		{
			testingDict.Add(kv.Key, kv.Value);
			sampleDict.Add(kv.Key, kv.Value);
		}

		foreach (var kv in testData) Assert.IsTrue(testingDict.ContainsKey(kv.Key) && testingDict.Contains(kv));
		Assert.AreEqual(testingDict.Count, sampleDict.Count);
		Assert.IsTrue(Enumerable.SequenceEqual(testingDict, sampleDict));
	}

	public static void ADD_DUPLICATE_KEY<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> testData,
													   IDictionary<TKey, TValue> testingDict)
	{
		testingDict.Add(testData.First());
		Assert.ThrowsException<NotSupportedException>(() => testingDict.Add(testData.First()));
	}

	public static void ADD_NULL<TKey, TValue>(IDictionary<TKey, TValue> testingDict)
	where TKey : class
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
				testingDict.Add(new KeyValuePair<TKey, TValue>(null, default(TValue))));
    }
    
    public static void REMOVE_NULL<TKey, TValue>(IDictionary<TKey, TValue> testingDict)
    where TKey : class
	{
		Assert.ThrowsException<ArgumentNullException>(() => testingDict.Remove(new KeyValuePair<TKey, TValue>(null, default(TValue))));
    }

    public static void REMOVE_ALL<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> testData,
												IDictionary<TKey, TValue> testingDict,
												IDictionary<TKey, TValue> sampleDict)
	{

		foreach (var kv in testData)
		{
            testingDict.Add(kv);
            sampleDict.Add(kv);
		}

        foreach (var kv in testData)
        {
            Assert.AreEqual(testingDict.Remove(kv), sampleDict.Remove(kv));
            Assert.IsFalse(testingDict.Contains(kv));
        }

        Assert.AreEqual(testingDict.Count, 0);
		Assert.IsTrue(Enumerable.Empty<KeyValuePair<TKey, TValue>>().SequenceEqual(testingDict));
	}

	public static void ADD_NULL_KV<TKey, TValue>(IDictionary<TKey, TValue> testingDict)
    where TKey : class =>
		Assert.ThrowsException<ArgumentNullException>(() => testingDict.Add(null, default(TValue)));

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