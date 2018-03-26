[System.Serializable]
public class BuildingUpgrade {
	public string name;
	public string quantityNeeded; // was int
	public string basePrice; // was long
	public string description;
	public string upgradeType;
	public string upgradeClass;

	public double cookiesPerSecondAddOn = 0.0;

	public bool enabled = false;
}