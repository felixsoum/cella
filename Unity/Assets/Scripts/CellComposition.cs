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
		RepositionCell( cells.Count - 1 );
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
		for( int i = 0; i < cells.Count; ++i )
		{
			RepositionCell( i );
		}
	}
	private void RepositionCell( int cellIndex )
	{
		if( cellIndex < cells.Count )
		{
			if( cellIndex == 0 )
			{
				// First cell on origin
				cells[cellIndex].transform.position = Vector3.zero;
			}
			else
			{
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
				
				// Derive number of cells on a square grid level.
				int gridLevel = Mathf.CeilToInt((Mathf.Sqrt(cellIndex+1) + 1.0f) / 2.0f) - 1;
				int totalPrevious = (int)Mathf.Pow(2 * gridLevel - 1, 2);
				int indexOnLevel = cellIndex - totalPrevious;
				
				Vector2 gridPosition = GetGridPosition( gridLevel, indexOnLevel, gridFillOrder );
				
				Vector3 position = Vector3.zero;
				position.x = cellSize.x * gridPosition.x;
				position.y = cellSize.y * gridPosition.y;
				cells[cellIndex].transform.position = position;
			}
		}
	}
	private Vector2 GetGridPosition( int gridLevel, int indexOnLevel, Vector2[] priorityOrder )
	{
		Vector2 gridPosition = Vector2.zero;
		if( indexOnLevel < priorityOrder.Count() )
		{
			gridPosition = priorityOrder[indexOnLevel] * gridLevel;
		}
		else
		{
			List<Vector2> remainingOrder = new List<Vector2>();
			int levelSize = 2 * gridLevel + 1;
			for( int row = 0; row < levelSize; ++row )
			{
				for( int col = 0; col < levelSize; ++col )
				{
					remainingOrder.Add( new Vector2(row - gridLevel, gridLevel - col) );
				}
			}
			remainingOrder = remainingOrder.Where( o => !priorityOrder.Contains(o / gridLevel) && (Mathf.Abs(o.x) == gridLevel || Mathf.Abs(o.y) == gridLevel) ).ToList();
			int remainingIndex = indexOnLevel - priorityOrder.Count();
			if( remainingIndex < remainingOrder.Count )
			{
				gridPosition = remainingOrder[remainingIndex];
			}
		}
		return gridPosition;
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

