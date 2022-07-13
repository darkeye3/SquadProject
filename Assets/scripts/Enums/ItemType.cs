namespace XEntity
{
    //이 열거형에는 항목 유형이 포함됩니다.
    //여기에 사용자 지정 항목 유형을 추가합니다.

    //열거형으로 되어있습니다.
    //열거형(Enum)을 공부해볼 기회
    public enum ItemType
    {
        Default,                    //0
        ToolOrWeapon,               //1
        Consumeable,                //2
        Placeable,                  //3
        Armor,                      //4
        MagicTool,                  //5
       
        //new
        Activatable,                //6
        
        //new
        Synthesizable               //7 이라는 값
    }
}
