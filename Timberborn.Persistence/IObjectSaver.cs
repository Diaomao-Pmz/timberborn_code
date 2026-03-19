using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Persistence
{
	// Token: 0x02000007 RID: 7
	public interface IObjectSaver
	{
		// Token: 0x06000027 RID: 39
		void Set(PropertyKey<int> key, int value);

		// Token: 0x06000028 RID: 40
		void Set(PropertyKey<float> key, float value);

		// Token: 0x06000029 RID: 41
		void Set(PropertyKey<bool> key, bool value);

		// Token: 0x0600002A RID: 42
		void Set(PropertyKey<string> key, string value);

		// Token: 0x0600002B RID: 43
		void Set(PropertyKey<char> key, char value);

		// Token: 0x0600002C RID: 44
		void Set(PropertyKey<Quaternion> key, Quaternion value);

		// Token: 0x0600002D RID: 45
		void Set(PropertyKey<Vector3> key, Vector3 value);

		// Token: 0x0600002E RID: 46
		void Set(PropertyKey<Vector3Int> key, Vector3Int value);

		// Token: 0x0600002F RID: 47
		void Set(PropertyKey<Vector2> key, Vector2 value);

		// Token: 0x06000030 RID: 48
		void Set(PropertyKey<Vector2Int> key, Vector2Int value);

		// Token: 0x06000031 RID: 49
		void Set(PropertyKey<Color> key, Color value);

		// Token: 0x06000032 RID: 50
		void Set(PropertyKey<Guid> key, Guid value);

		// Token: 0x06000033 RID: 51
		void Set<T>(PropertyKey<T> key, T value) where T : Enum;

		// Token: 0x06000034 RID: 52
		void Set<T>(PropertyKey<T> key, T value, IValueSerializer<T> serializer);

		// Token: 0x06000035 RID: 53
		void Set(ListKey<int> key, IReadOnlyCollection<int> values);

		// Token: 0x06000036 RID: 54
		void Set(ListKey<float> key, IReadOnlyCollection<float> values);

		// Token: 0x06000037 RID: 55
		void Set(ListKey<bool> key, IReadOnlyCollection<bool> values);

		// Token: 0x06000038 RID: 56
		void Set(ListKey<string> key, IReadOnlyCollection<string> values);

		// Token: 0x06000039 RID: 57
		void Set(ListKey<char> key, IReadOnlyCollection<char> values);

		// Token: 0x0600003A RID: 58
		void Set(ListKey<Quaternion> key, IReadOnlyCollection<Quaternion> values);

		// Token: 0x0600003B RID: 59
		void Set(ListKey<Vector3> key, IReadOnlyCollection<Vector3> values);

		// Token: 0x0600003C RID: 60
		void Set(ListKey<Vector3Int> key, IReadOnlyCollection<Vector3Int> values);

		// Token: 0x0600003D RID: 61
		void Set(ListKey<Vector2> key, IReadOnlyCollection<Vector2> values);

		// Token: 0x0600003E RID: 62
		void Set(ListKey<Vector2Int> key, IReadOnlyCollection<Vector2Int> values);

		// Token: 0x0600003F RID: 63
		void Set(ListKey<Color> key, IReadOnlyCollection<Color> values);

		// Token: 0x06000040 RID: 64
		void Set(ListKey<Guid> key, IReadOnlyCollection<Guid> values);

		// Token: 0x06000041 RID: 65
		void Set<T>(ListKey<T> key, IReadOnlyCollection<T> values) where T : Enum;

		// Token: 0x06000042 RID: 66
		void Set<T>(ListKey<T> key, IReadOnlyCollection<T> values, IValueSerializer<T> serializer);
	}
}
