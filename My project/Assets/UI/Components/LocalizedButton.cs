using UnityEngine.UIElements;

//------------------------------------------------------------------------------
public class LocalizedButton : Button, ILocalizedElement
{
    //--------------------------------------------------------------------------
    public string localizationTable { get; set; }
    public string localizationKey { get; set; }

    //--------------------------------------------------------------------------
    public LocalizedButton() { }

    //--------------------------------------------------------------------------
    public LocalizedButton(
        string text, string localizationTable, string localizationKey) : base()
    {
        this.text = text;
        this.localizationTable = localizationTable;
        this.localizationKey = localizationKey;
    }

    //--------------------------------------------------------------------------
    public new class UxmlFactory : UxmlFactory<LocalizedButton, UxmlTraits> { }

    //--------------------------------------------------------------------------
    public new class UxmlTraits : Button.UxmlTraits
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
            var button = ve as LocalizedButton;
            button.localizationTable = 
                localizationTableAttr.GetValueFromBag(bag, cc);
            button.localizationKey = 
                localizationKeyAttr.GetValueFromBag(bag, cc);
        }
    }
}