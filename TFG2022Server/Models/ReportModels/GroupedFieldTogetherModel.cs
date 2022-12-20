namespace TFG2022Server.Models.ReportModels
{
    public class GroupedFieldTogetherModel
    {
        public GroupedFieldTogetherModel(string v1, string v2, int v3)
        {
            this.GroupedFieldProduct1Key = v1;
            this.GroupedFieldProduct2Key = v2;
            this.Cantidad = v3;
        }

        public string GroupedFieldProduct1Key { get; set; }
        public string GroupedFieldProduct2Key { get; set; }
        public int Cantidad { get; set; }
    }
}
