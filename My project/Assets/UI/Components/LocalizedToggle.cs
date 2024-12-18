using UnityEngine.UIElements;

//------------------------------------------------------------------------------
public class LocalizedToggle : Toggle, ILocalizedElement
{
    //--------------------------------------------------------------------------
    public string localizationTable { get; set; }
    public string localizationKey { get; set; }

    //--------------------------------------------------------------------------
    public LocalizedToggle() { }

    //--------------------------------------------------------------------------
    public LocalizedToggle(
        string text, string localizationTable, string localizationKey) : base()
    {
        this.text = text;
        this.localizationTable = localizationTable;
        this.localizationKey = localizationKey;
    }

    //--------------------------------------------------------------------------
    public new class UxmlFactory : UxmlFactory<LocalizedToggle, UxmlTraits> { }

    //--------------------------------------------------------------------------
    public new class UxmlTraits : Toggle.UxmlTraits
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
            var toggle = ve as LocalizedToggle;
            toggle.localizationTable = 
                localizationTableAttr.GetValueFromBag(bag, cc);
            toggle.localizationKey = 
                localizationKeyAttr.GetValueFromBag(bag, cc);
        }
    }
}