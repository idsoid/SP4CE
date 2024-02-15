
using UnityEngine;

public interface IItem
{
    public int GetItemID();

    //default - lmb click, lmb release
    public void OnPrimaryAction();
    public void OnPrimaryActionRelease();

    //default - rmb click, rmb release
    public void OnSecondaryAction();
    public void OnSecondaryActionRelease();
    public bool IsItemInUse();

    public void GetRequiredControllers(GameObject obj, GameObject sightController);
}
