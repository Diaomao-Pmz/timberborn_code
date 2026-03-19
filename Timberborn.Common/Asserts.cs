using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000007 RID: 7
	public static class Asserts
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002238 File Offset: 0x00000438
		[AssertionMethod]
		public static void FieldIsNull<T>(T owner, object value, string name) where T : class
		{
			if (value != null)
			{
				string objectName = Asserts.GetObjectName<T>(owner);
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Field of ",
					objectName,
					" named ",
					name,
					" isn't null"
				}));
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002280 File Offset: 0x00000480
		[AssertionMethod]
		public static void FieldIsNotNull<T>(T owner, object value, string name) where T : class
		{
			if (value == null)
			{
				string objectName = Asserts.GetObjectName<T>(owner);
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Field of ",
					objectName,
					" named ",
					name,
					" is null"
				}));
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C8 File Offset: 0x000004C8
		[AssertionMethod]
		public static void ValueIsInRange<T>(T value, T inclusiveMin, T inclusiveMax, string name) where T : IComparable<T>
		{
			if (value.CompareTo(inclusiveMin) < 0 || value.CompareTo(inclusiveMax) > 0)
			{
				throw new ArgumentException(string.Format("Value {0} named {1} is outside of the range {2} to {3}", new object[]
				{
					value,
					name,
					inclusiveMin,
					inclusiveMax
				}));
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000232C File Offset: 0x0000052C
		[AssertionMethod]
		public static void CollectionContains<T>(IReadOnlyCollection<T> collection, T item, string collectionName)
		{
			if (!collection.Contains(item))
			{
				throw new ArgumentException(string.Format("Collection {0} does not contain item {1}", collectionName, item));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000234E File Offset: 0x0000054E
		[AssertionMethod]
		public static void CollectionIsEmpty<T>(IReadOnlyCollection<T> collection, string collectionName)
		{
			if (collection == null)
			{
				throw new ArgumentException("Collection " + collectionName + " is null!");
			}
			if (collection.Count != 0)
			{
				throw new ArgumentException("Collection " + collectionName + " is not empty!");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002387 File Offset: 0x00000587
		[AssertionMethod]
		public static void CollectionIsNotEmpty<T>(IReadOnlyCollection<T> collection, string collectionName)
		{
			if (collection == null)
			{
				throw new ArgumentException("Collection " + collectionName + " is null!");
			}
			if (collection.Count == 0)
			{
				throw new ArgumentException("Collection " + collectionName + " is empty!");
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023C0 File Offset: 0x000005C0
		[AssertionMethod]
		public static void IsFalse<T>(T owner, bool value, string name) where T : class
		{
			if (value)
			{
				string objectName = Asserts.GetObjectName<T>(owner);
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Field of ",
					objectName,
					" named ",
					name,
					" is true"
				}));
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002408 File Offset: 0x00000608
		[AssertionMethod]
		public static void IsTrue<T>(T owner, bool value, string name) where T : class
		{
			if (!value)
			{
				string objectName = Asserts.GetObjectName<T>(owner);
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Field of ",
					objectName,
					" named ",
					name,
					" is false"
				}));
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002450 File Offset: 0x00000650
		public static string GetObjectName<T>(T obj) where T : class
		{
			MonoBehaviour monoBehaviour = obj as MonoBehaviour;
			if (monoBehaviour == null)
			{
				return typeof(T).Name;
			}
			return monoBehaviour.name + " (" + monoBehaviour.GetType().Name + ")";
		}
	}
}
