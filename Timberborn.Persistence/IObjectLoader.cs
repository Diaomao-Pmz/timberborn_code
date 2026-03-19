using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Persistence
{
	// Token: 0x02000006 RID: 6
	public interface IObjectLoader
	{
		// Token: 0x06000008 RID: 8
		int Get(PropertyKey<int> key);

		// Token: 0x06000009 RID: 9
		float Get(PropertyKey<float> key);

		// Token: 0x0600000A RID: 10
		bool Get(PropertyKey<bool> key);

		// Token: 0x0600000B RID: 11
		string Get(PropertyKey<string> key);

		// Token: 0x0600000C RID: 12
		char Get(PropertyKey<char> key);

		// Token: 0x0600000D RID: 13
		Quaternion Get(PropertyKey<Quaternion> key);

		// Token: 0x0600000E RID: 14
		Vector3 Get(PropertyKey<Vector3> key);

		// Token: 0x0600000F RID: 15
		Vector3Int Get(PropertyKey<Vector3Int> key);

		// Token: 0x06000010 RID: 16
		Vector2 Get(PropertyKey<Vector2> key);

		// Token: 0x06000011 RID: 17
		Vector2Int Get(PropertyKey<Vector2Int> key);

		// Token: 0x06000012 RID: 18
		Color Get(PropertyKey<Color> key);

		// Token: 0x06000013 RID: 19
		Guid Get(PropertyKey<Guid> key);

		// Token: 0x06000014 RID: 20
		T Get<T>(PropertyKey<T> key) where T : Enum;

		// Token: 0x06000015 RID: 21
		T Get<T>(PropertyKey<T> key, IValueSerializer<T> serializer);

		// Token: 0x06000016 RID: 22
		bool GetObsoletable<T>(PropertyKey<T> key, IValueSerializer<T> serializer, out T value);

		// Token: 0x06000017 RID: 23
		List<int> Get(ListKey<int> key);

		// Token: 0x06000018 RID: 24
		List<float> Get(ListKey<float> key);

		// Token: 0x06000019 RID: 25
		List<bool> Get(ListKey<bool> key);

		// Token: 0x0600001A RID: 26
		List<string> Get(ListKey<string> key);

		// Token: 0x0600001B RID: 27
		List<char> Get(ListKey<char> key);

		// Token: 0x0600001C RID: 28
		List<Quaternion> Get(ListKey<Quaternion> key);

		// Token: 0x0600001D RID: 29
		List<Vector3> Get(ListKey<Vector3> key);

		// Token: 0x0600001E RID: 30
		List<Vector3Int> Get(ListKey<Vector3Int> key);

		// Token: 0x0600001F RID: 31
		List<Vector2> Get(ListKey<Vector2> key);

		// Token: 0x06000020 RID: 32
		List<Vector2Int> Get(ListKey<Vector2Int> key);

		// Token: 0x06000021 RID: 33
		List<Color> Get(ListKey<Color> key);

		// Token: 0x06000022 RID: 34
		List<Guid> Get(ListKey<Guid> key);

		// Token: 0x06000023 RID: 35
		List<T> Get<T>(ListKey<T> key) where T : Enum;

		// Token: 0x06000024 RID: 36
		List<T> Get<T>(ListKey<T> key, IValueSerializer<T> serializer);

		// Token: 0x06000025 RID: 37
		bool Has<T>(PropertyKey<T> key);

		// Token: 0x06000026 RID: 38
		bool Has<T>(ListKey<T> key);
	}
}
