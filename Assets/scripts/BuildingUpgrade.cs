[System.Serializable]
public class BuildingUpgrade {
	public string name;
	public string quantityNeeded; // was int
	public string basePrice; // was long
	public string description;
	public string upgradeType;
	public string upgradeClass;

	public float cookiesPerSecondAddOn = 0f;

	public bool enabled = false;
}