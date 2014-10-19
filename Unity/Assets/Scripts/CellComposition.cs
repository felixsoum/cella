using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CellComposition : MonoBehaviour 
{
	// Specifies the default prefab to instantiate each cell.
	public GameObject defaultCellPrefab;

	// Number of cells to start with.
	public int startingCellCount;
	
	private List<GameObject> cells = new List<GameObject>();

	void Start()
	{
		for( int i = 0; i < startingCellCount; ++i )
		{
			Add();
		}
	}

	void Update()
	{
		//TEMPORARY FOR TESTING ONLY; REMOVE AFTER
		if( Input.GetMouseButtonDown(0) )
		{
			Add();
		}
		if( Input.GetMouseButtonDown(1) ) 
		{
			Remove();
		}
	}

	// Add a cell to the composition using the default prefab.
	public void Add()
	{
		Add( defaultCellPrefab );
	}

	// Add a cell to the composition by cloning the specified game object.
	public void Add( GameObject cell )
	{
		var cellClone = Instantiate( cell ) as GameObject;
		cellClone.transform.parent = this.transform;

		cells.Add( cellClone );
		RepositionStructure();
	}

	// Removes and destroys a cell from the composition; returns the destroyed cell game object, null if composition was empty.
	public GameObject Remove()
	{
		GameObject cellRemoved = null;
		if( cells.Count > 0 )
		{
			// Simply remove the last one added
			cellRemoved = cells.Last();
			cells.RemoveAt( cells.Count - 1 );
			Destroy( cellRemoved );
		}
		return cellRemoved;
	}

	public static void Transfer( CellComposition from, CellComposition to )
	{
		var cell = from.Remove();
		to.Add( cell );
	}

	private void RepositionStructure()
	{
		if( cells.Count > 0 )
		{
			// First cell on origin
			cells.First().transform.position = Vector3.zero;

			// Find out the cell sizes for positioning later based on size
			// Assume they are same size for now.
			Vector2 cellSize = GetSize( cells.First() );

			// Do a grid like structure; define the ordering here
			Vector2[] gridFillOrder = 
			{
				new Vector2(0, 1),	// Top
				new Vector2(0, -1),	// Bot
				new Vector2(-1, 0),	// Left
				new Vector2(1, 0),	// Right
				new Vector2(-1, 1),	// Top Left
				new Vector2(1, 1),	// Top Right
				new Vector2(-1, -1),// Bot Left
				new Vector2(1, -1),	// Bot Right
			};

			// Loop and position all cells after the first
			for( int i = 1; i < cells.Count; ++i )
			{
				// 8 cells per grid level
				int gridLevel = (i-1) / 8 + 1;
				int indexOnLevel = (i-1) % 8;

				Vector3 position = Vector3.zero;
				position.x = cellSize.x * gridFillOrder[indexOnLevel].x * gridLevel;
				position.y = cellSize.y * gridFillOrder[indexOnLevel].y * gridLevel;
				cells[i].transform.position = position;
			}
		}
	}

	// Should be in some utility static class and made an extension method but lazy for now.
	// Returns the size of the game object bounds using collider2D radiuses, encapsulating all children as well.
	private Vector2 GetSize( GameObject obj )
	{
		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
		
		if( obj != null )
		{
			bounds.center = obj.transform.position;
			var allChildren = obj.GetComponentsInChildren<Collider2D>();
			foreach( var child in allChildren )
			{
				if( child != null )
				{
					bounds.Encapsulate( child.bounds );
				}
			}
		}
		return bounds.size;
	}
}

