using UnityEngine;
using System.Collections;

public class StringTableManager : Singleton<StringTableManager> {
	// current string table
	private StringTable m_table;

	void Awake() {
		m_table = new StringTable("English");
	}

	///////////////////////////////////////////
	// Get()
	// Accesses the string value for i_key in
	// the current string table.
	///////////////////////////////////////////
	public string Get( string i_strKey ) {
		return m_table.Get( i_strKey );
	}
}
