using UnityEngine.UIElements;

//------------------------------------------------------------------------------
public class LocalizedLabel : Label, ILocalizedElement
{
    //--------------------------------------------------------------------------
    public string localizationTable { get; set; }
    public string localizationKey { get; set; }

    //--------------------------------------------------------------------------
    public LocalizedLabel() { }

    //--------------------------------------------------------------------------
    public LocalizedLabel(
        string text, string localizationTable, string localizationKey) : base(text)
    {
        this.localizationTable = localizationTable;
        this.localizationKey = localizationKey;
    }

    //--------------------------------------------------------------------------
    public new class UxmlFactory : UxmlFactory<LocalizedLabel, UxmlTraits> { }

    //--------------------------------------------------------------------------
    public new class UxmlTraits : Label.UxmlTraits
    {
        //----------------------------------------------------------------------
        private readonly UxmlStringAttributeDescription localizationTableAttr = 
            new UxmlStringAttributeDescription
            {
                name = "localization-table",
                defaultValue = ""
            };

        //----------------------------------------------------------------------
        private readonly UxmlStringAttributeDescription localizationKeyAttr = 
            new UxmlStringAttributeDescription
            {
                name = "localization-key",
                defaultValue = ""
            };

        //----------------------------------------------------------------------
        public override void Init(
            VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var label = ve as LocalizedLabel;
            label.localizationTable = 
                localizationTableAttr.GetValueFromBag(bag, cc);
            label.localizationKey = 
                localizationKeyAttr.GetValueFromBag(bag, cc);
        }
    }
}