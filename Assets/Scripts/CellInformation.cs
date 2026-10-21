using UnityEngine;
public class CellInformation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer mySprite;
    private int cellState;

    // Changes the state, changes the sprite as well depending on the state
    public void SetState(int newState)
    {
        cellState = newState;

        SetSprite();
    }

    public int GetState()
    {
        return cellState;
    }

    private void SetSprite()
    {
        if (cellState == 1)
        {
            mySprite.sprite = sprites[1];
        }
        else
        {
            mySprite.sprite = sprites[0];
        }
    }
}
