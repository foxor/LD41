using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DungeonObject : ScriptableObject {
	public int x;
	public int y;

	public bool MoveBy(int dx, int dy) {
		return MoveTo (x + dx, y + dy);
	}
	public virtual bool MoveTo(int x, int y) {
		if (Level.currentLevel.WithinBounds (x, y) && CanMoveTo (Level.currentLevel.GetTile (x, y))) {
			RemoveFromTile ();
			this.x = x;
			this.y = y;
			AddToTile ();
			return true;
		}
		return false;
	}

	public Tile MyTile {
		get {
			return Level.currentLevel.GetTile (x, y);
		}
	}

	public void Destroy() {
		RemoveFromTile ();
	}

	protected virtual bool CanMoveTo (Tile tile) {
		return !tile.IsWall;
	}
	protected abstract void RemoveFromTile ();
	protected abstract void AddToTile ();
}

public abstract class Loot : DungeonObject {
	protected override void AddToTile () {
		MyTile.LootOnFloor.Add (this);
	}

	protected override void RemoveFromTile () {
		MyTile.LootOnFloor.Remove (this);
	}

	public abstract void OnPickup ();
}

public abstract class Monster : DungeonObject {
	public string MonsterName;
	public int Health;

	public abstract void TakeTurn();

	protected override bool CanMoveTo (Tile tile) {
		return tile.MonsterHere == null && base.CanMoveTo(tile);
	}

	protected override void AddToTile () {
		MyTile.MonsterHere = this;
	}

	protected override void RemoveFromTile () {
		MyTile.MonsterHere = null;
	}

	public virtual Color MonsterColor() {
		return ColorManager.Instance.enemy;
	}
}

[System.Serializable]
public class Tile {
	public bool Explored = false;
	public bool IsWall = false;
	public bool ContainsStairsDown = false;
	public List<Loot> LootOnFloor = new List<Loot>();
	public Monster MonsterHere = null;
	public int x;
	public int y;

	public void SyncContents() {
		if (MonsterHere != null) {
			MonsterHere = (Monster) ScriptableObject.Instantiate (MonsterHere);
			MonsterHere.x = x;
			MonsterHere.y = y;
		}
		LootOnFloor = LootOnFloor.Select (loot => {
			Loot r = (Loot)ScriptableObject.Instantiate (loot);
			r.x = x;
			r.y = y;
			return r;
		}).ToList ();
	}

	public Color TextColor() {
		if (MonsterHere != null) {
			return MonsterHere.MonsterColor();
		}
		if (IsWall) {
			return ColorManager.Instance.wall;
		}
		if (ContainsStairsDown) {
			return Color.white;
		}
		if (LootOnFloor.Any ()) {
			return ColorManager.Instance.loot;
		}
		if (!Explored) {
			return ColorManager.Instance.blueForeground;
		}
		return ColorManager.Instance.blueForeground;
	}

	public override string ToString () {
		if (MonsterHere != null) {
			return MonsterHere.MonsterName;
		}
		if (IsWall) {
			return "#";
		}
		if (ContainsStairsDown) {
			return ">";
		}
		if (LootOnFloor.Any ()) {
			return "L";
		}
		if (!Explored) {
			return " ";
		}
		return ".";
	}
}

[CreateAssetMenu]
public class Level : ScriptableObject {
	public static Level currentLevel;

	public int width;
	public int height;
	public Tile[] Tiles = new Tile[0];
	public string[] nodeRewards = new string[0];
	public bool dirty;
	public Dictionary<string, object> memory = new Dictionary<string, object>();
	public string errorMessage;

	public virtual bool win {
		get {
			return !WithinBounds (Adventurer.instance.x, Adventurer.instance.y);
		}
	}
	public virtual bool lose {
		get {
			return Adventurer.instance.Health <= 0 || !string.IsNullOrEmpty(errorMessage);
		}
	}

	public bool isOver {
		get {
			return win || lose;
		}
	}

	public bool WithinBounds(int x, int y) {
		return x >= 0 && y >= 0 && x < width && y < height;
	}

	public Tile GetTile(int x, int y) {
		return Tiles [x + y * width];
	}
}