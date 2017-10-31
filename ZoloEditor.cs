using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ZoloEditor : EditorWindow 
{
	private const string PREFAB_INSTANCE = "Prefabs/TileSet1/";
    private const string EDITOR_TILE_PATH = "Prefabs/EditorGridTile";
    private const string TILE_SET_2_INSTANCE = "Prefabs/TileSet2/";
    private const string STATIC_TILE_NAME = "Tile";
    private const string PATH_TILE_NAME = "PathTile";
	private const string BUTTON_TILE_NAME = "ButtonTile";
	private const string START_TILE_NAME = "StartTile";
    private const string END_TILE_NAME = "EndTile";
    private const string SPIKE_TILE_NAME = "SpikeTile";
    private const string CORNER_TILE_NAME = "CornerTile";

    private static float m_GridSpacing = 1.025f;

    static GameObject[,] m_GridTiles;

    public enum Tiles
	{
		STATIC = 0,
		PATH   = 1,
		BUTTON = 2,
		START = 3,
        END = 4,
        SPIKE = 5,
        CORNER = 6,
	}

	public Tiles m_Tile;
    public bool m_IsGridTile;
	public int GridX, GridY;

	[MenuItem("ZoloEditor/Tile Generator")]

	private static void CreateWindow()
	{
		EditorWindow.GetWindow(typeof(ZoloEditor));
	}

    void OnGUI()
    {
        //CREATE A GRID WITH FIXED SIZE AND SPACING
        GUILayout.Label("Create a Grid", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        GUILayout.BeginVertical(GUILayout.Width(150), GUILayout.Height(100));
        GridX = (int)EditorGUILayout.IntField("Grid Size X", GridX);
        GridY = (int)EditorGUILayout.IntField("Grid Size Y", GridY);
        EditorGUILayout.Space();
        m_GridSpacing = EditorGUILayout.FloatField("Grid Spacing", m_GridSpacing);
        EditorGUILayout.Space();
        if (GUILayout.Button("Create Grid"))
        {
            CreateTileGrid(GridX, GridY);
        }
        GUILayout.EndVertical();
        EditorGUILayout.Space();

        //SELECT A TILE TO BE CREATED ON CLICK
        GUILayout.Label("Select a Tile", EditorStyles.boldLabel);
        GUILayout.BeginVertical(GUILayout.Width(150), GUILayout.Height(50));
        GUILayout.FlexibleSpace();
        m_Tile = (Tiles)EditorGUILayout.EnumPopup("Tile to create:", m_Tile);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create Tile"))
        {
            if (Selection.activeGameObject != null)
            {
                CreateTile(m_Tile, Selection.activeGameObject.transform.position);
            }
            else
            {
                Debug.LogWarning("Please Select a Grid Tile First");
            }
        }
        GUILayout.EndVertical();
    }

    static void CreateTile(Tiles tile,Vector3 spawnPos)
	    {
		switch (tile) 
		{	 
		default:
			break;
		case Tiles.STATIC:
			CreateTile (STATIC_TILE_NAME, spawnPos);
			break;
		case Tiles.BUTTON:
			CreateTile (PATH_TILE_NAME, spawnPos);
			break;
			case Tiles.PATH:
			CreateTile (PATH_TILE_NAME,spawnPos);
			break;
		case Tiles.START:
			CreateTile (START_TILE_NAME,spawnPos);
			break;
        case Tiles.END:
            CreateTile(END_TILE_NAME, spawnPos);
            break;
        case Tiles.SPIKE:
            CreateTile(SPIKE_TILE_NAME, spawnPos);
            break;
        case Tiles.CORNER:
            CreateTile(CORNER_TILE_NAME, spawnPos);
            break;
        }
	}

	static void CreateTileGrid (int x, int y)
	{
        m_GridTiles = new GameObject[x,y];
		GameObject GridAnchor = new GameObject ();
        GridAnchor.name = "GridAnchor";

		for (int i = 0; i < x; i++) 
		{
			for (int n = 0; n < y; n++) 
			{
                m_GridTiles[i, n] = GameObject.Instantiate(Resources.Load(EDITOR_TILE_PATH), Vector3.zero, Quaternion.identity) as GameObject;
                m_GridTiles[i, n].transform.parent = GridAnchor.transform;
				Vector3 newPosition = new Vector3 (GridAnchor.transform.position.x + (i * m_GridSpacing), GridAnchor.transform.position.y,GridAnchor.transform.position.z + (n * m_GridSpacing));
                m_GridTiles[i, n].transform.position = newPosition;
                m_GridTiles[i, n].name = "Tile" + i + n;
                m_GridTiles[i, n].tag = "EditorTile";
            }
		}
		
	}

    static void CreateTile(string tileName,Vector3 spawnPosition)
	{
		GameObject tile = GameObject.Instantiate(Resources.Load(PREFAB_INSTANCE + tileName), spawnPosition, Quaternion.identity) as GameObject;
	}
}
