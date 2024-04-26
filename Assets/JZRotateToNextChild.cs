using UnityEngine;

public class JZRotateToNextChild : MonoBehaviour
{
    GameObject[] children;
    int x = 0;
    void Start()
    {
        children = new GameObject[transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i).GetChild(0).gameObject;
            children[i].SetActive(false);
        }
        children[0].SetActive(true);
    }

    public void Next(int addThis)
    {
        for (int i = 0; i < children.Length; i++) children[i].SetActive(false);
        children[Mathf.Abs(x += addThis) % children.Length].SetActive(true);
    }
}