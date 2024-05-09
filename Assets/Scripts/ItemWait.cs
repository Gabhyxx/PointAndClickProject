public class ItemWait
{
    private int itemID;
    private int targetTable;
    public int GetItemID()
    {
        return itemID;
    }
    public void SetItemID(int itemID)
    {
        this.itemID = itemID;
    }
    public int GetTargetTable()
    {
        return targetTable;
    }
    public void SetTargetTable(int targetTable)
    {
        this.targetTable = targetTable;
    }
    public ItemWait(int itemID, int targetTable)
    {
        this.itemID = itemID;
        this.targetTable = targetTable;
    }
    
}