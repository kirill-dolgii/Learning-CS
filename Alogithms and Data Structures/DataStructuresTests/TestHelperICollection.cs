using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresTests;
public static class TestHelperICollection
{
	public static void CONSTRUCTION_FROM_ENUMERABLE<T>(ICollection<T> enumConstructedCollection, 
													   IEnumerable<T> orderedData)
	{
		Assert.AreEqual(orderedData.Count(), enumConstructedCollection.Count);
		Assert.IsTrue(orderedData.SequenceEqual(enumConstructedCollection));
		foreach (var item in orderedData) 
			Assert.IsTrue(enumConstructedCollection.Contains(item));
	}

	public static void CONSTRUCTION_BY_ADDING<T>(ICollection<T> emptyCollection, 
												 IEnumerable<T> orderedData)
	{
		foreach (var item in orderedData) emptyCollection.Add(item);
		Assert.IsTrue(orderedData.SequenceEqual(emptyCollection));
		foreach (var item in orderedData) Assert.IsTrue(emptyCollection.Contains(item));
		Assert.AreEqual(orderedData.Count(), emptyCollection.Count);
	}

	public static void CONSTRUCTION_NULL_ENUMERABLE<T>(Func<IEnumerable<T>, 
														   ICollection<T>> creator) 
		=> Assert.ThrowsException<ArgumentNullException>(() => creator.Invoke(null!));

	public static void COPY_TO_FROM_0<T>(ICollection<T> testCollection)
	{
		
	}
}
