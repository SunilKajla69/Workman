namespace WORKMAN.Config.ViewModels.AttributeViewModel
{
    public class AttributeVM
    {
        public long Id { get; set; }
        public string AttributeName { get; set; }
        public string AttributeType { get; set; }
        public string AttributeValue { get; set; }
        public int FieldTypeId{ get; set; }
        public bool IsRequired { get; set; }
    }
}
